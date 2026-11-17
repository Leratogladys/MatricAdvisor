// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Reads public TVET colleges from the institution directory.

using MatricConnect.Core.Interfaces;
using MatricConnect.Data;
using MatricConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace MatricConnect.Infrastructure.Repositories
{
    public class TvetCollegeRepository(MatricConnectContext context) : ITvetCollegeRepository
    {
        public TvetCollege? GetById(int id) => context.TvetColleges.AsNoTracking().SingleOrDefault(college => college.Id == id);

        public List<TvetCollege> GetByProvince(string province)
        {
            return context.TvetColleges.AsNoTracking()
                .Where(college => college.Province == province)
                .OrderBy(college => college.Name)
                .ToList();
        }
    }
}
