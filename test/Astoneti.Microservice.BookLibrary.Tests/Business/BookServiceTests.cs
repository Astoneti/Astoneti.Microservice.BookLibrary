using Astoneti.Microservice.BookLibrary.Business;
using Astoneti.Microservice.BookLibrary.Business.Dto;
using Astoneti.Microservice.BookLibrary.Data.Contracts;
using Astoneti.Microservice.BookLibrary.Data.Entities;
using AutoMapper;
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
            _mockBookRepository = new Mock<IBookRepository>(MockBehavior.Strict);

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
        public void Get_book_details_success()
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
    }
}
