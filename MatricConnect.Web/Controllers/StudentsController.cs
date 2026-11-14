// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Handles student profile creation, lookup and editing.

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
        private readonly IWebHostEnvironment _environment;

        public StudentsController(
            StudentService studentService,
            StudentValidator studentValidator,
            IWebHostEnvironment environment)
        {
            //
            // Name              : StudentsController(
            //                     StudentService studentService,
            //                     StudentValidator studentValidator,
            //                     IWebHostEnvironment environment)
            // Purpose           : Receives profile services and validation
            //                     through dependency injection.
            // Re-use            : None
            // Method Parameters : StudentService studentService
            //                     - service used to save student profiles
            //                     StudentValidator studentValidator
            //                     - shared student validation rules
            //                     IWebHostEnvironment environment
            //                     - checks if local profile editing is enabled
            // Output Type       : None
            //

            _studentService = studentService;
            _studentValidator = studentValidator;
            _environment = environment;
        } // end constructor

        [HttpGet]
        public IActionResult Create(string? returnUrl)
        {
            //
            // Name              : IActionResult Create(string? returnUrl)
            // Purpose           : Displays the profile creation form and retains
            //                     a local destination after creation.
            // Re-use            : None
            // Method Parameters : string? returnUrl
            //                     - requested destination after creation
            // Output Type       : IActionResult - student profile form
            //

            var model = new CreateStudentViewModel
            {
                ReturnUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : null
            };

            return View(model);
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

            var student = model.ToStudent();
            ValidateStudent(student);

            if (!ModelState.IsValid)
            {
                return View(model);
            } // end if

            var savedStudent = _studentService.AddStudent(student);

            HttpContext.Session.SetInt32("CurrentStudentId", savedStudent.Id);
            if (Url.IsLocalUrl(model.ReturnUrl))
            {
                TempData["ProfileCreatedMessage"] =
                    $"Profile created successfully. Your Student ID is {savedStudent.Id}. Keep this ID for future reference.";
                return LocalRedirect(model.ReturnUrl);
            } // end if

            TempData[nameof(ProfileCreated)] = savedStudent.Id;
            return RedirectToAction(nameof(ProfileCreated));

        } // end method Create

        [HttpGet]
        public IActionResult Existing(string? returnUrl)
        {

            //
            // Name              : IActionResult Existing(string? returnUrl)
            // Purpose           : Displays the student lookup form and retains
            //                     a local destination after profile selection.
            // Re-use            : None
            // Method Parameters : string? returnUrl
            //                     - requested destination after lookup
            // Output Type       : IActionResult - student lookup form
            //

            var model = new ExistingStudentViewModel
            {
                ReturnUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : null
            };

            return View(model);
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
                ModelState.AddModelError(string.Empty,
                    $"No profile found for ID {model.StudentId.Value}. Check your ID and try again.");

                return View(model);
            } // end if

            HttpContext.Session.SetInt32("CurrentStudentId", student.Id);

            if (Url.IsLocalUrl(model.ReturnUrl))
            {
                return LocalRedirect(model.ReturnUrl);
            } // end if

            TempData[nameof(ProfileFound)] = student.Id;
            return RedirectToAction(nameof(ProfileFound));
        } // end method Existing

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ProfileCreated()
        {
            return ShowProfileConfirmation(nameof(ProfileCreated));
        } // end method ProfileCreated

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ProfileFound()
        {
            return ShowProfileConfirmation(nameof(ProfileFound));
        } // end method ProfileFound

        private IActionResult ShowProfileConfirmation(string viewName)
        {
            if (TempData[viewName] is int confirmedId &&
                HttpContext.Session.GetInt32("CurrentStudentId") == confirmedId &&
                _studentService.GetStudentById(confirmedId) is Student student)
            {
                return View(viewName, student);
            } // end if

            return RedirectToAction(nameof(Profile));
        } // end method ShowProfileConfirmation

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Profile()
        {
            int? studentId = HttpContext.Session.GetInt32("CurrentStudentId");
            if (studentId is int id && _studentService.GetStudentById(id) is Student student)
            {
                return View(student);
            } // end if

            HttpContext.Session.Remove("CurrentStudentId");
            return SelectProfile(nameof(Profile));
        } // end method Profile

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Edit()
        {
            if (!_environment.IsDevelopment())
            {
                return NotFound();
            } // end if

            int? studentId = HttpContext.Session.GetInt32("CurrentStudentId");
            if (studentId is int id && _studentService.GetStudentById(id) is Student student)
            {
                var model = new EditStudentViewModel
                {
                    OriginalStudentId = id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Province = student.Province,
                    APSScore = student.APSScore,
                    MathematicsMark = student.MathematicsMark,
                    PhysicalScienceMark = student.PhysicalScienceMark
                };

                return View(model);
            } // end if

            HttpContext.Session.Remove("CurrentStudentId");
            return SelectProfile(nameof(Edit));
        } // end method Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Edit(EditStudentViewModel model)
        {
            if (!_environment.IsDevelopment())
            {
                return NotFound();
            } // end if

            int? studentId = HttpContext.Session.GetInt32("CurrentStudentId");
            if (studentId is not int id)
            {
                TempData["SuccessMessage"] = "Select your profile again before editing. Your changes were not saved.";
                return SelectProfile(nameof(Edit));
            } // end if

            // A form opened before switching profiles must not update the newly selected profile.
            if (model.OriginalStudentId != id)
            {
                TempData["ProfileStatus"] = "The selected profile changed. Open Edit profile again to make changes.";
                return RedirectToAction(nameof(Profile));
            } // end if

            if (!ModelState.IsValid)
            {
                return View(model);
            } // end if

            var details = model.ToStudent();
            ValidateStudent(details);

            if (!ModelState.IsValid)
            {
                return View(model);
            } // end if

            if (!_studentService.UpdateStudent(id, details))
            {
                HttpContext.Session.Remove("CurrentStudentId");
                TempData["SuccessMessage"] = "The selected profile could not be found. Select an existing profile to edit.";
                return SelectProfile(nameof(Edit));
            } // end if

            TempData["ProfileStatus"] = "Your profile was updated.";
            return RedirectToAction(nameof(Profile));
        } // end method Edit

        private IActionResult SelectProfile(string nextAction)
        {
            return RedirectToAction(nameof(Existing), new { returnUrl = Url.Action(nextAction, "Students") });
        } // end method SelectProfile

        private void ValidateStudent(Student student)
        {
            foreach (var error in _studentValidator.Validate(student))
            {
                ModelState.AddModelError(string.Empty, error);
            } // end foreach
        } // end method ValidateStudent

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