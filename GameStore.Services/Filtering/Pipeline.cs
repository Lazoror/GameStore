using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.Domain.Models.SqlModels.GameModels;

namespace GameStore.Services.Filtering
{
    public abstract class Pipeline<T>
    {
        internal readonly List<IFilter<T>> Filters = new List<IFilter<T>>();

        public abstract T Process(Expression<Func<Game, bool>> expression);
    }
}