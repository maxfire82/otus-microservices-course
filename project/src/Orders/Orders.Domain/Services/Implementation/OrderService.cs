using Common.DbException;
using Common.Extensions;
using Common.Idempotency.Services;
using EntityFramework.Exceptions.Common;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;
using Orders.Domain.Entity;
using Orders.Domain.Models;

namespace Orders.Domain.Services.Implementation;

public class OrderService: IOrderService
{
    private readonly OrderDbContext _dbContext;
    private readonly ISystemClock _systemClock;
    private readonly IIdempotencyService _idempotencyService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="systemClock"></param>
    public OrderService(OrderDbContext dbContext, ISystemClock systemClock, IIdempotencyService idempotencyService)
    {
        _dbContext = dbContext;
        _systemClock = systemClock;
        _idempotencyService = idempotencyService;
    }
    
    /// <inheritdoc />
    public async Task<OrderDTO> CreateAsync(OrderDTO item, long accountId)
    {
        var orderList = await GetAsync(accountId);
        var operationId = orderList.GetMd5Hash();

        if (!_idempotencyService.ValidateOperationId(operationId))
        {
            throw new ConcurrentDbException("Параллельный доступ к данным");
        }
        
        var order = item.Adapt<Order>();
        order.AccountId = accountId;
        order.State = OrderState.Created;
        order.Created = _systemClock.UtcNow;
        order.OperationId = operationId;

        try
        {
            var orderDb = (await _dbContext.Orders.AddAsync(order)).Entity;
            await _dbContext.SaveChangesAsync();
            return orderDb.Adapt<OrderDTO>();
        }
        catch (UniqueConstraintException e)
        {
            throw new ConcurrentDbException("Параллельный доступ к данным");
        }
    }
    
    /// <inheritdoc />
    public async Task<List<OrderDTO>> GetAsync(long accountId)
    {
        var orderList = await _dbContext.Orders.Where(g => g.AccountId == accountId).ToListAsync();
        return orderList.Adapt<List<OrderDTO>>();
    }
    
    /// <inheritdoc />
    public IEnumerable<Order> GetOrdersAsync(Func<Order, bool> predicate)
    {
        return _dbContext.Orders.Where(predicate).ToList();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(OrderDTO item)
    {
        var order = await _dbContext.Orders
            .FirstOrDefaultAsync(g => g.Id == item.Id);
        
        if (order == null)
        {
            throw new KeyNotFoundException();
        }

        order.Amount = item.Amount;
        order.Description = item.Description;
        order.State = item.State;

        _dbContext.Update(order);

        await _dbContext.SaveChangesAsync();
    }
}