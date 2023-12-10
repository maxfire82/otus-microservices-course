using System.Diagnostics;
using System.Text.Json;
using Confluent.Kafka;
using Orders.Domain.Models;

namespace Orders.Api.HostedServices;

public class KafkaConsumerService : BackgroundService
{
    private readonly string groupId = "test_group1";
    private readonly string bootstrapServers = "127.0.0.1:9092";
    private readonly string topic = "first-topic";
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("!!! CONSUMER STARTED 1 !!!\n");
        
        // Starting a new Task here because Consume() method is synchronous
        var task = Task.Run(() => ProcessQueue(stoppingToken), stoppingToken);

        return task;
    }
    
    private void ProcessQueue(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            GroupId = groupId,
            BootstrapServers = bootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        try
        {
            using (var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumerBuilder.Subscribe(topic);
                var cancelToken = new CancellationTokenSource();
                try
                {
                    while (true)
                    {
                        var consumer = consumerBuilder.Consume(stoppingToken);
                        var order = JsonSerializer.Deserialize<OrderDTO>(consumer.Message.Value);
                        Console.WriteLine($"{groupId} Processing Order Id: {order.Id}");
                    }
                }
                catch (OperationCanceledException)
                {
                    consumerBuilder.Close();
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }
}