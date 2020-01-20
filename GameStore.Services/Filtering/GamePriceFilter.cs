using System;
using System.Linq.Expressions;
using GameStore.Domain.Models.SqlModels.GameModels;

namespace GameStore.Services.Filtering
{
    public class GamePriceFilter : IFilter<Expression<Func<Game, bool>>>
    {
        private readonly decimal _priceFrom;
        private readonly decimal _priceTo;

        public GamePriceFilter(decimal priceFrom, decimal priceTo)
        {
            _priceFrom = priceFrom;
            _priceTo = priceTo;
        }

        public Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> expression)
        {
            Expression<Func<Game, bool>> newExp = x => x.Price >= _priceFrom && x.Price <= _priceTo;

            return expression.And(newExp);
        }
    }
}