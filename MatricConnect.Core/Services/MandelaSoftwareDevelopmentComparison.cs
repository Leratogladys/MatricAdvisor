// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Compares the two numeric requirements for Mandela's 2027 Software Development diploma.

using System.ComponentModel.DataAnnotations;
using MatricConnect.Models;

namespace MatricConnect.Services
{
    public enum MandelaMathsSubject
    {
        Mathematics = 1,
        [Display(Name = "Technical Mathematics")]
        TechnicalMathematics = 2,
        [Display(Name = "Mathematical Literacy")]
        MathematicalLiteracy = 3
    }

    public sealed record MandelaComparisonResult(string SubjectName, int ApplicantScore, decimal MathsMark,
        int RequiredApplicantScore, int RequiredMathsMark)
    {
        public bool MeetsApplicantScore => ApplicantScore >= RequiredApplicantScore;
        public bool MeetsMathsMark => MathsMark >= RequiredMathsMark;
    }

    public static class MandelaSoftwareDevelopmentComparison
    {
        public const string UniversityName = "Nelson Mandela University";
        public const string ProgrammeCode = "7224";
        public const int IntakeYear = 2027;
        public const int MathematicsScore = 330;
        public const int MathematicsMark = 40;
        public const int LiteracyScore = 345;
        public const int LiteracyMark = 60;
        public const int MaximumApplicantScore = 607; // Six subject percentages plus the possible seven-point allowance.

        public static bool Supports(University university, ProgrammeIntake? intake) =>
            university.Name == UniversityName &&
            intake is { OfficialProgrammeCode: ProgrammeCode, IntakeYear: IntakeYear };

        public static MandelaComparisonResult Compare(MandelaMathsSubject subject, int applicantScore, decimal mathsMark)
        {
            var (name, requiredScore, requiredMark) = subject switch
            {
                MandelaMathsSubject.Mathematics => ("Mathematics", MathematicsScore, MathematicsMark),
                MandelaMathsSubject.TechnicalMathematics => ("Technical Mathematics", MathematicsScore, MathematicsMark),
                MandelaMathsSubject.MathematicalLiteracy => ("Mathematical Literacy", LiteracyScore, LiteracyMark),
                _ => throw new ArgumentOutOfRangeException(nameof(subject))
            };

            return new MandelaComparisonResult(name, applicantScore, mathsMark, requiredScore, requiredMark);
        }
    }
}
