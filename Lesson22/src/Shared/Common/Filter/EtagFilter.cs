using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace Common.Filter;

public class ETagFilter : Attribute, IActionFilter
{
    private readonly int[] _statusCodes;

    public ETagFilter(params int[] statusCodes)
    {
        _statusCodes = statusCodes;
        if (statusCodes.Length == 0) _statusCodes = new[] { 200 };
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var request = context.HttpContext.Request;

        if (request.Method == "GET" &&
            context.Result is ObjectResult obj &&
            obj.Value is IGenerateETag entity)
        {
            string etag = entity.GenerateETag();

            // Value should be in quotes according to the spec
            if (!etag.EndsWith("\""))
                etag = "\"" + etag +"\"";

            string ifNoneMatch = request.Headers[HeaderNames.IfNoneMatch];

            if (ifNoneMatch == etag)
            {
                context.Result = new StatusCodeResult(304);
            }

            context.HttpContext.Response.Headers.Add(HeaderNames.ETag, etag);
        }
    }    
}