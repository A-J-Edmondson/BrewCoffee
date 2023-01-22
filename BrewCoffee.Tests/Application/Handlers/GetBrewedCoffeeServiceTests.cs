using BrewCoffee.Application.Services;
using BrewCoffee.Application.Models;
using FluentAssertions;
using System;
using System.Globalization;
using Xunit;

namespace BrewCoffee.Tests
{
    public class GetBrewedCoffeeServiceTests
    {
        [Fact]
        public void ShouldReturnNotNullResponse_WhenCallingGetBrewedCoffee()
        {
            //Arrange
            var service = new GetBrewedCoffeeService();

            //Act
            var result = service.GetBrewedCoffee();

            //Assert
            result.Should().BeOfType<Response>();
            result.Should().NotBeNull();
        }

        [Fact]
        public void ShouldReturnResponseWithCorrectMessage_WhenCallingGetBrewedCoffee()
        {
            //Arrange
            var expectedMessage = "Your piping hot coffee is ready";
            var service = new GetBrewedCoffeeService();

            //Act
            var result = service.GetBrewedCoffee();

            //Assert
            result.Should().BeOfType<Response>();
            result.Message.Should().BeEquivalentTo(expectedMessage);
        }

        [Fact]
        public void ShouldReturnResponseWithPreparedInTheCorrectDateFormat_WhenCallingGetBrewedCoffee()
        {
            //Arrange
            var expectedDateFormat = "yyyy-MM-ddTHH:mm:sszzz";
            var service = new GetBrewedCoffeeService();

            //Act
            var result = service.GetBrewedCoffee();

            //Assert
            result.Should().BeOfType<Response>();
            Assert.True(DateTime.TryParseExact(result.Prepared, expectedDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _));
        }
    }
}