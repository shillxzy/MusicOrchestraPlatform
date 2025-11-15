using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Commands.Discussions
{
    public class DeleteDiscussionCommand : ICommand
    {
        public string DiscussionId { get; }
        public string RequestedBy { get; }

        public DeleteDiscussionCommand(string discussionId, string requestedBy)
        {
            DiscussionId = discussionId;
            RequestedBy = requestedBy;
        }
    }
}
