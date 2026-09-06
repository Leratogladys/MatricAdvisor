using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatricConnect.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int APSScore { get; set; }
        public decimal MathematicsMark { get; set; }
        public decimal PhysicalScienceMark  { get; set; }
        public string Province { get; set; } = string.Empty;

        public ICollection<SavedProgramme> SavedProgrammes { get; set; } = new List<SavedProgramme>();
    }
}
