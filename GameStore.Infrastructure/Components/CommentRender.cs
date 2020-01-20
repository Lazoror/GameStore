using System.Threading.Tasks;
using GameStore.Domain.Models.SqlModels.CommentModels;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Infrastructure.Components
{
    public class CommentRender : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(DisplayCommentModel comment)
        {
            return await Task.FromResult((IViewComponentResult)View("CommentSection", comment));
        }
    }
}