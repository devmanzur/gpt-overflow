using System.Security.Claims;
using GPTOverflow.API.Modules.CrossCuttingConcerns.Models;
using GPTOverflow.Core.CrossCuttingConcerns.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GPTOverflow.API.Modules.CrossCuttingConcerns.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    protected AuthorizedUser AuthorizedUser => new(User.GetValue(ClaimTypes.Email),User.GetValue(ClaimTypes.NameIdentifier));
}