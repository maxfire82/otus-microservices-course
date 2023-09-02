using System.ComponentModel.DataAnnotations;

namespace Auth.Domain.Entities;

public class Account
{
    public long Id { get; set; }
    
    /// <summary>
    /// Логин
    /// </summary>
    [Required]
    public string Login { get; set; }
        
    /// <summary>
    /// Хеш пароля
    /// </summary>
    [Required]
    public string Hash { get; set; }
    
    /// <summary>
    /// Время создания
    /// </summary>
    public DateTimeOffset Created { get; set; }
}  