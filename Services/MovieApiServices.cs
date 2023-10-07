using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Movie.Models;
using Movie.Options;
using Newtonsoft.Json;

namespace Movie.Services
{
    public class MovieApiServices : IMovieApiServices
    {
        private readonly IMemoryCache memoryCache;

        public string BaseUrl { get; set; }

        public string ApiKey { get; set; }
        public HttpClient httpClient { get; set; }
        public MovieApiServices(IHttpClientFactory httpClientFactory,IOptions<MovieApiOptions> options, IMemoryCache memoryCache)
        {
            //BaseUrl = "http://www.omdbapi.com/";
            //ApiKey = "ed71c037";
            BaseUrl = options.Value.BaseUrl;
            ApiKey = options.Value.ApiKey;
            httpClient = httpClientFactory.CreateClient();
            this.memoryCache = memoryCache;
        }
        public async Task<MovieApiResponse> SearchByTitleAsync(string title)
        {
            MovieApiResponse result;
            if (!memoryCache.TryGetValue(title.ToLower(), out result))
            {
                var response = await httpClient.GetAsync($"{BaseUrl}?s={title}&apikey={ApiKey}");
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<MovieApiResponse>(json);
                if (result.Response == "False")
                {
                    throw new Exception(result.Error);
                }
                var casheTime = new MemoryCacheEntryOptions();
                casheTime.SetAbsoluteExpiration(TimeSpan.FromDays(10));
                memoryCache.Set(title.ToLower(), result);
            }
            return result;
        }

        public async Task<Cinema> SearchByIdAsync(string id)
        {
            Cinema result;
            if(!memoryCache.TryGetValue(id, out result))
            {
                var response = await httpClient.GetAsync($"{BaseUrl}?apikey={ApiKey}&i={id}");
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Cinema>(json);
                if (result.Response == "False")
                {
                    throw new Exception(result.Error);
                }
                var casheTime = new MemoryCacheEntryOptions();
                casheTime.SetAbsoluteExpiration(TimeSpan.FromDays(10));

                memoryCache.Set(id, result,casheTime);
            }
            
            return result;
        }
    }
}
