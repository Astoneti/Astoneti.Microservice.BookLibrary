using Astoneti.Microservice.BookLibrary.Business.Dto;
using System.Collections.Generic;

namespace Astoneti.Microservice.BookLibrary.Business.Contracts
{
    public interface IBookService
    {
        List<BookDto> GetList();
    }
}
