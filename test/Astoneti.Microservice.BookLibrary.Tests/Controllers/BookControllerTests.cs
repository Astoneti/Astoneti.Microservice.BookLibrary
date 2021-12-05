using Astoneti.Microservice.BookLibrary.Business.Contracts;
using Astoneti.Microservice.BookLibrary.Business.Dto;
using Astoneti.Microservice.BookLibrary.Controllers;
using Astoneti.Microservice.BookLibrary.Data.Entities;
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
        private readonly Mock<IBookService> _mockBookService;
        private readonly IMapper _mapper;

        private readonly BookController _controller;

        public BookControllerTests()
        {
            _mockBookService = new Mock<IBookService>();

            _mapper = new MapperConfiguration(
                    cfg => cfg.AddMaps(
                        typeof(Startup).Assembly
                    )
                )
                .CreateMapper();

            _controller = new BookController(
                _mockBookService.Object,
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

            _mockBookService
                .Setup(x => x.GetList())
                .Returns(dtos);

            // Act
            var result = _controller.GetList();

            // Assert
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var resultValue = Assert.IsAssignableFrom<IList<BookModel>>(okObjectResult.Value);

            resultValue
                .Should()
                .BeEquivalentTo(expectedResultValue);
        }

        [Fact]
        public void Get_WhenItemExists_Should_ReturnItem()
        {
            // Arrange
            const int id = 1;

            var dto = new BookDto()
            {
                Id = id,
                Author = "John",
                Title = "Test Tiitle"
            };

            var expectedResultValue = _mapper.Map<BookModel>(dto);

            _mockBookService
                .Setup(x => x.Get(id))
                .Returns(dto);

            // Act
            var result = _controller.Get(id);

            // Assert
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var resultValue = Assert.IsAssignableFrom<BookModel>(okObjectResult.Value);
            Assert.IsType<OkObjectResult>(result as OkObjectResult);

            resultValue
                .Should()
                .BeEquivalentTo(expectedResultValue);
        }

        [Fact]
        public void Get_WhenItemNotExists_Should_ReturnNotFound()
        {
            // Arrange
            const int id = 1;

            _mockBookService
                .Setup(_ => _.Get(id))
                .Returns(() => null);

            // Act
            var result = _controller.Get(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void Post_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testTitle = "Test Tiitle";
            var testBook = new BookPostModel
            {
                Title = testTitle
            };
            var item = new BookEntity
            {
                Title = testTitle,
                Id = 999
            };

            var itemDto =
                _mapper.Map<BookDto>(testBook);

            _mockBookService
                .Setup(x => x.Add(It.IsAny<BookDto>()))
                .Returns(item);

            // Act
            var result = _controller.Post(testBook);

            // Assert
            var createdResponse = result as CreatedAtActionResult;
            var resultValue = createdResponse.Value as BookEntity;

            resultValue.Should().BeEquivalentTo(item);

            Assert.IsType<CreatedAtActionResult>(createdResponse);
            Assert.IsType<BookEntity>(item);
            Assert.Equal("Test Tiitle", item.Title);
        }

        [Fact]
        public void Delete_WhenItemIsNotExists_Should_ReturnNotFound()
        {
            // Arrange
            const int id = 1;

            _mockBookService
                .Setup(_ => _.Get(id))
                .Returns(() => null);

            // Act
            var result = _controller.Delete(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_Should_RemovesOneItem()
        {
            // Arrange
            const int id = 1;

            var testItem = new BookDto()
            {
                Id = id,
                Author = "John",
                Title = "Test Tiitle"
            };

            _mapper.Map<BookModel>(testItem);

            _mockBookService
                .Setup(x => x.Get(id))
                .Returns(testItem);

            // Act
            var result = _controller.Delete(testItem.Id);
            

            Assert.Equal(1, testItem.Id);
            Assert.Equal("Test Tiitle", testItem.Title);
            Assert.Equal("John", testItem.Author);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Put_Should_UpdateCreatedItem()
        {

            // Arrange
            var item = new BookEntity()
            {
                Id = 1,
                Author = "John",
                Title = "Test Tiitle"
            };

            var testTitle = "Test Tiitle";
            var testBook = new BookModel
            {
                Title = testTitle
            };

            var itemDto =
                _mapper.Map<BookDto>(testBook);

            _mockBookService
                .Setup(x => x.Update(It.IsAny<BookDto>()))
                .Returns(item);

            // Act
            var result = _controller.Put(testBook);

            // Assert
            var createdResponse = result as OkObjectResult;
            var resultValue = createdResponse.Value as BookEntity;

            resultValue.Should().BeEquivalentTo(item);

            Assert.IsType<OkObjectResult>(createdResponse);
            Assert.IsType<BookEntity>(item);
            Assert.Equal("Test Tiitle", item.Title);
        }
    }
}
