namespace MessageAid.RabbitMqManagement.Tests;

/// <summary>
/// Run Tests Against the Root (Not scoped at all)
/// </summary>
public class WithBareConnection: BaseRabbitMqTests
{
    protected override Uri AmqpAddress()
    {
        return new Uri("amqp://guest:guest@localhost:5672");
    }

    protected override Uri ManagementAddress()
    {
        return new Uri("http://guest:guest@localhost:15672");
    }
}