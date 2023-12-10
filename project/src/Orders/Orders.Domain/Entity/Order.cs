using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Orders.Domain.Entity;

[Index(nameof(AccountId), nameof(OperationId), IsUnique = true)]
public class Order
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// Идентификатор аккаунта
    /// </summary>
    public long AccountId { get; set; }

    /// <summary>
    /// Дата и время совершения операции
    /// </summary>
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// Состояние заказа
    /// </summary>
    public OrderState State { get; set; }

    /// <summary>
    /// Сумма заказа 
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Описание заказа
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Уникльный идентификатор операции 
    /// </summary>
    [MaxLength(32)]
    public string OperationId { get; set; }
}