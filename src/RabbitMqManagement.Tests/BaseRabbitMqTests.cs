namespace MessageAid.RabbitMqManagement.Tests;

public abstract partial class BaseRabbitMqTests
{
    [SetUp]
    public async Task SetUp()
    {
        var u = RabbitMqUrlConverter.ConvertToManagementUrl(AmqpAddress());
        if (u.VirtualHost != "%2f")
        {
            var httpClient = new RabbitMqManagementClient(
                new HttpClient(), 
                ManagementAddress()
            );
            
            await httpClient.CreateVHost();
        }
     
        // Create User and Permissions
        
        var factory = new RabbitMQ.Client.ConnectionFactory();
        factory.Uri = AmqpAddress();
        
        await using var conn = await factory.CreateConnectionAsync();
        await using var channel = await conn.CreateChannelAsync();
        
        
        
        await channel.QueueDeclareAsync(ExistingQueueName, 
            true, 
            false,
            false);
        
        await channel.ExchangeDeclareAsync(ExistingExchangeName, 
            "direct",
            false,
            false);

        await channel.QueueBindAsync(
            ExistingQueueName,
            ExistingExchangeName,
            ""
        );
    }

    public string ExistingQueueName => "test-queue";
    public string ExistingExchangeName => "test-exchange";
    
    protected abstract Uri AmqpAddress();
    protected abstract Uri ManagementAddress();
}