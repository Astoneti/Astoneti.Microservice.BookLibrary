using Astoneti.Microservice.BookLibrary.Business.Contracts;
using Astoneti.Microservice.BookLibrary.Business.Dto;
using System.Collections.Generic;

namespace Astoneti.Microservice.BookLibrary.Business
{
    public class BookService : IBookService
    {
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
    }
}
