// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Represents a public TVET college in the institution directory.

namespace MatricConnect.Models
{
    public class TvetCollege
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public ICollection<ProgrammeReference> ProgrammeReferences { get; set; } = [];
        public string Website { get; set; } = string.Empty;
    }
}
