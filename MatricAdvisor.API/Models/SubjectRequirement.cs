using MatricAdvisor.API.Models;

namespace MatricAdvisor.API.Models
{
    public class SubjectRequirement
    {
        public int Id {get; set;} 
        public string SubjectName {get; set;} = string.Empty;
        public int MinimumMark {get; set;}
        public bool IsCompulsory {get; set;}

        //Foreign Key linking this requirement to a Programme
        public int ProgrammeId {get; set;}
        public Programme Programme {get; set;} = null!;
    }
}