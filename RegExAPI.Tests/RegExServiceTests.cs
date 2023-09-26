using Microsoft.Extensions.Logging;
using Moq;
using RegExAPI.Domain.Entities;
using RegExAPI.Infrastructure.Services;

namespace RegExAPI.Tests;

[TestFixture]
public class RegExServiceTests
{
    private RegExService _regexService;

    [SetUp]
    public void SetUp()
    {
        var loggerMock = new Mock<ILogger<RegExService>>();
        _regexService = new RegExService(loggerMock.Object);
    }

    [Test]
    public void Test_GetMatchingEntries_WithValidEntries_ShouldReturnMatchingEntries()
    {
        // Arrange
        var entries = new List<ReadEntry>
        {
            new ReadEntry { Id = 1, Title = "This is a test entry" },
            new ReadEntry { Id = 2, Title = "Another test entry" },
            new ReadEntry { Id = 3, Title = "Not a match" }
        };

        var query = new RegExQuery("test");

        // Act
        var result = _regexService.GetMatchingEntries(entries, query);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(1, result[0].Id);
        Assert.AreEqual(2, result[1].Id);
    }

    [Test]
    public void Test_GetMatchingEntries_WithNoMatches_ShouldReturnEmptyList()
    {
        // Arrange
        var entries = new List<ReadEntry>
        {
            new ReadEntry { Id = 1, Title = "This won't match the regex" },
            new ReadEntry { Id = 2, Title = "Another unmatched sentence" }
        };

        var query = new RegExQuery("test");

        // Act
        var result = _regexService.GetMatchingEntries(entries, query);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
    }
}