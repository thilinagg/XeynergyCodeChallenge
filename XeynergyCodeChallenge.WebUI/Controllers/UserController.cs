using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using XeynergyCodeChallenge.Application.Commands.User.CreateUser;
using XeynergyCodeChallenge.Application.Commands.User.DeleteUser;
using XeynergyCodeChallenge.Application.Commands.User.UpdateUser;
using XeynergyCodeChallenge.Application.Queries;
using XeynergyCodeChallenge.Application.Queries.GetUserById;

namespace XeynergyCodeChallenge.WebUI.Controllers
{
    public class UserController : ApiControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteUserCommand(id)));
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            return Ok(await Mediator.Send(new GetUsersListQuery()));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> UserById(Guid id)
        {
            return Ok(await Mediator.Send(new GetUserByIdQuery(id)));
        }
    }
}
