// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Handles saving programmes for the student
//                   selected in the current session.

using MatricConnect.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatricConnect.Web.Controllers
{
    public class SavedProgrammesController : Controller
    {

        private readonly SavedProgrammeService _savedProgrammeService;
        private readonly StudentService _studentService;
        private readonly UniversityService _universityService;

        public SavedProgrammesController(SavedProgrammeService savedProgrammeService, StudentService studentService, UniversityService universityService)
        {
            //
            // Name              : SavedProgrammesController
            // Purpose           : Receives services through dependency injection.
            // Re-use            : None
            // Method Parameters : SavedProgrammeService savedProgrammeService
            //                     - manages saved programmes
            //                     StudentService studentService
            //                     - retrieves the selected student
            //                     UniversityService universityService
            //                     - retrieves programme information
            // Output Type       : None
            //

            _savedProgrammeService = savedProgrammeService;
            _studentService = studentService;
            _universityService = universityService;
        } // end constructor

        [HttpGet]
        public IActionResult Index()
        {
            //
            // Name              : IActionResult Index()
            // Purpose           : Displays programmes saved by the student
            //                     selected in the current session.
            // Re-use            : StudentService.GetStudentById(),
            //                     SavedProgrammeService.GetSavedProgrammesByStudent()
            // Method Parameters : None
            // Output Type       : IActionResult
            //                     - saved programme list, or redirect to profile
            //                       selection when no valid student is selected
            //

            int? studentId =
                HttpContext.Session.GetInt32("CurrentStudentId");

            if (!studentId.HasValue)
            {
                TempData["SuccessMessage"] =
                    "Select a student profile to view saved programmes.";

                return RedirectToAction("Existing", "Students");
            } // end if

            var student = _studentService.GetStudentById(studentId.Value);

            if (student == null)
            {
                HttpContext.Session.Remove("CurrentStudentId");

                return RedirectToAction("Existing", "Students");
            } // end if

            var savedProgrammes = _savedProgrammeService
                .GetSavedProgrammesByStudent(student.Id);

            return View(savedProgrammes);
        } // end method Index

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(int id)
        {
            //
            // Name              : IActionResult Save(int id)
            // Purpose           : Saves a programme for the selected student.
            // Re-use            : StudentService.GetStudentById(),
            //                     UniversityService.GetProgrammeById(),
            //                     SavedProgrammeService.SaveProgramme()
            // Method Parameters : int id
            //                     - ID of the programme to save
            // Output Type       : IActionResult
            //                     - redirect to programme details or profile
            //                       selection, or 404 for an invalid programme
            //

            if (!ModelState.IsValid || id <= 0)
            {
                return NotFound();
            } // end if

            var programme = _universityService.GetProgrammeById(id);

            if (programme == null)
            {
                return NotFound();
            } // end if

            int? studentId = HttpContext.Session.GetInt32("CurrentStudentId");

            if (!studentId.HasValue)
            {
                TempData["SuccessMessage"] = "Select a student profile, then return to the programme to save it.";

                return RedirectToAction("Existing", "Students");
            } // end if

            var student = _studentService.GetStudentById(studentId.Value);

            if (student == null)
            {
                HttpContext.Session.Remove("CurrentStudentId");

                return RedirectToAction("Existing", "Students");
            } // end if

            bool saved = _savedProgrammeService.SaveProgramme(student.Id, programme.Id);

            TempData["SaveMessage"] = saved ? "Programme saved successfully." : "This programmed is already saved.";

            return RedirectToAction("ProgrammeDetails", "Universities",
                                     new { id = programme.Id });

        } // end method Save

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            //
            // Name              : IActionResult Remove(int id)
            // Purpose           : Removes a programme saved by the selected student.
            // Re-use            : StudentService.GetStudentById(),
            //                     SavedProgrammeService.RemoveSavedProgramme()
            // Method Parameters : int id
            //                     - ID of the programme to remove
            // Output Type       : IActionResult
            //                     - redirect to saved programmes or profile selection,
            //                       or 404 for an invalid programme ID
            //

            if (!ModelState.IsValid || id <= 0)
            {
                return NotFound();
            } // end if

            int? studentId =
                HttpContext.Session.GetInt32("CurrentStudentId");

            if (!studentId.HasValue)
            {
                TempData["SuccessMessage"] =
                    "Select a student profile to manage saved programmes.";

                return RedirectToAction("Existing", "Students");
            } // end if

            var student = _studentService.GetStudentById(studentId.Value);

            if (student == null)
            {
                HttpContext.Session.Remove("CurrentStudentId");

                return RedirectToAction("Existing", "Students");
            } // end if

            bool removed = _savedProgrammeService
                .RemoveSavedProgramme(student.Id, id);

            TempData["SavedProgrammesMessage"] = removed
                ? "Programme removed successfully."
                : "This programme was not found in your saved list.";

            return RedirectToAction(nameof(Index));
        } // end method Remove

    } // end class SavedProgrammesController
} // end namespace MatricConnect.Web.Controllers
