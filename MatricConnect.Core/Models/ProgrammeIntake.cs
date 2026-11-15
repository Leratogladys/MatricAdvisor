// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Stores verified requirements and dates for a programme intake.

namespace MatricConnect.Models
{
    public class ProgrammeIntake
    {
        public int ProgrammeId { get; set; }
        public int IntakeYear { get; set; }
        public string OfficialProgrammeCode { get; set; } = string.Empty;
        public DateOnly? ApplicationDeadline { get; set; }
        public TimeOnly? ApplicationClosingTime { get; set; }
        public string AdmissionScoreLabel { get; set; } = "APS";
        public int RequiredAps { get; set; }
        public decimal RequiredMathematics { get; set; }
        public decimal? RequiredEnglish { get; set; }
        public decimal? RequiredPhysicalScience { get; set; }
        public string AdditionalRequirements { get; set; } = string.Empty;
        public string RequirementsSourceUrl { get; set; } = string.Empty;
        public string ApplicationDatesSourceUrl { get; set; } = string.Empty;
        public string ApsGuideUrl { get; set; } = string.Empty;
        public DateOnly VerifiedOn { get; set; }

        public bool HasClosed(DateTimeOffset now)
        {
            if (ApplicationDeadline is not DateOnly deadline)
            {
                return false;
            } // end if

            var localNow = now.ToOffset(TimeSpan.FromHours(2)).DateTime; // South African Standard Time
            var localDate = DateOnly.FromDateTime(localNow);

            return localDate > deadline || (localDate == deadline &&
                ApplicationClosingTime is TimeOnly time && TimeOnly.FromDateTime(localNow) >= time);
        } // end method HasClosed
    } // end class ProgrammeIntake
} // end namespace MatricConnect.Models
