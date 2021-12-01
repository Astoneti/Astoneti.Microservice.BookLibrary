using Astoneti.Microservice.BookLibrary.Business.Contracts;
using Astoneti.Microservice.BookLibrary.Business.Dto;
using Astoneti.Microservice.BookLibrary.Data.Contracts;
using Astoneti.Microservice.BookLibrary.Data.Entities;
using AutoMapper;
using System.Collections.Generic;

namespace Astoneti.Microservice.BookLibrary.Business
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public List<BookDto> GetList()
        {
            var list = new List<BookDto>()
            {
                new BookDto(){ Id = 1, Title = "My First book", Author = "Anton Pashkun" },
                new BookDto(){ Id = 2, Title = "Code First", Author = "Anton Pashkun" },
                new BookDto(){ Id = 3, Title = "How to become a developer from scratch", Author = "Anton Pashkun" }
            };

            return list;
        }

        public BookDto Get(int id)
        {
            var book = _bookRepository.Get(id);
            return (
              _mapper.Map<BookDto>(
                  book
              )
          );
        }

        public void Create(BookDto book)
        {
            var item = _mapper.Map<BookEntity>(
                book
               );
            _bookRepository.Create(item);
        }
    }
}
