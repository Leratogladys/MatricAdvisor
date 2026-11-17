namespace MatricConnect.Models;

public class SavedProgrammeReference
{
    public int StudentId { get; set; }
    public int ProgrammeReferenceId { get; set; }
    public ProgrammeReference Reference { get; set; } = null!;
}
