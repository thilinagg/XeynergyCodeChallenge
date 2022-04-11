using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using XeynergyCodeChallenge.WebUI.Common;

namespace XeynergyCodeChallenge.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private ISender _mediator = null!;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        public OkObjectResult Ok<T>(T result) =>
            base.Ok(Envelope.Ok(result));
    }
}
