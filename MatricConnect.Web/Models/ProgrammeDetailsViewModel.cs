// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Stores programme and university information
//                   for the programme details page.

using MatricConnect.Models;
using MatricConnect.Services;

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

        public IReadOnlyList<EligibilityRequirement> EligibilityRequirements { get; set; } = [];

        public bool EligibilityChecked => EligibilityRequirements.Count > 0;

        public bool MeetsEvaluatedRequirements => EligibilityChecked &&
            EligibilityRequirements.All(requirement => requirement.Issue == null);

        public MandelaComparisonInputViewModel? MandelaComparison { get; set; }

        public MandelaComparisonResult? MandelaResult { get; set; }

        public ProgrammeSaveOutcome? SaveOutcome { get; set; }
    } // end class ProgrammeDetailsViewModel
} // end namespace MatricConnect.Web.Models
