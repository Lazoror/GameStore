using System.Collections.Generic;
using GameStore.Domain.Models.SqlModels.CommentModels;
using Newtonsoft.Json;

namespace GameStore.Domain.Models.SqlModels.GameModels
{
    public class GameState : BaseEntity
    {
        public string GameKey { get; set; }

        public int ViewCount { get; set; }

        [JsonIgnore]
        public ICollection<Comment> Comments { get; set; }
    }
}