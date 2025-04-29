namespace MessageAid.RabbitMqManagement;

public partial class RabbitMqManagementClient
{
    /// <summary>
    /// create a vhost
    /// </summary>
    public async Task CreateVHost(string name)
    {
        await _http.SimplePut($"/api/vhosts/{name}");
    }

    /// <summary>
    /// create a vhost
    /// </summary>
    public async Task CreateVHost()
    {
        await CreateVHost(_urlVirtualHost);
    }

    /// <summary>
    /// delete a vhost
    /// </summary>
    public async Task DeleteVHost(string name)
    {
        await _http.SimpleDelete($"/api/vhosts/{name}");
    }

    /// <summary>
    /// delete a vhost
    /// </summary>
    public async Task DeleteVHost()
    {
        await DeleteVHost(_urlVirtualHost);
    }

    /// <summary>
    /// Get a vhost
    /// </summary>
    public async Task<RabbitMqHttpVHost?> GetVHost()
    {
        return await GetVHost(_urlVirtualHost);
    }

    /// <summary>
    /// Get a vhost
    /// </summary>
    public async Task<RabbitMqHttpVHost?> GetVHost(string vhost)
    {
        return await _http.SimpleNullableGet<RabbitMqHttpVHost>($"/api/vhosts/{vhost}");
    }
}