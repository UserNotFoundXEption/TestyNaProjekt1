using System;
using System.Collections.Generic;
using System.Linq;
using TestApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class LibraryTests
{
    [TestMethod]
    public void AddBook_ShouldReturnTrue_WhenBookIsValid()
    {
        // Arrange
        var newBook = new Book(0, "Test Title", "Test Author", 10, "Test Period");

        // Act
        var result = Library.AddBook(newBook);

        // Assert
        Assert.IsTrue(result);
        Assert.IsTrue(Library.GetBooks().Contains(newBook));
    }

    [TestMethod]
    public void AddBook_ShouldReturnFalse_WhenBookIsInvalid()
    {
        // Arrange
        var invalidBook = new Book(0, "", "", -1, "");

        // Act
        var result = Library.AddBook(invalidBook);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GetBooks_ShouldReturnAllBooks()
    {
        // Act
        var books = Library.GetBooks();

        // Assert
        Assert.IsNotNull(books);
        Assert.IsTrue(books.Count > 0);
    }

    [TestMethod]
    public void GetBooksBySearch_ShouldReturnMatchingBooks()
    {
        // Arrange
        Library.SetSearchString("Homer");

        // Act
        var result = Library.GetBooksBySearch();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.All(book => book.author.Contains("Homer", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public void GetBooksBySearch_ShouldReturnNull_WhenSearchStringIsNotSet()
    {
        // Arrange
        Library.SetSearchString(null);

        // Act
        var result = Library.GetBooksBySearch();

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public void DeleteBook_ShouldReturnTrue_WhenBookExists()
    {
        // Arrange
        var bookId = Library.GetBooks().First().Id;

        // Act
        var result = Library.DeleteBook(bookId);

        // Assert
        Assert.IsTrue(result);
        Assert.IsFalse(Library.GetBooks().Any(book => book.Id == bookId));
    }

    [TestMethod]
    public void DeleteBook_ShouldReturnFalse_WhenBookDoesNotExist()
    {
        // Arrange
        var invalidId = -1;

        // Act
        var result = Library.DeleteBook(invalidId);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IncreaseCount_ShouldReturnTrue_WhenBookExists()
    {
        // Arrange
        var bookId = Library.GetBooks().First().Id;
        var initialCopies = Library.GetBooks().First(b => b.Id == bookId).copies;

        // Act
        var result = Library.IncreaseCount(bookId);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(initialCopies + 1, Library.GetBooks().First(b => b.Id == bookId).copies);
    }

    [TestMethod]
    public void DecreaseCount_ShouldReturnTrue_WhenBookExists()
    {
        // Arrange
        var bookId = Library.GetBooks().First().Id;
        var initialCopies = Library.GetBooks().First(b => b.Id == bookId).copies;

        // Act
        var result = Library.DecreaseCount(bookId);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(initialCopies - 1, Library.GetBooks().First(b => b.Id == bookId).copies);
    }

    [TestMethod]
    public void DecreaseCount_ShouldReturnFalse_WhenBookDoesNotExist()
    {
        // Arrange
        var invalidId = -1;

        // Act
        var result = Library.DecreaseCount(invalidId);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GetChartValues_ShouldReturnCorrectCounts()
    {
        // Act
        var chartValues = Library.GetChartValues();

        // Assert
        Assert.IsNotNull(chartValues);
        Assert.AreEqual(8, chartValues.Length);
        Assert.IsTrue(chartValues.All(value => value >= 0));
    }
}