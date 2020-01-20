using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModels.Comment
{
    public class CommentViewModel
    {
        public Guid CommentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Body { get; set; }
        
        public string GameKey { get; set; }
    }
}