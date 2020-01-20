using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModels.Game
{
    public class GameViewModel
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string Name { get; set; }

        public string NameRu { get; set; }

        [Required]
        public string Description { get; set; }

        public string DescriptionRu { get; set; }

        public string PublishDate { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public short UnitsInStock { get; set; }

        public string Publisher { get; set; }

        public bool Discontinued { get; set; }

        public decimal Rating { get; set; }

        public bool IsDeleted { get; set; }

        public List<string> GamePlatforms { get; set; }

        public List<string> GameGenres { get; set; }

        public List<string> Publishers { get; set; }
    }
}