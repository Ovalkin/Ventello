using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Vintello.Web.Api.Filters;

public class TestFilter : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var user = context.HttpContext.User;
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var role = user.FindFirst(ClaimTypes.Role)!.Value;
        
        var id = context.HttpContext.Request.RouteValues["id"];
        
        if (id is null) throw new Exception("id должен быть определен в запросе!");
        
        if (role == "Client" && userId != id.ToString())
            context.Result = new ForbidResult();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}