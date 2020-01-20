using GameStore.Domain.Models.SqlModels.FilterModels;
using GameStore.Services.Filtering;
using Xunit;

namespace GameStore.Services.Tests.Filtering
{
    public class GameSortingBuilderTest
    {
        private readonly GameSortingBuilder _gameSortingBuilder;

        public GameSortingBuilderTest()
        {
            _gameSortingBuilder = new GameSortingBuilder();
        }

        [Theory]
        [InlineData(SortType.None)]
        [InlineData(SortType.MostCommented)]
        [InlineData(SortType.MostPopular)]
        [InlineData(SortType.New)]
        [InlineData(SortType.PriceAsc)]
        [InlineData(SortType.PriceDesc)]
        public void ResolveSorting_ShouldReturnGamePipeline_WhenSortType(SortType sortType)
        {
            // Act
            var result = _gameSortingBuilder.ResolveSorting(sortType);

            // Assert
            Assert.IsType<GameSortingBuilder>(result);
        }
    }
}