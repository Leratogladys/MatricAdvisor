// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Stores province options, the selected province,
//                   institution type, and directory search results.

using MatricConnect.Models;


namespace MatricConnect.Web.Models
{
    public class UniversitySearchViewModel
    {
        public const string UniversitiesType = "universities";
        public const string TvetCollegesType = "tvet";

        public string InstitutionType { get; set; } = UniversitiesType;
        public bool ShowTvetColleges => InstitutionType == TvetCollegesType;
        public List<TvetCollege> Colleges { get; set; } = new();

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

        public List<UniversitySearchResult> Universities
        {
            //
            // Name            : property List<University> Universities
            // Purpose         : Provides universities matching the selection.
            // Re-use          : None
            // Input Parameter : List<University> value
            //                   - matching universities
            // Output Type     : List<UniversitySearchResult>
            //                   - university search results
            //

            get; set;
        } = new List<UniversitySearchResult>(); // end property Universities
    } // end class UniversitySearchViewModel
} // end namespace MatricConnect.Web.Models
