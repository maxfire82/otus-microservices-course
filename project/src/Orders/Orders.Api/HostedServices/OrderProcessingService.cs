using Orders.Domain;
using Orders.Domain.Entity;

namespace Orders.Api.HostedServices;

public class OrderProcessingService: BackgroundService
{
    private readonly IServiceScopeFactory _serviceProviderFactory;
    
    public OrderProcessingService(IServiceScopeFactory serviceProviderFactory)
    {
        _serviceProviderFactory = serviceProviderFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = _serviceProviderFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var orders = context.Orders.Where(x => x.State == OrderState.Created).ToList();
                orders.ForEach(x => x.State = OrderState.Processing);
                context.SaveChanges();

                foreach (var order in orders)
                {
                    order.State = OrderState.Completed;
                    // DO SOME WORK
                    await Task.Delay(1000);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // обработка ошибки однократного неуспешного выполнения фоновой задачи
            }
 
            await Task.Delay(5000);
        }
    }
}