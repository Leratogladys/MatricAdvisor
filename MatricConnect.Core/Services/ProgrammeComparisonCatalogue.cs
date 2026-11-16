using System.Text.Json;
using System.Text.Json.Serialization;
using MatricConnect.Models;

namespace MatricConnect.Services;

internal static class ProgrammeComparisonCatalogue
{
    private const string ResourceName = "MatricConnect.ProgrammeComparisons.json";
    private const string ApplicantRoute = "south-african-nsc";
    private static readonly Lazy<(Dictionary<ProgrammeKey, PublishedComparison> Entries, string? Error)> Catalogue =
        new(LoadEmbedded);

    private readonly record struct ProgrammeKey(string University, string Code, int Year);
    private sealed record PublishedComparison(
        string RequirementsUrl, string GuideUrl, DateOnly VerifiedOn, ProgrammeComparisonRules Rules);

    private sealed record Manifest(int Version, ScoreScheme[] ScoreSchemes, Entry[] Programmes);
    private sealed record ScoreScheme(string Id, string University, string Label,
        [property: JsonRequired] int Maximum, string GuideUrl);
    private sealed record Entry(string University, string ProgrammeCode, int IntakeYear, string ApplicantRoute,
        string ScoreScheme, string RequirementsUrl, DateOnly VerifiedOn, int? MinimumScore, SubjectOption[] SubjectOptions,
        string RemainingRequirements, MarkMinimum[]? RequiredMarks, ScoreRange? ExtendedCurriculum);
    private sealed record SubjectOption(string Subject,
        [property: JsonRequired] int MinimumMark, int? MinimumScore);
    private sealed record MarkMinimum(string Subject,
        [property: JsonRequired] int MinimumMark);
    private sealed record ScoreRange([property: JsonRequired] int From, [property: JsonRequired] int To);

    public static string? ConfigurationError => Catalogue.Value.Error;

    public static bool IsConfigured(string university, ProgrammeIntake intake) =>
        Catalogue.Value.Entries.ContainsKey(new(university, intake.OfficialProgrammeCode, intake.IntakeYear));

    public static ProgrammeComparisonRules? For(string university, ProgrammeIntake? intake) =>
        intake is not null &&
        Catalogue.Value.Entries.TryGetValue(new(university, intake.OfficialProgrammeCode, intake.IntakeYear), out var entry) &&
        entry.VerifiedOn == intake.VerifiedOn &&
        entry.RequirementsUrl == intake.RequirementsSourceUrl &&
        entry.GuideUrl == intake.ApsGuideUrl
            ? entry.Rules
            : null;

