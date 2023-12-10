using System.Net;
using System.Text.Json;
using Announcements.Domain.Models;
using Common.Configuration;
using Common.Queue.Services;
using Confluent.Kafka;

namespace Announcements.Api.HostedServices;

public class AnnouncementProducerService : BackgroundService
{
    private readonly string bootstrapServers;
    private readonly string topic = "announcements-topic";
    
    private IBackgroundQueue<AnnouncementDTO> _queue;

    public AnnouncementProducerService(IBackgroundQueue<AnnouncementDTO> queue, IConfiguration configuration)
    {
        _queue = queue;
        bootstrapServers = configuration.GetValueOrThrow<string>("KAFKA_BOOTSTRAP");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                ClientId = Dns.GetHostName()
            };

            var item = await _queue.DequeueAsync(CancellationToken.None);
            
            string message = JsonSerializer.Serialize(item);
            
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
        }
    }
}