using System;
using System.Linq.Expressions;
using GameStore.Domain.Models.SqlModels.GameModels;

namespace GameStore.Domain.Models.SqlModels.SortModels
{
    public class SortingModel
    {
        public SortDirection SortDirection { get; set; }

        public Expression<Func<Game, object>> OrderExpression { get; set; }
    }
}