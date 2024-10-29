using System.Globalization;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Repositories;

namespace Vintello.Web.Api.Filters;

public class ItemOwnershipFilter : Attribute, IAsyncActionFilter
{
    private readonly IItemRepository _repo;

    public ItemOwnershipFilter(IItemRepository repo)
    {
        _repo = repo;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var role = user.FindFirst(ClaimTypes.Role)!.Value;

        var itemId = context.HttpContext.Request.RouteValues["id"];
        if (itemId != null)
        {
            Item? item = await _repo.RetrieveByIdAsync(int.Parse((string)itemId));
            if (item is not null)
            {
                if (role == "Client" && userId != item.UserId.ToString())
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
            else
            {
                context.Result = new NotFoundResult();
                return;
            }
        }

        await next();
    }
}