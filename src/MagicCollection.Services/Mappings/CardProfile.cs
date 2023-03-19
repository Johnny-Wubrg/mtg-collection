using AutoMapper;
using MagicCollection.Data.Entities;
using MagicCollection.Services.Models;

namespace MagicCollection.Services.Mappings;

public class CardProfile : Profile
{
  public CardProfile()
  {
    CreateMap<Card, CardModel>();
  }
}