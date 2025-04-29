namespace MessageAid.RabbitMqManagement.Tests;

public partial class BaseRabbitMqTests
{
    [Test]
    public async Task ListQueues()
    {
        var httpClient = new RabbitMqManagementClient(
            new HttpClient(), 
            ManagementAddress()
        );
        
        var items = httpClient.Queues();

        var count = 0;
        await foreach (var q in items)
        {
            count += 1;
        }
        
        Assert.That(count, Is.EqualTo(1));
    }

    [Test]
    public async Task GetQueue()
    {
        var httpClient = new RabbitMqManagementClient(
            new HttpClient(),
            ManagementAddress()
        );

        var item = await httpClient.GetQueue(ExistingQueueName);

        Assert.That(item, Is.Not.Null);
    }

    [Test]
    public async Task CreateQueue()
    {
        var httpClient = new RabbitMqManagementClient(
            new HttpClient(),
            ManagementAddress()
        );

        await httpClient.CreateQueue("temp");
        var item = await httpClient.GetQueue("temp");
        Assert.That(item, Is.Not.Null);

        await httpClient.DeleteQueue("temp");
        item = await httpClient.GetQueue("temp");
        Assert.That(item, Is.Null);
    }
}