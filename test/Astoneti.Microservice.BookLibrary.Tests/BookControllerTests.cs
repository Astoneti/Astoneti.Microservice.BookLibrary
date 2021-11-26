using Astoneti.Microservice.BookLibrary.Business.Contracts;
using Astoneti.Microservice.BookLibrary.Business.Dto;
using Astoneti.Microservice.BookLibrary.Controllers;
using Astoneti.Microservice.BookLibrary.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Astoneti.Microservice.BookLibrary.Tests
{
    public class BookControllerTests
    {
        private readonly Mock<IBookService> _bookService;
        private readonly Mock<IMapper> _mapper;
        private readonly BookController undertest;


        public BookControllerTests()
        {
            _bookService = new Mock<IBookService>();
            _mapper = new Mock<IMapper>();
            undertest = new BookController(_bookService.Object, _mapper.Object);
        }

        [Fact]
        public void Get_WhenParametersIsValid_Should_ReturnAspectedResult()
        {
            // Arrange
            var id = 15;
            var list = new List<BookDto>() { new BookDto { Id = id, Author="Anton" } };
            var bookModel = new BookModel { Id = id, Author="Anton" };
            var bookModels = new List<BookModel> { bookModel };
            var expected = new List<BookModel> { bookModel };

            _bookService
                .Setup(x => x.GetList())
                .Returns(list);
            _mapper
                 .Setup(x => x.Map<List<BookDto>, List<BookModel>>(list))
                .Returns(bookModels);

            // Act
            var result = undertest.Get();
            
            // Assert
            var resultFromController = (result.Result as OkObjectResult).Value as IEnumerable<BookModel>;
            Assert.Equal(expected, resultFromController);
        }
    }
}
