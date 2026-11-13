namespace MatricConnect.Models
{
    public record EligibilityRequirement(
        string Label, decimal? Required, decimal? Actual, bool IsPercentage, string? Issue);
}
