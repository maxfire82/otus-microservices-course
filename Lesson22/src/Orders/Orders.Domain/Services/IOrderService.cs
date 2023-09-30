using Orders.Domain.Entity;
using Orders.Domain.Models;

namespace Orders.Domain.Services;

public interface IOrderService
{
    /// <summary>
    /// Создание заказа
    /// </summary>
    /// <param name="item"></param>
    /// <param name="accountId"></param>
    /// <returns></returns>
    Task<OrderDTO> CreateAsync(OrderDTO item, long accountId);
    
    /// <summary>
    /// Получение списка заказов
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    Task<List<OrderDTO>> GetAsync(long accountId);

    /// <summary>
    /// Обновление заказа
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    Task UpdateAsync(OrderDTO item);

    /// <summary>
    /// Список заказов
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    IEnumerable<Order> GetOrdersAsync(Func<Order, bool> predicate);
}