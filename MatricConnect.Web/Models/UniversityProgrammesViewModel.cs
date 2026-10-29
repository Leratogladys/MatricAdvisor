// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Stores the selected university and its programmes
//                   for display on the programme browsing page.

using MatricConnect.Models;


namespace MatricConnect.Web.Models
{
    public class UniversityProgrammesViewModel
    {
        public University University
        {
            //
            // Name            : property University University
            // Purpose         : Provides details of the selected university.
            // Re-use          : None
            // Input Parameter : University value
            //                   - selected university
            // Output Type     : University
            //                   - university whose programmes are displayed
            //

            get; set;
        } = new University(); // end property

      public List<Programme> Programmes
        {
            //
            // Name            : property List<Programme> Programmes
            // Purpose         : Provides programmes offered by the university.
            // Re-use          : None
            // Input Parameter : List<Programme> value
            //                   - programmes offered by the selected university
            // Output Type     : List<Programme>
            //                   - programmes available for browsing
            //

            get; set;
        } = new List<Programme>(); // end property
    } // end class UniversityProgrammesViewModel
} // end namespace MatricConnect.Web.Models
