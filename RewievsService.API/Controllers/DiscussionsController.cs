using MediatR;
using Microsoft.AspNetCore.Mvc;
using RewievsService.Application.Commands.Discussions;
using RewievsService.Application.Queries.Discussions;

namespace RewievsService.API.Controllers
{
    public class DiscussionsController : BaseApiController
    {
        public DiscussionsController(IMediator mediator) : base(mediator) { }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetDiscussionByIdQuery(id), cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("entity/{relatedEntityId}")]
        public async Task<IActionResult> GetByRelatedEntity(string relatedEntityId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetDiscussionsByRelatedEntityIdQuery(relatedEntityId), cancellationToken);
            return Ok(result);
        }

        [HttpGet("author/{authorId}")]
        public async Task<IActionResult> GetByAuthor(string authorId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetDiscussionsByAuthorIdQuery(authorId), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDiscussionCommand command, CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateDiscussionCommand command, CancellationToken cancellationToken)
        {
            command.DiscussionId = id;
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            var requestedBy = "system";
            var command = new DeleteDiscussionCommand(id, requestedBy);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
