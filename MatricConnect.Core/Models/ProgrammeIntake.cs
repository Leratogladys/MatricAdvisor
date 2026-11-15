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
        public int RequiredAps { get; set; }
        public decimal RequiredMathematics { get; set; }
        public string AdditionalRequirements { get; set; } = string.Empty;
        public string RequirementsSourceUrl { get; set; } = string.Empty;
        public string ApplicationDatesSourceUrl { get; set; } = string.Empty;
        public string ApsGuideUrl { get; set; } = string.Empty;
        public DateOnly VerifiedOn { get; set; }
    } // end class ProgrammeIntake
} // end namespace MatricConnect.Models
