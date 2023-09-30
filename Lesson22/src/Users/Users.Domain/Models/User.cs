using System.ComponentModel.DataAnnotations;

namespace Users.Domain.Models;

public class User
{
    public long Id { get; set; }
    
    /// <summary>
    /// Идентификатор аккаунта
    /// </summary>
    public long AccountId { get; set; }
    
    /// <summary>
    /// UserName
    /// </summary>
    [Required]
    [DataType(DataType.Text)]
    [MinLength(5)]
    public string UserName { get; set; }
        
    /// <summary>
    /// Имя
    /// </summary>
    [Required]
    [DataType(DataType.Text)]
    public string FirstName { get; set; }
        
    /// <summary>
    /// Фамилия
    /// </summary>
    [Required]
    [DataType(DataType.Text)]
    public string LastName { get; set; }
        
    /// <summary>
    /// Почта
    /// </summary>
    [Required]
    [EmailAddress]
    [MaxLength(256)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
        
    /// <summary>
    /// Телефон
    /// </summary>
    [DataType(DataType.PhoneNumber)]
    [MaxLength(256)]
    public string Phone { get; set; }
}  