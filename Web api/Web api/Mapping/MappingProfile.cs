using AutoMapper;
using Web_api.DTOs;
using Web_api.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web_api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<CreateBookDto, Book>();
        }
    }
}
