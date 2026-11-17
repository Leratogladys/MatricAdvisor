// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Handles web requests for university browsing.

using MatricConnect.Infrastructure.Repositories;
using MatricConnect.Services;
using MatricConnect.Web.Models;
using Microsoft.AspNetCore.Mvc;
using MatricConnect.Models;
using Microsoft.AspNetCore.Http;


namespace MatricConnect.Web.Controllers
{
    public class UniversitiesController : Controller
    {
        private readonly UniversityService _universityService;
        private readonly ProgrammeReferenceRepository _programmeReferences;
        private readonly TvetCollegeService _tvetCollegeService;
        private readonly EligibilityService _eligibilityService;
        private readonly StudentService _studentService;

        public UniversitiesController(UniversityService universityService, EligibilityService eligibilityService,
            StudentService studentService, TvetCollegeService tvetCollegeService, ProgrammeReferenceRepository programmeReferences)
        {
            //
            // Name              : UniversitiesController(
            //                     UniversityService universityService,
            //                     EligibilityService eligibilityService,
            //                     StudentService studentService)
            // Purpose           : Receives browsing, eligibility, and student
            //                     services through dependency injection.
            // Re-use            : None
            // Method Parameters : UniversityService universityService
            //                     - retrieves universities and programmes
            //                     EligibilityService eligibilityService
            //                     - evaluates programme requirements
            //                     StudentService studentService
            //                     - retrieves the selected student profile
            // Output Type       : None
            //
            _universityService = universityService;
            _tvetCollegeService = tvetCollegeService;
            _programmeReferences = programmeReferences;
            _eligibilityService = eligibilityService;
            _studentService = studentService;
        } // end constructor

