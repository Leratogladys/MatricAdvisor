// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Adds verified 2027 computing qualifications for Nelson Mandela University.

using MatricConnect.Models;
using static MatricConnect.Services.MandelaSoftwareDevelopmentComparison;
using Microsoft.EntityFrameworkCore;

namespace MatricConnect.Data
{
    internal static class MandelaIntakeSeed
    {
        private const string EbetProspectus =
            "https://www.mandela.ac.za/getmedia/8d278dd0-b607-4ac3-a7d1-8450ba54a8e2/Prospectus-Faculty-of-EBET?disposition=attachment";
        private const string ScienceProspectus =
            "https://www.mandela.ac.za/getmedia/0bd96b8d-b5cc-4c60-a6a9-d596fa4396d6/Prospectus-Faculty-of-Science?disposition=attachment";
        private const string ApplicationDatesUrl =
            "https://www.mandela.ac.za/getmedia/a2444ae4-f32e-458e-ad9a-ebfa6c7864e7/01guidelines?disposition=attachment";
        private const string ScoreGuideUrl =
            "https://www.mandela.ac.za/getmedia/29267715-7b08-41cc-80ed-e23dde7772ec/NMU-Z-CARD-UNDERGRAD-PROGRAMME_DIGITAL-FA?disposition=attachment";
        private static readonly string DiplomaRequirements =
            "North Campus, Gqeberha.\nNSC with a Diploma pass or higher.\n" +
            $"Mathematics or Technical Mathematics: at least {MathematicsMark}%. Mathematical Literacy: at least {LiteracyMark}%.";
        private static readonly string DiplomaScore =
            $"{MathematicsScore} or higher with Mathematics or Technical Mathematics; {LiteracyScore} or higher with Mathematical Literacy.";

        private sealed record ProgrammeFacts(string Name, string Field, string Code, int Score, string Requirements,
            string Source = EbetProspectus, string? ScoreBySubject = null, decimal? Mathematics = null);

        private static readonly ProgrammeFacts[] Programmes =
        [
            new("Bachelor of Information Technology", "Information Technology", "72001", 370,
                "North Campus, Gqeberha, is listed in the faculty prospectus. Confirm campus availability with the university.\n" +
                "NSC with a bachelor's pass.\nMathematics or Technical Mathematics: at least 50%. Mathematical Literacy is not listed for this degree."),
            new("BSc (Computer Science)", "Computer Science", "20053", 410,
                "South Campus, Gqeberha.\nNSC with a bachelor's pass.",
                Source: ScienceProspectus, Mathematics: 65),
            new("Diploma in Information Technology (Software Development)", "Software Development", ProgrammeCode, MathematicsScore,
                DiplomaRequirements, ScoreBySubject: DiplomaScore),
            new("Diploma in Information Technology (Support Services)", "Information Technology", "7228", MathematicsScore,
                DiplomaRequirements, ScoreBySubject: DiplomaScore),
            new("Higher Certificate in Information Technology (User Support Services)", "Information Technology", "70003", 290,
                "North Campus, Gqeberha, and George Campus.\nNSC with a Higher Certificate pass or higher.\n" +
                "Mathematics or Technical Mathematics: at least 35%. Mathematical Literacy: at least 55%.",
                ScoreBySubject: "290 or higher with Mathematics or Technical Mathematics; 305 or higher with Mathematical Literacy.")
        ];

        public static void AddPublishedIntakes(MatricConnectContext context)
        {
            var deadline = new DateOnly(2026, 9, 30);
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
                        // Mandela's Applicant Score is not an APS; store it only on the verified intake.
                        ApplicationDeadline = deadline.ToDateTime(TimeOnly.MinValue)
                    };
                    context.Programmes.Add(programme);
                }

                if (programme.Intakes.Any(intake => intake.IntakeYear == IntakeYear))
                {
                    continue;
                }

                programme.Intakes.Add(new ProgrammeIntake
                {
                    IntakeYear = IntakeYear,
                    OfficialProgrammeCode = facts.Code,
                    ApplicationDeadline = deadline,
                    AdmissionScoreLabel = "Mandela AS",
                    RequiredAps = facts.Score,
                    AdmissionScoreBySubject = facts.ScoreBySubject,
                    RequiredMathematics = facts.Mathematics,
                    AdditionalRequirements = facts.Requirements,
                    RequirementsSourceUrl = facts.Source,
                    ApplicationDatesSourceUrl = ApplicationDatesUrl,
                    ApsGuideUrl = ScoreGuideUrl,
                    VerifiedOn = new DateOnly(2026, 11, 15)
                });
            }

            context.SaveChanges();
        }
    }
}
