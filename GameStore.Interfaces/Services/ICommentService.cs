using System;
using System.Collections.Generic;
using GameStore.Domain.Models.SqlModels.AccountModels;
using GameStore.Domain.Models.SqlModels.CommentModels;

namespace GameStore.Interfaces.Services
{
    public interface ICommentService
    {
        void AddComment(Comment entity, string gameKey);

        void AnswerComment(Comment entity, string gameKey);

        void DeleteComment(Guid commentId);

        void Ban(Guid commentId, BanDuration duration);

        Comment GetCommentById(Guid commentId);

        List<DisplayCommentModel> GetAllCommentsByGameKey(string gameKey);
    }
}