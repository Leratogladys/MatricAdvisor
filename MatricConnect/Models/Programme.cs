using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatricConnect.Models
{
    public class Programme
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string FieldOfStudy { get; set; } = string.Empty;

        public int RequiredAPS { get; set; }

        public decimal RequiredMathematics { get; set; }

        public decimal? RequiredPhysicalScience { get; set; }

        public DateTime ApplicationDeadline { get; set; }
        public int UniversityId { get; set; }
        public University University { get; set; } = null!;
        public ICollection<SavedProgramme> SavedProgrammes { get; set; }
            = new List<SavedProgramme>();
    }
}