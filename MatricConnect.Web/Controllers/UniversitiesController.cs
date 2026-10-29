// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Handles web requests for university browsing.

using MatricConnect.Services;
using MatricConnect.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;


namespace MatricConnect.Web.Controllers
{
    public class UniversitiesController : Controller
    {
        private readonly UniversityService _universityService;

        public UniversitiesController(UniversityService universityService)
        {
            //
            // Name              : UniversitiesController(
            //                     UniversityService universityService)
            // Purpose           : Receives the university service through
            //                     dependency injection.
            // Re-use            : None
            // Method Parameters : UniversityService universityService
            //                     - service used to retrieve browsing data
            // Output Type       : None
            //

            _universityService = universityService;
        } // end constructor

        [HttpGet]
        public IActionResult Index(string? selectedProvince)
        {
            //
            // Name              : IActionResult Index(string? selectedProvince)
            // Purpose           : Displays province options and retrieves
            //                     universities for a valid province selection.
            // Re-use            : UniversityService.GetProvince(),
            //                     UniversityService.GetUniversitiesByProvince()
            // Method Parameters : string? selectedProvince
            //                     - province selected for searching
            // Output Type       : IActionResult
            //                     - university search view and its results
            //

            var model = new UniversitySearchViewModel
            {
                Provinces = _universityService.GetProvince(),
                SelectedProvince = selectedProvince
            };

            if (Request.Query.ContainsKey(nameof(UniversitySearchViewModel.SelectedProvince)))
            {
                if (string.IsNullOrWhiteSpace(selectedProvince) ||
                    !model.Provinces.Contains(selectedProvince))
                {
                    ModelState.AddModelError(
                        nameof(UniversitySearchViewModel.SelectedProvince),
                        "Select a province from the list.");
                } // end if
                else
                {
                    model.Universities = _universityService.GetUniversitiesByProvince(selectedProvince);
                } // end else
            } // end if

            return View(model);
       
        } // end method Index

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
                Programmes = _universityService.GetProgrammesByUniversity(id)
            };

            return View(model);
        } // end method Programmes
    } // end class UniversitiesController
} // end namespace MatricConnect.Web.Controllers