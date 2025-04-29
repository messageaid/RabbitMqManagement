// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global
#pragma warning disable CS8618
namespace MessageAid.RabbitMqManagement;

/// <summary>
/// a rabbitmq node stats
/// </summary>
public class RabbitMqNode
{
    // partitions: []

    /// <summary>
    /// the OS PID on the node
    /// </summary>
    public string OsPid { get; set; } = "";

    /// <summary>
    /// total file descriptors
    /// </summary>
    public int FdTotal { get; set; } = 0;

    // "sockets_total": 943629,
    // "mem_limit": 3334959923,
    // "mem_alarm": false,
    // "disk_free_limit": 50000000,
    // "disk_free_alarm": false,
    // "proc_total": 1048576,
    // "rates_mode": "basic",

    /// <summary>
    /// how long has this been up?
    /// </summary>
    public int Uptime { get; set; } = 0;

    // run_queue: 1
    // processors: 5
    // exchange types []
    // auth_mechanisms []
    // applications []
    // contexts []
    // log_files: []
    // db_dir: ""
    // config_files: []
    // net_ticktime: 60

    /// <summary>
    /// what plugins have been enabled
    /// </summary>
    public List<string> EnabledPlugins { get; set; } = new();

    // mem_calculation_strategy
    // ra_open_file_metrics

    /// <summary>
    /// what is the name
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// what is the type
    /// </summary>
    public string Type { get; set; } = "";

    /// <summary>
    /// is it running
    /// </summary>
    public bool Running { get; set; } = false;

    // mem_used
    // mem_used_details
    // fd_used
    // fd_used_details
    // sockets_used
    // sockets_used_detail
    // ...bunch of other bull
}