using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class NewsStoryController : ControllerBase
{
    private readonly IHackerNewsService _hackerNewsService;

    public NewsStoryController(IHackerNewsService hackerNewsService)
    {
        _hackerNewsService = hackerNewsService;
    }

    [HttpGet]
    public async Task<IEnumerable<Story>> GetNewestStories()
    {
       return await _hackerNewsService.GetNewestStories();
    }
}