    private static (Dictionary<ProgrammeKey, PublishedComparison>, string?) LoadEmbedded()
    {
        try
        {
            using var resource = typeof(ProgrammeComparisonCatalogue).Assembly.GetManifestResourceStream(ResourceName)
                ?? throw new InvalidDataException("Embedded comparison rules are missing.");
            using var document = JsonDocument.Parse(resource);
            RejectDuplicateFields(document.RootElement);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
            };
            var manifest = document.RootElement.Deserialize<Manifest>(options)
                ?? throw new InvalidDataException("Expected a comparison rules object.");
            return (Validate(manifest), null);
        }
        catch (Exception error) when (error is JsonException or InvalidDataException or IOException)
        {
            // Keep programme details usable as reference pages if the rule file is invalid.
            return ([], error.Message);
        }
    }

    private static Dictionary<ProgrammeKey, PublishedComparison> Validate(Manifest manifest)
    {
        Require(manifest.Version == 1, "Unsupported comparison rules version.");
        Require(manifest.ScoreSchemes is { Length: > 0 } && manifest.Programmes is { Length: > 0 },
            "Comparison rules need score schemes and programme entries.");

        var schemes = new Dictionary<string, ScoreScheme>(StringComparer.Ordinal);
        foreach (var scheme in manifest.ScoreSchemes!)
        {
            if (scheme is null || !IsLine(scheme.Id) || !IsLine(scheme.University) ||
                !IsLine(scheme.Label) || scheme.Maximum <= 0 || !IsSourceUrl(scheme.GuideUrl) ||
                !schemes.TryAdd(scheme.Id, scheme))
                throw new InvalidDataException("Each comparison score scheme needs a unique ID, university, label, positive maximum and HTTPS guide.");
        }

        var today = DateOnly.FromDateTime(DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(2)).DateTime);
        var entries = new Dictionary<ProgrammeKey, PublishedComparison>();
        foreach (var entry in manifest.Programmes!)
        {
            if (entry is null || !IsLine(entry.University) || !IsLine(entry.ProgrammeCode) ||
                entry.IntakeYear is < 2008 or > 2100 || entry.ApplicantRoute != ApplicantRoute ||
                !IsSourceUrl(entry.RequirementsUrl) || entry.VerifiedOn == default || entry.VerifiedOn > today ||
                !IsLine(entry.RemainingRequirements))
                throw new InvalidDataException("Each comparison needs a university, code, intake, South African NSC route, reviewed source and remaining requirements.");

            string description = $"{entry.University} / {entry.ProgrammeCode} / {entry.IntakeYear}";
            if (entry.ScoreScheme is null || !schemes.TryGetValue(entry.ScoreScheme, out var scheme) ||
                scheme.University != entry.University)
                throw new InvalidDataException($"{description}: use a score scheme for the same institution.");

            if (entry.SubjectOptions is not { Length: > 0 })
                throw new InvalidDataException($"{description}: at least one subject option is required.");

            var subjects = new HashSet<ComparisonSubject>();
            var alternatives = new List<SubjectComparisonRule>();
            foreach (var option in entry.SubjectOptions)
            {
                var (subject, label) = ParseSubject(option?.Subject);
                if (option is null || option.MinimumMark is < 0 or > 100 || !subjects.Add(subject) ||
                    option.MinimumScore.HasValue == entry.MinimumScore.HasValue ||
                    (option.MinimumScore ?? entry.MinimumScore) is not int score || score < 0 || score > scheme.Maximum)
                    throw new InvalidDataException($"{description}: use unique subjects, valid marks and a score minimum either for the programme or for each subject.");

                alternatives.Add(new(subject, label, score, option.MinimumMark));
            }

            var required = new List<RequiredSubjectMark>();
            foreach (var mark in entry.RequiredMarks ?? [])
            {
                var (subject, label) = ParseSubject(mark?.Subject);
                if (mark is null || mark.MinimumMark is < 0 or > 100 || !subjects.Add(subject))
                    throw new InvalidDataException($"{description}: required marks must be valid and use subjects outside the alternatives.");

                required.Add(new(subject, label, mark.MinimumMark));
            }

            (int, int)? extended = null;
            if (entry.ExtendedCurriculum is { } range)
            {
                if (range.From < 0 || range.To < range.From || range.To >= alternatives.Min(option => option.MinimumScore))
                    throw new InvalidDataException($"{description}: the Extended Curriculum range must be below the mainstream minimums.");
                extended = (range.From, range.To);
            }

            var key = new ProgrammeKey(entry.University, entry.ProgrammeCode, entry.IntakeYear);
            var rules = new ProgrammeComparisonRules(scheme.Label, scheme.Maximum, alternatives.AsReadOnly(),
                entry.RemainingRequirements, required.AsReadOnly(), extended);
            Require(entries.TryAdd(key, new(entry.RequirementsUrl, scheme.GuideUrl, entry.VerifiedOn, rules)),
                $"{description}: duplicate comparison.");
        }
        return entries;
    }

    private static (ComparisonSubject Subject, string Label) ParseSubject(string? subject) => subject switch
    {
        "mathematics" => (ComparisonSubject.Mathematics, "Mathematics"),
        "technicalMathematics" => (ComparisonSubject.TechnicalMathematics, "Technical Mathematics"),
        "mathematicalLiteracy" => (ComparisonSubject.MathematicalLiteracy, "Mathematical Literacy"),
        "computerScience" => (ComparisonSubject.ComputerScience, "Computer Science"),
        "english" => (ComparisonSubject.English, "English"),
        "physicalSciences" => (ComparisonSubject.PhysicalSciences, "Physical Sciences"),
        _ => throw new InvalidDataException($"Unknown comparison subject '{subject}'.")
    };

    private static void RejectDuplicateFields(JsonElement element)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            var fields = new HashSet<string>(StringComparer.Ordinal);
            foreach (var property in element.EnumerateObject())
            {
                Require(fields.Add(property.Name), $"Duplicate comparison rules field: {property.Name}.");
                RejectDuplicateFields(property.Value);
            }
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in element.EnumerateArray())
                RejectDuplicateFields(item);
        }
    }

    private static bool IsLine(string? text) =>
        !string.IsNullOrWhiteSpace(text) && text == text.Trim() && !text.Any(char.IsControl);

    private static bool IsSourceUrl(string? value) =>
        IsLine(value) && Uri.TryCreate(value, UriKind.Absolute, out var uri) &&
        uri.Scheme == Uri.UriSchemeHttps && uri.UserInfo.Length == 0 && uri.IsDefaultPort;

    private static void Require(bool valid, string message)
    {
        if (!valid)
            throw new InvalidDataException(message);
    }
}
