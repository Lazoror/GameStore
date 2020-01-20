using GameStore.Web.ViewModels.Comment;

namespace GameStore.Web.ViewModels.Game
{
    public class DisplayGameDetailsByKeyRequestModel
    {
        public string GameKey { get; set; }

        public GameViewModel GameViewModel { get; set; }

        public DisplayCommentViewModel Comments { get; set; }
    }
}