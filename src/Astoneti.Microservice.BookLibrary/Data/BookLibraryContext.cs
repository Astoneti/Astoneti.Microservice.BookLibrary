using Astoneti.Microservice.BookLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Astoneti.Microservice.BookLibrary.Data
{
    public class BookLibraryContext : DbContext
    {
        public BookLibraryContext(DbContextOptions<BookLibraryContext> options)
        : base(options)
        {
        }

        public DbSet<BookEntity> Books { get; set; }
    }
}
