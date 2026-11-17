using System.Text.Json;
using System.Text.Json.Serialization;

namespace MatricConnect.Models;

// A reviewed, non-executable representation of the minimums published for a reference listing.
public sealed record ReferenceComparisonRules(ReferenceRoute[] Routes, string? Note, string? ScoreGuideUrl)
{
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web)
    {
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
    };

    public static ReferenceComparisonRules? Read(string? json)
    {
        try
        {
            var rules = json == null ? null : JsonSerializer.Deserialize<ReferenceComparisonRules>(json, Options);
            return rules?.IsValid() == true ? rules : null;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    public string ToJson() => JsonSerializer.Serialize(this, Options);

    public bool IsValid() =>
        Routes is { Length: > 0 and <= 12 } &&
        Routes.All(route => route != null && route.IsValid()) &&
        Routes.Select(route => route.Label).Distinct(StringComparer.OrdinalIgnoreCase).Count() == Routes.Length &&
        (Note == null || IsText(Note, 650)) &&
        (ScoreGuideUrl == null || Uri.TryCreate(ScoreGuideUrl, UriKind.Absolute, out var link) &&
            link.Scheme == Uri.UriSchemeHttps && link.IsDefaultPort && link.UserInfo.Length == 0);

    internal static bool IsText(string? value, int max) =>
        !string.IsNullOrWhiteSpace(value) && value == value.Trim() && value.Length <= max && !value.Any(char.IsControl);
}

public sealed record ReferenceRoute(string Label, ReferenceMinimum[] Minimums, string? Note)
{
    internal bool IsValid() => ReferenceComparisonRules.IsText(Label, 100) &&
        Minimums is { Length: > 0 and <= 16 } && Minimums.All(item => item != null && item.IsValid()) &&
        Minimums.Select(item => item.Label).Distinct(StringComparer.OrdinalIgnoreCase).Count() == Minimums.Length &&
        (Note == null || ReferenceComparisonRules.IsText(Note, 650));
}

public sealed record ReferenceMinimum(string Label, decimal Minimum, string Scale)
{
    [JsonIgnore]
    public int Lower => Scale switch { "level" => 1, "grade" => 1, _ => 0 };
    [JsonIgnore]
    public int Upper => Scale switch
    {
        "yesNo" => 1, "level" => 7, "grade" => 12, "nqf" => 10, "aps" => 57, "percent" => 100, _ => 0
    };

    public bool IsValid() => ReferenceComparisonRules.IsText(Label, 175) && Upper > 0 &&
        IsValidValue(Minimum) && (Scale != "yesNo" || Minimum == 1);

    public bool IsValidValue(decimal value) => value >= Lower && value <= Upper &&
        (Scale is "percent" or "aps" || decimal.Truncate(value) == value);

    public string Display(decimal value) => Scale switch
    {
        "yesNo" => value == 1 ? "Yes" : "No",
        "level" => $"Level {value:0}",
        "grade" => $"Grade {value:0}",
        "nqf" => $"NQF Level {value:0}",
        "percent" => $"{value:0.##}%",
        "aps" => $"{value:0.##}",
        _ => $"{value:0}"
    };
}
