namespace WebAPI.Services
{
    public interface IHackerNewsService
    {
        Task<List<Story>> GetNewestStories();
    }
}