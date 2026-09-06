using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MatricAdvisor.API.Models
{
    public class University
    {
        [Key] public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public string City {get; set;} = string.Empty;
        public string Province {get; set;} = string.Empty;
        public string Website {get; set;} = string.Empty;

        public List<Programme> Programmes {get; set;} = new List<Programme>();

    }
}