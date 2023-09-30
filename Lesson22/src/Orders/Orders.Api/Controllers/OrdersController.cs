using Common.Authentication.Services;
using Common.DbException;
using Common.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Api.Models.Response;
using Orders.Domain.Models;
using Orders.Domain.Services;

namespace Orders.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IIdentityService _identityService;

        /// <inheritdoc />
        public OrdersController(IOrderService orderService, IIdentityService identityService)
        {
            _orderService = orderService;
            _identityService = identityService;
        }
        
        /// <summary>
        ///     Создание заказа.
        /// </summary>
        /// <param name="order">Данные по заказу</param>
        /// <returns>Заказ создан</returns>
        /// <response code="200">Заказ создан</response>
        /// <response code="400">Некорректные данные.</response>
        [IfMatchFilter]
        [HttpPost]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDTO>> CreateAsync([FromBody] OrderDTO order)
        {
            if (!ModelState.IsValid || order == null)
            {
                return BadRequest("Некорректные данные");
            }
            
            var accountId = _identityService.GetAccountId();
            try
            {
                var createdOrder = await _orderService.CreateAsync(order, accountId);
                return Ok(createdOrder);
            }
            catch (ConcurrentDbException ex)
            {
                return Conflict(ex.Message);
            }
        }
        
        /// <summary>
        ///     Получение списка заказов.
        /// </summary>
        /// <returns>Список</returns>
        /// <response code="200">Список</response>
        [ETagFilter]
        [HttpGet]
        [ProducesResponseType(typeof(OrdersResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrdersResponse>> GetListAsync()
        {
            var accountId = _identityService.GetAccountId();
            var orderList = await _orderService.GetAsync(accountId);
            return Ok(new OrdersResponse
            {
                Data = orderList
            });
        }
        
    }
}
