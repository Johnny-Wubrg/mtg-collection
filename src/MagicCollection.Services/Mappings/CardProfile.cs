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
    CreateMap<Print, PrintModel>()
      .ForMember(d => d.Name, 
        opt => opt.MapFrom(s => s.Card.Name))
      .ForMember(d => d.AvailableTreatments, 
        opt => opt.MapFrom(s => s.AvailableTreatments.Select(t => t.Treatment)));
    CreateMap<Edition, EditionModel>();
    CreateMap<EditionType, EditionTypeModel>();
    CreateMap<Treatment, TreatmentModel>();
    CreateMap<Rarity, RarityModel>();
    CreateMap<Language, LanguageModel>();
  }
}