using Astoneti.Microservice.BookLibrary.Models;
using System.Collections.Generic;

namespace Astoneti.Microservice.BookLibrary.Business.Contracts
{
    public interface IBookService
    {
        List<BookModel> GetList();
    }
}
