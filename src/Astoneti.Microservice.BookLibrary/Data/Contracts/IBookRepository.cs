using Astoneti.Microservice.BookLibrary.Data.Entities;
using System.Collections.Generic;

namespace Astoneti.Microservice.BookLibrary.Data.Contracts
{
    public interface IBookRepository
    {
        public List<BookEntity> GetList();
    }
}
