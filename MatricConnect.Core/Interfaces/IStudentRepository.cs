// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Defines the operations required to store and
//                   retrieve student profiles.

using MatricConnect.Models;

namespace MatricConnect.Core.Interfaces
{
    public interface IStudentRepository
    {

        //
        // Name              : AddStudent
        // Purpose           : Stores a new student profile.
        // Re-use            : None
        // Method Parameters : Student student
        //                     - student profile to store
        // Output Type       : Student
        //                     - stored student with its assigned ID
        //

        Student AddStudent(Student student);

        //
        // Name              : GetStudentById
        // Purpose           : Retrieves a student using their ID.
        // Re-use            : None
        // Method Parameters : int studentId
        //                     - ID of the student to retrieve
        // Output Type       : Student?
        //                     - matching student, or null if not found
        //

        Student? GetStudentById(int studentId);
    } // end interface IStudentRepository
} // end namespace MatricConnect.Core.Interfaces
