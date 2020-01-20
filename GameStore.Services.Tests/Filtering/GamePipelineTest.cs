using System;
using System.Linq.Expressions;
using GameStore.Domain.Models.SqlModels.GameModels;
using GameStore.Services.Filtering;
using Xunit;

namespace GameStore.Services.Tests.Filtering
{
    public class GamePipelineTest
    {
        private Pipeline<Expression<Func<Game, bool>>> _gamePipeline;

        public GamePipelineTest()
        {
            _gamePipeline = new GamePipeline();
        }

        [Fact]
        public void Process_ShouldReturnExpressionWhenExpression()
        {
            var exp = _gamePipeline.Process(_ => true);

            Assert.NotNull(exp);
        }
    }
}