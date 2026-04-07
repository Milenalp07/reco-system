using System.Text.Json;

namespace reco_system.Services
{
    public class GoogleBooksService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GoogleBooksService> _logger;

        public GoogleBooksService(HttpClient httpClient, ILogger<GoogleBooksService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<object>> SearchBooksAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<object>();

            var url = $"https://www.googleapis.com/books/v1/volumes?q={Uri.EscapeDataString(query)}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    _logger.LogWarning("Google Books API quota exceeded.");
                    return new List<object>();
                }

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Google Books API failed with status code: {StatusCode}", response.StatusCode);
                    return new List<object>();
                }

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                var results = new List<object>();

                if (!doc.RootElement.TryGetProperty("items", out var items))
                    return results;

                foreach (var item in items.EnumerateArray())
                {
                    var volumeInfo = item.TryGetProperty("volumeInfo", out var vi) ? vi : default;

                    string title = volumeInfo.ValueKind != JsonValueKind.Undefined &&
                                   volumeInfo.TryGetProperty("title", out var titleProp)
                        ? titleProp.GetString() ?? "Untitled"
                        : "Untitled";

                    string author = "Unknown Author";
                    if (volumeInfo.ValueKind != JsonValueKind.Undefined &&
                        volumeInfo.TryGetProperty("authors", out var authorsProp) &&
                        authorsProp.ValueKind == JsonValueKind.Array)
                    {
                        author = string.Join(", ",
                            authorsProp.EnumerateArray()
                                       .Select(a => a.GetString())
                                       .Where(a => !string.IsNullOrWhiteSpace(a))!);
                    }

                    string description = volumeInfo.ValueKind != JsonValueKind.Undefined &&
                                         volumeInfo.TryGetProperty("description", out var descProp)
                        ? descProp.GetString() ?? ""
                        : "";

                    string imageUrl = "https://via.placeholder.com/300x450?text=No+Image";
                    if (volumeInfo.ValueKind != JsonValueKind.Undefined &&
                        volumeInfo.TryGetProperty("imageLinks", out var imageLinks) &&
                        imageLinks.TryGetProperty("thumbnail", out var thumbProp))
                    {
                        imageUrl = thumbProp.GetString() ?? imageUrl;

                        if (!string.IsNullOrEmpty(imageUrl))
                        {
                            imageUrl = imageUrl.Replace("http://", "https://");
                        }
                    }

                    string publishedDate = volumeInfo.ValueKind != JsonValueKind.Undefined &&
                                           volumeInfo.TryGetProperty("publishedDate", out var publishedProp)
                        ? publishedProp.GetString() ?? ""
                        : "";

                    string category = "";
                    if (volumeInfo.ValueKind != JsonValueKind.Undefined &&
                        volumeInfo.TryGetProperty("categories", out var categoriesProp) &&
                        categoriesProp.ValueKind == JsonValueKind.Array)
                    {
                        category = string.Join(", ",
                            categoriesProp.EnumerateArray()
                                          .Select(c => c.GetString())
                                          .Where(c => !string.IsNullOrWhiteSpace(c))!);
                    }

                    results.Add(new
                    {
                        id = item.TryGetProperty("id", out var idProp) ? idProp.GetString() ?? "" : "",
                        title,
                        author,
                        description,
                        imageUrl,
                        publishedDate,
                        genre = category
                    });
                }

                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching Google Books");
                return new List<object>();
            }
        }
    }
}