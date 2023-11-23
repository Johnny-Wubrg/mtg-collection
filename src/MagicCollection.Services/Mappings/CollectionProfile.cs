using AutoMapper;
using MagicCollection.Data.Entities;
using MagicCollection.Services.Models.Collection;

namespace MagicCollection.Services.Mappings;

/// <inheritdoc />
public class CollectionProfile : Profile
{
  /// <inheritdoc />
  public CollectionProfile()
  {
    CreateMap<CardEntry, CardEntryModel>();
    CreateMap<CardEntryModel, CardEntry>()
      .ForMember(d => d.Section, opt => opt.Ignore())
      .ForMember(d => d.SectionId, 
        opt => opt.MapFrom(s => s.Section.Id))
      .ForMember(d => d.Language, opt => opt.Ignore())
      .ForMember(d => d.LanguageIdentifier, 
        opt => opt.MapFrom(s => s.Language.Identifier))
      .ForMember(d => d.Treatment, opt => opt.Ignore())
      .ForMember(d => d.TreatmentIdentifier, 
        opt => opt.MapFrom(s => s.Treatment.Identifier))
      .ForMember(d => d.Print, opt => opt.Ignore())
      .ForMember(d => d.PrintId,
        opt => opt.MapFrom(s => s.Print.Id));
    
    CreateMap<Bin, BinModel>();
    CreateMap<Section, SectionModel>();

  }
}