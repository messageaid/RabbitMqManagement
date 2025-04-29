namespace MessageAid.RabbitMqManagement;

using System.Runtime.CompilerServices;

public partial class RabbitMqManagementClient
{
   
    /// <summary>
    /// get exchanges
    /// </summary>
    public IAsyncEnumerable<RabbitMqHttpExchange> Exchanges(CancellationToken ct = default)
    {
        return Exchanges(RabbitPagination.Default(), ct);
    }
    
    /// <summary>
    /// get exchanges
    /// </summary>
    public IAsyncEnumerable<RabbitMqHttpExchange> Exchanges(int page, int perPage, CancellationToken ct = default)
    {
        return Exchanges(new RabbitPagination
        {
            Page = page,
            PageSize = perPage,
        }, ct);
    }
    
    /// <summary>
    /// get exchanges
    /// </summary>
    public async IAsyncEnumerable<RabbitMqHttpExchange> Exchanges(RabbitPagination pagination, [EnumeratorCancellation] CancellationToken ct = default)
    {
        var path =$"/api/exchanges/{_urlVirtualHost}?{pagination.ToQueryString()}";
        var page = await _http.SimpleGet<RabbitMqHttpPaginationResponse<RabbitMqHttpExchange>>(path, ct);

        if (page == null)
            yield break;

        while (page.Page <= page.PageCount)
        {
            var exchanges = page.Items;

            foreach (var exchange in exchanges)
            {
                yield return exchange;
            }

            pagination.Page += 1;

            if (page.Page >= page.PageCount)
                yield break;

            path = $"/api/exchanges/{_urlVirtualHost}?{pagination.ToQueryString()}";
            page = await _http.SimpleGet<RabbitMqHttpPaginationResponse<RabbitMqHttpExchange>>(path, ct);
            if (page == null)
                yield break;
        }
    }
    
    /// <summary>
    /// Get an exchange
    /// </summary>
    public async Task<RabbitMqHttpExchange?> GetExchange(string name, CancellationToken ct = default)
    {
        return await _http.SimpleGet<RabbitMqHttpExchange>($"/api/exchanges/{_urlVirtualHost}/{name}", ct);
    }
    
    /// <summary>
    /// get exchanges
    /// </summary>
    public async IAsyncEnumerable<RabbitMqHttpExchange> Exchanges()
    {
        var pagination = RabbitPagination.Default();
        var uri = $"/api/exchanges/{_urlVirtualHost}?{pagination.ToQueryString()}";
        var page = await _http.SimpleGet<RabbitMqHttpPaginationResponse<RabbitMqHttpExchange>>(uri);

        if (page == null)
            yield break;

        while (page.Page <= page.PageCount)
        {
            var exchanges = page.Items
                .Where(element => !element.VHost.StartsWith("amq."));

            foreach (var exchange in exchanges)
            {
                yield return exchange;
            }

            pagination.Page += 1;

            if (page.Page >= page.PageCount)
                yield break;

            uri = $"/api/exchanges/{_urlVirtualHost}?{pagination.ToQueryString()}";
            page = await _http.SimpleGet<RabbitMqHttpPaginationResponse<RabbitMqHttpExchange>>(uri);
            if (page == null)
                yield break;
        }
    }

    /// <summary>
    /// Get an exchange
    /// </summary>
    public async Task<RabbitMqHttpExchange?> GetExchange(string name)
    {
        return await _http.SimpleNullableGet<RabbitMqHttpExchange>($"/api/exchanges/{_urlVirtualHost}/{name}");
    } 
    
    /// <summary>
    /// create an exchange
    /// </summary>
    public async Task CreateExchange(string name)
    {
        await CreateExchange(_urlVirtualHost, name);
    }

    /// <summary>
    /// create an exchange
    /// </summary>
    public async Task CreateExchange(string vhost, string name)
    {
        var payload = new CreateRabbitMqExchange("direct", false, true);
        await _http.SimplePut($"/api/exchanges/{vhost}/{name}", payload);
    }

    /// <summary>
    /// delete an exchange
    /// </summary>
    public async Task DeleteExchange(string name)
    {
        await _http.SimpleDelete($"/api/exchanges/{_urlVirtualHost}/{name}");
    }
}