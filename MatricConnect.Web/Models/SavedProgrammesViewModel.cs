// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Provides the selected student and their saved
//                   programmes to the saved programmes page.

using MatricConnect.Models;
using MatricConnect.Web.Models;

namespace MatricConnect.Web.Models
{
    public class SavedProgrammesViewModel
    {
        public Student Student { get; set; } = new Student();

        public List<SavedProgramme> SavedProgrammes { get; set; }
        = new List<SavedProgramme>();
    } // end class SavedProgrammesViewModel
} // end namespace MatricConnect.Web.Models
