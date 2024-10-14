using Moq;
using WebAPI.Controllers;
using WebAPI.Services;

public class NewsStoryControllerTests
{
    private readonly Mock<IHackerNewsService> _hackerNewsServiceMock;
    private readonly NewsStoryController _controller;

    public NewsStoryControllerTests()
    {
        _hackerNewsServiceMock = new Mock<IHackerNewsService>();
        _controller = new NewsStoryController(_hackerNewsServiceMock.Object);
    }

    [Fact]
    public async Task GetNewestStories_ReturnsOk_WithValidStories()
    {
        // Arrange.
        var mockStories = new List<Story> 
        { 
            new Story { Title = "Story 1", Url = "http://hackernews.com/story1" },
            new Story { Title = "Story 2", Url = "http://.com/story2" }
        };

        _hackerNewsServiceMock.Setup(service => service.GetNewestStories()).ReturnsAsync(mockStories);

        // Act.
        var result = await _controller.GetNewestStories();

        // Assert.
        Assert.Equal(2, result.Count()); 
        Assert.True(result.Any(story => story.Title == "Story 1"), "Expected story with title 'Story 1' was found.");
        Assert.True(result.Any(story => story.Url == "http://.com/story2"), "Expected story with URL 'http://.com/story2' was found.");
    }

    [Fact]
    public async Task GetNewestStories_ReturnsNull_WhenServiceReturnsNull()
    {
        // Arrange.
        _hackerNewsServiceMock.Setup(service => service.GetNewestStories()).ReturnsAsync((List<Story>)null);

        // Act.
        var result = await _controller.GetNewestStories();

        // Assert.
        Assert.Null(result);
    }
}