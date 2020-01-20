using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModels.Publisher
{
    public class PublisherViewModel
    {
        [Required]
        public string CompanyName { get; set; }

        public string CompanyNameRu { get; set; }

        [Required]
        public string Description { get; set; }

        public string DescriptionRu { get; set; }

        public string HomePage { get; set; }

        public string HomePageRu { get; set; }

        public string OldCompanyName { get; set; }
    }
}