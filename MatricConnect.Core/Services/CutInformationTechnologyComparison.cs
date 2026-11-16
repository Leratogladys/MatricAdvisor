// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Defines the comparison for CUT's 2027 Diploma in Information Technology.

using MatricConnect.Models;

namespace MatricConnect.Services
{
    public static class CutInformationTechnologyComparison
    {
        public const string UniversityName = "Central University of Technology";
        public const string ProgrammeCode = "DP_ITC";
        public const int IntakeYear = 2027;
        public const int MinimumScore = 27;
        public const int MathematicsOrComputerScienceMark = 40;
        public const int MathematicalLiteracyMark = 60;
        private const int MaximumScore = 49; // Six subjects at up to eight points each, plus one for Life Orientation.

        public static bool Supports(University university, ProgrammeIntake? intake) =>
            university.Name == UniversityName &&
            intake is { OfficialProgrammeCode: ProgrammeCode, IntakeYear: IntakeYear };

        public static ProgrammeComparisonRules Rules { get; } = new(
            "CUT APS", MaximumScore,
            [
                new(ComparisonSubject.Mathematics, "Mathematics", MinimumScore, MathematicsOrComputerScienceMark),
                new(ComparisonSubject.ComputerScience, "Computer Science", MinimumScore, MathematicsOrComputerScienceMark),
                new(ComparisonSubject.MathematicalLiteracy, "Mathematical Literacy", MinimumScore, MathematicalLiteracyMark)
            ],
            "Confirm your NSC Diploma pass or higher. Review CUT’s English guidance, National Benchmark Test requirements and selection process.",
            ExtendedCurriculumScoreRange: (22, 26));
    }
}
