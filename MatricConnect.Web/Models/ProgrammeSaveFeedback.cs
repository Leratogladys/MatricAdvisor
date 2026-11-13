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
    }
}
