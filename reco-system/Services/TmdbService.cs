using System.Text.Json;

namespace reco_system.Services
{
    public class TmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "57ba8bbd5d70e245c2ace5620a8bbbbc";

        public TmdbService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TmdbMovie>> SearchMovies(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<TmdbMovie>();

            var url = $"https://api.themoviedb.org/3/search/movie?api_key={_apiKey}&query={Uri.EscapeDataString(query)}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<TmdbMovie>();

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<TmdbSearchResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return data?.Results ?? new List<TmdbMovie>();
        }

        public async Task<TmdbMovieDetails?> GetMovieDetails(int id)
        {
            var url = $"https://api.themoviedb.org/3/movie/{id}?api_key={_apiKey}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TmdbMovieDetails>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }

    public class TmdbSearchResponse
    {
        public List<TmdbMovie>? Results { get; set; }
    }

    public class TmdbMovie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Poster_Path { get; set; }
        public double Vote_Average { get; set; }
    }

    public class TmdbMovieDetails
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Overview { get; set; }
        public string? Poster_Path { get; set; }
        public string? Backdrop_Path { get; set; }
        public string? Release_Date { get; set; }
        public double Vote_Average { get; set; }
        public int Runtime { get; set; }
        public List<TmdbGenre>? Genres { get; set; }
    }

    public class TmdbGenre
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}