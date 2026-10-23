// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Provides saved programme operations through
//                   the saved programme repository contract.

using MatricConnect.Core.Interfaces;
using MatricConnect.Models;

namespace MatricConnect.Services
{
    public class SavedProgrammeService
    {
        private readonly ISavedProgrammeRepository _savedProgrammeRepository;

        public SavedProgrammeService(
            ISavedProgrammeRepository savedProgrammeRepository)
        {
            //
            // Name              : SavedProgrammeService(
            //                     ISavedProgrammeRepository savedProgrammeRepository)
            // Purpose           : Initializes the service with a repository
            //                     supplied by the caller.
            // Re-use            : None
            // Method Parameters : ISavedProgrammeRepository savedProgrammeRepository
            //                     - repository used to manage saved programmes
            // Output Type       : None
            //

            _savedProgrammeRepository = savedProgrammeRepository;
        } // end constructor

        public bool SaveProgramme(int studentId, int programmeId)
        {
            //
            // Name              : bool SaveProgramme(
            //                     int studentId, int programmeId)
            // Purpose           : Saves a programme unless already saved.
            // Re-use            : ISavedProgrammeRepository.SaveProgramme
            // Method Parameters : int studentId
            //                     - ID of the student saving the programme
            //                     int programmeId
            //                     - ID of the programme to save
            // Output Type       : bool
            //                     - true when saved
            //                     - false when already saved
            //

            return _savedProgrammeRepository
                .SaveProgramme(studentId, programmeId);
        } // end method SaveProgramme

        public List<SavedProgramme> GetSavedProgrammesByStudent(
            int studentId)
        {
            //
            // Name              : List<SavedProgramme>
            //                     GetSavedProgrammesByStudent(int studentId)
            // Purpose           : Retrieves a student's saved programmes,
            //                     including programme and university details.
            // Re-use            : ISavedProgrammeRepository.GetSavedProgrammesByStudent
            // Method Parameters : int studentId
            //                     - ID of the student whose saves to retrieve
            // Output Type       : List<SavedProgramme>
            //                     - saved programmes belonging to the student
            //

            return _savedProgrammeRepository
                .GetSavedProgrammesByStudent(studentId);
        } // end method GetSavedProgrammesByStudent

        public bool RemoveSavedProgramme(int studentId, int programmeId)
        {
            //
            // Name              : bool RemoveSavedProgramme(
            //                     int studentId, int programmeId)
            // Purpose           : Removes a programme saved by a student.
            // Re-use            : ISavedProgrammeRepository.RemoveSavedProgramme
            // Method Parameters : int studentId
            //                     - ID of the student
            //                     int programmeId
            //                     - ID of the programme to remove
            // Output Type       : bool
            //                     - true when removed
            //                     - false when no matching save exists
            //

            return _savedProgrammeRepository
                .RemoveSavedProgramme(studentId, programmeId);
        } // end method RemoveSavedProgramme
    } // end class SavedProgrammeService
} // end namespace MatricConnect.Services