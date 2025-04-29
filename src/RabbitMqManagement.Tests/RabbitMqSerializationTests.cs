namespace MessageAid.RabbitMqManagement.Tests;

public class RabbitMqSerializationTests
{
    [Test]
    public void Deserialize()
    {
        var json = @"{
        ""consumer_details"": [],
        ""arguments"": {
            ""x-queue-type"": ""classic""
        },
        ""auto_delete"": false,
        ""backing_queue_status"": {
            ""avg_ack_egress_rate"": 0,
            ""avg_ack_ingress_rate"": 0,
            ""avg_egress_rate"": 0,
            ""avg_ingress_rate"": 0,
            ""delta"": [
            ""delta"",
            ""undefined"",
            0,
            0,
            ""undefined""
                ],
            ""len"": 0,
            ""mode"": ""default"",
            ""next_deliver_seq_id"": 0,
            ""next_seq_id"": 0,
            ""q1"": 0,
            ""q2"": 0,
            ""q3"": 0,
            ""q4"": 0,
            ""target_ram_count"": ""infinity"",
            ""version"": 1
        },
        ""consumer_capacity"": 0,
        ""consumer_utilisation"": 0,
        ""consumers"": 0,
        ""deliveries"": [],
        ""durable"": true,
        ""effective_policy_definition"": {},
        ""exclusive"": false,
        ""exclusive_consumer_tag"": null,
        ""garbage_collection"": {
            ""fullsweep_after"": 65535,
            ""max_heap_size"": 0,
            ""min_bin_vheap_size"": 46422,
            ""min_heap_size"": 233,
            ""minor_gcs"": 326
        },
        ""head_message_timestamp"": null,
        ""idle_since"": ""2022-08-25T12:46:31.585+00:00"",
        ""incoming"": [],
        ""memory"": 15776,
        ""message_bytes"": 0,
        ""message_bytes_paged_out"": 0,
        ""message_bytes_persistent"": 0,
        ""message_bytes_ram"": 0,
        ""message_bytes_ready"": 0,
        ""message_bytes_unacknowledged"": 0,
        ""messages"": 0,
        ""messages_details"": {
            ""rate"": 0
        },
        ""messages_paged_out"": 0,
        ""messages_persistent"": 0,
        ""messages_ram"": 0,
        ""messages_ready"": 0,
        ""messages_ready_details"": {
            ""rate"": 0
        },
        ""messages_ready_ram"": 0,
        ""messages_unacknowledged"": 0,
        ""messages_unacknowledged_details"": {
            ""rate"": 0
        },
        ""messages_unacknowledged_ram"": 0,
        ""name"": ""test"",
        ""node"": ""rabbit@c4ef2965615d"",
        ""operator_policy"": null,
        ""policy"": null,
        ""recoverable_slaves"": null,
        ""reductions"": 424424,
        ""reductions_details"": {
            ""rate"": 0
        },
        ""single_active_consumer_tag"": null,
        ""state"": ""running"",
        ""type"": ""classic"",
        ""vhost"": ""test-temp""
    }";
        var http = new HttpClient();
        http.BaseAddress = new Uri("http://localhost:15672");

        var obj = http.Deserialize<RabbitMqHttpQueue>(json)!;

        Assert.That(obj.IdleSince, Is.Not.Null);
    }
}