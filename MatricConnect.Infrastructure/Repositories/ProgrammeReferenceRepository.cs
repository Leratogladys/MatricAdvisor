using MatricConnect.Data;
using MatricConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace MatricConnect.Infrastructure.Repositories;

public class ProgrammeReferenceRepository(MatricConnectContext context)
{
    public List<ProgrammeReference> ForUniversity(int id) =>
        context.ProgrammeReferences.AsNoTracking().Where(item => item.UniversityId == id).OrderBy(item => item.Name).ToList();

    public List<ProgrammeReference> ForCollege(int id) =>
        context.ProgrammeReferences.AsNoTracking().Where(item => item.TvetCollegeId == id).OrderBy(item => item.Name).ToList();

    public ProgrammeReference? Find(int id) =>
        context.ProgrammeReferences.AsNoTracking().Include(item => item.University)
            .Include(item => item.TvetCollege).SingleOrDefault(item => item.Id == id);

    public List<SavedProgrammeReference> ForStudent(int studentId) =>
        context.SavedProgrammeReferences.AsNoTracking().Where(item => item.StudentId == studentId)
            .Include(item => item.Reference).ThenInclude(item => item.University)
            .Include(item => item.Reference).ThenInclude(item => item.TvetCollege).ToList();

    public bool Save(int studentId, int referenceId)
    {
        if (context.SavedProgrammeReferences.Any(item =>
            item.StudentId == studentId && item.ProgrammeReferenceId == referenceId))
            return false;

        context.SavedProgrammeReferences.Add(new SavedProgrammeReference
        {
            StudentId = studentId,
            ProgrammeReferenceId = referenceId
        });
        context.SaveChanges();
        return true;
    }

    public bool Remove(int studentId, int referenceId) =>
        context.SavedProgrammeReferences.Where(item =>
            item.StudentId == studentId && item.ProgrammeReferenceId == referenceId).ExecuteDelete() > 0;

    public Dictionary<int, int> CountByCollege(string province) =>
        (from college in context.TvetColleges
         where college.Province == province
         select new { college.Id, Count = college.ProgrammeReferences.Count })
        .ToDictionary(college => college.Id, college => college.Count);
}