        [HttpGet]
        public IActionResult Index(string? selectedProvince, string? institutionType)
        {
            var model = new UniversitySearchViewModel
            {
                Provinces = _universityService.GetProvince(),
                SelectedProvince = selectedProvince,
                InstitutionType = institutionType ?? UniversitySearchViewModel.UniversitiesType
            };

            if (model.InstitutionType is not (UniversitySearchViewModel.UniversitiesType or UniversitySearchViewModel.TvetCollegesType))
            {
                ModelState.AddModelError(nameof(UniversitySearchViewModel.InstitutionType),
                    "Select an institution type from the list.");
            }

            if (Request.Query.ContainsKey(nameof(UniversitySearchViewModel.SelectedProvince)))
            {
                if (selectedProvince is null || !model.Provinces.Contains(selectedProvince))
                {
                    ModelState.AddModelError(nameof(UniversitySearchViewModel.SelectedProvince),
                        "Select a province from the list.");
                }
                else if (ModelState.IsValid)
                {
                    if (model.ShowTvetColleges)
                    {
                        model.Colleges = _tvetCollegeService.GetByProvince(selectedProvince);
                        model.CollegeProgrammeCounts = _programmeReferences.CountByCollege(selectedProvince);
                    }
                    else
                    {
                        model.Universities = _universityService.GetUniversitySearchResults(selectedProvince);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Programmes(int id)
        {
            //
            // Name              : IActionResult Programmes(int id)
            // Purpose           : Displays programmes offered by a university.
            // Re-use            : UniversityService.GetUniversityById(),
            //                     UniversityService.GetProgrammesByUniversity()
            // Method Parameters : int id
            //                     - ID of the selected university
            // Output Type       : IActionResult
            //                     - programme list view, or a 404 response
            //                       when the university does not exist
            //

            if(!ModelState.IsValid || id <= 0)
            {
                return NotFound();

            } // end if 

            var universty = _universityService.GetUniversityById(id);

            if(universty == null)
            {
                return NotFound();
            } // end if

            var model = new UniversityProgrammesViewModel
            {
                University = universty,
                Programmes = _universityService.GetProgrammesByUniversity(id),
                References = _programmeReferences.ForUniversity(id)
            };

            return View(model);
        } // end method Programmes

        [HttpGet]
        public IActionResult CollegeProgrammes(int id)
        {
            if (!ModelState.IsValid || id <= 0 || _tvetCollegeService.GetById(id) is not { } college)
                return NotFound();

            return View(new CollegeProgrammesViewModel(college, _programmeReferences.ForCollege(id)));
        }

        [HttpGet]
        public IActionResult ReferenceDetails(int id)
        {
            if (!ModelState.IsValid || id <= 0 || _programmeReferences.Find(id) is not { } reference ||
                (reference.University == null && reference.TvetCollege == null))
                return NotFound();

            ProgrammeSaveOutcome? saveOutcome = null;
            if (HttpContext.Session.GetInt32("CurrentStudentId") is int studentId &&
                TempData[ProgrammeSaveFeedback.ReferenceKey(studentId, id)] is string feedback &&
                Enum.TryParse<ProgrammeSaveOutcome>(feedback, out var parsed))
            {
                saveOutcome = parsed;
            }

            return View(new ProgrammeReferenceDetailsViewModel(reference, saveOutcome,
                ReferenceComparisonRules.Read(reference.ComparisonRulesJson)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult CompareReference([FromRoute] int id, [FromForm] int? route, [FromForm] decimal?[] values)
        {
            if (id <= 0 || _programmeReferences.Find(id) is not { } reference ||
                (reference.University == null && reference.TvetCollege == null) ||
                ReferenceComparisonRules.Read(reference.ComparisonRulesJson) is not { } rules ||
                route is not int routeIndex || routeIndex < 0 || routeIndex >= rules.Routes.Length)
                return NotFound();

            var minimums = rules.Routes[routeIndex].Minimums;
            if (values.Length != minimums.Length)
                ModelState.AddModelError(nameof(values), "Complete every field in this entry route.");
            for (var i = 0; i < Math.Min(values.Length, minimums.Length); i++)
            {
                if (values[i] is not decimal value || !minimums[i].IsValidValue(value))
                    ModelState.AddModelError(nameof(values), $"Enter a valid value for {minimums[i].Label}.");
            }

            var submission = new ReferenceComparisonSubmission(routeIndex, values, ModelState.IsValid);
            return View("ReferenceDetails", new ProgrammeReferenceDetailsViewModel(reference, null, rules, submission));
        }

        [HttpGet]
        public IActionResult ProgrammeDetails(int id)
        {
            //
            // Name              : IActionResult ProgrammeDetails(int id)
            // Purpose           : Displays a programme and its entry requirements.
            // Re-use            : UniversityService.GetProgrammeById(),
            //                     UniversityService.GetUniversityById()
            //                     StudentService.GetStudentById()
            // Method Parameters : int id
            //                     - ID of the selected programme
            // Output Type       : IActionResult
            //                     - programme details view, or a 404 response
            //                       when the programme or university is not found
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

            var university = _universityService.GetUniversityById(programme.UniversityId);
            if(university == null)
            {
                return NotFound();
            } // end if

            var model = new ProgrammeDetailsViewModel
            {
                Programme = programme,
                University = university
            };

            int? studentId = HttpContext.Session.GetInt32("CurrentStudentId");

            if(studentId.HasValue)
            {
                var student = _studentService.GetStudentById(studentId.Value);

                if(student != null)
                {
                    var saveKey = ProgrammeSaveFeedback.Key(student.Id, programme.Id);
                    if (TempData[saveKey] is string feedback &&
                        Enum.TryParse<ProgrammeSaveOutcome>(feedback, out var outcome))
                    {
                        model.SaveOutcome = outcome;
                    }

                    model.EligibilityInput = new EligibilityInputViewModel
                    {
                        APSScore = student.APSScore,
                        MathematicsMark = student.MathematicsMark,
                        PhysicalScienceMark = student.PhysicalScienceMark
                    };
                } // end if
                else
                {
                    HttpContext.Session.Remove("CurrentStudentId");
                }
            }

            return View(model);
        } // end method ProgrammeDetails

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult CompareResults([FromRoute] int id,
            [Bind(Prefix = nameof(ProgrammeDetailsViewModel.Comparison))] ProgrammeComparisonInputViewModel input)
        {
            if (id <= 0 || _universityService.GetProgrammeById(id) is not { } programme ||
                _universityService.GetUniversityById(programme.UniversityId) is not { } university ||
                ProgrammeComparisonRules.For(university, programme.LatestVerifiedIntake) is not { } rules)
            {
                return NotFound();
            }

            var model = new ProgrammeDetailsViewModel
            {
                Programme = programme,
                University = university,
                Comparison = input
            };

            var subjectRule = rules.Subjects.FirstOrDefault(option => option.Subject == input.Subject);
            if (input.Subject.HasValue && subjectRule == null)
            {
                ModelState.AddModelError($"{nameof(model.Comparison)}.{nameof(input.Subject)}", "Choose a subject from the list.");
            }

            if (input.Score is int score && (score < 0 || score > rules.MaximumScore))
            {
                ModelState.AddModelError($"{nameof(model.Comparison)}.{nameof(input.Score)}",
                    $"Enter a {rules.ScoreLabel} between 0 and {rules.MaximumScore}.");
            }

            foreach (var requirement in rules.RequiredMarks)
            {
                var field = $"{nameof(model.Comparison)}.{nameof(input.RequiredMarks)}[{(int)requirement.Subject}]";
                if (ModelState.TryGetValue(field, out var state) && state.Errors.Count > 0)
                {
                    continue;
                }

                var requiredMark = input.RequiredMarks.GetValueOrDefault(requirement.Subject);
                if (!requiredMark.HasValue || requiredMark is < 0 or > 100)
                {
                    ModelState.AddModelError(field, $"Enter your {requirement.Label} mark between 0 and 100.");
                }
            }

            if (ModelState.IsValid && subjectRule != null)
            {
                model.ComparisonResult = rules.Compare(subjectRule, input.Score!.Value,
                    input.Mark!.Value, input.RequiredMarks);
            }

            return View("ProgrammeDetails", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckEligibility( int id,
        [Bind(Prefix = "EligibilityInput")] EligibilityInputViewModel input)
        {
            //
            // Name              : IActionResult CheckEligibility(
            //                     int id, EligibilityInputViewModel input)
            // Purpose           : Validates academic input and evaluates it
            //                     against the selected programme's requirements.
            // Re-use            : UniversityService.GetProgrammeById(),
            //                     UniversityService.GetUniversityById(),
            //                     EligibilityService.EvaluateRequirements()
            // Method Parameters : int id
            //                     - ID of the programme being evaluated
            //                     EligibilityInputViewModel input
            //                     - academic results submitted through the form
            // Output Type       : IActionResult
            //                     - programme details with validation or results,
            //                       or a 404 response for an invalid programme
            //

            if (id <= 0 || ModelState.GetFieldValidationState(nameof(id)) == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return NotFound();
            } // end if

            var programme = _universityService.GetProgrammeById(id);

            if (programme == null)
            {
                return NotFound();
            } // end if

            // Verified intakes are shown with their official criteria. The stored profile APS
            // does not identify its scoring system, so do not use it for this comparison.
            if (programme.LatestVerifiedIntake != null)
            {
                return RedirectToAction(nameof(ProgrammeDetails), new { id });
            } // end if

            var university = _universityService
                .GetUniversityById(programme.UniversityId);

            if (university == null)
            {
                return NotFound();
            } // end if

            var model = new ProgrammeDetailsViewModel
            {
                Programme = programme,
                University = university,
                EligibilityInput = input
            };

            if (!ModelState.IsValid)
            {
                return View("ProgrammeDetails", model);
            } // end if

            var student = new Student
            {
                APSScore = input.APSScore!.Value,
                MathematicsMark = input.MathematicsMark!.Value,
                PhysicalScienceMark = input.PhysicalScienceMark
            };

            model.EligibilityRequirements = _eligibilityService.EvaluateRequirements(student, programme);

            return View("ProgrammeDetails", model);
        } // end method CheckEligibility
    } // end class UniversitiesController
} // end namespace MatricConnect.Web.Controllers