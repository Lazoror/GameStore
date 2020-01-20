using GameStore.Domain.Models.SqlModels.FilterModels;
using GameStore.Services.Filtering;
using Xunit;

namespace GameStore.Services.Tests.Filtering
{
    public class GameReleaseDateFilterTest
    {
        [Theory]
        [InlineData(ReleaseDate.None)]
        [InlineData(ReleaseDate.LastWeek)]
        [InlineData(ReleaseDate.LastMonth)]
        [InlineData(ReleaseDate.LastYear)]
        [InlineData(ReleaseDate.TwoYears)]
        [InlineData(ReleaseDate.ThreeYears)]
        public void Execute_ShouldReturnNotNull_WhenReleaseDate(ReleaseDate releaseDate)
        {
            // Arrange
            var releaseDateFilter = new GameReleaseDateFilter(releaseDate);

            // Act
            var result = releaseDateFilter.Execute(_ => true);

            // Assert
            Assert.NotNull(result.Compile());
        }
    }
}