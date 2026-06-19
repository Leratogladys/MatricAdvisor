namespace MatricAdvisor.Practice.Models
{
    public class Programme
    {
        public string Name {get; set;} = string.Empty;
        public string Faculty {get; set;} = string.Empty;
        public int MinimumAps {get; set;} 

        public List<SubjectRequirement> SubjectRequirements {get; set;} = new List<SubjectRequirement>();

    }
}