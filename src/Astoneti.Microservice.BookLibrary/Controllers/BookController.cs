using Astoneti.Microservice.BookLibrary.Business.Contracts;
using Astoneti.Microservice.BookLibrary.Business.Dto;
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
        private IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookServise, IMapper mapper)
        {
            _bookService = bookServise;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<BookModel>> Get()
        {
            var list = _bookService.GetList();

            return Ok(_mapper.Map<List<BookModel>, List<BookDto>>(list));
        }
    }
}
