using GameStore.Domain.Models.SqlModels.FilterModels;
using GameStore.Domain.Models.SqlModels.SortModels;

namespace GameStore.Services.Filtering
{
    public class GameSortingBuilder
    {
        private SortingModel _sortingModel;

        public GameSortingBuilder()
        {
            _sortingModel = new SortingModel();
        }

        public GameSortingBuilder ResolveSorting(SortType sortType)
        {
            switch (sortType)
            {
                case SortType.PriceDesc:
                    _sortingModel = new SortingModel
                    { SortDirection = SortDirection.Descending, OrderExpression = x => x.Price };
                    break;
                case SortType.PriceAsc:
                    _sortingModel = new SortingModel
                    { SortDirection = SortDirection.Ascending, OrderExpression = x => x.Price };
                    break;
                case SortType.MostCommented:
                    _sortingModel = new SortingModel
                    { SortDirection = SortDirection.Descending, OrderExpression = x => x.GameState.Comments.Count };
                    break;
                case SortType.MostPopular:
                    _sortingModel = new SortingModel
                    { SortDirection = SortDirection.Descending, OrderExpression = x => x.GameState.ViewCount };
                    break;
                case SortType.New:
                    _sortingModel = new SortingModel
                    { SortDirection = SortDirection.Descending, OrderExpression = x => x.AddDate };
                    break;
            }

            return this;
        }

        public SortingModel Build()
        {
            var result = _sortingModel;
            _sortingModel = new SortingModel();

            return result;
        }
    }
}