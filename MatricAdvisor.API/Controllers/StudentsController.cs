using Microsoft.AspNetCore.Mvc;

namespace MatricAdvisor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        [HttpPost("calculate-aps")]
        public ActionResult<int> CalculateAps([FromBody] ApsRequest request)
        {
            int total = 0;
            foreach(var subject in request.Subjects)
            {
                total += GetApsPoints(subject.Mark);
            }

            return Ok(total);
        }

        private int GetApsPoints(int mark)
        {
            if (mark >= 80) return 7;
            if (mark >= 70) return 6;
            if (mark >= 60) return 5;
            if (mark >= 50) return 4;
            if (mark >= 40) return 3;
            if (mark >= 30) return 2;
            return 1;
        }
    }

    public class ApsRequest
    {
        public List<SubjectMarkInput> Subjects {get; set;} = new();
    }

    public class SubjectMarkInput
    {
        public string SubjectName {get; set;} = string.Empty;
        public int Mark {get; set;}
    }
}