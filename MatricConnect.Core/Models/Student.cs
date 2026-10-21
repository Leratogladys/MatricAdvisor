// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Represents a student profile and stores the academic
//                   information used for programme eligibility evaluation.

namespace MatricConnect.Models
{
    public class Student
    {
        public int Id
        {
            //
            // Name            : property int Id
            // Purpose         : Provides access to the unique student ID.
            // Re-use          : None
            // Input Parameter : int value
            //                   - new value for the student ID
            // Output Type     : int
            //                   - value stored as the student ID
            //

            get; set;
        } // end property

        public string FirstName
        {
            //
            // Name            : property string FirstName
            // Purpose         : Provides access to the student's first name.
            // Re-use          : None
            // Input Parameter : string value
            //                   - new value for the student's first name
            // Output Type     : string
            //                   - value stored as the student's first name
            //

            get; set; } = string.Empty; // end property

        public string LastName
        {
            //
            // Name            : property string LastName
            // Purpose         : Provides access to the student's last name.
            // Re-use          : None
            // Input Parameter : string value
            //                   - new value for the student's last name
            // Output Type     : string
            //                   - value stored as the student's last name
            //

            get; set; } = string.Empty; // end property

        public int APSScore
        {
            //
            // Name            : property int APSScore
            // Purpose         : Provides access to the student's APS score.
            // Re-use          : None
            // Input Parameter : int value
            //                   - new APS score for the student
            // Output Type     : int
            //                   - value stored as the student's APS score
            //

            get; set;
        } // end property

        public decimal MathematicsMark
        {
            //
            // Name            : property decimal MathematicsMark
            // Purpose         : Provides access to the student's
            //                   Mathematics mark.
            // Re-use          : None
            // Input Parameter : decimal value
            //                   - new Mathematics mark for the student
            // Output Type     : decimal
            //                   - value stored as the Mathematics mark
            //

            get; set;
        } // end property

        public decimal? PhysicalScienceMark
        {
            //
            // Name            : property decimal? PhysicalScienceMark
            // Purpose         : Provides access to the student's optional
            //                   Physical Science mark.
            // Re-use          : None
            // Input Parameter : decimal? value
            //                   - new Physical Science mark or null when
            //                     the student does not take the subject
            // Output Type     : decimal?
            //                   - stored Physical Science mark or null
            //

            get; set;
        } // end property

        public string Province
        {
            //
            // Name            : property string Province
            // Purpose         : Provides access to the student's province.
            // Re-use          : None
            // Input Parameter : string value
            //                   - new province for the student
            // Output Type     : string
            //                   - value stored as the student's province
            //

            get; set;} = string.Empty; // end property

        public ICollection<SavedProgramme> SavedProgrammes
        {
            //
            // Name            : property ICollection<SavedProgramme>
            //                   SavedProgrammes
            // Purpose         : Provides access to programmes saved by
            //                   the student.
            // Re-use          : None
            // Input Parameter : ICollection<SavedProgramme> value
            //                   - new collection of saved programmes
            // Output Type     : ICollection<SavedProgramme>
            //                   - programmes currently saved by the student
            //

            get; set; } = new List<SavedProgramme>(); // end property
    } // end class Student
} // end namespace MatricConnect.Models