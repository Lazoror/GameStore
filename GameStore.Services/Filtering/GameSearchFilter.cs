using GameStore.Domain.Models.SqlModels.GameModels;
using System;
using System.Linq.Expressions;

namespace GameStore.Services.Filtering
{
    public class GameSearchFilter : IFilter<Expression<Func<Game, bool>>>
    {
        private readonly string _searchString;

        public GameSearchFilter(string searchString)
        {
            _searchString = searchString;
        }

        public Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> expression)
        {
            Expression<Func<Game, bool>> searchExpression = x => x.Name.Contains(_searchString);

            return expression.And(searchExpression);
        }
    }
}