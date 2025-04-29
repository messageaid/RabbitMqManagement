namespace MessageAid.RabbitMqManagement;

public partial class RabbitMqManagementClient
{
    /// <summary>
    /// Get all nodes
    /// </summary>
    public async Task<List<RabbitMqNode>> GetNodes()
    {
        // CloudAMQP - 401 "Not a monitor user"
        var nodes = await _http.SimpleGet<List<RabbitMqNode>>("/api/nodes");

        if (nodes == null)
            return new List<RabbitMqNode>();

        return nodes;
    }
}