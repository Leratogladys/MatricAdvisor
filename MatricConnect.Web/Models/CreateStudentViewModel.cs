// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Stores student profile creation input and its return path.

namespace MatricConnect.Web.Models
{
    public class CreateStudentViewModel : StudentProfileInput
    {
        public string? ReturnUrl
        {
            //
            // Name            : property string? ReturnUrl
            // Purpose         : Stores the destination after profile creation.
            // Re-use          : None
            // Input Parameter : string? value
            //                   - requested return URL
            // Output Type     : string?
            //                   - destination to validate before redirecting
            //

            get; set;
        } // end property ReturnUrl
    } // end class CreateStudentViewModel
} // end namespace MatricConnect.Web.Models
