// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
namespace MessageAid.RabbitMqManagement;

/// <summary>
/// Create a rabbit mq exchange
/// </summary>
public class CreateRabbitMqExchange
{
    /// <summary>
    /// ctor
    /// </summary>
    public CreateRabbitMqExchange(string type, bool autoDelete, bool durable)
    {
        Type = type;
        AutoDelete = autoDelete;
        Durable = durable;
    }

    /// <summary>
    /// the type of exchange
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// should it be auto deleted
    /// </summary>
    public bool AutoDelete { get; set; }

    /// <summary>
    /// is it durable
    /// </summary>
    public bool Durable { get; set; }
}