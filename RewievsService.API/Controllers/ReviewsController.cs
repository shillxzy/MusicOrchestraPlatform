using MediatR;
using Microsoft.AspNetCore.Mvc;
using RewievsService.API.Controllers;
using RewievsService.Application.Commands.Reviews;
using RewievsService.Application.Queries.Reviews;

namespace ReviewsService.API.Controllers
{
    public class ReviewsController : BaseApiController
    {
        public ReviewsController(IMediator mediator) : base(mediator) { }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetReviewByIdQuery(id), cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("target/{targetId}")]
        public async Task<IActionResult> GetByTarget(string targetId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetReviewsByTargetIdQuery(targetId), cancellationToken);
            return Ok(result);
        }

        [HttpGet("author/{authorId}")]
        public async Task<IActionResult> GetByAuthor(string authorId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetReviewsByAuthorIdQuery(authorId), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewCommand command, CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateReviewCommand command, CancellationToken cancellationToken)
        {
            command.ReviewId = id;
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            var command = new DeleteReviewCommand(id, "system");
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
