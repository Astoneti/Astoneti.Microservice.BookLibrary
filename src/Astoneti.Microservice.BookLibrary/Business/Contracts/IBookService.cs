using Astoneti.Microservice.BookLibrary.Business.Dto;
using Astoneti.Microservice.BookLibrary.Data.Entities;
using System.Collections.Generic;

namespace Astoneti.Microservice.BookLibrary.Business.Contracts
{
    public interface IBookService
    {
        List<BookDto> GetList();

        BookDto Get(int id);

        BookEntity Add(BookDto dto);

        BookEntity Update(BookDto dto);    

        BookEntity Remove(BookDto dto);
    }
}
