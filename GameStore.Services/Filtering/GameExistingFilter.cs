using GameStore.Domain.Models.SqlModels.GameModels;
using System;
using System.Linq.Expressions;

namespace GameStore.Services.Filtering
{
    public class GameExistingFilter : IFilter<Expression<Func<Game, bool>>>
    {
        public Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> expression)
        {
            Expression<Func<Game, bool>> newExp = g => g.IsDeleted == false;

            return expression.And(newExp);
        }
    }
}