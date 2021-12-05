using Astoneti.Microservice.BookLibrary.Data.Entities;
using System.Collections.Generic;

namespace Astoneti.Microservice.BookLibrary.Data.Contracts
{
    public interface IBookRepository
    {
        public List<BookEntity> GetList();

        public BookEntity Get(int id);

        public BookEntity Create(BookEntity entity);

        public BookEntity Update(BookEntity entity);

        public BookEntity Delete(int id);
    }
}
