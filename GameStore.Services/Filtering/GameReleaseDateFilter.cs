using System;
using System.Linq.Expressions;
using GameStore.Domain.Models.SqlModels.FilterModels;
using GameStore.Domain.Models.SqlModels.GameModels;

namespace GameStore.Services.Filtering
{
    public class GameReleaseDateFilter : IFilter<Expression<Func<Game, bool>>>
    {
        private readonly ReleaseDate _releaseDate;

        public GameReleaseDateFilter(ReleaseDate releaseDate)
        {
            _releaseDate = releaseDate;
        }

        public Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> expression)
        {
            Expression<Func<Game, bool>> newExp = _ => true;

            switch (_releaseDate)
            {
                case ReleaseDate.LastWeek:
                    var sevenDaysAgo = DateTime.UtcNow.AddDays(-7);
                    newExp = x => x.PublishDate >= sevenDaysAgo;
                    break;

                case ReleaseDate.LastMonth:
                    var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);
                    newExp = x => x.PublishDate >= oneMonthAgo;
                    break;

                case ReleaseDate.LastYear:
                    var oneYearAgo = DateTime.UtcNow.AddYears(-1);
                    newExp = x => x.PublishDate >= oneYearAgo;
                    break;

                case ReleaseDate.TwoYears:
                    var twoYearsAgo = DateTime.UtcNow.AddYears(-2);
                    newExp = x => x.PublishDate >= twoYearsAgo;
                    break;

                case ReleaseDate.ThreeYears:
                    var threeYearsAgo = DateTime.UtcNow.AddYears(-3);
                    newExp = x => x.PublishDate >= threeYearsAgo;
                    break;
            }

            return expression.And(newExp);
        }
    }
}