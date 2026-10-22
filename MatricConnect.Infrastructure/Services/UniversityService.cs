// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Provides database operations for retrieving
//                   universities, provinces, and programme information.

using MatricConnect.Data;
using MatricConnect.Models;

namespace MatricConnect.Services
{
    public class UniversityService
    {
        private readonly MatricConnectContext _context;

        public UniversityService(MatricConnectContext context)
        {
            //
            // Name              : UniversityService(
            //                     MatricConnectContext context)
            // Purpose           : Initializes the university service with
            //                     access to the Matric Connect database.
            // Re-use            : None
            // Method Parameters : MatricConnectContext context
            //                     - database context used to access
            //                       university and programme data
            // Output Type       : None
            //

            _context = context;
        } // end method

        public List<string> GetProvince()
        {
            //
            // Name              : List<string> GetProvince()
            // Purpose           : Retrieves the distinct provinces that
            //                     contain universities in the database.
            // Re-use            : None
            // Method Parameters : None
            // Output Type       : List<string>
            //                     - ordered list of university provinces
            //

            return _context.Universities
                .Select(u => u.Province)
                .Distinct()
                .OrderBy(p => p)
                .ToList();
        } // end method

        public List<University> GetUniversitiesByProvince(
            string province)
        {
            //
            // Name              : List<University>
            //                     GetUniversitiesByProvince(string province)
            // Purpose           : Retrieves universities located in the
            //                     selected province.
            // Re-use            : None
            // Method Parameters : string province
            //                     - province used to filter universities
            // Output Type       : List<University>
            //                     - ordered list of universities in the
            //                       selected province
            //

            return _context.Universities
                .Where(u => u.Province == province)
                .OrderBy(u => u.Name)
                .ToList();
        } // end method

        public List<Programme> GetProgrammesByUniversity(
            int universityId)
        {
            //
            // Name              : List<Programme>
            //                     GetProgrammesByUniversity(int universityId)
            // Purpose           : Retrieves programmes offered by a
            //                     selected university.
            // Re-use            : None
            // Method Parameters : int universityId
            //                     - ID of the university whose programmes
            //                       must be retrieved
            // Output Type       : List<Programme>
            //                     - ordered list of programmes offered by
            //                       the selected university
            //

            return _context.Programmes
                .Where(p => p.UniversityId == universityId)
                .OrderBy(p => p.Name)
                .ToList();
        } // end method

        public Programme? GetProgrammeById(int programmeId)
        {
            //
            // Name              : Programme?
            //                     GetProgrammeById(int programmeId)
            // Purpose           : Retrieves a programme using its unique ID.
            // Re-use            : None
            // Method Parameters : int programmeId
            //                     - ID of the programme to retrieve
            // Output Type       : Programme?
            //                     - matching programme when found
            //                     - null when no matching programme exists
            //

            return _context.Programmes
                .FirstOrDefault(p => p.Id == programmeId);
        } // end method
    } // end class UniversityService
} // end namespace MatricConnect.Services