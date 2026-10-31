// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Handles student profile creation through the website.

using MatricConnect.Models;
using MatricConnect.Services;
using MatricConnect.Validation;
using MatricConnect.Web.Models;
using Microsoft.AspNetCore.Mvc;

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

            TempData["SuccessMessage"] =
                $"Profile created successfully. Your Student ID is " +
                $"{savedStudent.Id}. Keep this ID for future reference.";

            return RedirectToAction("Index", "Universities");
        } // end method Create
    } // end class StudentsController
} // end namespace MatricConnect.Web.Controllers