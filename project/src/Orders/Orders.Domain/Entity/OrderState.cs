using System.Runtime.Serialization;

namespace Orders.Domain.Entity;

/// <summary>
/// Состояние заказа
/// </summary>
public enum OrderState
{
    [EnumMember(Value = "Created")]
    Created = 0,
    
    [EnumMember(Value = "Processing")]
    Processing = 1,
        
    [EnumMember(Value = "Cancelled")]
    Cancelled = 2,
    
    [EnumMember(Value = "Completed")]
    Completed = 3,
}