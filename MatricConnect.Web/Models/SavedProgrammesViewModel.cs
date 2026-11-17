// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Provides the selected student and their saved
//                   programmes to the saved programmes page.

using MatricConnect.Models;

namespace MatricConnect.Web.Models
{
    public class SavedProgrammesViewModel
    {
        public Student Student { get; set; } = new Student();

        public List<SavedProgrammeCard> SavedProgrammes { get; set; } = [];
    } // end class SavedProgrammesViewModel

    public sealed record SavedProgrammeCard(int Id, string Name, string InstitutionName, string Province, bool IsReference)
    {
        public string DetailsAction => IsReference ? "ReferenceDetails" : "ProgrammeDetails";
        public string RemoveAction => IsReference ? "RemoveReference" : "Remove";
    }
} // end namespace MatricConnect.Web.Models
