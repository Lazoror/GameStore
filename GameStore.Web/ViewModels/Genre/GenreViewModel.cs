using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModels.Genre
{
    public class GenreViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string NameRu { get; set; }

        public string ParentGenre { get; set; }

        public IEnumerable<string> AllGenres { get; set; }
    }
}