using Microsoft.Extensions.Logging;
using Moq;
using RegExAPI.Domain.Contracts;
using RegExAPI.Domain.Entities;
using RegExAPI.Domain.Features;

namespace RegExAPI.Tests;

[TestFixture]
public class RegExHandlerTests
{
    private RegExHandler _regExHandler;
    private Mock<ILogger<RegExHandler>> _loggerMock;
    private Mock<IJsonClient> _jsonClientMock;
    private Mock<IRegExService> _regExServiceMock;

    [SetUp]
    public void SetUp()
    {
        // Arrange: Create mocks for dependencies
        _loggerMock = new Mock<ILogger<RegExHandler>>();
        _jsonClientMock = new Mock<IJsonClient>();
        _regExServiceMock = new Mock<IRegExService>();

        // Arrange: Initialize the RegExHandler with the mocked dependencies
        _regExHandler = new RegExHandler(_loggerMock.Object, _jsonClientMock.Object, _regExServiceMock.Object);
    }

    [Test]
    public async Task Handle_WhenValidRequestProvided_ShouldReturnRegExResponse()
    {
        // Arrange
        var request = new RegExQuery("test");
        var cancellationToken = CancellationToken.None;

        var jsonEntries = new List<ReadEntry>
        {
            new ReadEntry { Id = 1, Title = "Test entry 1" },
            new ReadEntry { Id = 2, Title = "Test entry 2" }
        };

        var regexEntries = new List<RegExMatchInfo>
        {
            new RegExMatchInfo { Id = 1, Title = "Test entry 1", ExpressionIndex = 0 },
            new RegExMatchInfo { Id = 2, Title = "Test entry 2", ExpressionIndex = 0 }
        };

        _jsonClientMock.Setup(client => client.GetEntries()).ReturnsAsync(jsonEntries);
        _regExServiceMock.Setup(service => service.GetMatchingEntries(jsonEntries, request)).Returns(regexEntries);

        // Act
        var response = await _regExHandler.Handle(request, cancellationToken);

        // Assert
        Assert.IsNotNull(response);
        Assert.AreEqual(request.QueryString, response.RegExQuery);
        Assert.AreEqual(2, response.Count);
        Assert.AreEqual(jsonEntries.Count, response.Total);
        Assert.AreEqual(100, response.Percentage);
        Assert.AreEqual(jsonEntries.Count, response.Entries.Count);
        Assert.Greater(response.TimeElapsed, 0);
    }

}