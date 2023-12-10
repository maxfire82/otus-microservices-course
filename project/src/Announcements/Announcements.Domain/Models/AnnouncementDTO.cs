using System.Text.Json.Serialization;
using Announcements.Domain.Enums;

namespace Announcements.Domain.Models;

public class AnnouncementDTO
{
    /// <summary>
    /// Идентификатор объявления
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Заголовок
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Цена
    /// </summary>
    public string Price { get; set; }
    
    /// <summary>
    /// Категория
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Category Category { get; set; }
    
    /// <summary>
    /// Изображения
    /// </summary>
    public List<string> Images { get; set; }
}