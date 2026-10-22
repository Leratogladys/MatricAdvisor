// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Manages programmes saved by students, including
//                   saving, retrieving, and removing saved programmes.

using MatricConnect.Data;
using MatricConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace MatricConnect.Services
{
    public class SavedProgrammeService
    {
        private readonly MatricConnectContext _context;

        public SavedProgrammeService(MatricConnectContext context)
        {
            //
            // Name              : SavedProgrammeService(
            //                     MatricConnectContext context)
            // Purpose           : Initializes the saved programme service
            //                     with access to the database context.
            // Re-use            : None
            // Method Parameters : MatricConnectContext context
            //                     - database context used to access
            //                       saved programme data
            // Output Type       : None
            //

            _context = context;
        } // end method

        public bool SaveProgramme(
            int studentId,
            int programmeId)
        {
            //
            // Name              : bool SaveProgramme(
            //                     int studentId, int programmeId)
            // Purpose           : Saves a programme for a student when
            //                     the programme has not already been saved.
            // Re-use            : None
            // Method Parameters : int studentId
            //                     - ID of the student saving the programme
            //                     int programmeId
            //                     - ID of the programme to save
            // Output Type       : bool
            //                     - true when the programme is saved
            //                     - false when it is already saved
            //

            bool alreadySaved = _context.SavedProgrammes
                .Any(sp =>
                    sp.StudentId == studentId &&
                    sp.ProgrammeId == programmeId);

            if (alreadySaved)
            {
                return false;
            } // end if

            var savedProgramme = new SavedProgramme
            {
                StudentId = studentId,
                ProgrammeId = programmeId
            };

            _context.SavedProgrammes.Add(savedProgramme);
            _context.SaveChanges();

            return true;
        } // end method

        public List<SavedProgramme> GetSavedProgrammesByStudent(
            int studentId)
        {
            //
            // Name              : List<SavedProgramme>
            //                     GetSavedProgrammesByStudent(int studentId)
            // Purpose           : Retrieves all programmes saved by a
            //                     specific student.
            // Re-use            : None
            // Method Parameters : int studentId
            //                     - ID of the student whose saved programmes
            //                       must be retrieved
            // Output Type       : List<SavedProgramme>
            //                     - saved programmes belonging to the student
            //

            return _context.SavedProgrammes
                .Where(sp => sp.StudentId == studentId)
                .Include(sp => sp.Programme)
                .ThenInclude(p => p.University)
                .ToList();
        } // end method

        public bool RemoveSavedProgramme(
            int studentId,
            int programmeId)
        {
            //
            // Name              : bool RemoveSavedProgramme(
            //                     int studentId, int programmeId)
            // Purpose           : Removes a saved programme belonging
            //                     to a specific student.
            // Re-use            : None
            // Method Parameters : int studentId
            //                     - ID of the student
            //                     int programmeId
            //                     - ID of the programme to remove
            // Output Type       : bool
            //                     - true when the saved programme is removed
            //                     - false when no matching record is found
            //

            var savedProgramme = _context.SavedProgrammes
                .FirstOrDefault(sp =>
                    sp.StudentId == studentId &&
                    sp.ProgrammeId == programmeId);

            if (savedProgramme == null)
            {
                return false;
            } // end if

            _context.SavedProgrammes.Remove(savedProgramme);
            _context.SaveChanges();

            return true;
        } // end method
    } // end class SavedProgrammeService
} // end namespace MatricConnect.Services