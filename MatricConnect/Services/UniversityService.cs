using MatricConnect.Data;
using MatricConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatricConnect.Services
{
    public class UniversityService
    {
        private readonly MatricConnectContext _context;

        public UniversityService(MatricConnectContext context)
        {
            _context = context;
        }

        public List<string> GetProvince()
        {
            return _context.Universities
                   .Select(u => u.Province) 
                   .Distinct() 
                   .OrderBy(p => p)
                   .ToList();
        }

        public List<University> GetUniversitiesByProvince(string province)
        {
            return _context.Universities
                   .Where(u => u.Province == province)
                   .OrderBy(p => p.Name)
                   .ToList();
        }

        public List<Programme> GetProgrammesByUniversity(int universityId)
        { 
            return _context.Programmes
                    .Where(p => p.UniversityId == universityId)
                    .OrderBy (p => p.Name)
                    .ToList ();
        }

    }
}
