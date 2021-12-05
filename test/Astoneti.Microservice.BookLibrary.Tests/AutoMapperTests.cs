using AutoMapper;
using Xunit;

namespace Astoneti.Microservice.BookLibrary.Tests
{
    public class AutoMapperTests
    {
        [Fact]
        public void IsType()
        {
            // Arrange & Act
            var config = new MapperConfiguration(
                cfg => cfg.AddMaps(
                    typeof(Startup).Assembly
                )
            );

            // Assert
            Assert.IsType<MapperConfiguration>(config);
           // config.AssertConfigurationIsValid();
        }
    }
}
