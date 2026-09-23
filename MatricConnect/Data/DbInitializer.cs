// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Initializes the Matric Connect database with
//                   university and programme seed data.

using MatricConnect.Models;

namespace MatricConnect.Data
{
    public static class DbInitializer
    {
        public static void Seed(MatricConnectContext context)
        {
            //
            // Name              : void Seed(MatricConnectContext context)
            // Purpose           : Adds the required universities and
            //                     programmes to the database and updates
            //                     existing UFS programme information.
            // Re-use            : GetOrCreateUniversity(),
            //                     UpdateUfsProgrammeData()
            // Method Parameters : MatricConnectContext context
            //                     - database context used to access and
            //                       update Matric Connect data
            // Output Type       : None
            //

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
                        Name = "BSc (IT) majoring in Computer Science and Mathematics",
                        FieldOfStudy = "Computer Science",
                        RequiredAPS = 32,
                        RequiredMathematics = 70,
                        RequiredPhysicalScience = 60,
                        ApplicationDeadline = new DateTime(2026, 9, 30),
                        UniversityId = ufs.Id
                    },

                    new Programme
                    {
                        Name = "Bachelor of Computer Information Systems",
                        FieldOfStudy = "Computer Information Systems",
                        RequiredAPS = 33,
                        RequiredMathematics = 50,
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
            } // end if

            UpdateUfsProgrammeData(context, ufs.Id);
        } // end method

        // --------------------------------------------------
        // GET EXISTING UNIVERSITY OR CREATE IT
        // --------------------------------------------------

        private static University GetOrCreateUniversity(
            MatricConnectContext context,
            string name,
            string province,
            string website)
        {
            //
            // Name              : University GetOrCreateUniversity(
            //                     MatricConnectContext context,
            //                     string name,
            //                     string province,
            //                     string website)
            // Purpose           : Retrieves an existing university or
            //                     creates a new university when one with
            //                     the supplied name does not exist.
            // Re-use            : None
            // Method Parameters : MatricConnectContext context
            //                     - database context used to access universities
            //                     string name
            //                     - name of the university
            //                     string province
            //                     - province in which the university is located
            //                     string website
            //                     - website address of the university
            // Output Type       : University
            //                     - existing or newly created university
            //

            var university = context.Universities
                .FirstOrDefault(u => u.Name == name);

            if (university != null)
            {
                return university;
            } // end if

            university = new University
            {
                Name = name,
                Province = province,
                Website = website
            };

            context.Universities.Add(university);

            return university;
        } // end method

        private static void UpdateUfsProgrammeData( MatricConnectContext context, int universityId)
        {
            //
            // Name              : void UpdateUfsProgrammeData(
            //                     MatricConnectContext context,
            //                     int universityId)
            // Purpose           : Updates existing UFS programme records
            //                     with the current programme names and
            //                     admission requirements.
            // Re-use            : None
            // Method Parameters : MatricConnectContext context
            //                     - database context used to update programmes
            //                     int universityId
            //                     - ID of the University of the Free State
            // Output Type       : None
            //

            var computerScienceProgramme =
                context.Programmes.FirstOrDefault(
                    p => p.UniversityId == universityId &&
                    (p.Name == "BSc Computer Science" ||
                     p.Name == "BSc (IT) majoring in Computer Science and Mathematics"));

            if (computerScienceProgramme != null)
            {
                computerScienceProgramme.Name =
                    "BSc (IT) majoring in Computer Science and Mathematics";

                computerScienceProgramme.FieldOfStudy = "Computer Science";

                computerScienceProgramme.RequiredAPS = 32;
                computerScienceProgramme.RequiredMathematics = 70;
                computerScienceProgramme.RequiredPhysicalScience = 60;
                computerScienceProgramme.ApplicationDeadline =
                    new DateTime(2026, 9, 30);
            } // end if

            var bcisProgramme =
                context.Programmes.FirstOrDefault(
                    p => p.UniversityId == universityId &&
                    (p.Name == "BSc Information Technology" ||
                     p.Name == "Bachelor of Computer Information Systems"));

            if (bcisProgramme != null)
            {
                bcisProgramme.Name = "Bachelor of Computer Information Systems";

                bcisProgramme.FieldOfStudy = "Computer Information Systems";

                bcisProgramme.RequiredAPS = 33;
                bcisProgramme.RequiredMathematics = 50;
                bcisProgramme.RequiredPhysicalScience = null;
                bcisProgramme.ApplicationDeadline = new DateTime(2026, 9, 30);
            } // end if

            context.SaveChanges();
        } // end method
    } // end class DbInitializer
} // end namespace MatricConnect.Data