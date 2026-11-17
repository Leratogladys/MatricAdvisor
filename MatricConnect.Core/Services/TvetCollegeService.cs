// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Provides public TVET college listings to the website.

using MatricConnect.Core.Interfaces;
using MatricConnect.Models;

namespace MatricConnect.Services
{
    public class TvetCollegeService(ITvetCollegeRepository repository)
    {
        public TvetCollege? GetById(int id) => repository.GetById(id);
        public List<TvetCollege> GetByProvince(string province) => repository.GetByProvince(province);
    }
}
