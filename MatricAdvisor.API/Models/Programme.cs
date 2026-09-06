// Models/Programme.cs
using MatricAdvisor.API.Models;

namespace MatricAdvisor.API.Models
{
    public class Programme
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Faculty { get; set; } = string.Empty;
        public int MinimumAps { get; set; }
        public string Duration { get; set; } = string.Empty;  // e.g. "4 years"

        // Foreign key — this column stores the Id of the related University
        
        public int UniversityId { get; set; }

        // Navigation property — gives you easy access to the full University object
        public University University { get; set; } = null!;

        // A programme can require specific subjects
        public List<SubjectRequirement> SubjectRequirements { get; set; } = new List<SubjectRequirement>();
    }
}