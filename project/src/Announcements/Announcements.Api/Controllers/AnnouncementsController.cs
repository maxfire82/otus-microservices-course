using Announcements.Domain.Models;
using Announcements.Domain.Models.Response;
using Announcements.Domain.Stub;
using Common.Queue.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Announcements.Api.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize]
public class AnnouncementsController : ControllerBase
{
    private IBackgroundQueue<AnnouncementDTO> _queue;

    public AnnouncementsController(IBackgroundQueue<AnnouncementDTO> queue)
    {
        _queue = queue;
    }

    /// <summary>
    ///     Получение объявления.
    /// </summary>
    /// <param name="announcementId">Идентификатор объявления</param>
    /// <returns>Объявление</returns>
    /// <response code="200">Объявление найдено.</response>
    /// <response code="404">Объявление не найдено.</response>
    [HttpGet("{announcementId}")]
    [ProducesResponseType(typeof(AnnouncementDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<AnnouncementDTO> Get(int announcementId)
    {
        try
        {
            var announce = AnnouncementGenerator.Create(announcementId);
            return Ok(announce);
        }
        catch (KeyNotFoundException)
        {
            return new NotFoundResult();
        }
    }
    
    /// <summary>
    ///     Получение списка объявлений.
    /// </summary>
    /// <returns>Список</returns>
    /// <response code="200">Список</response>
    [HttpGet]
    [ProducesResponseType(typeof(AnnouncementsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<AnnouncementsResponse> GetList()
    {
        return Ok(new AnnouncementsResponse
        {
            Data = AnnouncementGenerator.CreateList()
        });
    }
    
    /// <summary>
    ///     Создание объявления.
    /// </summary>
    /// <param name="announcement">Данные объявления</param>
    /// <returns>Объявление создано</returns>
    /// <response code="200">Объявление создано</response>
    /// <response code="400">Некорректные данные.</response>
    [HttpPost]
    [ProducesResponseType(typeof(AnnouncementDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AnnouncementDTO>> CreateAsync([FromBody] AnnouncementDTO announcement)
    {
        if (!ModelState.IsValid || announcement == null)
        {
            return BadRequest("Некорректные данные");
        }
        
        try
        {
            await _queue.QueueAsync(announcement);
            return Ok(announcement);
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }
}