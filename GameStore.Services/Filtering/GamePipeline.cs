using GameStore.Domain.Models.SqlModels.GameModels;
using System;
using System.Linq.Expressions;

namespace GameStore.Services.Filtering
{
    public class GamePipeline : Pipeline<Expression<Func<Game, bool>>>
    {
        public override Expression<Func<Game, bool>> Process(Expression<Func<Game, bool>> expression)
        {
            foreach (var filter in Filters)
            {
                expression = filter.Execute(expression);
            }

            return expression;
        }
    }
}