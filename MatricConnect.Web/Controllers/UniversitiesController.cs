// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Handles web requests for university browsing.

using MatricConnect.Services;
using MatricConnect.Web.Models;
using Microsoft.AspNetCore.Mvc;


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
    } // end class UniversitiesController
} // end namespace MatricConnect.Web.Controllers