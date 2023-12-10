using System.ComponentModel.DataAnnotations;

namespace Auth.Api.Models;

public class AccountCreateResponse
{
    /// <summary>
    ///  Идентификатор пользователя
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; }
}