// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Records published 2027 intake facts for the UFS, UJ and UP catalogue entries.

using MatricConnect.Models;

namespace MatricConnect.Data
{
    internal static class VerifiedIntakeSeed
    {
        private const string UfsProspectus =
            "https://www.ufs.ac.za/docs/librariesprovider44/undergraduate/ufs---ug-prospectus-2027---jan.pdf";
        private const string UjProspectus =
            "https://www.uj.ac.za/wp-content/uploads/2026/06/uj_undergrad_prospectus2027.pdf";
        private const string UpRequirements =
            "https://drupalwebprod-files.up.ac.za/Public/2026-03/UP_DESA_Application%20requirements%20tables_2027_web.pdf?VersionId=vw30gCBk2BMAXzOhFSBflmkFOHOMFOoa";
        private const string UpProspectus =
            "https://drupalwebprod-files.up.ac.za/Public/2026-01/UP_UG%20Prospectus%202027_NSC-IEB_DevV5_web_0.pdf?VersionId=mFTqP2HthIDn48ISjWf4b4g6CtdE6beU";
        private const string UpDates = "https://www.up.ac.za/online-application/important-dates";

        private sealed record ProgrammeFacts(string Name, string Code, int Aps, decimal Mathematics,
            decimal English, decimal? PhysicalScience = null, string Notes = "");

        public static void Add2027Intakes(MatricConnectContext context, int ufsId, int ujId, int upId)
        {
            AddIntakes(context, ufsId, "UFS AP", new DateOnly(2026, 9, 30), null, UfsProspectus,
                "https://www.ufs.ac.za/natagri/", UfsProspectus,
                new("BSc (IT) majoring in Computer Science and Mathematics", "B4361", 32, 70, 50, 60,
                    "The academic plan code for the Computer Science and Mathematics combination is BC432238."),
                new("Bachelor of Computer Information Systems", "B4363", 33, 50, 50,
                    Notes: "The academic plan code is BC430156. The prospectus does not list a Physical Sciences minimum."));

            const string ujNotes =
                "Auckland Park Kingsway campus. The Faculty of Science does not accept Technical Mathematics or Technical Science for these degrees.";
            AddIntakes(context, ujId, "UJ APS", new DateOnly(2026, 10, 31), new TimeOnly(12, 0), UjProspectus,
                "https://www.uj.ac.za/admission-aid/undergraduate/", UjProspectus,
                new("BSc Information Technology", "B2I01Q", 30, 70, 60, Notes: ujNotes),
                new("BSc Computer Science and Informatics", "B2I02Q", 30, 70, 60, Notes: ujNotes),
                new("BSc Computer Science and Informatics Specialising in AI", "B2I04Q", 34, 80, 60, Notes: ujNotes));

            const string upNotes =
                "The English minimum can be met with English Home Language or English First Additional Language. UP excludes Life Orientation from its APS calculation.";
            AddIntakes(context, upId, "UP APS", new DateOnly(2026, 6, 30), null, UpRequirements,
                UpDates, UpProspectus,
                new("BSc Computer Science", "12134002", 30, 70, 60, Notes: upNotes),
                new("BSc Information Technology in Information and Knowledge Systems", "12133214", 30, 70, 50,
                    Notes: upNotes));

            context.SaveChanges();
        } // end method Add2027Intakes

        private static void AddIntakes(MatricConnectContext context, int universityId, string scoreLabel,
            DateOnly deadline, TimeOnly? closingTime, string requirementsUrl, string datesUrl,
            string scoreGuideUrl, params ProgrammeFacts[] programmes)
        {
            const int intakeYear = 2027;
            var names = programmes.Select(programme => programme.Name).ToArray();
            var programmesByName = context.Programmes
                .Where(programme => programme.UniversityId == universityId && names.Contains(programme.Name))
                .Select(programme => new { programme.Id, programme.Name })
                .ToDictionary(programme => programme.Name, programme => programme.Id);

            foreach (var facts in programmes)
            {
                // Keep the existing programme IDs and leave already recorded intakes in place.
                if (!programmesByName.TryGetValue(facts.Name, out var programmeId) ||
                    context.ProgrammeIntakes.Find(programmeId, intakeYear) is not null)
                {
                    continue;
                } // end if

                context.ProgrammeIntakes.Add(new ProgrammeIntake
                {
                    ProgrammeId = programmeId,
                    IntakeYear = intakeYear,
                    OfficialProgrammeCode = facts.Code,
                    ApplicationDeadline = deadline,
                    ApplicationClosingTime = closingTime,
                    AdmissionScoreLabel = scoreLabel,
                    RequiredAps = facts.Aps,
                    RequiredMathematics = facts.Mathematics,
                    RequiredEnglish = facts.English,
                    RequiredPhysicalScience = facts.PhysicalScience,
                    AdditionalRequirements = facts.Notes,
                    RequirementsSourceUrl = requirementsUrl,
                    ApplicationDatesSourceUrl = datesUrl,
                    ApsGuideUrl = scoreGuideUrl,
                    VerifiedOn = new DateOnly(2026, 11, 15)
                });
            } // end foreach
        } // end method AddIntakes
    } // end class VerifiedIntakeSeed
} // end namespace MatricConnect.Data
