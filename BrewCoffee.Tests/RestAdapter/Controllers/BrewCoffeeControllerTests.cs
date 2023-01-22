using BrewCoffee.Application.Models;
using BrewCoffee.Application.Ports.In;
using BrewCoffee.Controllers;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace BrewCoffee.Tests.RestAdapter.Controllers
{
    public class BrewCoffeeControllerTests
    {
        private readonly Response _serviceResponse;

        public BrewCoffeeControllerTests()
        {
            _serviceResponse = new Response()
            {
                Message = "Your piping hot coffee is ready",
                Prepared = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
            };
        }

        [Fact]
        public void ShouldReturnNotNullResponse_WhenCallingGetCoffee()
        {
            //Arrange
            var mockedGetCoffeeService = new Mock<IGetBrewedCoffeeService>();
            mockedGetCoffeeService.Setup(x => x.GetBrewedCoffee()).Returns(_serviceResponse);
            var controller = new BrewCoffeeController(mockedGetCoffeeService.Object);

            //Act
            var result = controller.GetCoffee();

            //Assert
            result.Should().BeOfType<Response>();
            result.Should().NotBeNull();
        }

        [Fact]
        public void ShouldReturnCorrectResponse_WhenCallingGetCoffee()
        {
            //Arrange
            var mockedGetCoffeeService = new Mock<IGetBrewedCoffeeService>();
            mockedGetCoffeeService.Setup(x => x.GetBrewedCoffee()).Returns(_serviceResponse);
            var controller = new BrewCoffeeController(mockedGetCoffeeService.Object);

            //Act
            var result = controller.GetCoffee();

            //Assert
            result.Should().BeOfType<Response>();
            result.Should().BeEquivalentTo(_serviceResponse);
        }
    }
}
