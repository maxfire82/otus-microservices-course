using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Confluent.Kafka;
using Orders.Domain;
using Orders.Domain.Entity;
using Orders.Domain.Models;

namespace Orders.Api.HostedServices;

public class KafkaProducerService : BackgroundService
{
    private readonly string bootstrapServers = "127.0.0.1:9092";
    private readonly string topic = "first-topick";
    private long _count = 1;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                ClientId = Dns.GetHostName()
            };

            var order = new OrderDTO()
            {
                Id = _count,
                Amount = 10,
                Description = "First message"
            };
            
            string message = JsonSerializer.Serialize(order);
            
            try
            {
                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync
                    (topic, new Message<Null, string>
                    {
                        Value = message
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }

            _count++;
            await Task.Delay(5000);
        }
    }
}