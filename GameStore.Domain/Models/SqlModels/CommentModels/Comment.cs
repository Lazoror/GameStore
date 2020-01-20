using System;
using GameStore.Domain.Models.SqlModels.GameModels;
using Newtonsoft.Json;

namespace GameStore.Domain.Models.SqlModels.CommentModels
{
    public class Comment : BaseEntity
    {
        public string Name { get; set; }

        public string Body { get; set; }

        public bool IsDeleted { get; set; }

        public string Quote { get; set; }

        public Guid? ParentCommentId { get; set; }

        [JsonIgnore]
        public Comment ParentComment { get; set; }

        public Guid GameStateId { get; set; }

        [JsonIgnore]
        public Game Game { get; set; }
    }
}