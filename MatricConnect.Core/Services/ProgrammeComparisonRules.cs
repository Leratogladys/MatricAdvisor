// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Holds the numeric thresholds for a published results comparison.

using MatricConnect.Models;

namespace MatricConnect.Services
{
    public enum ComparisonSubject
    {
        Mathematics = 1,
        TechnicalMathematics = 2,
        MathematicalLiteracy = 3,
        ComputerScience = 4
    }

    public sealed record SubjectComparisonRule(ComparisonSubject Subject, string Label, int MinimumScore, int MinimumMark)
    {
        public ProgrammeComparisonResult Compare(int score, decimal mark) => new(this, score, mark);
    }

    public sealed record ProgrammeComparisonResult(SubjectComparisonRule Requirement, int Score, decimal Mark)
    {
        public bool MeetsScore => Score >= Requirement.MinimumScore;
        public bool MeetsMark => Mark >= Requirement.MinimumMark;
    }

    public sealed record ProgrammeComparisonRules(
        string ScoreLabel, int MaximumScore, IReadOnlyList<SubjectComparisonRule> Subjects,
        string RemainingRequirements, (int From, int To)? ExtendedCurriculumScoreRange = null)
    {
        public static ProgrammeComparisonRules? For(University university, ProgrammeIntake? intake)
        {
            if (MandelaSoftwareDevelopmentComparison.Supports(university, intake))
            {
                return MandelaSoftwareDevelopmentComparison.Rules;
            }

            return CutInformationTechnologyComparison.Supports(university, intake)
                ? CutInformationTechnologyComparison.Rules
                : null;
        }
    }
}
