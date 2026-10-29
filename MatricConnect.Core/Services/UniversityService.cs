// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Provides university and programme browsing
//                   through the university repository contract.

using MatricConnect.Core.Interfaces;
using MatricConnect.Models;

namespace MatricConnect.Services
{
    public class UniversityService
    {
        private readonly IUniversityRepository _universityRepository;

        public UniversityService(
            IUniversityRepository universityRepository)
        {
            //
            // Name              : UniversityService(
            //                     IUniversityRepository universityRepository)
            // Purpose           : Initializes the service with a university
            //                     repository supplied by the caller.
            // Re-use            : None
            // Method Parameters : IUniversityRepository universityRepository
            //                     - repository used to retrieve university
            //                       and programme data
            // Output Type       : None
            //

            _universityRepository = universityRepository;
        } // end constructor

        public List<string> GetProvince()
        {
            //
            // Name              : List<string> GetProvince()
            // Purpose           : Retrieves provinces containing universities.
            // Re-use            : IUniversityRepository.GetProvince
            // Method Parameters : None
            // Output Type       : List<string>
            //                     - distinct provinces ordered alphabetically
            //

            return _universityRepository.GetProvince();
        } // end method GetProvince

        public List<University> GetUniversitiesByProvince(
            string province)
        {
            //
            // Name              : List<University>
            //                     GetUniversitiesByProvince(string province)
            // Purpose           : Retrieves universities in a selected province.
            // Re-use            : IUniversityRepository.GetUniversitiesByProvince
            // Method Parameters : string province
            //                     - province used to filter universities
            // Output Type       : List<University>
            //                     - matching universities ordered by name
            //

            return _universityRepository
                .GetUniversitiesByProvince(province);
        } // end method GetUniversitiesByProvince

        public List<Programme> GetProgrammesByUniversity(
            int universityId)
        {
            //
            // Name              : List<Programme>
            //                     GetProgrammesByUniversity(int universityId)
            // Purpose           : Retrieves programmes offered by a university.
            // Re-use            : IUniversityRepository.GetProgrammesByUniversity
            // Method Parameters : int universityId
            //                     - ID of the university whose programmes
            //                       must be retrieved
            // Output Type       : List<Programme>
            //                     - matching programmes ordered by name
            //

            return _universityRepository
                .GetProgrammesByUniversity(universityId);
        } // end method GetProgrammesByUniversity

        public Programme? GetProgrammeById(int programmeId)
        {
            //
            // Name              : Programme? GetProgrammeById(int programmeId)
            // Purpose           : Retrieves a programme using its unique ID.
            // Re-use            : IUniversityRepository.GetProgrammeById
            // Method Parameters : int programmeId
            //                     - ID of the programme to retrieve
            // Output Type       : Programme?
            //                     - matching programme, or null if not found
            //

            return _universityRepository.GetProgrammeById(programmeId);
        } // end method GetProgrammeById

        public University? GetUniversityById(int universityId)
        {
            //
            // Name              : University? GetUniversityById(int universityId)
            // Purpose           : Retrieves a university through the repository.
            // Re-use            : IUniversityRepository.GetUniversityById
            // Method Parameters : int universityId
            //                     - ID of the university to retrieve
            // Output Type       : University?
            //                     - matching university, or null if not found
            //

            return _universityRepository.GetUniversityById(universityId);
        } // end method GetUniverstyById
    } // end class UniversityService
} // end namespace MatricConnect.Services