using AutoMapper;
using MagicCollection.Data.Entities;
using MagicCollection.Services.Models.Cards;

namespace MagicCollection.Services.Mappings;

/// <inheritdoc />
public class CardProfile : Profile
{
  /// <inheritdoc />
  public CardProfile()
  {
    CreateMap<Card, CardModel>();
  }
}