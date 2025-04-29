namespace MessageAid.RabbitMqManagement;

using System.Net;

public partial class RabbitMqManagementClient
{
    
    /// <summary>
    /// Create a user
    /// </summary>
    public async Task CreateUser(string username, string password, params RoleTags[] tags)
    {
        var path = $"/api/users/{username}?{RabbitPagination.Default().ToQueryString()}";
        var x = await _http.SimpleNullableGet<UserResponse>(path);

        if (x == null)
        {
            var req = new CreateUserRequest
            {
                Password = password,
                Tags = string.Join(",", tags.Select(x => x.ToString().ToLower()))
            };

            var resp = await _http.SimplePut(path, req);
        }
    }

    /// <summary>
    /// Set Permissions
    /// </summary>
    public async Task<HttpStatusCode> SetPermissions(string vhost, string username, string configure, string write, string read)
    {
        var path = $"/api/permissions/{vhost}/{username}";
        var payload = new SetPermissionRequest
        {
            Configure = configure,
            Read = read,
            Write = write
        };

        return await _http.SimplePut(path, payload);
    }

    /// <summary>
    /// Get RabbitMQ Extensions
    /// </summary>
    public async Task<List<RabbitMqAdminExtensionResponse>> GetExtensions()
    {
        var path = "/api/extensions";
        return await _http.SimpleGet<List<RabbitMqAdminExtensionResponse>>(path) ?? [];
    }

    /// <summary>
    /// Get RabbitMq Feature Flags
    /// </summary>
    /// <returns></returns>
    public async Task<List<RabbitMqFeatureFlagResponse>> GetFeatureFlags()
    {
        var path = "/api/feature-flags";
        return await _http.SimpleGet<List<RabbitMqFeatureFlagResponse>>(path) ?? [];
    }

    /// <summary>
    /// Get Deprecated Features
    /// </summary>
    public async Task<List<RabbitMqDeprecatedFeatureResponse>> GetDeprecatedFeatures()
    {
        var path = "/api/deprecated-features";
        try
        {
            return await _http.SimpleGet<List<RabbitMqDeprecatedFeatureResponse>>(path) ?? [];
        }
        catch (HttpRequestException)
        {
            return [];
        }
    }
    
    /// <summary>
    /// Get Deprecated features that are used
    /// </summary>
    public async Task<List<RabbitMqDeprecatedFeatureResponse>> GetDeprecatedFeaturesUsed()
    {
        try
        {
            var path = "/api/deprecated-features/used";
            return await _http.SimpleGet<List<RabbitMqDeprecatedFeatureResponse>>(path) ?? [];
        }
        catch (HttpRequestException)
        {
            return [];
        }
    }
}