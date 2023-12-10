using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Orders.Domain.Entity;

namespace Orders.Domain.Models;

public class OrderDTO
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Дата и время совершения операции
    /// </summary>
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// Состояние заказа
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderState State { get; set; }

    /// <summary>
    /// Сумма заказа 
    /// </summary>
    [Required]
    public decimal Amount { get; set; }

    /// <summary>
    /// Описание заказа
    /// </summary>
    [Required]
    [MinLength(10)]
    public string Description { get; set; }
}