namespace MessageAid.RabbitMqManagement;

using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

public static class HttpExtensions
{
    static readonly JsonSerializerOptions _options;

    static HttpExtensions()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new RabbitMqDateTimeJsonConverter());
        _options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;    
    }
    
    /// <summary>
    /// A simple get
    /// </summary>
    public static async Task<T?> SimpleGet<T>(this HttpClient http, string path, CancellationToken ct = default)
        where T : class
    {
        var msg = new HttpRequestMessage(HttpMethod.Get, path);


        var response = await http.SendAsync(msg, ct);
        if (response.StatusCode == HttpStatusCode.Unauthorized)
            throw new HttpRequestException($"Unauthorized: {path}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new HttpRequestException($"Couldn't find {path}");


        await using var stream = await response.Content.ReadAsStreamAsync(ct);

        try
        {
            var payload = JsonSerializer.Deserialize<T>(stream, _options);
            return payload;
        }
        catch (JsonException)
        {
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            var str = await reader.ReadToEndAsync(ct);
            
            throw;
        }
    }
    
    /// <summary>
    /// A simple get
    /// </summary>
    public static async Task<T?> SimpleNullableGet<T>(this HttpClient http, string path, CancellationToken ct = default)
        where T : class
    {
        var msg = new HttpRequestMessage(HttpMethod.Get, path);


        var response = await http.SendAsync(msg, ct);
        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        await using var stream = await response.Content.ReadAsStreamAsync(ct);
        using var reader = new StreamReader(stream);
        var payload = JsonSerializer.Deserialize<T>(stream, _options);
        return payload;
    }
    
    /// <summary>
    /// delete
    /// </summary>
    public static async Task SimpleDelete(this HttpClient client, string path)
    {
        using var response = await client.DeleteAsync(path);
        // using var reader = new StreamReader(await response.Content.ReadAsStreamAsync());
        // using var jsonReader = new JsonTextReader(reader);
        // var serializer = new JsonSerializer();
        // var payload = serializer.Deserialize<TOutput>(jsonReader);
        // return payload;
    }
    
    /// <summary>
    /// POST
    /// </summary>
    public static async Task<HttpStatusCode> SimplePost(this HttpClient client, string path)
    {
        using var response = await client.PostAsync(path, null);
        return response.StatusCode;
    }
    
    /// <summary>
    /// put
    /// </summary>
    public static async Task<HttpStatusCode> SimplePut(this HttpClient client, string path)
    {
        HttpContent content = JsonContent.Create(null, typeof(object), MediaTypeHeaderValue.Parse("application/json"));
        using var response = await client.PutAsync(path, content);
        return response.StatusCode;
    }

    /// <summary>
    /// put
    /// </summary>
    public static async Task<HttpStatusCode> SimplePut<TInput>(this HttpClient client, string path, TInput input)
    {
        HttpContent content = JsonContent.Create(input);
        using var response = await client.PutAsync(path, content);
        return response.StatusCode;
    }
}