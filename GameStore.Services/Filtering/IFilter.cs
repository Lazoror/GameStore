using System;
using System.Linq.Expressions;
using GameStore.Domain.Models.SqlModels.GameModels;

namespace GameStore.Services.Filtering
{
    public interface IFilter<T>
    {
        T Execute(Expression<Func<Game, bool>> expression);
    }
}