namespace Search.Domain.Models;

public class Announcement
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
    public string Category { get; set; }
    
    /// <summary>
    /// Изображения
    /// </summary>
    public List<string> Images { get; set; }
}