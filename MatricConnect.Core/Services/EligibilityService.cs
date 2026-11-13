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

            return EvaluateRequirements(student, programme)
                .Where(requirement => requirement.Issue != null)
                .Select(requirement => requirement.Issue!)
                .ToList();
        } // end method GetEligibilityIssues

        public List<EligibilityRequirement> EvaluateRequirements(Student student, Programme programme)
        {
            var requiredScience = programme.RequiredPhysicalScience;
            var scienceMark = student.PhysicalScienceMark;

            return new List<EligibilityRequirement>
            {
                new("APS score", programme.RequiredAPS, student.APSScore, false,
                    student.APSScore < programme.RequiredAPS
                        ? $"Your APS score ({student.APSScore}) is below the required minimum APS ({programme.RequiredAPS})."
                        : null),
                new("Mathematics", programme.RequiredMathematics, student.MathematicsMark, true,
                    student.MathematicsMark < programme.RequiredMathematics
                        ? $"Your Mathematics mark ({student.MathematicsMark:0.##}) is below the required minimum mark ({programme.RequiredMathematics:0.##})."
                        : null),
                new("Physical Science", requiredScience, scienceMark, true,
                    requiredScience.HasValue
                        ? scienceMark == null
                            ? "A Physical Science mark is required for this programme."
                            : scienceMark < requiredScience
                                ? $"Your Physical Science mark ({scienceMark:0.##}) is below the required minimum mark ({requiredScience:0.##})."
                                : null
                        : null)
            };
        } // end method EvaluateRequirements

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