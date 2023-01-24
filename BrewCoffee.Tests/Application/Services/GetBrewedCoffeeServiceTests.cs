using BrewCoffee.Application.Services;
using BrewCoffee.Application.Models;
using FluentAssertions;
using System;
using Xunit;
using System.Globalization;
using BrewCoffee.Application.Extensions;
using System.IO;
using Newtonsoft.Json;
using BrewCoffee.Application.Exceptions;
using Moq;
using BrewCoffee.Application.Interfaces;

namespace BrewCoffee.Tests.Application.Services;

public class GetBrewedCoffeeServiceTests
{
    [Fact]
    public void ShouldReturnNotNullResponse_WhenCallingGetBrewedCoffee()
    {
        //Arrange
        var mockDateExtensions = new Mock<IDateExtensions>();
        mockDateExtensions.Setup(x => x.GetCurrentDateInMonthsAndDays()).Returns("03-02");
        var service = new GetBrewedCoffeeService(mockDateExtensions.Object);

        //Act
        var result = service.GetBrewedCoffee();

        //Assert
        result.Should().BeOfType<Response>();
        result.Should().NotBeNull();
    }

    [Fact]
    public void ShouldReturnResponseWithCorrectMessageAndPreparedInTheCorrectDateFormat_WhenCallingGetBrewedCoffee()
    {
        //Arrange
        var mockDateExtensions = new Mock<IDateExtensions>();
        mockDateExtensions.Setup(x => x.GetCurrentDateInMonthsAndDays()).Returns("03-02");
        var service = new GetBrewedCoffeeService(mockDateExtensions.Object);
        var expectedMessage = "Your piping hot coffee is ready";
        var expectedDateFormat = "yyyy-MM-ddTHH:mm:sszzz";

        //Act
        if (GetCoffeeMachineUseCount() % 5 == 0)
        {
            service.GetBrewedCoffee();
        }

        var result = service.GetBrewedCoffee();

        //Assert
        result.Should().BeOfType<Response>();
        Assert.True(DateTime.TryParseExact(result.Prepared, expectedDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _));
        result.Message.Should().BeEquivalentTo(expectedMessage);
    }

    [Fact]
    public void ShouldThrowOutOfCoffeeException_WhenCallingGetBrewedCoffeeForThe5thTime()
    {
        //Arrange
        var mockDateExtensions = new Mock<IDateExtensions>();
        mockDateExtensions.Setup(x => x.GetCurrentDateInMonthsAndDays()).Returns("03-02");
        var service = new GetBrewedCoffeeService(mockDateExtensions.Object);

        //Act
        int useCount = GetCoffeeMachineUseCount();
        useCount++;
        while (useCount % 5 != 0) {
            service.GetBrewedCoffee();
            useCount++;
        }

        //Assert
        Assert.Throws<OutOfCoffeeException>(() => { service.GetBrewedCoffee(); });
    }

    [Fact]
    public void ShouldThrowOutOfServiceException_WhenCallingGetBrewedCoffeeOnApril1st()
    {
        //Arrange
        var mockDateExtensions = new Mock<IDateExtensions>();
        mockDateExtensions.Setup(x => x.GetCurrentDateInMonthsAndDays()).Returns("04-01");
        var service = new GetBrewedCoffeeService(mockDateExtensions.Object);

        //Assert
        Assert.Throws<OutOfServiceException>(() => { service.GetBrewedCoffee(); });
    }

    #region Private Methods

    private int GetCoffeeMachineUseCount()
    {
        // Get filepath of CoffeeMachine.json
        var solutionDirectory = DirectoryExtensions.GetSolutionDirectory();

        // Fetch data from CoffeeMachine.json
        string? coffeeMachineData;
        try
        {
            coffeeMachineData = File.ReadAllText(Path.Combine(solutionDirectory, "BrewCoffee.Application\\Cache\\CoffeeMachine.json"));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        var coffeeMachine = JsonConvert.DeserializeObject<CoffeeMachine>(coffeeMachineData);
        return Convert.ToInt32(coffeeMachine?.UseCount);
    }

    #endregion

}
