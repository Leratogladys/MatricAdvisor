using System;
using System.Collections.Generic;


namespace MatricAdvisor.Practice.Models
{
    public class University
    {
        public string Name {get; set;} = string.Empty;
        public string City {get; set;} = string.Empty;
        public string Province {get; set; } = string.Empty;
        public string Website {get; set; } = string.Empty;

        public List<Programme> Programmes {get; set;} = new List<Programme>();
    }
}