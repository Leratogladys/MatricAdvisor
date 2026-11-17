using MatricConnect.Data;
using MatricConnect.Models;
using MatricConnect.Services;
using Microsoft.EntityFrameworkCore;

namespace MatricConnect.CatalogueImport;

internal sealed class CatalogueImportPlan
{
    public List<Programme> Additions { get; } = [];
    public List<CatalogueEntry> Unchanged { get; } = [];
    public List<string> Errors { get; } = [];

    public static CatalogueImportPlan Build(MatricConnectContext context, CatalogueBatch batch)
    {
        var plan = new CatalogueImportPlan();
        var universities = context.Universities.AsNoTracking()
            .Where(university => university.Name == batch.University).Take(2).ToList();
        if (universities.Count != 1 ||
            !Uri.TryCreate(universities[0].Website, UriKind.Absolute, out var website) || website.IdnHost.Length == 0)
        {
            plan.Errors.Add("Match exactly one university from the directory with an official website.");
            return plan;
        }

        var university = universities[0];
        var officialDomain = website.IdnHost.StartsWith("www.", StringComparison.OrdinalIgnoreCase)
            ? website.IdnHost[4..] : website.IdnHost;
        var programmes = context.Programmes.AsNoTracking().Include(programme => programme.Intakes)
            .Where(programme => programme.UniversityId == university.Id).ToList();
        var byName = programmes.ToLookup(programme => programme.Name, StringComparer.OrdinalIgnoreCase);
        var byCode = programmes.SelectMany(programme => programme.Intakes
                .Select(intake => (intake.OfficialProgrammeCode, Programme: programme)))
            .ToLookup(pair => pair.OfficialProgrammeCode, pair => pair.Programme, StringComparer.OrdinalIgnoreCase);

        var referenceNames = context.ProgrammeReferences.Where(item => item.UniversityId == university.Id)
            .Select(item => item.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var entry in batch.Entries!)
        {
            var row = entry!;
            var sourceErrors = row.ValidateSources(officialDomain).ToList();
            if (sourceErrors.Count != 0)
            {
                plan.Errors.AddRange(sourceErrors.Select(error => $"{row.Code}: {error}"));
                continue;
            }

            var incomingIntake = row.CreateIntake(batch);
            var matches = byName[row.Name!].Concat(byCode[row.Code!]).DistinctBy(programme => programme.Id).ToArray();
            if (matches.Length == 1 && matches[0].Name == row.Name && matches[0].FieldOfStudy == row.FieldOfStudy &&
                (row.Faculty == null || matches[0].Faculty == row.Faculty) &&
                matches[0].Intakes.SingleOrDefault(intake => intake.IntakeYear == batch.IntakeYear) is { } existing &&
                SameIntake(existing, incomingIntake))
            {
                plan.Unchanged.Add(row);
                continue;
            }

            if (matches.Length > 0)
            {
                plan.Errors.Add($"{row.Code}: code or name matches an existing programme with different data. This importer cannot update existing programmes or add an intake to them.");
                continue;
            }

            if (referenceNames.Contains(row.Name!))
            {
                plan.Errors.Add($"{row.Code}: already listed as a reference. Review the promotion separately.");
                continue;
            }

            if (ProgrammeComparisonRules.IsConfigured(university, incomingIntake))
            {
                plan.Errors.Add($"{row.Code}: a comparison already exists for this code and intake. Add this programme through a dedicated review.");
                continue;
            }

            plan.Additions.Add(new Programme
            {
                Name = row.Name!,
                FieldOfStudy = row.FieldOfStudy!,
                Faculty = row.Faculty,
                UniversityId = university.Id,
                Intakes = [incomingIntake]
            });
        }

        return plan;
    }

    private static bool SameIntake(ProgrammeIntake existing, ProgrammeIntake incoming) =>
        existing.OfficialProgrammeCode == incoming.OfficialProgrammeCode &&
        existing.AdmissionScoreLabel == incoming.AdmissionScoreLabel &&
        existing.RequiredAps == incoming.RequiredAps &&
        existing.AdmissionScoreBySubject == incoming.AdmissionScoreBySubject &&
        existing.RequiredMathematics == incoming.RequiredMathematics &&
        existing.RequiredEnglish == incoming.RequiredEnglish &&
        existing.RequiredPhysicalScience == incoming.RequiredPhysicalScience &&
        existing.ApplicationDeadline == incoming.ApplicationDeadline &&
        existing.ApplicationClosingTime == incoming.ApplicationClosingTime &&
        existing.NextApplicationCycleNote == incoming.NextApplicationCycleNote &&
        existing.RequirementsSourceUrl == incoming.RequirementsSourceUrl &&
        existing.ApplicationDatesSourceUrl == incoming.ApplicationDatesSourceUrl &&
        existing.ApsGuideUrl == incoming.ApsGuideUrl &&
        existing.AdditionalRequirements.Replace("\r\n", "\n") == incoming.AdditionalRequirements &&
        existing.VerifiedOn == incoming.VerifiedOn;
}
