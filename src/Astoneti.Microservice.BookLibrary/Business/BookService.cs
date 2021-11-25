using Astoneti.Microservice.BookLibrary.Business.Contracts;
using Astoneti.Microservice.BookLibrary.Models;
using System.Collections.Generic;

namespace Astoneti.Microservice.BookLibrary.Business
{
    public class BookService : IBookService
    {
        public List<BookModel> GetList()
        {
            var list = new List<BookModel>()
            {
                new BookModel(){ Id = 1, Title = "My First book", Author = "Anton Pashkun" },
                new BookModel(){ Id = 2, Title = "Code First", Author = "Anton Pashkun" },
                new BookModel(){ Id = 3, Title = "How to become a developer from scratch", Author = "Anton Pashkun" }
            };

            return list;
        }
    }
}
