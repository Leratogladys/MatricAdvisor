// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Evaluates student academic results against programme
//                   admission requirements supported by Matric Connect.

using MatricConnect.Models;

namespace MatricConnect.Services
{
    public class EligibilityService
    {
        public List<string> GetEligibilityIssues(
            Student student,
            Programme programme)
        {
            //
            // Name              : List<string> GetEligibilityIssues(
            //                     Student student, Programme programme)
            // Purpose           : Identifies programme requirements that
            //                     the student does not meet.
            // Re-use            : None
            // Method Parameters : Student student
            //                     - student whose academic results are checked
            //                     Programme programme
            //                     - programme containing admission requirements
            // Output Type       : List<string>
            //                     - list describing requirements not met
            //                     - empty list when all evaluated requirements
            //                       are met
            //

            var issues = new List<string>();

            if (student.APSScore < programme.RequiredAPS)
            {
                issues.Add(
                    $"Your APS score ({student.APSScore}) " +
                    $"is below the required minimum APS " +
                    $"({programme.RequiredAPS}).");
            } // end if

            if (student.MathematicsMark <
                programme.RequiredMathematics)
            {
                issues.Add(
                    $"Your Mathematics mark " +
                    $"({student.MathematicsMark:0}) " +
                    $"is below the required minimum mark " +
                    $"({programme.RequiredMathematics:0}).");
            } // end if

            if (programme.RequiredPhysicalScience.HasValue)
            {
                if (student.PhysicalScienceMark == null)
                {
                    issues.Add(
                        "A Physical Science mark is required " +
                        "for this programme.");
                } // end if
                else if (student.PhysicalScienceMark <
                         programme.RequiredPhysicalScience.Value)
                {
                    issues.Add(
                        $"Your Physical Science mark " +
                        $"({student.PhysicalScienceMark:0}) " +
                        $"is below the required minimum mark " +
                        $"({programme.RequiredPhysicalScience.Value:0}).");
                } // end else if
            } // end if

            return issues;
        } // end method

        public bool IsEligible(
            Student student,
            Programme programme)
        {
            //
            // Name              : bool IsEligible(
            //                     Student student, Programme programme)
            // Purpose           : Determines whether the student meets all
            //                     requirements currently evaluated by
            //                     Matric Connect.
            // Re-use            : GetEligibilityIssues()
            // Method Parameters : Student student
            //                     - student whose academic results are checked
            //                     Programme programme
            //                     - programme containing admission requirements
            // Output Type       : bool
            //                     - true when all evaluated requirements are met
            //                     - false when one or more requirements
            //                       are not met
            //

            return GetEligibilityIssues(
                student,
                programme).Count == 0;
        } // end method
    } // end class EligibilityService
} // end namespace MatricConnect.Services