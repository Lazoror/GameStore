using System;
using System.Collections.Generic;
using GameStore.Domain.Models.SqlModels;
using GameStore.Domain.Models.SqlModels.GameModels;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.Domain.Models.MongoModels
{
    public class GameModel : MongoModel
    {
        public Guid GameId { get; set; }

        public string Name { get; set; }

        public string Key { get; set; }

        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public int SupplierId { get; set; }

        [BsonElement("UnitPrice")]
        public decimal Price { get; set; }

        [BsonElement("UnitsInStock")]
        public short UnitsInStock { get; set; }

        [BsonElement("QuantityPerUnit")]
        public string Description { get; set; }

        [BsonElement("Discontinued")]
        public bool Discontinued { get; set; }

        public Publisher Publisher { get; set; }

        public GameState GameState { get; set; }

        public ICollection<GameGenre> GameGenres { get; set; }

        public ICollection<GamePlatform> GamePlatforms { get; set; }

    }
}