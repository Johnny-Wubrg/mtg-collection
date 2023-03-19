using System.Reflection;
using MagicCollection.Services.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

var AllowEverythingTemp = "_temporarilyAllowEverything"; // TODO

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowEverythingTemp,
        policy =>
        {
            policy.WithOrigins("*");
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.Name.Replace("Model", ""));

    var currentAssembly = Assembly.GetExecutingAssembly();
    var xmlDocs = currentAssembly.GetReferencedAssemblies()
        .Union(new[] { currentAssembly.GetName() })
        .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
        .Where(f => File.Exists(f)).ToArray();

    Array.ForEach(xmlDocs, d =>
    {
        options.IncludeXmlComments(d);
    });
});
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddMagicCollection(builder.Configuration);

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue; // if don't set default value is: 30 MB
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
app.UseCors(AllowEverythingTemp);

app.Run();
