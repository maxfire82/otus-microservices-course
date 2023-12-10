using Microsoft.AspNetCore.Mvc;
using Search.Domain.Models.Response;
using Search.Domain.Services;

namespace Search.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    /// <summary>
    ///     Получение списка объявлений.
    /// </summary>
    /// <returns>Список</returns>
    /// <response code="200">Список</response>
    [HttpGet]
    [ProducesResponseType(typeof(SearchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SearchResponse>> GetList()
    {
        var search = await _searchService.SearchAsync();
        return Ok(new SearchResponse
        {
            Data = search
        });
    }
}