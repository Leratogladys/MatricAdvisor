using System.ComponentModel.DataAnnotations;
using MatricConnect.Services;

namespace MatricConnect.Web.Models
{
    public sealed class MandelaComparisonInputViewModel
    {
        [Required(ErrorMessage = "Choose your maths subject.")]
        [EnumDataType(typeof(MandelaMathsSubject), ErrorMessage = "Choose a maths subject from the list.")]
        [Display(Name = "Maths subject")]
        public MandelaMathsSubject? Subject { get; set; }

        [Required(ErrorMessage = "Enter your maths mark.")]
        [Range(typeof(decimal), "0", "100", ErrorMessage = "Enter a mark between 0 and 100.")]
        [Display(Name = "Maths mark (%)")]
        public decimal? MathsMark { get; set; }

        [Required(ErrorMessage = "Enter your Mandela Applicant Score.")]
        [Range(0, MandelaSoftwareDevelopmentComparison.MaximumApplicantScore,
            ErrorMessage = "Enter a Mandela Applicant Score between 0 and 607.")]
        [Display(Name = "Mandela Applicant Score (AS)")]
        public int? ApplicantScore { get; set; }
    }
}
