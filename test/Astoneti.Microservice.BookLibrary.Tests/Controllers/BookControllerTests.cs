using Astoneti.Microservice.BookLibrary.Business.Contracts;
using Astoneti.Microservice.BookLibrary.Business.Dto;
using Astoneti.Microservice.BookLibrary.Controllers;
using Astoneti.Microservice.BookLibrary.Models;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Astoneti.Microservice.BookLibrary.Tests
{
    public class BookControllerTests
    {
        private readonly Mock<IBookService> _bookService;
        private readonly IMapper _mapper;

        private readonly BookController _controller;

        public BookControllerTests()
        {
            _bookService = new Mock<IBookService>(MockBehavior.Strict);

            _mapper = new MapperConfiguration(
                    cfg => cfg.AddMaps(
                        typeof(Startup).Assembly
                    )
                )
                .CreateMapper();

            _controller = new BookController(
                _bookService.Object,
                _mapper
            );
        }

        [Fact]
        public void GetList_WhenParametersIsValid_Should_ReturnExpectedResult()
        {
            // Arrange
            var dtos = new List<BookDto>()
            {
                new BookDto
                {
                    Id = 1,
                    Author = "John"
                }
            };

            var expectedResultValue = _mapper.Map<IList<BookModel>>(dtos);

            _bookService
                .Setup(x => x.GetList())
                .Returns(dtos);

            // Act
            var result = _controller.GetList();

            // Assert
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var resultValue = Assert.IsAssignableFrom<IList<BookModel>>(okObjectResult.Value);

            resultValue.Should().BeEquivalentTo(expectedResultValue);
        }

        [Fact]
        public void Get_WhenParametersIsValid_Should_ReturnExpectedResultById()
        {
            // Arrange
            int testBookId = 1;

            var dtos = new BookDto()
            {
                Id = 1,
                Author = "Johny",
                Title = "Test Book"
            };

            var expectedResultValue = _mapper.Map<BookModel>(dtos);

            _bookService
                .Setup(x => x.Get(testBookId))
                .Returns(dtos);

            // Act
            var result = _controller.Get(testBookId);

            // Assert
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var resultValue = Assert.IsAssignableFrom<BookModel>(okObjectResult.Value);

            resultValue.Should().BeEquivalentTo(expectedResultValue);
        }

        [Fact]
        public void Get_ReturnsNotFoundResultWhenItemNotFound()
        {
            // Arrange
            int testBookById = 5;

            _bookService
                .Setup(_ => _.Get(testBookById))
                .Returns(null as BookDto);

            // Act
            var result = _controller.Get(testBookById);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
