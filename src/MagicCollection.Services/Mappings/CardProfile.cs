using AutoMapper;
using MagicCollection.Data.Entities;
using MagicCollection.Services.Models;

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