// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Stores and validates student profile input
//                   submitted through the website.

using System.ComponentModel.DataAnnotations;

namespace MatricConnect.Web.Models
{
    public class CreateStudentViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First name")]
        public string FirstName
        {
            //
            // Name            : property string FirstName
            // Purpose         : Stores the student's first name.
            // Re-use          : None
            // Input Parameter : string value - entered first name
            // Output Type     : string - current first name
            //

            get; set;
        } = string.Empty; // end property FirstName

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last name")]
        public string LastName
        {
            //
            // Name            : property string LastName
            // Purpose         : Stores the student's last name.
            // Re-use          : None
            // Input Parameter : string value - entered last name
            // Output Type     : string - current last name
            //

            get; set;
        } = string.Empty; // end property LastName

        [Required(ErrorMessage = "Home province is required.")]
        [Display(Name = "Home province")]
        public string Province
        {
            //
            // Name            : property string Province
            // Purpose         : Stores the student's home province.
            // Re-use          : None
            // Input Parameter : string value - entered province
            // Output Type     : string - current home province
            //

            get; set;
        } = string.Empty; // end property Province

        [Required(ErrorMessage = "Enter your APS score.")]
        [Range(0, 57, ErrorMessage = "APS score must be between 0 and 57.")]
        [Display(Name = "APS score")]
        public int? APSScore
        {
            //
            // Name            : property int? APSScore
            // Purpose         : Stores APS input while allowing a blank form.
            // Re-use          : None
            // Input Parameter : int? value - entered score or null
            // Output Type     : int? - current APS input
            //

            get; set;
        } // end property APSScore

        [Required(ErrorMessage = "Enter your Mathematics mark.")]
        [Range(typeof(decimal), "0", "100",
            ErrorMessage = "Mathematics mark must be between 0 and 100.")]
        [Display(Name = "Mathematics mark (%)")]
        public decimal? MathematicsMark
        {
            //
            // Name            : property decimal? MathematicsMark
            // Purpose         : Stores Mathematics input, including blank input.
            // Re-use          : None
            // Input Parameter : decimal? value - entered mark or null
            // Output Type     : decimal? - current Mathematics input
            //

            get; set;
        } // end property MathematicsMark

        [Range(typeof(decimal), "0", "100",
            ErrorMessage = "Physical Science mark must be between 0 and 100.")]
        [Display(Name = "Physical Science mark (%) — optional")]
        public decimal? PhysicalScienceMark
        {
            //
            // Name            : property decimal? PhysicalScienceMark
            // Purpose         : Stores the optional Physical Science mark.
            // Re-use          : None
            // Input Parameter : decimal? value - entered mark or null
            // Output Type     : decimal? - current Physical Science input
            //

            get; set;
        } // end property PhysicalScienceMark
    } // end class CreateStudentViewModel
} // end namespace MatricConnect.Web.Models