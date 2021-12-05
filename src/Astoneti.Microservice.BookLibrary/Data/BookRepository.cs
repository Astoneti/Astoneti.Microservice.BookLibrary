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

        public BookEntity Get(int id)
        {
            return DbContext.Set<BookEntity>().FirstOrDefault(_ => _.Id == id);
        }

        public BookEntity Create(BookEntity entity)
        {
            DbContext.Set<BookEntity>().Add(entity);
            DbContext.SaveChanges();
            return entity;
        }

        public BookEntity Update(BookEntity entity)
        {
            DbContext.Set<BookEntity>().Update(entity);
            DbContext.SaveChanges();
            return entity;
        }

        public BookEntity Delete(int id)
        {
            var entity = DbContext.Set<BookEntity>().Find(id);
            DbContext.Set<BookEntity>().Remove(entity);
            DbContext.SaveChanges();
            return entity;
        }
    }
}
