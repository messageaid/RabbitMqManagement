// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace MessageAid.RabbitMqManagement;

/// <summary>
/// Create a rabbit mq queue
/// </summary>
public class CreateRabbitMqQueue
{
    /// <summary>
    /// should it auto delete
    /// </summary>
    public bool AutoDelete { get; set; }

    /// <summary>
    /// is it durable
    /// </summary>
    public bool Durable { get; set; }
}