using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Commands.Discussions
{
    public class UpdateDiscussionCommand : ICommand
    {
        public string DiscussionId { get; set; }
        public string? NewTopic { get; }
        public string UpdatedBy { get; }

        public UpdateDiscussionCommand(string discussionId, string updatedBy, string? newTopic = null)
        {
            DiscussionId = discussionId;
            UpdatedBy = updatedBy;
            NewTopic = newTopic;
        }
    }
}
