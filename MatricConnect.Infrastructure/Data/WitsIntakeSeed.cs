// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Adds source-verified Wits intakes to the existing catalogue.

using MatricConnect.Models;

namespace MatricConnect.Data
{
    internal static class WitsIntakeSeed
    {
        public static void AddVerifiedIntakes(MatricConnectContext context, int universityId)
        {
            const int intakeYear = 2027;
            const string computerScience = "BSc Computer Science";
            const string appliedMathematics = "BSc Computational and Applied Mathematics";
            var programmes = context.Programmes
                .Where(programme => programme.UniversityId == universityId &&
                    (programme.Name == computerScience || programme.Name == appliedMathematics))
                .ToList();

            foreach (var programme in programmes)
            {
                // An intake is linked to the existing programme, so saved programme IDs stay valid.
                if (context.ProgrammeIntakes.Find(programme.Id, intakeYear) is not null)
                {
                    continue;
                } // end if

                var isComputerScience = programme.Name == computerScience;
                var coursePath = isComputerScience ? "computer-science" : "computational-and-applied-mathematics";
                context.ProgrammeIntakes.Add(new ProgrammeIntake
                {
                    ProgrammeId = programme.Id,
                    IntakeYear = intakeYear,
                    OfficialProgrammeCode = isComputerScience ? "SBA13" : "SBA21",
                    ApplicationDeadline = new DateOnly(2026, 9, 30),
                    AdmissionScoreLabel = "Wits APS",
                    RequiredAps = 44,
                    RequiredMathematics = 70,
                    AdditionalRequirements = """
                        NSC with a bachelor’s pass.
                        English Home Language or First Additional Language: Level 5 (60% or higher).
                        National Benchmark Tests (NBT): the Academic and Quantitative Literacy and Mathematics tests had to be taken in one session by 31 October 2026.
                        Applicants with a Wits APS of 41–43 could be waitlisted if places were available.
                        """,
                    RequirementsSourceUrl = $"https://www.wits.ac.za/course-finder/undergraduate/science/{coursePath}/",
                    ApplicationDatesSourceUrl = "https://www.wits.ac.za/undergraduate/apply-to-wits/",
                    ApsGuideUrl = "https://www.wits.ac.za/undergraduate/entry-requirements/",
                    VerifiedOn = new DateOnly(2026, 11, 15)
                });
            } // end foreach

            context.SaveChanges();
        } // end method AddVerifiedIntakes
    } // end class WitsIntakeSeed
} // end namespace MatricConnect.Data
