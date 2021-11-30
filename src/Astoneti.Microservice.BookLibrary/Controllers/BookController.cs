using Astoneti.Microservice.BookLibrary.Business.Contracts;
using Astoneti.Microservice.BookLibrary.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Astoneti.Microservice.BookLibrary.Controllers
{
    [Route("books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookServise, IMapper mapper)
        {
            _bookService = bookServise;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetList()
        {
            return Ok(
                _mapper.Map<IList<BookModel>>(
                    _bookService.GetList()
                )
            );
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var item = _bookService.Get(id);

            if (item == null)
            {
                return NotFound();
            }
               
            return Ok(
                _mapper.Map<BookModel>(
                    item
                )
            );
        }
    }
}
