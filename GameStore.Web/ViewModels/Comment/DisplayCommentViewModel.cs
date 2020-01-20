using System.Collections.Generic;
using GameStore.Domain.Models.SqlModels.CommentModels;

namespace GameStore.Web.ViewModels.Comment
{
    public class DisplayCommentViewModel
    {
        public string GameKey { get; set; }

        public List<DisplayCommentModel> Comments { get; set; }
    }
}