using MatricConnect.Data;
using MatricConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace MatricConnect.CatalogueImport;

internal sealed record ReferenceBatch(int Version, string? Province, string? ReviewStatus,
    string? ReviewedBy, DateOnly? VerifiedOn, ReferenceInstitution?[]? Institutions)
{
    public (List<ProgrammeReference> Additions, List<ProgrammeReference> Updates, int Unchanged) Preview(
        MatricConnectContext context, DateOnly today, bool refresh)
    {
        if (Version != 1 || !CatalogueEntry.IsLine(Province) || ReviewStatus != "reviewed" ||
            !CatalogueEntry.IsLine(ReviewedBy) || VerifiedOn is not DateOnly verified || verified > today ||
            verified.Year < 2008 || Institutions is not { Length: > 0 })
            throw new InvalidDataException("A reference batch needs version 1, a province, a reviewer, a verification date and institutions.");

        var additions = new List<ProgrammeReference>();
        var updates = new List<ProgrammeReference>();
        var unchanged = 0;
        var seenInstitutions = new HashSet<(string Kind, string Name)>();
        foreach (var institution in Institutions)
        {
            if (institution is null || !CatalogueEntry.IsLine(institution.Name) ||
                institution.Kind is not ("university" or "tvet") || institution.Entries is not { Length: > 0 } ||
                !seenInstitutions.Add((institution.Kind, institution.Name!.ToUpperInvariant())))
                throw new InvalidDataException("Each institution needs a unique kind and name, and at least one entry.");

            int? universityId = null, collegeId = null;
            string website;
            if (institution.Kind == "university")
            {
                var university = context.Universities.AsNoTracking()
                    .SingleOrDefault(item => item.Province == Province && item.Name == institution.Name)
                    ?? throw new InvalidDataException($"Unknown university in {Province}: {institution.Name}.");
                universityId = university.Id;
                website = university.Website;
            }
            else
            {
                var college = context.TvetColleges.AsNoTracking()
                    .SingleOrDefault(item => item.Province == Province && item.Name == institution.Name)
                    ?? throw new InvalidDataException($"Unknown TVET college in {Province}: {institution.Name}.");
                collegeId = college.Id;
                website = college.Website;
            }

            if (!Uri.TryCreate(website, UriKind.Absolute, out var official) || official.IdnHost.Length == 0)
                throw new InvalidDataException($"Invalid directory website for {institution.Name}.");
            var domain = official.IdnHost.StartsWith("www.") ? official.IdnHost[4..] : official.IdnHost;
            var previous = context.ProgrammeReferences.AsNoTracking().Where(item =>
                item.UniversityId == universityId && item.TvetCollegeId == collegeId).ToList();
            var programmeNames = universityId.HasValue
                ? context.Programmes.Where(item => item.UniversityId == universityId).Select(item => item.Name)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase)
                : new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var seenNames = new HashSet<(string Group, string Name)>();
            foreach (var entry in institution.Entries)
            {
                if (entry is null || !CatalogueEntry.IsLine(entry.Name) || !CatalogueEntry.IsLine(entry.Group) ||
                    !CatalogueEntry.IsLine(entry.Availability) ||
                    (entry.EntryRequirements != null && !entry.EntryRequirements.Split('\n').All(CatalogueEntry.IsLine)) ||
                    (entry.VerifiedOn is DateOnly entryDate && (entryDate < verified || entryDate > today)) ||
                    (entry.Campuses != null && !CatalogueEntry.IsLine(entry.Campuses)) || entry.IntakeYear is < 2008 or > 2100 ||
                    !CatalogueEntry.IsLine(entry.SourceUrl) || !Uri.TryCreate(entry.SourceUrl, UriKind.Absolute, out var source) ||
                    source.Scheme != Uri.UriSchemeHttps || source.UserInfo.Length != 0 || !source.IsDefaultPort ||
                    !IsOfficial(source, domain))
                    throw new InvalidDataException($"Invalid reference in {institution.Name}: {entry?.Name}. Check the title, group, and official HTTPS source.");

                if (!seenNames.Add((entry.Group!.ToUpperInvariant(), entry.Name!.ToUpperInvariant())) ||
                    programmeNames.Contains(entry.Name!))
                    throw new InvalidDataException($"Duplicate listing in {institution.Name}: {entry.Name}.");

                var existing = previous.SingleOrDefault(item =>
                    string.Equals(item.Group, entry.Group, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(item.Name, entry.Name, StringComparison.OrdinalIgnoreCase));

                if (entry.Comparison != null &&
                    (!entry.Comparison.IsValid() || string.IsNullOrEmpty(entry.EntryRequirements) ||
                     (entry.Comparison.ScoreGuideUrl is { } scoreGuide && !IsOfficial(new Uri(scoreGuide), domain))))
                    throw new InvalidDataException($"Invalid published comparison for {institution.Name}: {entry.Name}.");

                var comparison = entry.Comparison?.ToJson();
                var verifiedOn = entry.VerifiedOn ?? verified;
                if (existing != null)
                {
                    if (existing.Name == entry.Name && existing.Group == entry.Group && existing.Campuses == entry.Campuses &&
                        existing.EntryRequirements == entry.EntryRequirements && existing.Availability == entry.Availability &&
                        existing.IntakeYear == entry.IntakeYear && existing.SourceUrl == entry.SourceUrl &&
                        existing.VerifiedOn == verifiedOn && existing.ComparisonRulesJson == comparison)
                    {
                        unchanged++;
                        continue;
                    }

                    if (!refresh || existing.VerifiedOn > verifiedOn || existing.Name != entry.Name || existing.Group != entry.Group)
                        throw new InvalidDataException($"Conflicting existing reference in {institution.Name}: {entry.Name}. Use a current reviewed refresh for the same listing.");
                    existing.Campuses = entry.Campuses;
                    existing.EntryRequirements = entry.EntryRequirements;
                    existing.Availability = entry.Availability!;
                    existing.IntakeYear = entry.IntakeYear;
                    existing.SourceUrl = entry.SourceUrl!;
                    existing.VerifiedOn = verifiedOn;
                    existing.ComparisonRulesJson = comparison;
                    updates.Add(existing);
                    continue;
                }

                additions.Add(new ProgrammeReference
                {
                    UniversityId = universityId, TvetCollegeId = collegeId, Name = entry.Name!,
                    Group = entry.Group!, Campuses = entry.Campuses, EntryRequirements = entry.EntryRequirements,
                    Availability = entry.Availability!, IntakeYear = entry.IntakeYear, SourceUrl = entry.SourceUrl!, VerifiedOn = verifiedOn, ComparisonRulesJson = comparison
                });
            }
        }

        return (additions, updates, unchanged);
    }

    private static bool IsOfficial(Uri uri, string domain) =>
        uri.IdnHost.Equals(domain, StringComparison.OrdinalIgnoreCase) ||
        uri.IdnHost.EndsWith("." + domain, StringComparison.OrdinalIgnoreCase) ||
        // Taletso links its official college app from the directory website.
        (domain.Equals("taletso.edu.za", StringComparison.OrdinalIgnoreCase) &&
         uri.IdnHost.Equals("taletsotvetcollege.app", StringComparison.OrdinalIgnoreCase));
}

internal sealed record ReferenceInstitution(string? Kind, string? Name, ReferenceEntry?[]? Entries);
internal sealed record ReferenceEntry(string? Name, string? Group, string? Campuses, string? EntryRequirements,
    string? Availability, int? IntakeYear, string? SourceUrl, DateOnly? VerifiedOn, ReferenceComparisonRules? Comparison);
