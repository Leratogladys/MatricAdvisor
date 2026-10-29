// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Defines the operations required to retrieve
//                   provinces, universities, and programmes.

using MatricConnect.Models;

namespace MatricConnect.Core.Interfaces
{
    public interface IUniversityRepository
    {
        //
        // Name              : List<string> GetProvince()
        // Purpose           : Retrieves provinces containing universities.
        // Re-use            : None
        // Method Parameters : None
        // Output Type       : List<string>
        //                     - distinct provinces ordered alphabetically
        //
        List<string> GetProvince();

        //
        // Name              : List<University>
        //                     GetUniversitiesByProvince(string province)
        // Purpose           : Retrieves universities in a selected province.
        // Re-use            : None
        // Method Parameters : string province
        //                     - province used to filter universities
        // Output Type       : List<University>
        //                     - matching universities ordered by name
        //
        List<University> GetUniversitiesByProvince(string province);

        //
        // Name              : List<Programme>
        //                     GetProgrammesByUniversity(int universityId)
        // Purpose           : Retrieves programmes offered by a university.
        // Re-use            : None
        // Method Parameters : int universityId
        //                     - ID of the university to retrieve programmes for
        // Output Type       : List<Programme>
        //                     - matching programmes ordered by name
        //
        List<Programme> GetProgrammesByUniversity(int universityId);

        //
        // Name              : Programme? GetProgrammeById(int programmeId)
        // Purpose           : Retrieves a programme using its unique ID.
        // Re-use            : None
        // Method Parameters : int programmeId
        //                     - ID of the programme to retrieve
        // Output Type       : Programme?
        //                     - matching programme, or null if not found
        //
        Programme? GetProgrammeById(int programmeId);

        //
        // Name              : University? GetUniversityById(int universityId)
        // Purpose           : Retrieves a university using its unique ID.
        // Re-use            : None
        // Method Parameters : int universityId
        //                     - ID of the university to retrieve
        // Output Type       : University?
        //                     - matching university, or null if not found
        //
        University? GetUniversityById(int universityId);
    } // end interface IUniversityRepository

    
} // end namespace MatricConnect.Core.Interfaces