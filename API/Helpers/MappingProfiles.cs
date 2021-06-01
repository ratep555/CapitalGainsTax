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

            CreateMap<StockToCreateDto, Stock>().ReverseMap();
            CreateMap<StockToEditDto, Stock>().ReverseMap();

            /*  CreateMap<Stock, StockToCreateDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.CategoryName))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Country.CountryName))
                .ForMember(d => d.CategoryId, o => o.MapFrom(s => s.CategoryId))
                .ForMember(d => d.CountryId, o => o.MapFrom(s => s.CountryId))
                .ReverseMap()
                .ForPath(s => s.Category.CategoryName, o => o.MapFrom(s => s.Category))
                .ForPath(s => s.Country.CountryName, o => o.MapFrom(s => s.Country))
                .ForPath(s => s.CategoryId, o => o.MapFrom(s => s.CategoryId))
                .ForPath(s => s.CountryId, o => o.MapFrom(s => s.CountryId)); */

            
            CreateMap<Stock, StockToReturnDto1>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.CategoryName))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Country.CountryName));
            
            // ovako treba ići reverse kod složenih modela, to je unflattening
            CreateMap<Stock, StockToReturnDto3>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.CategoryName))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Country.CountryName)).ReverseMap()
                .ForPath(s => s.Category.CategoryName, o => o.MapFrom(s => s.Category))
                .ForPath(s => s.Country.CountryName, o => o.MapFrom(s => s.Country));

            
            CreateMap<StockTransaction, TransactionToReturnDto>()
                .ForMember(d => d.Stock, o => o.MapFrom(s => s.Stock.Symbol));
        }
    }
}



