using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatricConnect.Models
{
    public class SavedProgramme
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int ProgrammeId { get; set; }
        public Programme Programme { get; set; } = null!;
    }
}
