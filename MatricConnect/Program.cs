// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Controls application startup, console navigation,
//                   student profile selection, university searching,
//                   eligibility checking, and saved programme management.

using MatricConnect.Data;
using MatricConnect.Models;
using MatricConnect.Services;
using Microsoft.EntityFrameworkCore;

namespace MatricConnect
{
    public static class Program
    {
        public static void Main()
        {
            //
            // Name              : void Main()
            // Purpose           : Configures the database, creates application
            //                     services, selects a student profile, and
            //                     starts the Matric Connect application.
            // Re-use            : GetConnectionString(),
            //                     MatricConnectContext(),
            //                     Seed(),
            //                     UniversityService(),
            //                     EligibilityService(),
            //                     StudentService(),
            //                     SavedProgrammeService(),
            //                     SelectStudentProfile(),
            //                     RunApplication()
            // Method Parameters : None
            // Output Type       : None
            //

            var options = new DbContextOptionsBuilder<MatricConnectContext>()
                    .UseSqlite(DatabaseConfiguration.GetConnectionString())
                    .Options;

            using var context = new MatricConnectContext(options);

            context.Database.Migrate();

            DbInitializer.Seed(context);

            var universityService = new UniversityService(context);

            var eligibilityService = new EligibilityService();

            var studentService = new StudentService(context);

            var savedProgrammeService = new SavedProgrammeService(context);

            var currentStudent = SelectStudentProfile(studentService);

            if (currentStudent != null)
            {
                RunApplication(
                    universityService,
                    eligibilityService,
                    savedProgrammeService,
                    currentStudent);
            } // end if
        } // end method

