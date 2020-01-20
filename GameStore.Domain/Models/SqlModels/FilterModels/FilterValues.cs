using System.Collections.Generic;

namespace GameStore.Domain.Models.SqlModels.FilterModels
{
    public class FilterValues
    {
        public List<string> Genres { get; set; }

        public List<string> Publishers { get; set; }

        public List<string> Platforms { get; set; }
    }
}