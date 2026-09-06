using MatricConnect.Models;

namespace MatricConnect.Data;

public static class DbInitializer
{
    public static void Seed(MatricConnectContext context)
    {
        // --------------------------------------------------
        // UNIVERSITIES
        // --------------------------------------------------

        var ufs = GetOrCreateUniversity(
            context,
            "University of the Free State",
            "Free State",
            "https://www.ufs.ac.za");

        var uj = GetOrCreateUniversity(
            context,
            "University of Johannesburg",
            "Gauteng",
            "https://www.uj.ac.za");

        var up = GetOrCreateUniversity(
            context,
            "University of Pretoria",
            "Gauteng",
            "https://www.up.ac.za");

        var wits = GetOrCreateUniversity(
            context,
            "University of the Witwatersrand",
            "Gauteng",
            "https://www.wits.ac.za");

        context.SaveChanges();


        // --------------------------------------------------
        // PROGRAMMES
        // --------------------------------------------------

        if (!context.Programmes.Any())
        {
            var programmes = new List<Programme>
            {
                // UFS
                new Programme
                {
                    Name = "BSc Computer Science",
                    FieldOfStudy = "Computer Science",
                    RequiredAPS = 30,
                    RequiredMathematics = 60,
                    RequiredPhysicalScience = null,
                    ApplicationDeadline = new DateTime(2026, 9, 30),
                    UniversityId = ufs.Id
                },

                new Programme
                {
                    Name = "BSc Information Technology",
                    FieldOfStudy = "Information Technology",
                    RequiredAPS = 30,
                    RequiredMathematics = 60,
                    RequiredPhysicalScience = null,
                    ApplicationDeadline = new DateTime(2026, 9, 30),
                    UniversityId = ufs.Id
                },

                // UJ
                new Programme
                {
                    Name = "BSc Information Technology",
                    FieldOfStudy = "Information Technology",
                    RequiredAPS = 30,
                    RequiredMathematics = 70,
                    RequiredPhysicalScience = null,
                    ApplicationDeadline = new DateTime(2026, 10, 31),
                    UniversityId = uj.Id
                },

                new Programme
                {
                    Name = "BSc Computer Science and Informatics",
                    FieldOfStudy = "Computer Science",
                    RequiredAPS = 30,
                    RequiredMathematics = 70,
                    RequiredPhysicalScience = null,
                    ApplicationDeadline = new DateTime(2026, 10, 31),
                    UniversityId = uj.Id
                },

                new Programme
                {
                    Name = "BSc Computer Science and Informatics Specialising in AI",
                    FieldOfStudy = "Artificial Intelligence",
                    RequiredAPS = 34,
                    RequiredMathematics = 80,
                    RequiredPhysicalScience = null,
                    ApplicationDeadline = new DateTime(2026, 10, 31),
                    UniversityId = uj.Id
                },

                // UP
                new Programme
                {
                    Name = "BSc Computer Science",
                    FieldOfStudy = "Computer Science",
                    RequiredAPS = 30,
                    RequiredMathematics = 70,
                    RequiredPhysicalScience = null,
                    ApplicationDeadline = new DateTime(2026, 6, 30),
                    UniversityId = up.Id
                },

                new Programme
                {
                    Name = "BSc Information Technology in Information and Knowledge Systems",
                    FieldOfStudy = "Information Technology",
                    RequiredAPS = 30,
                    RequiredMathematics = 70,
                    RequiredPhysicalScience = null,
                    ApplicationDeadline = new DateTime(2026, 6, 30),
                    UniversityId = up.Id
                },

                // Wits
                new Programme
                {
                    Name = "BSc Computer Science",
                    FieldOfStudy = "Computer Science",
                    RequiredAPS = 44,
                    RequiredMathematics = 70,
                    RequiredPhysicalScience = null,
                    ApplicationDeadline = new DateTime(2026, 9, 30),
                    UniversityId = wits.Id
                },

                new Programme
                {
                    Name = "BSc Computational and Applied Mathematics",
                    FieldOfStudy = "Computational Mathematics",
                    RequiredAPS = 44,
                    RequiredMathematics = 70,
                    RequiredPhysicalScience = null,
                    ApplicationDeadline = new DateTime(2026, 9, 30),
                    UniversityId = wits.Id
                }
            };

            context.Programmes.AddRange(programmes);
            context.SaveChanges();
        }
    }


    // --------------------------------------------------
    // GET EXISTING UNIVERSITY OR CREATE IT
    // --------------------------------------------------

    private static University GetOrCreateUniversity(
        MatricConnectContext context,
        string name,
        string province,
        string website)
    {
        var university = context.Universities
            .FirstOrDefault(u => u.Name == name);

        if (university != null)
        {
            return university;
        }

        university = new University
        {
            Name = name,
            Province = province,
            Website = website
        };

        context.Universities.Add(university);

        return university;
    }
}