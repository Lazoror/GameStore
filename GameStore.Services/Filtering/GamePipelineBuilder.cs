using System;
using System.Linq.Expressions;
using GameStore.Domain.Models.SqlModels.FilterModels;
using GameStore.Domain.Models.SqlModels.GameModels;

namespace GameStore.Services.Filtering
{
    public class GamePipelineBuilder
    {
        private readonly FilterDataModel _filtersGameData;
        private Pipeline<Expression<Func<Game, bool>>> _gamePipeline;

        public GamePipelineBuilder(FilterDataModel gameFilters)
        {
            _filtersGameData = gameFilters;
            _gamePipeline = new GamePipeline();
        }

        public GamePipelineBuilder WithSearchFilter()
        {
            if (!String.IsNullOrWhiteSpace(_filtersGameData.SearchString) && _filtersGameData.SearchString.Length >= 3)
            {
                _gamePipeline.Filters.Add(new GameSearchFilter(_filtersGameData.SearchString));
            }

            return this;
        }

        public GamePipelineBuilder WithGamePriceFilter()
        {
            if (_filtersGameData.PriceFrom >= 0
                && _filtersGameData.PriceFrom <= _filtersGameData.PriceTo
                && _filtersGameData.PriceFrom + _filtersGameData.PriceTo != 0)
            {
                _gamePipeline.Filters.Add(new GamePriceFilter(_filtersGameData.PriceFrom, _filtersGameData.PriceTo));
            }

            return this;
        }

        public GamePipelineBuilder WithGameReleaseDateFilter()
        {
            if (_filtersGameData.ReleaseDate != ReleaseDate.None)
            {
                _gamePipeline.Filters.Add(new GameReleaseDateFilter(_filtersGameData.ReleaseDate));
            }

            return this;
        }

        public GamePipelineBuilder WithGamePublisherFilter()
        {
            if (_filtersGameData.Publishers != null)
            {
                _gamePipeline.Filters.Add(new GamePublisherFilter(_filtersGameData.Publishers));
            }

            return this;
        }

        public GamePipelineBuilder WithGameGenreFilter()
        {
            if (_filtersGameData.Genres != null)
            {
                _gamePipeline.Filters.Add(new GameGenreFilter(_filtersGameData.Genres));
            }

            return this;
        }

        public GamePipelineBuilder WithGamePlatformsFilter()
        {
            if (_filtersGameData.Platforms != null)
            {
                _gamePipeline.Filters.Add(new GamePlatformFilter(_filtersGameData.Platforms));
            }

            return this;
        }

        public Expression<Func<Game, bool>> Build()
        {
            var result = _gamePipeline.Process(x => x.UnitsInStock > 0);
            _gamePipeline = new GamePipeline();

            return result;
        }
    }
}