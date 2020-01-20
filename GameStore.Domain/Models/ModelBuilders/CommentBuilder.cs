using System;
using GameStore.Domain.Models.SqlModels.CommentModels;

namespace GameStore.Services.Tests.ModelBuilders
{
    public class CommentBuilder
    {
        private Comment _comment;

        public CommentBuilder()
        {
            _comment = new Comment();
        }

        public CommentBuilder WithId(Guid id)
        {
            _comment.Id = id;

            return this;
        }

        public CommentBuilder WithName(string name)
        {
            _comment.Name = name;

            return this;
        }

        public CommentBuilder WithBody(string body)
        {
            _comment.Body = body;

            return this;
        }

        public CommentBuilder WithGameId(Guid gameId)
        {
            _comment.GameStateId = gameId;

            return this;
        }

        public Comment Build()
        {
            var result = _comment;
            _comment = new Comment();

            return result;
        }
    }
}