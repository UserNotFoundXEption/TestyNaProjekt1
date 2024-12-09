using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TestApp1;

[TestClass]
public class CountChangeControllerTests
{
    private CountChangeController _controller;

    [TestInitialize]
    public void Setup()
    {
        _controller = new CountChangeController();
    }

    [TestMethod]
    public void Change_ShouldReturnOk_WhenIncreaseCommandIsValid()
    {
        // Arrange
        var book = new Book(0, "Test Book", "Test Author", 10, "Test Period");
        Library.AddBook(book);
        var command = $"i{book.Id}";

        // Act
        var result = _controller.Change(command) as OkResult;

        // Assert
        Assert.IsNotNull(result, "Result should not be null");
        Assert.AreEqual(200, result.StatusCode, "Status code should be 200 (OK)");
        Assert.AreEqual(11, Library.GetBooks().Find(b => b.Id == book.Id).copies, "Book copies should be incremented by 1");
    }

    [TestMethod]
    public void Change_ShouldReturnOk_WhenDecreaseCommandIsValid()
    {
        // Arrange
        var book = new Book(0, "Test Book", "Test Author", 10, "Test Period");
        Library.AddBook(book);
        var command = $"d{book.Id}";

        // Act
        var result = _controller.Change(command) as OkResult;

        // Assert
        Assert.IsNotNull(result, "Result should not be null");
        Assert.AreEqual(200, result.StatusCode, "Status code should be 200 (OK)");
        Assert.AreEqual(9, Library.GetBooks().Find(b => b.Id == book.Id).copies, "Book copies should be decremented by 1");
    }

    [TestMethod]
    public void Change_ShouldReturnBadRequest_WhenCommandIsInvalid()
    {
        // Arrange
        var command = "x123";

        // Act
        var result = _controller.Change(command) as BadRequestObjectResult;

        // Assert
        Assert.IsNotNull(result, "Result should not be null");
        Assert.AreEqual(400, result.StatusCode, "Status code should be 400 (Bad Request)");
        var response = result.Value;
        Assert.IsNotNull(response, "Response value should not be null");
        Assert.AreEqual("Wrong command for increase/decrease", response.GetType().GetProperty("message")?.GetValue(response)?.ToString(), "Message should indicate wrong command");
    }

    [TestMethod]
    public void Change_ShouldReturnBadRequest_WhenBookDoesNotExist()
    {
        // Arrange
        var command = "i9999";

        // Act
        var result = _controller.Change(command) as BadRequestObjectResult;

        // Assert
        Assert.IsNotNull(result, "Result should not be null");
        Assert.AreEqual(400, result.StatusCode, "Status code should be 400 (Bad Request)");
        var response = result.Value;
        Assert.IsNotNull(response, "Response value should not be null");
        Assert.AreEqual("Couldn't decrease/increase", response.GetType().GetProperty("message")?.GetValue(response)?.ToString(), "Message should indicate failure to change count");
    }
}
