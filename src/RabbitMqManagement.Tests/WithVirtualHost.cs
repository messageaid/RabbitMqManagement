namespace MessageAid.RabbitMqManagement.Tests;

/// <summary>
/// Run Tests Against a Virtual Host Connection
/// </summary>
public class WithVirtualHost: BaseRabbitMqTests
{
    protected override Uri AmqpAddress()
    {
        return new Uri("amqp://guest:guest@localhost:5672/a-vhost");
    }

    protected override Uri ManagementAddress()
    {
        return new Uri("http://guest:guest@localhost:15672/a-vhost");
    }
}