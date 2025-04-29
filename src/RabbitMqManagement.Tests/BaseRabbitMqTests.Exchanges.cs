namespace MessageAid.RabbitMqManagement.Tests;

public partial class BaseRabbitMqTests
{
    [Test]
    public async Task ListExchanges()
    {
        var httpClient = new RabbitMqManagementClient(
            new HttpClient(), 
            ManagementAddress()
        );
        
        var items = httpClient.Exchanges();

        var count = 0;
        await foreach (var q in items)
        {
            count += 1;
        }
        
        Assert.That(count, Is.GreaterThanOrEqualTo(1));
    }

    [Test]
    public async Task GetExchange()
    {
        var httpClient = new RabbitMqManagementClient(
            new HttpClient(),
            ManagementAddress()
        );

        var item = await httpClient.GetExchange(ExistingExchangeName);

        Assert.That(item, Is.Not.Null);
    }
    
    [Test]
    public async Task CreateExchange()
    {
        var httpClient = new RabbitMqManagementClient(
            new HttpClient(),
            ManagementAddress()
        );

        await httpClient.CreateExchange("temp");
        var item = await httpClient.GetExchange("temp");
        Assert.That(item, Is.Not.Null);

        await httpClient.DeleteExchange("temp");
        item = await httpClient.GetExchange("temp");
        Assert.That(item, Is.Null);
    }
}