using GPTOverflow.API.Modules.CrossCuttingConcerns.Controllers;
using GPTOverflow.API.Modules.CrossCuttingConcerns.Models;
using GPTOverflow.API.Modules.UserManagement.Models;
using GPTOverflow.Core.UserManagement.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GPTOverflow.API.Modules.UserManagement.Controllers;

public class UsersController : ApiController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<Envelope<GetUser.Response>>> GetProfile()
    {
        var profile = await _mediator.Send(new GetUser.Query(AuthorizedUser.Username));
        return Ok(Envelope.Ok(profile));
    }

    /// <summary>
    /// Called by Auth0 using POST USER REGISTRATION FLOW
    /// https://auth0.com/docs/customize/actions/flows-and-triggers/post-user-registration-flow
    /// </summary>
    /// <example>
    ///  const axios = require("axios");
    ///  /*  @param {Event} event - Details about registration event */
    ///  
    ///  exports.onExecutePostUserRegistration = async (event) => {
    ///     await axios.post("https://my-api-url.com/api/users", { params: { email: event.user.email }});
    ///  };
    /// </example>
    /// <returns></returns>
    
    // TODO: Implement authorization here
    [HttpPost]
    public async Task<ActionResult<Envelope<CreateUser.Response>>> CreateUser(CreateUserRequest request)
    {
        var createUser = await _mediator.Send(new CreateUser.Command(request.Email));
        if (createUser.IsSuccess)
        {
            return Ok(Envelope.Ok(createUser.Value));
        }

        return BadRequest(Envelope.Error(createUser.Error));
    }
}