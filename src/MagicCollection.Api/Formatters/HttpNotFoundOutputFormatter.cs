using Microsoft.AspNetCore.Mvc.Formatters;

namespace MagicCollection.Api.Formatters;

/// <summary>
/// Sets the status code to 404 if the content is null.
/// </summary>
public class HttpNotFoundOutputFormatter : IOutputFormatter
{

  /// <inheritdoc />
  public bool CanWriteResult(OutputFormatterCanWriteContext context)
  {
    if (context.ObjectType == typeof(void) || context.ObjectType == typeof(Task))
    {
      return true;
    }

    return context.Object == null;
  }

  /// <inheritdoc />
  public Task WriteAsync(OutputFormatterWriteContext context)
  {
    var response = context.HttpContext.Response;

    if (response.StatusCode == StatusCodes.Status200OK)
    {
      response.StatusCode = StatusCodes.Status404NotFound;
    }

    return Task.CompletedTask;
  }
}
