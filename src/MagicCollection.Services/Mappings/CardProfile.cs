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
        opt => opt.MapFrom(s => s.Card.Name));
    CreateMap<Edition, EditionModel>();
    CreateMap<EditionType, EditionTypeModel>();
    CreateMap<Treatment, TreatmentModel>();
    CreateMap<PrintTreatment, PrintTreatmentModel>()
      .ForMember(d => d.Identifier,
        opt => opt.MapFrom(s => s.Treatment.Identifier))
      .ForMember(d => d.Label,
        opt => opt.MapFrom(s => s.Treatment.Label));
    CreateMap<Rarity, RarityModel>();
    CreateMap<Language, LanguageModel>();
  }
}