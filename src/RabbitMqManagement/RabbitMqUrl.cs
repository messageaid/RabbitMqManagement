namespace MessageAid.RabbitMqManagement;

/// <summary>
/// Helper class
/// </summary>
public static class RabbitMqUrlConverter
{
    /// <summary>
    /// convert string to rabbitmq parts
    /// </summary>
    public static RabbitMqUrl ConvertToManagementUrl(string input)
    {
        return ConvertToManagementUrl(new Uri(input));
    }

    /// <summary>
    /// Convert Url into rabbitmq parts
    /// </summary>
    public static RabbitMqUrl ConvertToManagementUrl(Uri url)
    {
        var scheme = url.Scheme switch
        {
            "rabbitmq" => "http",
            "rabbitmqs" => "https",
            "amqp" => "http",
            "amqps" => "https",
            _ => url.Scheme
        };
        
        var port = scheme switch
        {
            "http" => url.Port == -1 ? 80 : url.Port,
            "https" => url.Port == -1 ? 443 : url.Port,
            _ => 15673
        };
        
        
        
        var urb = new UriBuilder
        {
            Scheme = scheme,
            Host = url.Host,
            Port = port,
        };

        string? basicCredentials = null;
        if (url.UserInfo != "")
        {
            basicCredentials = url.UserInfo;
        }
        
        var vhost = url.PathAndQuery == "/" ? "%2f" : url.PathAndQuery[1..];

        return new RabbitMqUrl(urb.Uri, vhost, basicCredentials);
    }
}

/// <summary>
/// RabbitMq Parts
/// </summary>
public record RabbitMqUrl(Uri Uri, string VirtualHost, string? BasicCredentials);