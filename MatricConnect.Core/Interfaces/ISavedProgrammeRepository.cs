// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Defines operations for saving, retrieving,
//                   and removing a student's saved programmes.

using MatricConnect.Models;

namespace MatricConnect.Core.Interfaces
{
    public interface ISavedProgrammeRepository
    {
        //
        // Name              : SaveProgramme
        // Purpose           : Saves a programme unless already saved.
        // Re-use            : None
        // Method Parameters : int studentId - student's ID
        //                     int programmeId - programme's ID
        // Output Type       : bool - true when saved; false if already saved
        //
        bool SaveProgramme(int studentId, int programmeId);

        //
        // Name              : GetSavedProgrammesByStudent
        // Purpose           : Retrieves a student's saved programmes.
        // Re-use            : None
        // Method Parameters : int studentId - student's ID
        // Output Type       : List<SavedProgramme>
        //                     - saves including programme and university details
        //
        List<SavedProgramme> GetSavedProgrammesByStudent(int studentId);

        //
        // Name              : RemoveSavedProgramme
        // Purpose           : Removes a programme saved by a student.
        // Re-use            : None
        // Method Parameters : int studentId - student's ID
        //                     int programmeId - programme's ID
        // Output Type       : bool - true when removed; false if not found
        //
        bool RemoveSavedProgramme(int studentId, int programmeId);
    } // end interface ISavedProgrammeRepository
} // end namespace MatricConnect.Core.Interfaces