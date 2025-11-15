using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace RewievsService.API.Controllers
{
    [ApiController]
    [Route("api/Reviews/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly IMediator _mediator;

        protected BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
