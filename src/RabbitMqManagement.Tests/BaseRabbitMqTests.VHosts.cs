namespace MessageAid.RabbitMqManagement.Tests;

public partial class BaseRabbitMqTests
{
    [Test]
    public async Task GetVHost()
    {
        var httpClient = new RabbitMqManagementClient(
            new HttpClient(),
            ManagementAddress()
        );

        var item = await httpClient.GetVHost();

        Assert.That(item, Is.Not.Null);
    }
}