        private static void RunApplication(
            UniversityService universityService,
            EligibilityService eligibilityService,
            SavedProgrammeService savedProgrammeService,
            Student currentStudent)
        {
            //
            // Name              : void RunApplication(
            //                     UniversityService universityService,
            //                     EligibilityService eligibilityService,
            //                     SavedProgrammeService savedProgrammeService,
            //                     Student currentStudent)
            // Purpose           : Displays the main menu and controls the
            //                     primary navigation of the application.
            // Re-use            : SearchUniversities(),
            //                     ViewSavedProgrammes(),
            //                     Pause()
            // Method Parameters : UniversityService universityService
            //                     - service used to retrieve university data
            //                     EligibilityService eligibilityService
            //                     - service used to evaluate requirements
            //                     SavedProgrammeService savedProgrammeService
            //                     - service used to manage saved programmes
            //                     Student currentStudent
            //                     - student currently using the application
            // Output Type       : None
            //

            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("              MATRIC CONNECT");
                Console.WriteLine("========================================");
                Console.WriteLine();

                Console.WriteLine($"Welcome, {currentStudent.FirstName}");

                Console.WriteLine();

                Console.WriteLine("1. Search Universities");
                Console.WriteLine("2. View Saved Programmes");
                Console.WriteLine("3. Exit");

                Console.WriteLine();
                Console.Write("Select an option: ");

                string? option = Console.ReadLine();

                if (option == "1")
                {
                    SearchUniversities(
                        universityService,
                        eligibilityService,
                        savedProgrammeService,
                        currentStudent);
                } // end if
                else if (option == "2")
                {
                    ViewSavedProgrammes(
                        savedProgrammeService,
                        currentStudent);
                } // end else if
                else if (option == "3")
                {
                    running = false;

                    Console.WriteLine();
                    Console.WriteLine("Exiting application.");
                } // end else if
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("invalid option.");

                    Pause();
                } // end else
            } // end while
        } // end method

        private static void SearchUniversities(
            UniversityService universityService,
            EligibilityService eligibilityService,
            SavedProgrammeService savedProgrammeService,
            Student currentStudent)
        {
            //
            // Name              : void SearchUniversities(
            //                     UniversityService universityService,
            //                     EligibilityService eligibilityService,
            //                     SavedProgrammeService savedProgrammeService,
            //                     Student currentStudent)
            // Purpose           : Allows the student to select a province,
            //                     university, and programme, view programme
            //                     details, evaluate requirements, and save
            //                     the selected programme.
            // Re-use            : GetProvince(),
            //                     GetUniversitiesByProvince(),
            //                     GetProgrammesByUniversity(),
            //                     GetProgrammeById(),
            //                     GetEligibilityIssues(),
            //                     SaveProgramme(),
            //                     Pause()
            // Method Parameters : UniversityService universityService
            //                     - service used to retrieve university data
            //                     EligibilityService eligibilityService
            //                     - service used to evaluate requirements
            //                     SavedProgrammeService savedProgrammeService
            //                     - service used to save programmes
            //                     Student currentStudent
            //                     - student whose results are evaluated
            // Output Type       : None
            //

            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("         SEARCH UNIVERSITIES");
            Console.WriteLine("======================================");
            Console.WriteLine();

            var provinces =
                universityService.GetProvince();

            for (int i = 0; i < provinces.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {provinces[i]}");
            } // end for

            Console.WriteLine();
            Console.Write("Select a Province: ");

            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int selection) ||
                selection < 1 ||
                selection > provinces.Count)
            {
                Console.WriteLine("\nInvalid selection.");
                Pause();
                return;
            } // end if

            string selectedProvince = provinces[selection - 1];

            var universities = universityService.GetUniversitiesByProvince(selectedProvince);

            Console.Clear();

            Console.WriteLine("==============================================");
            Console.WriteLine();
            Console.WriteLine( $"     UNIVERSITIES IN {selectedProvince.ToUpper()}");
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine();

            for (int i = 0; i < universities.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {universities[i].Name}");
            } // end for

            Console.WriteLine();
            Console.Write("Select a university: ");

            string? universityInput = Console.ReadLine();

            if (!int.TryParse(universityInput, out int universitySelection) ||
                universitySelection < 1 || universitySelection > universities.Count)
            {
                Console.WriteLine("\nInvalid selection.");
                Pause();
                return;
            } // end if

            var selectedUniversity = universities[universitySelection - 1];

            var programmes = universityService.GetProgrammesByUniversity(selectedUniversity.Id);

            Console.Clear();

            Console.WriteLine("===================================================");
            Console.WriteLine($"      PROGRAMMES AT {selectedUniversity.Name.ToUpper()}");
            Console.WriteLine("===================================================");
            Console.WriteLine();

            for (int i = 0; i < programmes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {programmes[i].Name}");
            } // end for

            Console.WriteLine();
            Console.Write("Select a programme: ");

            string? programmesInput = Console.ReadLine();

            if (!int.TryParse(programmesInput, out int programmesSelection) ||
                programmesSelection < 1 || programmesSelection > programmes.Count)
            {
                Console.WriteLine("\nInvalid selection.");
                Pause();
                return;
            } // end if

            Console.Clear();

            var selectedProgramme = programmes[programmesSelection - 1];

            var programmeDetails = universityService.GetProgrammeById(selectedProgramme.Id);

            Console.Clear();

            Console.WriteLine("==============================================");
            Console.WriteLine("             PROGRAMME DETAILS");
            Console.WriteLine("==============================================");
            Console.WriteLine();

            Console.WriteLine($"Programme: {programmeDetails?.Name}");

            Console.WriteLine($"University: {selectedUniversity.Name}");

            Console.WriteLine($"Field: {programmeDetails?.FieldOfStudy}");

            Console.WriteLine();
            Console.WriteLine("Entry Requirements");
            Console.WriteLine("-----------------------------------------");

            Console.WriteLine($"Minimum APS: {programmeDetails?.RequiredAPS}");

            Console.WriteLine($"Mathematics: {programmeDetails?.RequiredMathematics:0}%");

            if (programmeDetails?.RequiredPhysicalScience == null)
            {
                Console.WriteLine("Physical Science: Not required");
            } // end if
            else
            {
                Console.WriteLine($"Physical Science: {programmeDetails.RequiredPhysicalScience:0}%");
            } // end else

            Console.WriteLine();

            Console.WriteLine($"Application Deadline: {programmeDetails?.ApplicationDeadline:dd MMMM yyyy}");

            Console.WriteLine();

            Console.WriteLine("Note: Universities may have additional admission requirements.");

            Console.WriteLine();

            Console.WriteLine("------------------");
            Console.WriteLine("ELIGIBILITY RESULT");
            Console.WriteLine("------------------");

            var eligibilityIssues = eligibilityService.GetEligibilityIssues(currentStudent, programmeDetails!);

            Console.WriteLine();

            if (eligibilityIssues.Count == 0)
            {
                Console.WriteLine("You MEET the requirements currently evaluated by Matric Connect.");
            } // end if
            else
            {
                Console.WriteLine();

                Console.WriteLine("You DO NOT meet all the requirements currently evaluated by Matric Connect.");

                Console.WriteLine();
                Console.WriteLine("Requirements not met:");

                foreach (var issue in eligibilityIssues)
                {
                    Console.WriteLine($"- {issue}");
                } // end foreach
            } // end else

            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Save this programme");
            Console.WriteLine("2. Return to Main Menu");

            Console.WriteLine();
            Console.Write("Select an option: ");

            string? option = Console.ReadLine();

            if (option == "1")
            {
                bool programmeSaved = savedProgrammeService.SaveProgramme( currentStudent.Id,
                        selectedProgramme.Id);

                Console.WriteLine();

                if (programmeSaved)
                {
                    Console.WriteLine("Programme saved successfully.");
                } // end if
                else
                {
                    Console.WriteLine("This programme is already saved.");
                } // end else

                Pause();
            } // end if
            else if (option == "2")
            {
                return;
            } // end else if
            else
            {
                Console.WriteLine();
                Console.WriteLine("Invalid option.");

                Pause();
            } // end else
        } // end method

        private static void ViewSavedProgrammes(
            SavedProgrammeService savedProgrammeService,
            Student currentStudent)
        {
            //
            // Name              : void ViewSavedProgrammes(
            //                     SavedProgrammeService savedProgrammeService,
            //                     Student currentStudent)
            // Purpose           : Displays programmes saved by the current
            //                     student and allows a saved programme
            //                     to be removed.
            // Re-use            : GetSavedProgrammesByStudent(),
            //                     RemoveSavedProgramme(),
            //                     Pause()
            // Method Parameters : SavedProgrammeService savedProgrammeService
            //                     - service used to retrieve and remove
            //                       saved programmes
            //                     Student currentStudent
            //                     - student whose saved programmes are shown
            // Output Type       : None
            //

            var savedProgrammes =
                savedProgrammeService.GetSavedProgrammesByStudent(
                    currentStudent.Id);

            Console.Clear();

            Console.WriteLine("==============================================");
            Console.WriteLine("              SAVED PROGRAMMES");
            Console.WriteLine("==============================================");
            Console.WriteLine();

            if (savedProgrammes.Count == 0)
            {
                Console.WriteLine("You have no saved programmes.");

                Pause();
                return;
            } // end if

            for (int i = 0; i < savedProgrammes.Count; i++)
            {
                var savedProgramme = savedProgrammes[i];

                Console.WriteLine($"{i + 1}. {savedProgramme.Programme.Name} - {savedProgramme.Programme.University.Name}");
                Console.WriteLine();
            } // end for

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Remove a saved programme");
            Console.WriteLine("2. Return to Main Menu");

            Console.WriteLine();
            Console.Write("Select an option: ");

            string? option = Console.ReadLine();

            if (option == "1")
            {
                Console.WriteLine();

                Console.Write("Enter the number of the programme you want to remove: ");

                string? selectionInput =
                    Console.ReadLine();

                if (!int.TryParse(selectionInput, out int selection) ||
                    selection < 1 || selection > savedProgrammes.Count)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid selection.");

                    Pause();
                    return;
                } // end if

                var selectedSavedProgramme = savedProgrammes[selection - 1];

                bool removed = savedProgrammeService.RemoveSavedProgramme(
                        currentStudent.Id, selectedSavedProgramme.ProgrammeId);

                Console.WriteLine();

                if (removed)
                {
                    Console.WriteLine("Programme removed successfully.");
                } // end if
                else
                {
                    Console.WriteLine("Programme could not be removed.");
                } // end else

                Pause();
            } // end if
            else if (option == "2")
            {
                return;
            } // end else if
            else
            {
                Console.WriteLine();
                Console.WriteLine("Invalid option.");

                Pause();
            } // end else
        } // end method

        private static Student? SelectStudentProfile(StudentService studentService)
        {
            //
            // Name              : Student? SelectStudentProfile(
            //                     StudentService studentService)
            // Purpose           : Allows the user to create a new student
            //                     profile, retrieve an existing profile,
            //                     or exit the application.
            // Re-use            : AddStudent(),
            //                     GetStudentById(),
            //                     Pause()
            // Method Parameters : StudentService studentService
            //                     - service used to create and retrieve
            //                       student profiles
            // Output Type       : Student?
            //                     - selected or newly created student profile
            //                     - null when the user chooses to exit
            //

            while (true)
            {
                Console.Clear();

                Console.WriteLine("===============================");
                Console.WriteLine("         MATRIC CONNECT");
                Console.WriteLine("===============================");
                Console.WriteLine();

                Console.WriteLine("1. Create New Student Profile");
                Console.WriteLine("2. Existing Student");
                Console.WriteLine("3. Exit");

                Console.WriteLine();
                Console.Write("Select an option: ");

                string? option = Console.ReadLine();

                if (option == "1")
                {
                    Console.Clear();

                    Console.WriteLine("=============================");
                    Console.WriteLine("   CREATE STUDENT PROFILE");
                    Console.WriteLine("=============================");
                    Console.WriteLine();

                    Console.Write("Enter your first name: ");
                    string? firstName = Console.ReadLine();

                    Console.Write("Enter your last name: ");
                    string? lastName = Console.ReadLine();

                    Console.Write("Enter your province: ");
                    string? province = Console.ReadLine();

                    Console.Write("Enter your APS score: ");
                    string? apsInput = Console.ReadLine();

                    Console.Write("Enter your Mathematics mark: ");
                    string? mathematicsInput = Console.ReadLine();

                    Console.Write("Enter your Physical Science mark (leave blank if not applicable): ");

                    string? physicalScienceInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(firstName) ||
                        string.IsNullOrWhiteSpace(lastName) ||
                        string.IsNullOrWhiteSpace(province))
                    {
                        Console.WriteLine();

                        Console.WriteLine("First name, last name and province are required.");

                        Pause();
                        continue;
                    } // end if

                    if (!int.TryParse(apsInput,out int apsScore) || apsScore < 0)
                    {
                        Console.WriteLine();

                        Console.WriteLine("APS score cannot be negative.");

                        Pause();
                        continue;
                    } // end if

                    if (!decimal.TryParse(mathematicsInput, out decimal mathematicsMark) ||
                        mathematicsMark < 0 || mathematicsMark > 100)
                    {
                        Console.WriteLine();

                        Console.WriteLine("Mathematics mark must be between 0 and 100.");

                        Pause();
                        continue;
                    } // end if

                    decimal? physicalScienceMark = null;

                    if (!string.IsNullOrWhiteSpace(
                            physicalScienceInput))
                    {
                        if (!decimal.TryParse(physicalScienceInput,
                            out decimal physicalScienceValue) ||
                            physicalScienceValue < 0 ||
                            physicalScienceValue > 100)
                        {
                            Console.WriteLine();

                            Console.WriteLine("Physical Science mark must be between 0 and 100.");

                            Pause();
                            continue;
                        } // end if

                        physicalScienceMark = physicalScienceValue;
                    } // end if

                    var student = new Student
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Province = province,
                        APSScore = apsScore,
                        MathematicsMark = mathematicsMark,
                        PhysicalScienceMark = physicalScienceMark
                    };

                    var savedStudent = studentService.AddStudent(student);

                    Console.WriteLine();

                    Console.WriteLine("Student profile created successfully.");

                    Console.WriteLine($"Your Student ID is {savedStudent.Id}");

                    Console.WriteLine();

                    Console.WriteLine("Keep this ID. You will use it to access your profile.");

                    Pause();

                    return savedStudent;
                } // end if

                if (option == "2")
                {
                    Console.WriteLine();
                    Console.Write("Enter your Student ID: ");

                    string? studentIdInput =
                        Console.ReadLine();

                    if (!int.TryParse(studentIdInput,
                        out int studentId) || studentId <= 0)
                    {
                        Console.WriteLine();

                        Console.WriteLine("Invalid Student ID.");

                        Pause();
                        continue;
                    } // end if

                    var student = studentService.GetStudentById(studentId);

                    if (student == null)
                    {
                        Console.WriteLine();

                        Console.WriteLine("No student profile was found with that ID.");

                        Pause();
                        continue;
                    } // end if

                    Console.WriteLine();

                    Console.WriteLine($"Welcome back, {student.FirstName} {student.LastName}!.");

                    Pause();

                    return student;
                } // end if

                if (option == "3")
                {
                    return null;
                } // end if

                Console.WriteLine();
                Console.WriteLine("Invalid option.");

                Pause();
            } // end while
        } // end method

        private static void Pause()
        {
            //
            // Name              : void Pause()
            // Purpose           : Pauses application execution until the
            //                     user presses a key.
            // Re-use            : None
            // Method Parameters : None
            // Output Type       : None
            //

            Console.WriteLine();
            Console.WriteLine("Press any key to continue....");

            Console.ReadKey();
        } // end method
    } // end class Program
} // end namespace MatricConnect
