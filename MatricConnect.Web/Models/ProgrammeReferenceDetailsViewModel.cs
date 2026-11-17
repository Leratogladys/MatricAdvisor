using MatricConnect.Models;

namespace MatricConnect.Web.Models;

public sealed record ProgrammeReferenceDetailsViewModel(
    ProgrammeReference Reference, ProgrammeSaveOutcome? SaveOutcome,
    ReferenceComparisonRules? Rules, ReferenceComparisonSubmission? Submission = null);

public sealed record ReferenceComparisonSubmission(int Route, decimal?[] Values, bool Compared)
{
    public decimal? ValueAt(int index) => index < Values.Length ? Values[index] : null;
}
