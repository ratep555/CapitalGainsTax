using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Stock, StockToReturnDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.CategoryName))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Country.CountryName));
            
            CreateMap<StockTransaction, TransactionToReturnDto>()
                .ForMember(d => d.Stock, o => o.MapFrom(s => s.Stock.Symbol));
        }
    }
}