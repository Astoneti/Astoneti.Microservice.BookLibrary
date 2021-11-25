using Astoneti.Microservice.BookLibrary.Data.Contracts;
using Astoneti.Microservice.BookLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Astoneti.Microservice.BookLibrary.Data
{
    public class BookRepository : IBookRepository
    {
        protected DbContext DbContext { get; }

        public BookRepository(BookLibraryContext dbContext)
        {
            DbContext = dbContext;
        }
        public List<BookEntity> GetList()
        {
            return DbContext.Set<BookEntity>().ToList();
        }
    }
}
