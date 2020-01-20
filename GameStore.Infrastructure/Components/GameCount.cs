using System.Threading.Tasks;
using GameStore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Infrastructure.Components
{
    public class GameCount : ViewComponent
    {
        private readonly IGameService _gameService;

        public GameCount(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var gameCount = _gameService.CountAllGames(_ => true);

            return await Task.FromResult((IViewComponentResult)View("TotalGames", gameCount));
        }
    }
}