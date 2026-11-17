// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Handles saving programmes for the student
//                   selected in the current session.

using MatricConnect.Infrastructure.Repositories;
using MatricConnect.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MatricConnect.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace MatricConnect.Web.Controllers
{
    public class SavedProgrammesController : Controller
    {

        private readonly SavedProgrammeService _savedProgrammeService;
        private readonly StudentService _studentService;
        private readonly UniversityService _universityService;
        private readonly ProgrammeReferenceRepository _programmeReferences;

        private readonly ILogger<SavedProgrammesController> _logger;

        public SavedProgrammesController(SavedProgrammeService savedProgrammeService, StudentService studentService,
            UniversityService universityService, ProgrammeReferenceRepository programmeReferences,
            ILogger<SavedProgrammesController> logger)
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
            //                     ILogger<SavedProgrammesController> logger
            //                     - records unexpected storage failures
            // Output Type       : None
            //

            _savedProgrammeService = savedProgrammeService;
            _studentService = studentService;
            _universityService = universityService;
            _programmeReferences = programmeReferences;
            _logger = logger;
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

                return RedirectToAction("Existing", "Students",
                       new { returnUrl = Url.Action("Index", "SavedProgrammes")});
            } // end if

            var student = _studentService.GetStudentById(studentId.Value);

            if (student == null)
            {
                HttpContext.Session.Remove("CurrentStudentId");

                return RedirectToAction("Existing", "Students",
                       new { returnUrl = Url.Action("Index", "SavedProgrammes")});
            } // end if

            var savedProgrammes = _savedProgrammeService.GetSavedProgrammesByStudent(student.Id)
                .Select(item => new SavedProgrammeCard(item.ProgrammeId, item.Programme.Name,
                    item.Programme.University.Name, item.Programme.University.Province, IsReference: false));

            var savedReferences = _programmeReferences.ForStudent(student.Id)
                .Select(item => new SavedProgrammeCard(item.ProgrammeReferenceId, item.Reference.Name,
                    item.Reference.University?.Name ?? item.Reference.TvetCollege!.Name,
                    item.Reference.University?.Province ?? item.Reference.TvetCollege!.Province, IsReference: true));

            var model = new SavedProgrammesViewModel
            {
                Student = student,
                SavedProgrammes = savedProgrammes.Concat(savedReferences)
                    .OrderBy(item => item.Name).ThenBy(item => item.InstitutionName).ToList()
            };

            return View(model);
        } // end method Index

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(int id)
        {
            //
            // Name              : IActionResult Save(int id)
            // Purpose           : Saves a programme for the selected student,
            //                     or requests profile selection with a return
            //                     path to the programme details.
            // Re-use            : StudentService.GetStudentById(),
            //                     UniversityService.GetProgrammeById(),
            //                     SavedProgrammeService.SaveProgramme()
            // Method Parameters : int id
            //                     - ID of the programme to save
            // Output Type       : IActionResult
            //                     - redirect to programme details or profile
            //                       selection, or 404 for an invalid programme
            //

            if(!ModelState.IsValid || id <=  0)
            {
                return NotFound();
            } // end if

            var programme = _universityService.GetProgrammeById(id);

            if(programme == null)
            {
                return NotFound();
            } // end if

            var returnUrl = Url.Action(
                            "ProgrammeDetails",
                            "Universities",
                            new { id = programme.Id });

            int? studentId = HttpContext.Session.GetInt32("CurrentStudentId");
            if(!studentId.HasValue)
            {
                TempData["SuccessMessage"] = programme.LatestVerifiedIntake != null
                    ? "Select a student profile. You will return to this programme to save it."
                    : "Select a student profile. You will return to this programme to review your results and save it.";

                return RedirectToAction("Existing", "Students", new { returnUrl });
            } // end if 

            var student = _studentService.GetStudentById(studentId.Value);
            if(student == null)
            {
                HttpContext.Session.Remove("CurrentStudentId");

                TempData["SuccessMessage"] = "Your selected profile could not be found. Select a " +
                                             "profile to return to this programme.";

                return RedirectToAction("Existing", "Students", new {returnUrl});

            } // end if

            ProgrammeSaveOutcome outcome;
            try
            {
                outcome = _savedProgrammeService.SaveProgramme(student.Id, programme.Id)
                    ? ProgrammeSaveOutcome.Saved
                    : ProgrammeSaveOutcome.AlreadySaved;
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqliteException { SqliteExtendedErrorCode: 1555 })
            {
                // The composite primary key also protects against simultaneous saves.
                outcome = ProgrammeSaveOutcome.AlreadySaved;
            }
            catch (Exception ex) when (ex is DbUpdateException or SqliteException)
            {
                _logger.LogError(ex, "Unable to save programme {ProgrammeId}.", programme.Id);
                outcome = ProgrammeSaveOutcome.Failed;
            }

            TempData[ProgrammeSaveFeedback.Key(student.Id, programme.Id)] = outcome.ToString();

            return RedirectToAction("ProgrammeDetails", "Universities", new { id = programme.Id });
        } // end method Save

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveReference(int id)
        {
            if (!ModelState.IsValid || id <= 0 || _programmeReferences.Find(id) == null)
                return NotFound();

            var returnUrl = Url.Action("ReferenceDetails", "Universities", new { id });
            if (GetSelectedStudentId() is not int studentId)
            {
                TempData["SuccessMessage"] = "Select a student profile. You will return to this programme to save it.";
                return RedirectToAction("Existing", "Students", new { returnUrl });
            }

            ProgrammeSaveOutcome outcome;
            try
            {
                outcome = _programmeReferences.Save(studentId, id)
                    ? ProgrammeSaveOutcome.Saved : ProgrammeSaveOutcome.AlreadySaved;
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqliteException { SqliteExtendedErrorCode: 1555 })
            {
                outcome = ProgrammeSaveOutcome.AlreadySaved;
            }
            catch (Exception ex) when (ex is DbUpdateException or SqliteException)
            {
                _logger.LogError(ex, "Unable to save programme reference {ReferenceId}.", id);
                outcome = ProgrammeSaveOutcome.Failed;
            }

            TempData[ProgrammeSaveFeedback.ReferenceKey(studentId, id)] = outcome.ToString();
            return RedirectToAction("ReferenceDetails", "Universities", new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveReference(int id)
        {
            if (!ModelState.IsValid || id <= 0)
                return NotFound();

            if (GetSelectedStudentId() is not int studentId)
            {
                TempData["SuccessMessage"] = "Select a student profile to manage saved programmes.";
                return RedirectToAction("Existing", "Students", new { returnUrl = Url.Action(nameof(Index), "SavedProgrammes") });
            }

            TempData["SavedProgrammesMessage"] = _programmeReferences.Remove(studentId, id)
                ? "Programme removed successfully." : "This programme was not found in your saved list.";
            return RedirectToAction(nameof(Index));
        }

        private int? GetSelectedStudentId()
        {
            if (HttpContext.Session.GetInt32("CurrentStudentId") is not int id)
                return null;

            if (_studentService.GetStudentById(id) != null)
                return id;

            HttpContext.Session.Remove("CurrentStudentId");
            return null;
        }

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

                return RedirectToAction("Existing", "Students",
                       new { returnUrl = Url.Action("Index", "SavedProgrammes") });
            } // end if

            var student = _studentService.GetStudentById(studentId.Value);

            if (student == null)
            {
                HttpContext.Session.Remove("CurrentStudentId");

                return RedirectToAction("Existing", "Students",
                       new { returnUrl = Url.Action("Index", "SavedProgrammes") });
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
