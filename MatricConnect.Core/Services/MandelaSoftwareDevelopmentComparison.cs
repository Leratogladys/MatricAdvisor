// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Compares the two numeric requirements for Mandela's 2027 Software Development diploma.

using MatricConnect.Models;

namespace MatricConnect.Services
{
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

        public static ProgrammeComparisonRules Rules { get; } = new(
            "Mandela Applicant Score (AS)", MaximumApplicantScore,
            [
                new(ComparisonSubject.Mathematics, "Mathematics", MathematicsScore, MathematicsMark),
                new(ComparisonSubject.TechnicalMathematics, "Technical Mathematics", MathematicsScore, MathematicsMark),
                new(ComparisonSubject.MathematicalLiteracy, "Mathematical Literacy", LiteracyScore, LiteracyMark)
            ],
            "Confirm that your NSC is endorsed for Diploma study or higher.");
    }
}
