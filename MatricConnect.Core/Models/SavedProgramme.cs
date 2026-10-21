// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Represents a saved programme record that links
//                   a student to a university programme.

namespace MatricConnect.Models
{
    public class SavedProgramme
    {
        public int Id
        {
            //
            // Name            : property int Id
            // Purpose         : Provides access to the unique saved
            //                   programme record ID.
            // Re-use          : None
            // Input Parameter : int value
            //                   - new value for the saved programme ID
            // Output Type     : int
            //                   - value stored as the saved programme ID
            //

            get; set;
        } // end property

        public int StudentId
        {
            //
            // Name            : property int StudentId
            // Purpose         : Provides access to the ID of the student
            //                   who saved the programme.
            // Re-use          : None
            // Input Parameter : int value
            //                   - new student ID
            // Output Type     : int
            //                   - value stored as the student ID
            //

            get; set;
        } // end property

        public Student Student
        {
            //
            // Name            : property Student Student
            // Purpose         : Provides access to the student associated
            //                   with the saved programme record.
            // Re-use          : None
            // Input Parameter : Student value
            //                   - new student associated with the record
            // Output Type     : Student
            //                   - student associated with the record
            //

            get; set;
        } = null!; // end property

        public int ProgrammeId
        {
            //
            // Name            : property int ProgrammeId
            // Purpose         : Provides access to the ID of the programme
            //                   saved by the student.
            // Re-use          : None
            // Input Parameter : int value
            //                   - new programme ID
            // Output Type     : int
            //                   - value stored as the programme ID
            //

            get; set;
        } // end property

        public Programme Programme
        {
            //
            // Name            : property Programme Programme
            // Purpose         : Provides access to the programme associated
            //                   with the saved programme record.
            // Re-use          : None
            // Input Parameter : Programme value
            //                   - new programme associated with the record
            // Output Type     : Programme
            //                   - programme associated with the record
            //

            get; set;
        } = null!; // end property
    } // end class SavedProgramme
} // end namespace MatricConnect.Models