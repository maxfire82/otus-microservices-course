using System.ComponentModel.DataAnnotations;

namespace Auth.Api.Models;

public class AccountCreateRequest
{
    /// <summary>
    ///  Пароль
    /// </summary>
    [Required]
    [MinLength(7)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    /// <summary>
    /// Подтверждение пароля
    /// </summary>
    [Required]
    [MinLength(7)]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string PasswordConfirmation { get; set; }
    
    /// <summary>
    /// Логин пользователя
    /// </summary>
    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Login { get; set; }
}