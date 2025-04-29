namespace MessageAid.RabbitMqManagement.Tests;

/// <summary>
/// Run Tests Against a Virtual Host (%2f) Connection
/// meaning because we have the `/` we should only show those.
/// </summary>
public class WithRootVirtualHost: BaseRabbitMqTests
{
    protected override Uri AmqpAddress()
    {
        return new Uri("amqp://guest:guest@localhost:5672/");
    }

    protected override Uri ManagementAddress()
    {
        return new Uri("http://guest:guest@localhost:15672/");
    }
}