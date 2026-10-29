// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Stores province options, the selected province,
//                   and university search results for the web page.

using MatricConnect.Models;


namespace MatricConnect.Web.Models
{
    public class UniversitySearchViewModel
    {
        public List<string> Provinces
        {
            //
            // Name            : property List<string> Provinces
            // Purpose         : Provides the available province options.
            // Re-use          : None
            // Input Parameter : List<string> value
            //                   - available provinces
            // Output Type     : List<string>
            //                   - available province options
            //

            get; set;
        } = new List<string>(); // end property Provinces

        public string? SelectedProvince
        {
            //
            // Name            : property string? SelectedProvince
            // Purpose         : Stores the province selected for searching.
            // Re-use          : None
            // Input Parameter : string? value
            //                   - selected province, or null before selection
            // Output Type     : string?
            //                   - current province selection
            //

            get; set;
        } // end property SelectedProvince

        public List<University> Universities
        {
            //
            // Name            : property List<University> Universities
            // Purpose         : Provides universities matching the selection.
            // Re-use          : None
            // Input Parameter : List<University> value
            //                   - matching universities
            // Output Type     : List<University>
            //                   - university search results
            //

            get; set;
        } = new List<University>(); // end property Universities
    } // end class UniversitySearchViewModel
} // end namespace MatricConnect.Web.Models
