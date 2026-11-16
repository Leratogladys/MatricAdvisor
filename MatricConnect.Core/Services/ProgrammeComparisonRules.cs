// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Evaluates reviewed minimums for a programme intake.

using MatricConnect.Models;

namespace MatricConnect.Services
{
    public enum ComparisonSubject
    {
        Mathematics = 1,
        TechnicalMathematics = 2,
        MathematicalLiteracy = 3,
        ComputerScience = 4,
        English = 5,
        PhysicalSciences = 6
    }

    public sealed record SubjectComparisonRule(ComparisonSubject Subject, string Label, int MinimumScore, int MinimumMark);

    public sealed record RequiredSubjectMark(ComparisonSubject Subject, string Label, int MinimumMark);

    public sealed record RequiredSubjectResult(RequiredSubjectMark Requirement, decimal Mark)
    {
        public bool MeetsMinimum => Mark >= Requirement.MinimumMark;
    }

    public sealed record ProgrammeComparisonResult(
        SubjectComparisonRule Requirement, int Score, decimal Mark, IReadOnlyList<RequiredSubjectResult> RequiredMarks)
    {
        public bool MeetsScore => Score >= Requirement.MinimumScore;
        public bool MeetsMark => Mark >= Requirement.MinimumMark;
    }

    public sealed record ProgrammeComparisonRules(
        string ScoreLabel, int MaximumScore, IReadOnlyList<SubjectComparisonRule> Subjects,
        string RemainingRequirements, IReadOnlyList<RequiredSubjectMark> RequiredMarks,
        (int From, int To)? ExtendedCurriculumScoreRange)
    {
        public ProgrammeComparisonResult Compare(SubjectComparisonRule subject, int score, decimal mark,
            IReadOnlyDictionary<ComparisonSubject, decimal?> requiredMarks) => new(subject, score, mark,
                RequiredMarks.Select(requirement => new RequiredSubjectResult(
                    requirement, requiredMarks[requirement.Subject]!.Value)).ToArray());

        public static ProgrammeComparisonRules? For(University university, ProgrammeIntake? intake) =>
            ProgrammeComparisonCatalogue.For(university.Name, intake);

        public static bool IsConfigured(University university, ProgrammeIntake intake) =>
            ProgrammeComparisonCatalogue.IsConfigured(university.Name, intake);

        public static string? ConfigurationError => ProgrammeComparisonCatalogue.ConfigurationError;
    }
}
