using Astoneti.Microservice.BookLibrary.Business;
using Astoneti.Microservice.BookLibrary.Business.Dto;
using Astoneti.Microservice.BookLibrary.Data.Contracts;
using Astoneti.Microservice.BookLibrary.Data.Entities;
using AutoMapper;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Astoneti.Microservice.BookLibrary.Tests
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly IMapper _mapper;

        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _mockBookRepository = new Mock<IBookRepository>();

            _mapper = new MapperConfiguration(
                    cfg => cfg.AddMaps(
                        typeof(Startup).Assembly
                    )
                )
                .CreateMapper();

            _bookService = new BookService(
               _mockBookRepository.Object,
               _mapper
           );
        }

        [Fact]
        public void GetList_Should_ReturnExpectedResult()
        {
            // Arrange
            var entities = new List<BookEntity>()
            {
                new BookEntity(){ Id = 1, Title = "My First book", Author = "Anton Pashkun" },
                new BookEntity(){ Id = 2, Title = "Code First", Author = "Anton Pashkun" },
                new BookEntity(){ Id = 3, Title = "How to become a developer from scratch", Author = "Anton Pashkun" }
            };

            var expectedResultValue = _mapper.Map<IList<BookDto>>(entities);

            _mockBookRepository
                .Setup(x => x.GetList())
                .Returns(entities);

            // Act
            var result = _bookService.GetList();

            // Assert
            Assert.IsType<List<BookDto>>(result);
            Assert.NotNull(result);
            Assert.Equal(expectedResultValue.Count, result.Count);
        }

        [Fact]
        public void Get_Should_ReturnItemById()
        {
            // Arrange
            const int id = 1;

            var entities = new BookEntity()
            {
                Id = id,
                Author = "John",
                Title = "Test Tiitle"
            };

            var expectedResultValue = _mapper.Map<BookDto>(entities);

            _mockBookRepository
                .Setup(x => x.Get(id))
                .Returns(entities);

            // Act
            var result = _bookService.Get(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<BookDto>(result);
            Assert.Equal(expectedResultValue.Id, result.Id);
            Assert.Equal(expectedResultValue.Title, result.Title);
            Assert.Equal(expectedResultValue.Author, result.Author);
            Assert.Equal(1, 1);
        }

        [Fact]
        public void Post_Should_CreateNewItemAndAddInDb()
        {
            // Arrange
            var book = new BookDto
            {
                Id = 111
            };

            var item = _mapper.Map<BookEntity>(book);

            var expectedItem = new BookEntity();

            _mockBookRepository
                .Setup(x => x.Create(It.IsAny<BookEntity>()))
                .Callback((BookEntity entity )=>
                {
                    expectedItem = entity;
                });

            // Act
            _bookService.Add(book);

            // Assert
            expectedItem
                .Should().BeEquivalentTo(item);
            _mockBookRepository
                .Verify(x => x.Create(It.IsAny<BookEntity>()), Times.Once);
            Assert.NotNull(expectedItem);
        }

        [Fact]
        public void  Delete_Should_FindItemByIdAndRemoveFromDb()
        {
            const int id = 1;
            // Arrange
            var book = new BookDto
            {
                Id = id,
                Author = "John",
                Title = "Test Tiitle"
            };
            var testItem = _mapper.Map<BookEntity>(book);



            _mockBookRepository
                .Setup(x => x.Get(id))
                .Returns(testItem);
               

            // Act
            var result = _bookService.Remove(book);

            //Assert
            testItem
                .Should().BeEquivalentTo(result);
            Assert.Equal(1, testItem.Id);
            Assert.Equal(1, result.Id);
           // Assert.IsAssignableFrom<BookDto>(result);    
        }

        [Fact]
        public void Put_Should_UpdateAndReturnsUpdatedItem()
        {
            const int id = 1;
            // Arrange
            var book = new BookDto
            {
                Id = id,
                Author = "John",
                Title = "Test Tiitle"
            };
            var item = _mapper.Map<BookEntity>(book);

            var expectedItem = new BookEntity();

            _mockBookRepository
                .Setup(x => x.Update(It.IsAny<BookEntity>()))
                .Callback((BookEntity entity) =>
                {
                    expectedItem = entity;
                });
            // Act
            _bookService.Update(book);
            //Assert
           expectedItem
                .Should().BeEquivalentTo(item);
            Assert.Equal(1, expectedItem.Id);
           // Assert.IsAssignableFrom<BookDto>(expectedItem);
            
        }
    }
}

