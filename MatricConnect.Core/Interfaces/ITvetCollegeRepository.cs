// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Defines the public TVET directory query.

using MatricConnect.Models;

namespace MatricConnect.Core.Interfaces
{
    public interface ITvetCollegeRepository
    {
        List<TvetCollege> GetByProvince(string province);
    }
}
