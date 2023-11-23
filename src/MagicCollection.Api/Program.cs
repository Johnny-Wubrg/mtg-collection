using System.Reflection;
using MagicCollection.Api.Formatters;
using MagicCollection.Services.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var allowEverythingTemp = "_temporarilyAllowEverything"; // TODO

// Add services to the container.
builder.Services.AddCors(options =>
{
  options.AddPolicy(allowEverythingTemp,
    policy =>
    {
      policy.WithOrigins("*");
      policy.AllowAnyHeader();
      policy.AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers(opt =>
{
  opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
  opt.OutputFormatters.Add(new HttpNotFoundOutputFormatter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.CustomSchemaIds(type => type.Name.Replace("Model", ""));

  var currentAssembly = Assembly.GetExecutingAssembly();
  var xmlDocs = currentAssembly.GetReferencedAssemblies()
    .Union(new[] { currentAssembly.GetName() })
    .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location) ?? string.Empty, $"{a.Name}.xml"))
    .Where(File.Exists).ToArray();

  Array.ForEach(xmlDocs, d => { options.IncludeXmlComments(d); });

  options.MapType<DateOnly>(() => new OpenApiSchema
  {
    Type = "string",
    Format = "date"
  });
});
builder.Services.AddRouting(options =>
{
  options.LowercaseUrls = true;
  options.LowercaseQueryStrings = true;
});
builder.Services.AddMagicCollection(builder.Configuration);

builder.Services.Configure<KestrelServerOptions>(options =>
{
  options.Limits.MaxRequestBodySize = int.MaxValue; // if don't set default value is: 30 MB
  options.Limits.MaxRequestBodySize = int.MaxValue; // if don't set default value is: 30 MB
});


builder.Services.Configure<FormOptions>(x =>
{
  x.ValueLengthLimit = int.MaxValue;
  x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(allowEverythingTemp);

app.Run();