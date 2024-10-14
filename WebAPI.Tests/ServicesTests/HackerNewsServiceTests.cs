using Moq;
using System.Net;
using WebAPI.Services;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Moq.Protected;
using Microsoft.Extensions.Caching.Memory;

public class HackerNewsServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly Mock<ILogger<HackerNewsService>> _loggerMock;
    private readonly HackerNewsService _hackerNewsService;
    private readonly Mock<IMemoryCache> _memoryCacheMock;


    public HackerNewsServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _loggerMock = new Mock<ILogger<HackerNewsService>>();
        _memoryCacheMock = new Mock<IMemoryCache>();
        _hackerNewsService = new HackerNewsService(_httpClient, _loggerMock.Object, _memoryCacheMock.Object);
    }

    [Fact]
    public async Task GetStoryIds_ReturnsEmptyList_WhenApiCallReturnsEmptyResponse()
    {
        // Arrange.
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("[]")
        };

        _httpMessageHandlerMock
        .Protected()
        .Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        )
        .ReturnsAsync(responseMessage);


        // Act.
        var result = await _hackerNewsService.GetStoryIds();

        // Assert.
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetStoryDetailById_ReturnsStory_WhenApiCallSucceeds()
    {
        // Arrange.
        var mockStory = new Story
        {
            Title = "Story 1",
            Url = "http://example.com/story1"
        };
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(mockStory))
        };

        _httpMessageHandlerMock
        .Protected()
        .Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        )
        .ReturnsAsync(responseMessage);

        // Act.
        var result = await _hackerNewsService.GetStoryDetailById(1);

        // Assert.
        Assert.NotNull(result);
        Assert.Equal("Story 1", result.Title);
    }

    [Fact]
    public async Task GetStoryDetailById_ReturnsNullValues_WhenApiCallReturnsEmpty()
    {
        // Arrange.
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("")
        };

        _httpMessageHandlerMock
        .Protected()
        .Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        )
        .ReturnsAsync(responseMessage);

        // Act.
        var result = await _hackerNewsService.GetStoryDetailById(1);

        // Assert.
        Assert.True(result.Title == null);
        Assert.True(result.Url == null);
    }

}