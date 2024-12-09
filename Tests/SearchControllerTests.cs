using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TestApp1;

[TestClass]
public class SearchControllerTests
{
    private SearchController _controller;

    [TestInitialize]
    public void Setup()
    {
        _controller = new SearchController();
    }

    [TestMethod]
    public void SaveSearchInfo_ShouldReturnOk_WhenSearchStringIsValid()
    {
        // Arrange
        var searchString = "Test Author";

        // Act
        var result = _controller.SaveSearchInfo(searchString) as OkObjectResult;

        // Assert
        Assert.IsNotNull(result, "Result should not be null");
        Assert.AreEqual(200, result.StatusCode, "Status code should be 200 (OK)");
        var response = result.Value;
        Assert.IsNotNull(response, "Response value should not be null");
        Assert.AreEqual("is good", response.GetType().GetProperty("message")?.GetValue(response)?.ToString(), "Message should indicate success");
    }

    [TestMethod]
    public void GetSearchResults_ShouldReturnBooks_WhenSearchStringIsSet()
    {
        // Arrange
        Library.SetSearchString("Homer");

        // Act
        var result = _controller.GetSearchResults() as OkObjectResult;

        // Assert
        Assert.IsNotNull(result, "Result should not be null");
        Assert.AreEqual(200, result.StatusCode, "Status code should be 200 (OK)");
        var books = result.Value as List<Book>;
        Assert.IsNotNull(books, "Books should not be null");
        Assert.IsTrue(books.Count > 0, "Books list should contain results");
        Assert.IsTrue(books.TrueForAll(b => b.author.Contains("Homer")), "All returned books should match the search string");
    }

    [TestMethod]
    public void GetSearchResults_ShouldReturnEmptyList_WhenSearchStringDoesNotMatch()
    {
        // Arrange
        Library.SetSearchString("NonExistentAuthor");

        // Act
        var result = _controller.GetSearchResults() as OkObjectResult;

        // Assert
        Assert.IsNotNull(result, "Result should not be null");
        Assert.AreEqual(200, result.StatusCode, "Status code should be 200 (OK)");
        var books = result.Value as List<Book>;
        Assert.IsNotNull(books, "Books should not be null");
        Assert.AreEqual(0, books.Count, "Books list should be empty");
    }
}
