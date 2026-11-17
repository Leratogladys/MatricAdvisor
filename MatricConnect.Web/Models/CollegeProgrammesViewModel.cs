using MatricConnect.Models;

namespace MatricConnect.Web.Models;

public sealed record CollegeProgrammesViewModel(TvetCollege College, IReadOnlyList<ProgrammeReference> Programmes);
