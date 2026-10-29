// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Stores programme and university information
//                   for the programme details page.

using MatricConnect.Models;

namespace MatricConnect.Web.Models
{
    public class ProgrammeDetailsViewModel
    {
        public Programme Programme
        {
            //
            // Name            : property Programme Programme
            // Purpose         : Provides the selected programme's details.
            // Re-use          : None
            // Input Parameter : Programme value
            //                   - programme selected for viewing
            // Output Type     : Programme
            //                   - selected programme and its requirements
            //
            get;  set;
        } = new Programme(); // end property Programme

        public University University {
            
            //
            // Name            : property University University
            // Purpose         : Provides the university offering the programme.
            // Re-use          : None
            // Input Parameter : University value
            //                   - university offering the selected programme
            // Output Type     : University
            //                   - programme's university
            //

            get; set; } = new University(); // end property
    } // end class ProgrammeDetailsViewModel
} // end namespace MatricConnect.Web.Models
