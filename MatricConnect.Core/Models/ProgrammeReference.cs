namespace MatricConnect.Models;

/// <summary>A sourced offering whose admission route is not handled by the numeric comparison.</summary>
public class ProgrammeReference
{
    public int Id { get; set; }
    public int? UniversityId { get; set; }
    public int? TvetCollegeId { get; set; }
    public University? University { get; set; }
    public TvetCollege? TvetCollege { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;
    public string? Campuses { get; set; }
    public string? EntryRequirements { get; set; }
    public int? IntakeYear { get; set; }
    public string Availability { get; set; } = string.Empty;
    public string SourceUrl { get; set; } = string.Empty;
    public DateOnly VerifiedOn { get; set; }
    public string? ComparisonRulesJson { get; set; }
}
