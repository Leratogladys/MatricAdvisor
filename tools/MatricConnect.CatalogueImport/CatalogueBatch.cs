using MatricConnect.Models;

namespace MatricConnect.CatalogueImport;

internal sealed record CatalogueBatch(int Version, string? Scope, string? University, int IntakeYear,
    string? ReviewStatus, string? ReviewedBy, DateOnly? VerifiedOn, CatalogueEntry?[]? Entries)
{
    public IEnumerable<string> Validate(DateOnly today)
    {
        if (Version != 1 || Scope != "university-undergraduate-nsc")
            yield return "Expected version 1 and scope university-undergraduate-nsc.";
        if (!CatalogueEntry.IsLine(University) || !CatalogueEntry.IsLine(ReviewedBy) || ReviewStatus != "reviewed")
            yield return "Provide a university and reviewer, and set reviewStatus to reviewed after checking the sources.";
        if (IntakeYear is < 2008 or > 2100)
            yield return "Provide a valid NSC intake year (2008–2100).";
        if (VerifiedOn is not DateOnly verified || verified > today || verified.Year < 2008)
            yield return "Provide a verification date that is not in the future.";
        if (Entries is not { Length: > 0 })
        {
            yield return "Supply at least one entry.";
            yield break;
        }

        var codes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (var i = 0; i < Entries.Length; i++)
        {
            var row = $"Entry {i + 1}";
            if (Entries[i] is not { } entry)
            {
                yield return $"{row}: entry cannot be null.";
                continue;
            }

            foreach (var issue in entry.Validate(IntakeYear))
                yield return $"{row}: {issue}";
            if (CatalogueEntry.IsLine(entry.Code) && !codes.Add(entry.Code!))
                yield return $"{row}: duplicate programme code {entry.Code}.";
            if (CatalogueEntry.IsLine(entry.Name) && !names.Add(entry.Name!))
                yield return $"{row}: duplicate programme name {entry.Name}.";
        }
    }
}

internal sealed record CatalogueEntry(
    string? Code, string? Name, string? FieldOfStudy, string? ScoreLabel, int? MinimumScore,
    string? ScoreBySubject, decimal? MinimumMathematics, decimal? MinimumEnglish, decimal? MinimumPhysicalScience,
    DateOnly? ApplicationDeadline, TimeOnly? ApplicationClosingTime, string? NextApplicationCycleNote,
    string? RequirementsSourceUrl, string? ApplicationDatesSourceUrl, string? ScoreGuideUrl, string?[]? Requirements, string? Faculty)
{
    public IEnumerable<string> Validate(int intakeYear)
    {
        if (!IsLine(Code) || !IsLine(Name) || !IsLine(FieldOfStudy) || !IsLine(ScoreLabel))
            yield return "Code, name, field of study and score label are required single lines.";
        if (Faculty != null && !IsLine(Faculty))
            yield return "Provide a nonblank faculty name or omit it.";
        // The current data model needs a numeric score or a published description of subject-specific scores.
        if ((MinimumScore.HasValue == (ScoreBySubject != null)) || MinimumScore is <= 0 ||
            (ScoreBySubject != null && !IsLine(ScoreBySubject)))
            yield return "Provide one positive published minimumScore or one nonblank scoreBySubject, not both.";
        if (MinimumMathematics is < 0 or > 100 || MinimumEnglish is < 0 or > 100 || MinimumPhysicalScience is < 0 or > 100)
            yield return "Subject marks must be between 0 and 100.";
        if (ApplicationClosingTime.HasValue && !ApplicationDeadline.HasValue)
            yield return "A closing time requires a deadline.";
        if (ApplicationDeadline is DateOnly date && (date.Year < intakeYear - 1 || date.Year > intakeYear))
            yield return "The deadline must fall in the intake year or the preceding calendar year.";
        if (NextApplicationCycleNote != null && !IsLine(NextApplicationCycleNote))
            yield return "Use a single line for the next cycle note or leave it null.";
        if (Requirements is not { Length: > 0 } || Requirements.Any(requirement => !IsLine(requirement)))
            yield return "Provide at least one nonblank requirement, one per array item.";
    }

    public IEnumerable<string> ValidateSources(string officialDomain)
    {
        foreach (var (label, value) in new[] {
            ("requirementsSourceUrl", RequirementsSourceUrl),
            ("applicationDatesSourceUrl", ApplicationDatesSourceUrl),
            ("scoreGuideUrl", ScoreGuideUrl)
        })
        {
            if (!IsLine(value) || !Uri.TryCreate(value, UriKind.Absolute, out var uri) ||
                uri.Scheme != Uri.UriSchemeHttps || uri.UserInfo.Length != 0 || !uri.IsDefaultPort ||
                !(uri.IdnHost.Equals(officialDomain, StringComparison.OrdinalIgnoreCase) ||
                  uri.IdnHost.EndsWith("." + officialDomain, StringComparison.OrdinalIgnoreCase)))
            {
                yield return $"{label} must be an HTTPS link on {officialDomain} or one of its subdomains.";
            }
        }
    }

    public ProgrammeIntake CreateIntake(CatalogueBatch batch) => new()
    {
        IntakeYear = batch.IntakeYear,
        OfficialProgrammeCode = Code!,
        AdmissionScoreLabel = ScoreLabel!,
        RequiredAps = MinimumScore.GetValueOrDefault(),
        AdmissionScoreBySubject = ScoreBySubject,
        RequiredMathematics = MinimumMathematics,
        RequiredEnglish = MinimumEnglish,
        RequiredPhysicalScience = MinimumPhysicalScience,
        ApplicationDeadline = ApplicationDeadline,
        ApplicationClosingTime = ApplicationClosingTime,
        NextApplicationCycleNote = NextApplicationCycleNote,
        RequirementsSourceUrl = RequirementsSourceUrl!,
        ApplicationDatesSourceUrl = ApplicationDatesSourceUrl!,
        ApsGuideUrl = ScoreGuideUrl!,
        AdditionalRequirements = string.Join('\n', Requirements!),
        VerifiedOn = batch.VerifiedOn!.Value
    };

    public static bool IsLine(string? value) =>
        !string.IsNullOrWhiteSpace(value) && value == value.Trim() && !value.Any(char.IsControl);
}
