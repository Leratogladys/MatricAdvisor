using MatricConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatricConnect.Services
{
    public class EligibilityService
    {
        public bool IsEligible(Student student, Programme programme)
        {
            if(student.APSScore < programme.RequiredAPS)
            {
                return false;
            }

            if(student.MathematicsMark < programme.RequiredMathematics)
            {
                return false;
            }
            if(programme.RequiredPhysicalScience.HasValue)
            {
                if (student.PhysicalScienceMark == null ||
                    student.PhysicalScienceMark < 
                    programme.RequiredPhysicalScience.Value)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
