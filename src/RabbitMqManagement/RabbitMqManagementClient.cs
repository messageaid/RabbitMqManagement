namespace MessageAid.RabbitMqManagement;

using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

/// <summary>
/// https://pulse.mozilla.org/api/index.html
/// </summary>
[DebuggerDisplay("{DebuggerDisplay()}")]
public partial class RabbitMqManagementClient: IDisposable
{
    readonly HttpClient _http;
    readonly string _urlVirtualHost;
    readonly string _virtualHost;

    /// <summary>
    /// Create with a general HttpClient
    /// </summary>
    public RabbitMqManagementClient(Uri uri)
        : this(new HttpClient(), uri)
    {
        
    }

    /// <summary>
    /// Create with a specific Http Client
    /// </summary>
    public RabbitMqManagementClient(HttpClient http, Uri uri)
    {
        _http = http;

        var ru = RabbitMqUrlConverter.ConvertToManagementUrl(uri);
        _http.BaseAddress = ru.Uri;
        _http.Timeout = TimeSpan.FromSeconds(10);
        
        _urlVirtualHost = ru.VirtualHost;
        _virtualHost = ru.VirtualHost;
        if (_virtualHost == "%2f")
            _virtualHost = "/";

        
        var ui = uri.UserInfo;
        if (ui != "")
        {
            var byteArray = Encoding.ASCII.GetBytes(ui);
            var b64 = Convert.ToBase64String(byteArray);
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", b64);    
        }
    }
    
    /// <summary>
    /// check the connection
    /// </summary>
    public async Task CheckConnection()
    {
        var vhost = await _http.SimpleGet<RabbitMqHttpVHost>("/api/whoami");
    }


    /// <inheritdoc />
    public void Dispose()
    {
        _http.Dispose();
    }

    string DebuggerDisplay()
    {
        return _http.BaseAddress?.ToString() ?? "unknown";
    }
}