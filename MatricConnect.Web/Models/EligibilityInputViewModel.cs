// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Stores and validates academic results entered
//                   for a programme eligibility check.

using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace MatricConnect.Web.Models
{
    public class EligibilityInputViewModel
    {
        [Required(ErrorMessage = "Enter your APS score.")]
        [Range(0, 57, ErrorMessage = "Your APS must be between 0 and 57")]
        [Display(Name = "APS score")]
        public int? APSScore
        {
            //
            // Name            : property int? APSScore
            // Purpose         : Stores the entered APS score.
            // Re-use          : None
            // Input Parameter : int? value
            //                   - entered score, or null when blank
            // Output Type     : int?
            //                   - current APS input
            //

            get; set;
        } // end property APSScore

        [Required(ErrorMessage = "Enter your Mathematics marks.")]
        [Range(typeof(decimal), "0", "100", ErrorMessage = "Mathematics marks must be be tween 0 and 100.")]
        [Display(Name = "Mathematics mark (%)")]
        public decimal? MathematicsMark
        {

            //
            // Name            : property decimal? MathematicsMark
            // Purpose         : Stores the entered Mathematics mark.
            // Re-use          : None
            // Input Parameter : decimal? value
            //                   - entered mark, or null when blank
            // Output Type     : decimal?
            //                   - current Mathematics input
            //

            get; set;
        } // end property MathematicsMark

        [Range(typeof(decimal), "0", "100", ErrorMessage = "Physical Science mark must be between 0 and 100")]
        [Display(Name = "Physical Science mark (%) - optional")]
        public decimal? PhysicalScienceMark
        {

            //
            // Name            : property decimal? PhysicalScienceMark
            // Purpose         : Stores the optional Physical Science mark.
            // Re-use          : None
            // Input Parameter : decimal? value
            //                   - entered mark, or null when not provided
            // Output Type     : decimal?
            //                   - current Physical Science input
            //

            get; set;
        } // end property PhysicalScienceMark

    } // end class EligibilityInputViewModel
} // end namespace MatricConnect.Web.Models
