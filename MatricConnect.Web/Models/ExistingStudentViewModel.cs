// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Stores and validates the student ID entered
//                   when retrieving an existing profile.

using System.ComponentModel.DataAnnotations;

namespace MatricConnect.Web.Models
{
    public class ExistingStudentViewModel
    {
        [Required(ErrorMessage = "Enter your Student ID.")]
        [Range(1, int.MaxValue, ErrorMessage = "Student ID must be a positive whole number.")]
        [Display(Name = "Student ID")]
        public int? StudentId
        {
            //
            // Name            : property int? StudentId
            // Purpose         : Stores the ID entered for profile retrieval.
            // Re-use          : None
            // Input Parameter : int? value
            //                   - entered student ID, or null when blank
            // Output Type     : int?
            //                   - current student ID input
            //

            get; set;
        } // end property StudentId

        public string? ReturnUrl
        {
            //
            // Name            : property string? ReturnUrl
            // Purpose         : Stores the destination after profile selection.
            // Re-use          : None
            // Input Parameter : string? value
            //                   - requested return URL
            // Output Type     : string?
            //                   - destination to validate before redirecting
            //

            get; set;
        } // end property ReturnUrl
    } // end class ExistingStudentViewModel
} // end namespace MatricConnect.Web.Models
