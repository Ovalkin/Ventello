using Microsoft.AspNetCore.Mvc;
using Vintello.Common.EntityModel.PostgreSql;
using Vintello.Web.Api.Repositories;

namespace Vintello.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleRepository _repo;

    public RolesController(IRoleRepository repo)
    {
        _repo = repo;
    }
}