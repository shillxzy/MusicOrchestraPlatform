using RewievsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Queries.Discussions
{
    public class GetDiscussionByIdQuery : IQuery<Discussion?>
    {
        public string DiscussionId { get; }

        public GetDiscussionByIdQuery(string discussionId)
        {
            DiscussionId = discussionId;
        }
    }
}
