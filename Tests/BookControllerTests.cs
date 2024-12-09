using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TestApp1;

[TestClass]
public class BooksControllerTests
{
    private BooksController _controller;

    [TestInitialize]
    public void Setup()
    {
        _controller = new BooksController();
    }

    [TestMethod]
    public void AddBook_ShouldReturnOk_WhenBookIsValid()
    {
        // Arrange
        var validBook = new Book(0, "Test Title", "Test Author", 10, "Test Period");

        // Act
        var result = _controller.AddBook(validBook) as OkObjectResult;

        // Assert
        Assert.IsNotNull(result, "Result should not be null");
        Assert.AreEqual(200, result.StatusCode, "Status code should be 200 (OK)");
        var response = result.Value;
        Assert.IsNotNull(response, "Response value should not be null");
        Assert.AreEqual("Book added", response.GetType().GetProperty("message")?.GetValue(response)?.ToString(), "Message should indicate success");
    }

    [TestMethod]
    public void AddBook_ShouldReturnBadRequest_WhenBookIsInvalid()
    {
        // Arrange
        var invalidBook = new Book(0, "", "", -1, "");

        // Act
        var result = _controller.AddBook(invalidBook) as BadRequestObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(400, result.StatusCode);
        var response = result.Value;
        Assert.IsNotNull(response);
        Assert.AreEqual("Bad book info", response.GetType().GetProperty("message")?.GetValue(response)?.ToString());
    }

    [TestMethod]
    public void DeleteBook_ShouldReturnOk_WhenBookExists()
    {
        // Arrange
        var existingBook = Library.GetBooks().First();

        // Act
        var result = _controller.DeleteBook(existingBook.Id) as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        var response = result.Value;
        Assert.IsNotNull(response);
        Assert.AreEqual("Book deleted", response.GetType().GetProperty("message")?.GetValue(response)?.ToString());
    }

    [TestMethod]
    public void DeleteBook_ShouldReturnNotFound_WhenBookDoesNotExist()
    {
        // Arrange
        var nonExistentBookId = -1;

        // Act
        var result = _controller.DeleteBook(nonExistentBookId) as NotFoundObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(404, result.StatusCode);
        var response = result.Value;
        Assert.IsNotNull(response);
        Assert.AreEqual("Book not found", response.GetType().GetProperty("message")?.GetValue(response)?.ToString());
    }

    [TestMethod]
    public void GetBooks_ShouldReturnListOfBooks_WhenTypeIsList()
    {
        // Act
        var result = _controller.GetBooks("list") as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        Assert.IsInstanceOfType(result.Value, typeof(List<Book>));
    }

    [TestMethod]
    public void GetBooks_ShouldReturnChartValues_WhenTypeIsChart()
    {
        // Act
        var result = _controller.GetBooks("chart") as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        Assert.IsInstanceOfType(result.Value, typeof(int[]));
    }

    [TestMethod]
    public void GetBooks_ShouldReturnBadRequest_WhenTypeIsInvalid()
    {
        // Act
        var result = _controller.GetBooks("invalid") as BadRequestObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(400, result.StatusCode);
        var response = result.Value;
        Assert.IsNotNull(response);
        Assert.AreEqual("Bad target", response.GetType().GetProperty("message")?.GetValue(response)?.ToString());
    }
}
