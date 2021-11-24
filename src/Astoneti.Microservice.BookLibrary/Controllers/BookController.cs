using Astoneti.Microservice.BookLibrary.Business.Contracts;
using Astoneti.Microservice.BookLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Astoneti.Microservice.BookLibrary.Controllers
{
    [Route("books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookService _bookService;

        public BookController(IBookService bookServise)
        {
            _bookService = bookServise;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<BookModel>> GetList()
        {
            var list = _bookService.GetList();

            return Ok(list);
        }
    }
}
