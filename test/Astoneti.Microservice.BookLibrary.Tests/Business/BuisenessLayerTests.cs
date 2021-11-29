using Astoneti.Microservice.BookLibrary.Business;
using Astoneti.Microservice.BookLibrary.Business.Dto;
using System.Collections.Generic;
using Xunit;

namespace Astoneti.Microservice.BookLibrary.Tests
{
    public class BuisenessLayerTests
    {
        [Fact]
        public void GetList_WhenParametersIsValid_Should_ReturnAspectedResult()
        {
            var expected = new List<BookDto>()
            {
                new BookDto(){ Id = 1, Title = "My First book", Author = "Anton Pashkun" },
                new BookDto(){ Id = 2, Title = "Code First", Author = "Anton Pashkun" },
                new BookDto(){ Id = 3, Title = "How to become a developer from scratch", Author = "Anton Pashkun" }
            };

            var undertest = new BookService();

            var result = undertest.GetList();

            Assert.Equal(expected, result);
        }
    }
}
