using System.Collections.Generic;
using GameStore.Domain.Models.SqlModels.GameModels;

namespace GameStore.Domain.Models.SqlModels.FilterModels
{
    public class GamesFilters
    {
        public IEnumerable<Game> Games { get; set; }

        public FilterDataModel Filters { get; set; }

        public FilterValues DefaultValues { get; set; }
    }
}