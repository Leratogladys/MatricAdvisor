namespace MatricConnect.Web.Models
{
    public enum ProgrammeSaveOutcome
    {
        Saved,
        AlreadySaved,
        Failed
    }

    public static class ProgrammeSaveFeedback
    {
        public static string Key(int studentId, int programmeId) => $"ProgrammeSave:{studentId}:{programmeId}";

        public static string ReferenceKey(int studentId, int referenceId) => $"ProgrammeReferenceSave:{studentId}:{referenceId}";
    }
}
