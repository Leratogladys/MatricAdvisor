// Programmer name: Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Implements database operations for creating and
//                   retrieving student profiles.

using MatricConnect.Core.Interfaces;
using MatricConnect.Data;
using MatricConnect.Models;
using Microsoft.EntityFrameworkCore;


namespace MatricConnect.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly MatricConnectContext _context;

        public StudentRepository(MatricConnectContext context)
        {
            //
            // Name              : StudentRepository(MatricConnectContext context)
            // Purpose           : Initializes the repository with the
            //                     database context used for student data.
            // Re-use            : None
            // Method Parameters : MatricConnectContext context
            //                     - database context used to access students
            // Output Type       : None
            //

            _context = context;

        } // end constructor

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
        }// end method AddStudent

        public Student? GetStudentById(int studentId)
        {
            //
            // Name              : Student? GetStudentById(int studentId)
            // Purpose           : Retrieves a student profile using its ID.
            // Re-use            : None
            // Method Parameters : int studentId
            //                     - ID of the student profile to retrieve
            // Output Type       : Student?
            //                     - matching student, or null if not found
            //

            return _context.Students.FirstOrDefault(s => s.Id == studentId);
        } // end method GetStudentById

        public bool UpdateStudent(int studentId, Student details)
        {
            return _context.Students
                .Where(student => student.Id == studentId)
                .ExecuteUpdate(update => update
                    .SetProperty(student => student.FirstName, details.FirstName)
                    .SetProperty(student => student.LastName, details.LastName)
                    .SetProperty(student => student.Province, details.Province)
                    .SetProperty(student => student.APSScore, details.APSScore)
                    .SetProperty(student => student.MathematicsMark, details.MathematicsMark)
                    .SetProperty(student => student.PhysicalScienceMark, details.PhysicalScienceMark)) == 1;
        } // end method UpdateStudent
    } // end class StudentRepository
} // end namespace MatricConnect.Infrastructure.Repositories