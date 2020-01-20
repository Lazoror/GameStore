using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModels.Comment
{
    public class CreateCommentViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Body { get; set; }

        public string Quote { get; set; }

        public string GameKey { get; set; }

        public Guid ParentCommentId { get; set; }
    }
}