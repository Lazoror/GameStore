using System;
using System.Collections.Generic;

namespace GameStore.Domain.Models.SqlModels.CommentModels
{
    public class DisplayCommentModel
    {
        public Guid CommentId { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public string GameKey { get; set; }

        public string Quote { get; set; }

        public List<DisplayCommentModel> ChildrenComments { get; set; }
    }
}