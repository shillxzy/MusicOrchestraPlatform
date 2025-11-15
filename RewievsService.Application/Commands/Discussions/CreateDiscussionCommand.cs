using RewievsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Application.Commands.Discussions
{
    public class CreateDiscussionCommand : ICommand<Discussion>
    {
        public Discussion Discussion { get; }

        public CreateDiscussionCommand(Discussion discussion)
        {
            Discussion = discussion;
        }
    }
}
