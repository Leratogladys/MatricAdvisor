using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatricConnect.Models
{
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string Website {  get; set; } = string.Empty;
         
        public ICollection<Programme> Programmes { get; set; }
    }
}
