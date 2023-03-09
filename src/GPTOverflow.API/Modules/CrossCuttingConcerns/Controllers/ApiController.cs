using Microsoft.AspNetCore.Mvc;

namespace GPTOverflow.API.Modules.CrossCuttingConcerns.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    // protected AuthorizedUser AuthorizedUser => new(User.GetUserId(), User.GetUserEmail());
    // todo: implement get authorized user id
    protected Guid GetUserId()
    {
        return Guid.NewGuid();
    }
}