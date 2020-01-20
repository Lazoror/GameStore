using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Models.SqlModels.FilterModels
{
    public enum ReleaseDate
    {
        [Display(Name = "None")]
        None,

        [Display(Name = "Last Week")]
        LastWeek,

        [Display(Name = "Last Month")]
        LastMonth,

        [Display(Name = "Last Year")]
        LastYear,

        [Display(Name = "Two Years")]
        TwoYears,

        [Display(Name = "Three Years")]
        ThreeYears
    }
}