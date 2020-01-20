using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.Domain.Models.SqlModels.GameModels;

namespace GameStore.Services.Filtering
{
    public class GamePublisherFilter : IFilter<Expression<Func<Game, bool>>>
    {
        private readonly List<string> _publishers;

        public GamePublisherFilter(List<string> publishers)
        {
            _publishers = publishers;
        }

        public Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> expression)
        {
            Expression<Func<Game, bool>> newExp = x => _publishers.Contains(x.Publisher.CompanyName);

            return expression.And(newExp);
        }
    }
}