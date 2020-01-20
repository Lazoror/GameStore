using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModels.Platform
{
    public class PlatformTypeViewModel
    {
        [Required]
        public string Name { get; set; }

        public string NameRu { get; set; }

        public string OldName { get; set; }
    }
}