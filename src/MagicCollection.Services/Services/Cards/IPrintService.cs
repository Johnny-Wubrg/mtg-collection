using MagicCollection.Services.Models.Cards;
using MagicCollection.Services.Models.Request;

namespace MagicCollection.Services.Cards;

/// <summary>
/// Service to get card prints
/// </summary>
public interface IPrintService
{
  /// <summary>
  /// Get card prints by card ID
  /// </summary>
  /// <param name="cardId"></param>
  /// <returns></returns>
  Task<IEnumerable<PrintModel>> GetByCardId(Guid cardId);

  /// <summary>
  /// Get by set code and collector number
  /// </summary>
  /// <param name="setCode"></param>
  /// <param name="cn"></param>
  /// <returns></returns>
  Task<IEnumerable<PrintModel>> GetBySetCode(string setCode, string cn);


  /// <summary>
  /// Search for prints
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  Task<IEnumerable<PrintModel>> Search(PrintSearchModel model);
}