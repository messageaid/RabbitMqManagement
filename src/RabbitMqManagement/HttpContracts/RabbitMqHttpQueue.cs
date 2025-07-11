// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global
#pragma warning disable CS8618
namespace MessageAid.RabbitMqManagement;

using System.Diagnostics;
using System.Text.Json.Serialization;

/// <summary>
/// This is a DTO for the RabbitMQManagement API
/// </summary>
[DebuggerDisplay("{DebuggerDisplay()}")]
public class RabbitMqHttpQueue
{
    /// <summary>
    /// auto-delete
    /// </summary>
    public bool AutoDelete { get; set; }

    /// <summary>
    /// queue type
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// node name
    /// </summary>
    public string Node { get; set; }

    /// <summary>
    /// durable
    /// </summary>
    public bool Durable { get; set; }

    /// <summary>
    /// number of messages
    /// </summary>
    public int Messages { get; set; }

    /// <summary>
    /// message byte size
    /// </summary>
    public int MessageBytes { get; set; }

    /// <summary>
    /// queue has been idle
    /// </summary>
    public DateTime? IdleSince { get; set; }

    /// <summary>
    /// timestamp on the head message
    /// </summary>
    public DateTime? HeadMessageTimestamp { get; set; }

    /// <summary>
    /// </summary>
    public int MessagesPagedOut { get; set; }

    /// <summary>
    /// </summary>
    public int MessagesPersistent { get; set; }

    /// <summary>
    /// </summary>
    public int MessagesRam { get; set; }

    /// <summary>
    /// </summary>
    public int MessagesReady { get; set; }

    /// <summary>
    /// </summary>
    public int MessageUnacknowledged { get; set; }

    /// <summary>
    /// is this queue in a vhost
    /// </summary>
    [JsonPropertyName("vhost")]
    public string VHost { get; set; }

    /// <summary>
    /// queue name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Message Details
    /// </summary>
    public RabbitMqHttpQueueDetails MessagesDetails { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? Policy { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? OperatorPolicy { get; set; }

    string DebuggerDisplay()
    {
        if (VHost == "/")
            return $"/{Name}";
        
        return $"{VHost}/{Name}";
    }
}

/// <summary>
/// Queue Details
/// </summary>
public record RabbitMqHttpQueueDetails(decimal Rate);