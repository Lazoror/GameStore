using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.Models.SqlModels.GameModels;

namespace GameStore.Services.Filtering
{
    public class GamePlatformFilter : IFilter<Expression<Func<Game, bool>>>
    {
        private readonly List<string> _platforms;

        public GamePlatformFilter(List<string> platforms)
        {
            _platforms = platforms;
        }

        public Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> expression)
        {
            Expression<Func<Game, bool>> newExp = x =>
                x.GamePlatforms.Select(y => y.PlatformType.Name).Intersect(_platforms).Any();

            return expression.And(newExp);
        }
    }
}