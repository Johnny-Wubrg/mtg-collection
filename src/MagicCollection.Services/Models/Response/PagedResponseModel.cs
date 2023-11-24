namespace MagicCollection.Services.Models.Response;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public record PagedResponseModel<T>
{
  /// <summary>
  /// The collection of items
  /// </summary>
  public IEnumerable<T> Data { get; set; }
  
  /// <summary>
  /// The current page number
  /// </summary>
  public int Page { get; set; }

  /// <summary>
  /// The current number of elements per page
  /// </summary>
  public int PageElements => Data.Count();
  
  /// <summary>
  /// The total count of all elements
  /// </summary>
  public int TotalElements { get; set; }

  /// <summary>
  /// The total count of pages
  /// </summary>
  public int TotalPages => (int)Math.Ceiling((decimal)TotalElements / PageElements);
}