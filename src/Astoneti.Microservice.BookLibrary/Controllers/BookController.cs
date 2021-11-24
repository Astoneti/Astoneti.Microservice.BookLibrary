using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Astoneti.Microservice.BookLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<BookModel> GetBooks()
        {
            var list = new List<BookModel>()
            {
                new BookModel(){ Id = 1, Title = "My First boob", Author = "Anton Pashkun" },
                new BookModel(){ Id = 2, Title = "Code First", Author = "Anton Pashkun" },
                new BookModel(){ Id = 3, Title = "How to become a developer from scratch", Author = "Anton Pashkun" }
            };
            return list;
        }
    }
}
