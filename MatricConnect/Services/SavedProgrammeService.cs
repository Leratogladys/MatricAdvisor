using MatricConnect.Data;
using MatricConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace MatricConnect.Services;

public class SavedProgrammeService(MatricConnectContext context)
{
    public bool SaveProgramme(
        int studentId,
        int programmeId)
    {
        bool alreadySaved = context.SavedProgrammes
            .Any(sp =>
                sp.StudentId == studentId &&
                sp.ProgrammeId == programmeId);

        if (alreadySaved)
        {
            return false;
        }

        var savedProgramme = new SavedProgramme
        {
            StudentId = studentId,
            ProgrammeId = programmeId
        };

        context.SavedProgrammes.Add(savedProgramme);
        context.SaveChanges();

        return true;
    }

    public List<SavedProgramme> GetSavedProgrammesByStudent(
        int studentId)
    {
        return context.SavedProgrammes
            .Where(sp => sp.StudentId == studentId)
            .Include(sp => sp.Programme)
            .ThenInclude(p => p.University)
            .ToList();
    }

    public bool RemoveSavedProgramme(
        int studentId,
        int programmeId)
    {
        var savedProgramme = context.SavedProgrammes
            .FirstOrDefault(sp =>
                sp.StudentId == studentId &&
                sp.ProgrammeId == programmeId);

        if (savedProgramme == null)
        {
            return false;
        }

        context.SavedProgrammes.Remove(savedProgramme);
        context.SaveChanges();

        return true;
    }
}