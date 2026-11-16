// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Adds CUT's published entry-level IT programmes and admissions information.

using MatricConnect.Models;
using MatricConnect.Services;
using Microsoft.EntityFrameworkCore;

namespace MatricConnect.Data
{
    internal static class CutIntakeSeed
    {
        private const string UniversityName = CutInformationTechnologyComparison.UniversityName;
        private const string ApplicationDatesUrl = "https://www.cut.ac.za/application-process";
        private const string ScoreGuideUrl = "https://www.cut.ac.za/admission-points-ap";
        private const string NextCycle =
            "CUT lists its general 2028 undergraduate application window as April 2027 to 30 September 2027. " +
            "Confirm programme availability and requirements for 2028 with CUT.";
        private const string Nbt =
            "First-year applicants must write the Academic and Quantitative Literacy and Mathematics NBTs before registration.";
        private static readonly string DiplomaSubjects =
            $"Meet one of these subject marks: {CutInformationTechnologyComparison.MathematicsOrComputerScienceMark}% in Mathematics, " +
            $"{CutInformationTechnologyComparison.MathematicsOrComputerScienceMark}% in Computer Science, " +
            $"or {CutInformationTechnologyComparison.MathematicalLiteracyMark}% in Mathematical Literacy.";
        private const string DiplomaEnglish =
            "CUT generally expects at least 50% in English Home Language or English First Additional Language.";
        private const string DiplomaSelection =
            "Admission is subject to selection. First-time applicants with 22–26 CUT APS points may be considered for the Extended Curriculum Programme.";

        private sealed record ProgrammeFacts(string Name, string Field, string Code, string SourcePage,
            string Requirements);

        private static readonly ProgrammeFacts[] Programmes =
        [
            new("Higher Certificate in Information Technology", "Information Technology", "HC_ITC",
                "hcert-information-technology",
                "Offered at Bloemfontein and Welkom.\n" +
                "NSC with a Higher Certificate pass or higher, plus English proficiency at NQF level 4.\n" +
                "Applicants with 22–26 CUT APS points must complete a selection process, which may include a test.\n" + Nbt),
            new("Diploma in Computer Networking", "Computer Networking", "DP_CMN",
                "dip-computer-networking",
                "Offered at Bloemfontein.\nNSC with a Diploma pass or higher.\n" +
                DiplomaSubjects + "\n" + DiplomaEnglish + "\n" + DiplomaSelection + "\n" + Nbt),
            new("Diploma in Information Technology", "Information Technology", CutInformationTechnologyComparison.ProgrammeCode,
                "dp-information-technology",
                "Offered at Bloemfontein and Welkom.\nNSC with a Diploma pass or higher.\n" +
                DiplomaSubjects + "\n" + DiplomaEnglish + "\n" + DiplomaSelection + "\n" + Nbt)
        ];

        public static void AddPublishedIntakes(MatricConnectContext context)
        {
            const int intakeYear = CutInformationTechnologyComparison.IntakeYear;
            const int requiredAps = CutInformationTechnologyComparison.MinimumScore;
            var applicationDeadline = new DateOnly(2026, 9, 30);
            var universityId = context.Universities.Single(university => university.Name == UniversityName).Id;
            var names = Programmes.Select(facts => facts.Name).ToArray();
            var programmesByName = context.Programmes.Include(programme => programme.Intakes)
                .Where(programme => programme.UniversityId == universityId && names.Contains(programme.Name))
                .ToDictionary(programme => programme.Name);

            foreach (var facts in Programmes)
            {
                if (!programmesByName.TryGetValue(facts.Name, out var programme))
                {
                    programme = new Programme
                    {
                        Name = facts.Name,
                        FieldOfStudy = facts.Field,
                        UniversityId = universityId,
                        RequiredAPS = requiredAps,
                        ApplicationDeadline = applicationDeadline.ToDateTime(TimeOnly.MinValue)
                    };
                    context.Programmes.Add(programme);
                }

                if (programme.Intakes.Any(intake => intake.IntakeYear == intakeYear))
                {
                    continue;
                }

                // The sourced intake is added with the programme. The legacy numeric comparison
                // is not used for these diplomas' alternative subjects or the certificate.
                programme.Intakes.Add(new ProgrammeIntake
                {
                    IntakeYear = intakeYear,
                    OfficialProgrammeCode = facts.Code,
                    ApplicationDeadline = applicationDeadline,
                    NextApplicationCycleNote = NextCycle,
                    AdmissionScoreLabel = "CUT APS",
                    RequiredAps = requiredAps,
                    AdditionalRequirements = facts.Requirements,
                    RequirementsSourceUrl = $"https://www.cut.ac.za/programmes/{facts.SourcePage}",
                    ApplicationDatesSourceUrl = ApplicationDatesUrl,
                    ApsGuideUrl = ScoreGuideUrl,
                    VerifiedOn = new DateOnly(2026, 11, 15)
                });
            }

            context.SaveChanges();
        }
    }
}
