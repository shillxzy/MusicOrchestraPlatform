using RewievsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Queries.Discussions
{
    public class GetDiscussionsByRelatedEntityIdQuery : IQuery<IReadOnlyList<Discussion>>
    {
        public string RelatedEntityId { get; }

        public GetDiscussionsByRelatedEntityIdQuery(string relatedEntityId)
        {
            RelatedEntityId = relatedEntityId;
        }
    }
}
