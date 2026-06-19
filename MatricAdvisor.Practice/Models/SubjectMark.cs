using System;
using System.Collections.Generic;

namespace MatricAdvisor.Practice.Models
{
    public class SubjectMark
    {
        public string SubjectName {get; set;} = string.Empty;
        public int Mark {get; set;} 

        public int ApsPoints
        {
            get
            {
                if (Mark >= 80) return 7;
                if (Mark >= 70) return 6;
                if (Mark >= 60) return 5;
                if (Mark >= 50) return 4;
                if (Mark >= 40) return 3;
                if (Mark >= 30) return 2;
                return 1;
            }
        }
    }
}