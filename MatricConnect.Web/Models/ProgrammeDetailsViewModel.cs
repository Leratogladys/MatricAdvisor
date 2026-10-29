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

        public EligibilityInputViewModel EligibilityInput
        {
            //
            // Name            : property EligibilityInputViewModel EligibilityInput
            // Purpose         : Stores academic results entered for evaluation.
            // Re-use          : None
            // Input Parameter : EligibilityInputViewModel value
            //                   - academic results entered in the form
            // Output Type     : EligibilityInputViewModel
            //                   - current eligibility form input
            //

            get; set;
        } = new EligibilityInputViewModel(); // end property EligibilityInput

        public bool EligibilityChecked
        {
            //
            // Name            : property bool EligibilityChecked
            // Purpose         : Indicates whether valid results have been evaluated.
            // Re-use          : None
            // Input Parameter : bool value
            //                   - whether an eligibility check has completed
            // Output Type     : bool
            //                   - true after evaluation; otherwise false
            //
            get; set;
        } // end property EligibilityChecked

        public List<string> EligibilityIssues
        {
            //
            // Name            : property List<string> EligibilityIssues
            // Purpose         : Stores requirements the entered results do not meet.
            // Re-use          : None
            // Input Parameter : List<string> value
            //                   - issues returned by the shared eligibility service
            // Output Type     : List<string>
            //                   - unmet requirements, or an empty list when all are met
            //

            get; set;
        } = new List<string>(); // end property EligibilityIssues

    } // end class ProgrammeDetailsViewModel
} // end namespace MatricConnect.Web.Models
