using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.Models.SqlModels.GameModels;

namespace GameStore.Services.Filtering
{
    public class GameGenreFilter : IFilter<Expression<Func<Game, bool>>>
    {
        private readonly List<string> _genres;

        public GameGenreFilter(List<string> genres)
        {
            _genres = genres;
        }

        public Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> expression)
        {
            Expression<Func<Game, bool>> newExp = x =>
                x.GameGenres.Select(y => y.Genre.Name).Intersect(_genres).Any();

            return expression.And(newExp);
        }
    }
}