using Astoneti.Microservice.BookLibrary.Business.Dto;
using System.Collections.Generic;

namespace Astoneti.Microservice.BookLibrary.Business.Contracts
{
    public interface IBookService
    {
        List<BookDto> GetList();

        BookDto Get(int id);

        void Create(BookDto book);
    }
}
