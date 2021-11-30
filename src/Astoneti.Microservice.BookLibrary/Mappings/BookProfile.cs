using Astoneti.Microservice.BookLibrary.Business.Dto;
using Astoneti.Microservice.BookLibrary.Data.Entities;
using Astoneti.Microservice.BookLibrary.Models;
using AutoMapper;

namespace Astoneti.Microservice.BookLibrary.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookModel, BookDto>().ReverseMap();
            CreateMap<BookEntity, BookDto>().ReverseMap();
        }
    }
}
