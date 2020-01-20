using System.Collections.Generic;
using GameStore.Domain.Models.SqlModels.FilterModels;
using GameStore.Services.Filtering;
using Xunit;

namespace GameStore.Services.Tests.Filtering
{
    public class GamePipelineBuilderTest
    {
        private readonly GamePipelineBuilder _gamePipelineBuilder;

        public GamePipelineBuilderTest()
        {
            _gamePipelineBuilder = new GamePipelineBuilder(new FilterDataModel()
            {
                Genres = new List<string> { "Genre" },
                Platforms = new List<string> { "Platform" },
                Publishers = new List<string> { "Publisher" },
                SortType = SortType.MostCommented,
                SearchString = "Search",
                PriceFrom = 0,
                PriceTo = 100,
                ReleaseDate = ReleaseDate.LastMonth,
            });
        }

        [Fact]
        public void WithSearchFilter_ShouldReturnTypeWhenValues()
        {
            // Act
            var result = _gamePipelineBuilder.WithSearchFilter();

            // Assert
            Assert.IsType<GamePipelineBuilder>(result);
        }

        [Fact]
        public void WithGamePriceFilter_ShouldReturnTypeWhenValues()
        {
            // Act
            var result = _gamePipelineBuilder.WithGamePriceFilter();

            // Assert
            Assert.IsType<GamePipelineBuilder>(result);
        }

        [Fact]
        public void WithGamePlatformsFilter_ShouldReturnTypeWhenValues()
        {
            // Act
            var result = _gamePipelineBuilder.WithGamePlatformsFilter();

            // Assert
            Assert.IsType<GamePipelineBuilder>(result);
        }

        [Fact]
        public void WithGameReleaseDateFilter_ShouldReturnTypeWhenValues()
        {
            // Act
            var result = _gamePipelineBuilder.WithGameReleaseDateFilter();

            // Assert
            Assert.IsType<GamePipelineBuilder>(result);
        }

        [Fact]
        public void WithGamePublisherFilter_ShouldReturnTypeWhenValues()
        {
            // Act
            var result = _gamePipelineBuilder.WithGamePublisherFilter();

            // Assert
            Assert.IsType<GamePipelineBuilder>(result);
        }

        [Fact]
        public void WithGameGenreFilter_ShouldReturnTypeWhenValues()
        {
            // Act
            var result = _gamePipelineBuilder.WithGameGenreFilter();

            // Assert
            Assert.IsType<GamePipelineBuilder>(result);
        }
    }
}