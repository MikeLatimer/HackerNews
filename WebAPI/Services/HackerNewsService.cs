using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace WebAPI.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private const string BaseUrl = "https://hacker-news.firebaseio.com/v0/";
        private readonly HttpClient _httpClient;
        private readonly ILogger<HackerNewsService> _logger;
        private readonly IMemoryCache _memoryCache;

        public HackerNewsService(HttpClient httpClient, ILogger<HackerNewsService> logger, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        // Fetches the list of story IDs.
        public async Task<List<int>> GetStoryIds()
        {
            return await FetchDataAsync<List<int>>($"{BaseUrl}newstories.json");
        }

        // Fetches the details for a specific story by its ID.
        public async Task<Story> GetStoryDetailById(int id)
        {
            var story = await FetchDataAsync<Story>($"{BaseUrl}item/{id}.json");
            return new Story
            {
                Title = story?.Title,
                Url = story?.Url
            };
        }

        // Fetches the newest stories.
        public async Task<List<Story>> GetNewestStories()
        {
            const string cacheKey = "NewestStories";

            // Try to get data from the cache first.
            if (_memoryCache.TryGetValue(cacheKey, out List<Story> cachedStories))
            {
                return cachedStories; 
            }

            // If cache does not contain data, fetch from the API.
            var storyIds = await GetStoryIds();
            var stories = await Task.WhenAll(storyIds.Select(GetStoryDetailById));
            var result = stories.Where(story => !string.IsNullOrEmpty(story.Url)).ToList();

            // Store the result in cache with expiration of 15 minutes.
            _memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15), 
            });

            return result;
        }

        // Helper method for fetching and deserializing data from the given URL.
        private async Task<T> FetchDataAsync<T>(string url)
        {
            try
            {
                var response = await _httpClient.GetStringAsync(url);
                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning($"Received empty response from {url}"); 
                    return default;
                }
                return JsonConvert.DeserializeObject<T>(response);          
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, $"Error fetching data from {url}");
                return default;
            }
            catch (JsonSerializationException jsonEx)
            {
                _logger.LogError(jsonEx, $"Error deserializing data from {url}");
                return default;
            }
        }
    }
}