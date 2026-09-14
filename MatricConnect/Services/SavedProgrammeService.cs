using MatricConnect.Data;
using MatricConnect.Models;

namespace MatricConnect.Services;

public class SavedProgrammeService(MatricConnectContext context)
{
    public SavedProgramme SaveProgramme(
        int studentId,
        int programmeId)
    {
        var savedProgramme = new SavedProgramme
        {
            StudentId = studentId,
            ProgrammeId = programmeId
        };

        context.SavedProgrammes.Add(savedProgramme);
        context.SaveChanges();

        return savedProgramme;
    }
}