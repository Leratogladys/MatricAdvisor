using System.ComponentModel.DataAnnotations;
using MatricConnect.Services;

namespace MatricConnect.Web.Models
{
    public sealed class ProgrammeComparisonInputViewModel
    {
        [Required(ErrorMessage = "Choose a subject to compare.")]
        [Display(Name = "Subject to compare")]
        public ComparisonSubject? Subject { get; set; }

        [Required(ErrorMessage = "Enter your subject mark.")]
        [Range(typeof(decimal), "0", "100", ErrorMessage = "Enter a mark between 0 and 100.")]
        [Display(Name = "Subject mark (%)")]
        public decimal? Mark { get; set; }

        [Required(ErrorMessage = "Enter your admission score.")]
        public int? Score { get; set; }

        public Dictionary<ComparisonSubject, decimal?> RequiredMarks { get; set; } = [];
    }
}
