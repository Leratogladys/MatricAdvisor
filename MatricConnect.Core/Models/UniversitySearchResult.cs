// Project name : Matric Connect
// Purpose      : Holds the catalogue information shown on a university result card.

namespace MatricConnect.Models
{
    public record UniversitySearchResult(int Id, string Name, string Province, string Website,
        bool IsUniversityOfTechnology, int ProgrammeCount);
}
