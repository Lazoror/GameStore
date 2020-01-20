using System.Collections.Generic;
using GameStore.Services.Filtering;
using Xunit;

namespace GameStore.Services.Tests.Filtering
{
    public class GameFiltersTest
    {
        [Fact]
        public void ExecuteGamePriceFilter_ShouldReturnNotNull_WhenFromPriceTo()
        {
            // Arrange
            var releaseDateFilter = new GamePriceFilter(0, 10);

            // Act
            var result = releaseDateFilter.Execute(_ => true);

            // Assert
            Assert.NotNull(result.Compile());
        }

        [Fact]
        public void ExecuteGameSearchFilter_ShouldReturnNotNull_WhenSearch()
        {
            // Arrange
            var releaseDateFilter = new GameSearchFilter("Search");

            // Act
            var result = releaseDateFilter.Execute(_ => true);

            // Assert
            Assert.NotNull(result.Compile());
        }

        [Fact]
        public void ExecuteGamePublisherFilter_ShouldReturnNotNull_WhenPublishers()
        {
            // Arrange
            var releaseDateFilter = new GamePublisherFilter(new List<string> { "publisher" });

            // Act
            var result = releaseDateFilter.Execute(_ => true);

            // Assert
            Assert.NotNull(result.Compile());
        }

        [Fact]
        public void ExecuteGamePlatformFilter_ShouldReturnNotNull_WhenPlatforms()
        {
            // Arrange
            var releaseDateFilter = new GamePlatformFilter(new List<string> { "platform" });

            // Act
            var result = releaseDateFilter.Execute(_ => true);

            // Assert
            Assert.NotNull(result.Compile());
        }

        [Fact]
        public void ExecuteGameGenreFilter_ShouldReturnNotNull_WhenGenres()
        {
            // Arrange
            var releaseDateFilter = new GameGenreFilter(new List<string> { "genre" });

            // Act
            var result = releaseDateFilter.Execute(_ => true);

            // Assert
            Assert.NotNull(result.Compile());
        }
    }
}