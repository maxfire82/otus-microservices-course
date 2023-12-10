using System.Text.Json;
using Common.Configuration;
using Confluent.Kafka;
using Search.Domain.Models;
using Search.Domain.Services;

namespace Search.Api.HostedServices;

public class AnnouncementConsumerService : BackgroundService
{
    private readonly string groupId = "announcements_search_group";
    private readonly string bootstrapServers;
    private readonly string topic = "announcements-topic";

    private IAnnouncementStore _announcementStore;

    public AnnouncementConsumerService(IAnnouncementStore announcementStore, IConfiguration configuration)
    {
        _announcementStore = announcementStore;
        bootstrapServers = configuration.GetValueOrThrow<string>("KAFKA_BOOTSTRAP");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
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
                        try
                        {
                            var announcement = JsonSerializer.Deserialize<Announcement>(consumer.Message.Value);
                            _announcementStore.Store.Add(announcement);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
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