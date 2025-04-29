namespace MessageAid.RabbitMqManagement;

using System.Runtime.CompilerServices;

public partial class RabbitMqManagementClient
{
    /// <summary>
    /// get bindings
    /// </summary>
    public async IAsyncEnumerable<RabbitMqHttpBinding> Bindings([EnumeratorCancellation] CancellationToken ct = default)
    {
        var bindings = await _http.SimpleGet<List<RabbitMqHttpBinding>>($"/api/bindings/{_urlVirtualHost}", ct);

        if (bindings == null)
            yield break;

        foreach (var binding in bindings)
        {
            yield return binding;
        }
    }
    
    /// <summary>
    /// Get a binding
    /// </summary>
    public async Task<RabbitMqHttpExchange?> GetBinding(string source, string destinationType, string destinationName, string propKey, CancellationToken ct = default)
    {
        var uri = $"/api/bindings/{_urlVirtualHost}/e/{source}/e/{destinationName}/{propKey}";
        if(destinationType == "queue")
            uri = $"/api/bindings/{_urlVirtualHost}/e/{source}/q/{destinationName}/{propKey}";
        
        return await _http.SimpleGet<RabbitMqHttpExchange>(uri, ct);
    }
    
    /// <summary>
    /// create a binding
    /// </summary>
    public async Task CreateQueueBinding(string exchange, string queue)
    {
        await CreateQueueBinding(_urlVirtualHost, exchange, queue);
    }

    /// <summary>
    /// create a binding
    /// </summary>
    public async Task CreateQueueBinding(string vhost, string exchange, string queue)
    {
        await _http.SimplePost($"/api/bindings/{vhost}/e/{exchange}/q/{queue}");
    }
    
    /// <summary>
    /// create a binding
    /// </summary>
    public async Task CreateExchangeBinding(string exchange1, string exchange2)
    {
        await CreateExchangeBinding(_urlVirtualHost, exchange1, exchange2);
    }

    /// <summary>
    /// create a binding
    /// </summary>
    public async Task CreateExchangeBinding(string vhost, string exchange1, string exchange2)
    {
        await _http.SimplePost($"/api/bindings/{vhost}/e/{exchange1}/e/{exchange2}");
    }
}