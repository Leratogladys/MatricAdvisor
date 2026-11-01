// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Handles student profile creation through the website.

using MatricConnect.Models;
using MatricConnect.Services;
using MatricConnect.Validation;
using MatricConnect.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MatricConnect.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentService _studentService;
        private readonly StudentValidator _studentValidator;

        public StudentsController(
            StudentService studentService,
            StudentValidator studentValidator)
        {
            //
            // Name              : StudentsController(
            //                     StudentService studentService,
            //                     StudentValidator studentValidator)
            // Purpose           : Receives profile services and validation
            //                     through dependency injection.
            // Re-use            : None
            // Method Parameters : StudentService studentService
            //                     - service used to save student profiles
            //                     StudentValidator studentValidator
            //                     - shared student validation rules
            // Output Type       : None
            //

            _studentService = studentService;
            _studentValidator = studentValidator;
        } // end constructor

        [HttpGet]
        public IActionResult Create()
        {
            //
            // Name              : IActionResult Create()
            // Purpose           : Displays an empty student profile form.
            // Re-use            : None
            // Method Parameters : None
            // Output Type       : IActionResult - student profile form
            //

            return View(new CreateStudentViewModel());
        } // end method Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateStudentViewModel model)
        {
            //
            // Name              : IActionResult Create(
            //                     CreateStudentViewModel model)
            // Purpose           : Validates and saves a new student profile.
            // Re-use            : StudentValidator.Validate(),
            //                     StudentService.AddStudent()
            // Method Parameters : CreateStudentViewModel model
            //                     - profile values submitted through the form
            // Output Type       : IActionResult
            //                     - form with errors, or redirect after saving
            //

            if (!ModelState.IsValid)
            {
                return View(model);
            } // end if

            var student = new Student
            {
                FirstName = model.FirstName.Trim(),
                LastName = model.LastName.Trim(),
                Province = model.Province.Trim(),
                APSScore = model.APSScore!.Value,
                MathematicsMark = model.MathematicsMark!.Value,
                PhysicalScienceMark = model.PhysicalScienceMark
            };

            var validationErrors = _studentValidator.Validate(student);

            foreach (var error in validationErrors)
            {
                ModelState.AddModelError(string.Empty, error);
            } // end foreach

            if (!ModelState.IsValid)
            {
                return View(model);
            } // end if

            var savedStudent = _studentService.AddStudent(student);
            HttpContext.Session.SetInt32("CurrentStudentId", savedStudent.Id);

            TempData["SuccessMessage"] =
                $"Profile created successfully. Your Student ID is " +
                $"{savedStudent.Id}. Keep this ID for future reference.";

            return RedirectToAction("Index", "Universities");
        } // end method Create

        [HttpGet]
        public IActionResult Existing()
        {
            //
            // Name              : IActionResult Existing()
            // Purpose           : Displays the existing student lookup form.
            // Re-use            : None
            // Method Parameters : None
            // Output Type       : IActionResult - student lookup form
            //
            
            return View(new ExistingStudentViewModel());
        }// end method Exisiting

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Existing(ExistingStudentViewModel model)
        {
            //
            // Name              : IActionResult Existing(
            //                     ExistingStudentViewModel model)
            // Purpose           : Finds an existing student using the entered ID.
            // Re-use            : StudentService.GetStudentById()
            // Method Parameters : ExistingStudentViewModel model
            //                     - student ID submitted through the form
            // Output Type       : IActionResult
            //                     - form with errors, or redirect when found
            //

            if(!ModelState.IsValid)
            {
                return View(model);
            } // end if

            var student = _studentService.GetStudentById(model.StudentId!.Value);

            if(student == null)
            {
                ModelState.AddModelError(nameof(ExistingStudentViewModel.StudentId),
                    "No student profile was found with that ID.");

                return View(model);
            } // end if

            HttpContext.Session.SetInt32("CurrentStudentId", student.Id);

            TempData["SuccessMessage"] = $"Welcome back, {student.FirstName} {student.LastName}!";

            return RedirectToAction("Index", "Universities");
        } // end method Existing

        [HttpGet]
        public IActionResult Profile()
        {
            //
            // Name              : IActionResult Profile()
            // Purpose           : Displays the student selected in the current session.
            // Re-use            : StudentService.GetStudentById()
            // Method Parameters : None
            // Output Type       : IActionResult
            //                     - selected student's profile, or redirect
            //                       to profile lookup when no selection exists
            //

            int? studentId = HttpContext.Session.GetInt32("CurrentStudentId");

            if(!studentId.HasValue)
            {
                return RedirectToAction(nameof(Existing));
            } // end if

            var student = _studentService.GetStudentById(studentId.Value);

            if (student == null)
            {
                HttpContext.Session.Remove("CurrentStudentId");

                return RedirectToAction(nameof(Existing));
            } // end if

            return View(student);
        } // end method Profile

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ClearProfile()
        {
            //
            // Name              : IActionResult ClearProfile()
            // Purpose           : Clears the selected student from the session.
            // Re-use            : None
            // Method Parameters : None
            // Output Type       : IActionResult
            //                     - redirect to the home page
            //

            HttpContext.Session.Remove("CurrentStudentId");

            TempData["SuccessMessage"] = "Your profile selection has been cleared.";

            return RedirectToAction("Index", "Home");
        } // end method ClearProfile
    } // end class StudentsController
} // end namespace MatricConnect.Web.Controllers