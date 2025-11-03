using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Domain.Entities.Parameters
{
    internal class DiscussionParameters : QueryStringParameters
    {
        public string? TopicId { get; set; }
        public string? AuthorId { get; set; }
        public bool? IncludeReplies { get; set; } = false;
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
