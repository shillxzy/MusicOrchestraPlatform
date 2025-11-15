using MediatR;
using Microsoft.AspNetCore.Mvc;
using RewievsService.Application.Commands.Comments;
using RewievsService.Application.Queries.Comments;

namespace RewievsService.API.Controllers
{
    public class CommentsController : BaseApiController
    {
        public CommentsController(IMediator mediator) : base(mediator) { }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCommentByIdQuery(id), cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("review/{reviewId}")]
        public async Task<IActionResult> GetByReview(string reviewId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCommentsByReviewIdQuery(reviewId), cancellationToken);
            return Ok(result);
        }

        [HttpGet("author/{authorId}")]
        public async Task<IActionResult> GetByAuthor(string authorId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCommentsByAuthorIdQuery(authorId), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentCommand command, CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateCommentCommand command, CancellationToken cancellationToken)
        {
            command.CommentId = id;
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string commentId, CancellationToken cancellationToken)
        {
            var requestedBy = User?.Identity?.Name ?? "system";

            var command = new DeleteCommentCommand(commentId, requestedBy);
            await _mediator.Send(command, cancellationToken);

            return NoContent(); 
        }
    }
}
