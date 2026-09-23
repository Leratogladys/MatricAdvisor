// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Provides database operations for creating and
//                   retrieving student profiles.

using MatricConnect.Data;
using MatricConnect.Models;

namespace MatricConnect.Services
{
    public class StudentService
    {
        private readonly MatricConnectContext _context;

        public StudentService(MatricConnectContext context)
        {
            //
            // Name              : StudentService(MatricConnectContext context)
            // Purpose           : Initializes the student service with access
            //                     to the Matric Connect database context.
            // Re-use            : None
            // Method Parameters : MatricConnectContext context
            //                     - database context used to access student data
            // Output Type       : None
            //

            _context = context;
        } // end method

        public Student AddStudent(Student student)
        {
            //
            // Name              : Student AddStudent(Student student)
            // Purpose           : Saves a new student profile to the database.
            // Re-use            : None
            // Method Parameters : Student student
            //                     - student profile that must be saved
            // Output Type       : Student
            //                     - saved student including its generated ID
            //

            _context.Students.Add(student);
            _context.SaveChanges();

            return student;
        } // end method

        public Student? GetStudentById(int studentId)
        {
            //
            // Name              : Student? GetStudentById(int studentId)
            // Purpose           : Retrieves a student profile using its ID.
            // Re-use            : None
            // Method Parameters : int studentId
            //                     - ID of the student profile to retrieve
            // Output Type       : Student?
            //                     - matching student when found
            //                     - null when no matching student exists
            //

            return _context.Students
                .FirstOrDefault(s => s.Id == studentId);
        } // end method
    } // end class StudentService
} // end namespace MatricConnect.Services