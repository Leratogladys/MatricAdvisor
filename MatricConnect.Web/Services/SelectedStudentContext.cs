// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Provides the profile selected for the current web request.

using MatricConnect.Models;
using MatricConnect.Services;

namespace MatricConnect.Web.Services
{
    public class SelectedStudentContext
    {
        private readonly StudentService _studentService;
        private bool _loaded;
        private Student? _student;

        public SelectedStudentContext(StudentService studentService)
        {
            //
            // Name              : SelectedStudentContext(StudentService studentService)
            // Purpose           : Receives the service used to look up profiles.
            // Re-use            : None
            // Method Parameters : StudentService studentService
            // Output Type       : None
            //

            _studentService = studentService;
        } // end constructor

        public Student? GetStudent(HttpContext httpContext)
        {
            //
            // Name              : Student? GetStudent(HttpContext httpContext)
            // Purpose           : Reads the selected profile once per request.
            // Re-use            : StudentService.GetStudentById()
            // Method Parameters : HttpContext httpContext - current request
            // Output Type       : Student? - selected profile, or null
            //
            if (!_loaded)
            {
                var studentId = httpContext.Session.GetInt32("CurrentStudentId");
                _student = studentId.HasValue
                    ? _studentService.GetStudentById(studentId.Value)
                    : null;
                _loaded = true;
            }

            return _student;
        } // end method GetStudent
    } // end class SelectedStudentContext
} // end namespace MatricConnect.Web.Services
