//Controllers/UniversitiesController.cs

using Microsoft.AspNetCore.Mvc;

namespace MatricAdvisor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class UniversitiesController : ControllerBase
    {

        //GET: api/iniversities
        //This endpoint returns a list of all universities
        //ActionResult<T> returns either T or an HTTP error
        [HttpGet]
        public ActionResult<List<string>> GetAll()
        {
            //For now, we return fake data
            List<string> universities = new List<string>
            {
                "University of Cape Town",
                "University of Witwatersrand",
                "University of Pretoria",
                "University of Johannesburg",
            };

            //Ok() wraps the data in a 200 response (200 = success)
            return Ok(universities);
        }

        //GET: api/universities/1
        //The {id} == this endpoint accepts a variable in the URL
        [HttpGet("{id}")]
        public ActionResult<string> GetById(int id)
        {
            //This is a placeholder
            if(id == 1)
            {
                return Ok("University of Cape Town");
            }
            return NotFound("University not found");
        }
    }
}