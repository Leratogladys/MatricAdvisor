// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Stores edits and identifies which student's form was opened.

namespace MatricConnect.Web.Models
{
    public class EditStudentViewModel : StudentProfileInput
    {
        public int OriginalStudentId { get; set; }
    } // end class EditStudentViewModel
} // end namespace MatricConnect.Web.Models
