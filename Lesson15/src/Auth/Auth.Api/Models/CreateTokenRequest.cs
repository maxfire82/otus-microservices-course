using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Auth.Api.Models;

public class CreateTokenRequest
{
    /// <summary>
    ///  Пароль
    /// </summary>
    [Required]
    public string Password { get; set; }

    /// <summary>
    /// Логин пользователя
    /// </summary>
    [Required]
    public string Login { get; set; }
    
    /// <summary>
    /// Область действия токена
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Scope? Scope { get; set; }
}

public enum Scope
{
    [EnumMember(Value = "GENERAL")]
    General = 0,
    [EnumMember(Value = "USERS")]
    Users = 1
}