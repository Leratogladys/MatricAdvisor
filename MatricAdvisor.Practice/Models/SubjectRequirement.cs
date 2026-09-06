namespace MatricAdvisor.Practice.Models
{
    public class SubjectRequirement
    {
        public string SubjectName {get; set;} = string.Empty;
        public int MinimumMark {get; set;}
        public bool isCompulsory {get; set;}
    }
}