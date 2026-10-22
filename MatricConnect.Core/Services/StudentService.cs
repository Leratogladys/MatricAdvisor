// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Provides student profile operations through
//                   the student repository contract.

using MatricConnect.Core.Interfaces;
using MatricConnect.Models;

namespace MatricConnect.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            //
            // Name              : StudentService(IStudentRepository studentRepository)
            // Purpose           : Initializes the service with a student
            //                     repository supplied by the caller.
            // Re-use            : None
            // Method Parameters : IStudentRepository studentRepository
            //                     - repository used to store and retrieve students
            // Output Type       : None
            //

            _studentRepository = studentRepository;
        } // end constructor

        public Student AddStudent(Student student)
        {
            //
            // Name              : Student AddStudent(Student student)
            // Purpose           : Stores a new student profile.
            // Re-use            : IStudentRepository.AddStudent
            // Method Parameters : Student student
            //                     - student profile that must be saved
            // Output Type       : Student
            //                     - saved student including its generated ID
            //

            return _studentRepository.AddStudent(student);
        } // end method AddStudent

        public Student? GetStudentById(int studentId)
        {
            //
            // Name              : Student? GetStudentById(int studentId)
            // Purpose           : Retrieves a student profile using its ID.
            // Re-use            : IStudentRepository.GetStudentById
            // Method Parameters : int studentId
            //                     - ID of the student profile to retrieve
            // Output Type       : Student?
            //                     - matching student, or null if not found
            //

            return _studentRepository.GetStudentById(studentId);
        } // end method GetStudentById
    } // end class StudentService
} // end namespace MatricConnect.Services