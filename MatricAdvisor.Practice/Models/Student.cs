using static System.Console;
using System.Collections.Generic;
using System;
//Model/Student.cs
//This class describes what a student looks like to our system\.

namespace MatricAdvisor.Practice.Models
{
    public class Student
    {
        public string Name {get; set; } =string.Empty;
        public string Surname {get; set;} = string.Empty;
        public int MatricYear {get; set;}

        public List<SubjectMark> Subjects { get; set; } = new List<SubjectMark>();


        public int TotalAps
        {
            get
            {
                int total = 0;
                foreach(SubjectMark subject in Subjects)
                {
                    ///Skip Life Orientation
                    if(subject.SubjectName == "Life Orientation") continue;
                    total += subject.ApsPoints;
                }

                return total;
            }
        }

        public string GetSummary()
        {
            return $"{Name} {Surname} | Year: {MatricYear} | APS: {TotalAps}";
        }

    }

    
}