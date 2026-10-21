//Programmer name  : Lerato Molefe
//Project name     : Matric Connect
//Purpose          : Validates student profile values shared by 
//                   the console and web applicationns

using MatricConnect.Models;

namespace MatricConnect.Validation
{
    public class StudentValidator
    {
        public List<string> Validate(Student student)
        {
            //
            // Name              : List<string> Validate(Student student)
            // Purpose           : Identifies invalid student profile values.
            // Re-use            : None
            // Method Parameters : Student student
            //                     - student profile to validate
            // Output Type       : List<string>
            //                     - validation errors; empty when valid
            //

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(student.FirstName))
            {
                errors.Add("First name is required.");
            } // end if

            if(string.IsNullOrWhiteSpace(student.LastName))
            {
                errors.Add("Last name is required.");
            } // end if

            if (string.IsNullOrWhiteSpace(student.Province))
            {
                errors.Add("Home provinde is required.");
            } // end if

            if(student.APSScore < 0 || student.APSScore > 57)
            {
                errors.Add("APS score must be betweeen 0 and 57.");
            } // end if

            if(student.MathematicsMark < 0 || student.MathematicsMark > 100)
            {
                errors.Add("Mathematics marks must be between 0 and 100");
            } // end if

            if(student.PhysicalScienceMark.HasValue)
            {
                decimal scienceMark = student.PhysicalScienceMark.Value;
                
                if(scienceMark > 0 || scienceMark > 100)
                {
                    errors.Add("Physical Science mark must be between 0 and 100.");
                } // end if

            } // end if

            return errors;
        } // end method 
    } // end class StudentValidator
} // end namespace MatricConnect.Validation
