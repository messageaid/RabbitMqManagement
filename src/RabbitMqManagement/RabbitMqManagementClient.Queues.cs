namespace MessageAid.RabbitMqManagement;

using System.Runtime.CompilerServices;

public partial class RabbitMqManagementClient
{
    /// <summary>
    /// get queues
    /// </summary>
    public IAsyncEnumerable<RabbitMqHttpQueue> Queues(CancellationToken ct = default)
    {
        return Queues(RabbitPagination.Default(), ct);
    }
    
    /// <summary>
    /// get queues
    /// </summary>
    public IAsyncEnumerable<RabbitMqHttpQueue> Queues(int page, int perPage, CancellationToken ct = default)
    {
        return Queues(new RabbitPagination
        {
            Page = page,
            PageSize = perPage,
        }, ct);
    }
    
    /// <summary>
    /// get queues
    /// </summary>
    public async IAsyncEnumerable<RabbitMqHttpQueue> Queues(RabbitPagination pagination, [EnumeratorCancellation] CancellationToken ct = default)
    {
        var path = $"/api/queues/{_urlVirtualHost}?{pagination.ToQueryString()}";
        var page = await _http.SimpleGet<RabbitMqHttpPaginationResponse<RabbitMqHttpQueue>>(path, ct);

        if (page == null)
            yield break;

        while (page.Page <= page.PageCount)
        {
            var queues = page.Items;

            // TODO: A concept of "SYSTEM" or "DEFAULT" queues (that are filtered out by default)
            foreach (var queue in queues.Where(element => !element.VHost.StartsWith("amq.")))
            {
                // 2025-02-27 .. RabbitMQ's list API is missing data points such as "Idle Since"
                var fullQueue = await GetQueue(queue.Name, ct);
                if (fullQueue != null)
                    yield return fullQueue;
                else
                    yield return queue;
            }

            pagination.Page += 1;

            if (page.Page >= page.PageCount)
                yield break;

            path = $"/api/queues/{_urlVirtualHost}?{pagination.ToQueryString()}";
            page = await _http.SimpleGet<RabbitMqHttpPaginationResponse<RabbitMqHttpQueue>>(path, ct);
            if (page == null)
                yield break;
        }
    }
    
    public Task PurgeQueue(string name)
    {
        return PurgeQueue(_virtualHost, name);
    }

    public async Task PurgeQueue(string vhost, string name)
    {
        // /api/queues/vhost/name/contents
        await _http.SimpleDelete($"/api/queues/{vhost}/{name}/contents");
    }
    
    /// <summary>
    /// Get a queue
    /// </summary>
    public async Task<RabbitMqHttpQueue?> GetQueue(string name, CancellationToken ct = default)
    {
        return await _http.SimpleNullableGet<RabbitMqHttpQueue>($"/api/queues/{_urlVirtualHost}/{name}", ct);
    }
    
    /// <summary>
    /// </summary>
    public async Task CreateQueue(string name)
    {
        await CreateQueue(_urlVirtualHost, name);
    }

    public async Task CreateQueue(string vhost, string name)
    {
        var payload = new CreateRabbitMqQueue
        {
            Durable = true,
            AutoDelete = false
        };

        await _http.SimplePut($"/api/queues/{vhost}/{name}", payload);
    }
    
    /// <summary>
    /// delete a queue
    /// </summary>
    public async Task DeleteQueue(string name)
    {
        await _http.SimpleDelete($"/api/queues/{_urlVirtualHost}/{name}");
    }
}