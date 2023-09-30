using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace Common.Filter;

public class IfMatchFilter : Attribute, IActionFilter
{

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context.HttpContext.Request;

        if (request.Method == "POST")
        {
            string ifNoneMatch = request.Headers[HeaderNames.IfMatch];

            if (string.IsNullOrEmpty(ifNoneMatch))
            {
                context.Result = new StatusCodeResult(400);
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }    
}