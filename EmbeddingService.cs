using System.Net.Http.Json;
using System.Text.Json.Serialization;

class EmbeddingService
{
    private readonly HttpClient _http;
    private readonly string _model;

    public EmbeddingService(string baseUrl = "http://localhost:11434", string model = "nomic-embed-text")
    {
        _http = new HttpClient { BaseAddress = new Uri(baseUrl) };
        _model = model;
    }

    public async Task<float[]> GenerateAsync(string text)
    {
        var response = await _http.PostAsJsonAsync("/api/embeddings", new { model = _model, prompt = text });
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<EmbedResponse>();
        return result!.Embedding;
    }

    private record EmbedResponse([property: JsonPropertyName("embedding")] float[] Embedding);
}
