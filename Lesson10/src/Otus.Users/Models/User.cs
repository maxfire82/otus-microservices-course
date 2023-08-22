using System.ComponentModel.DataAnnotations;

namespace Otus.Users.Models;

public class User
{
    public long Id { get; set; }
    
    /// <summary>
    /// UserName
    /// </summary>
    [Required]
    public string UserName { get; set; }
        
    /// <summary>
    /// Имя
    /// </summary>
    [Required]
    public string FirstName { get; set; }
        
    /// <summary>
    /// Фамилия
    /// </summary>
    [Required]
    public string LastName { get; set; }
        
    /// <summary>
    /// Почта
    /// </summary>
    [Required]
    [MaxLength(256)]
    public string Email { get; set; }
        
    /// <summary>
    /// Телефон
    /// </summary>
    public string Phone { get; set; }
}  