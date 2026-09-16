using MatricConnect.Data;
using MatricConnect.Models;
using MatricConnect.Services;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Xml;
using System.Xml.Linq;

var databasePath = Path.Combine(AppContext.BaseDirectory, "MatricConnect.db");

var options = new DbContextOptionsBuilder<MatricConnectContext>()
            .UseSqlite($"Data Source={databasePath}")
            .Options;

using var context = new MatricConnectContext(options);

DbInitializer.Seed(context);

var universityService = new UniversityService(context);
var eligibilityService = new EligibilityService();
var studentService = new StudentService(context);
var savedProgrammeService = new SavedProgrammeService(context);
var currentStudent = SelectStudentProfile(studentService);

static void RunApplication(
          UniversityService universityService,
          EligibilityService eligibilityService,
          SavedProgrammeService savedProgrammeService,
          Student currentStudent)
{
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
        }

        else if (option == "2")
        {
            ViewSavedProgrammes(
               savedProgrammeService,
               currentStudent);
        }
        else if (option == "3")
        {
            running = false;

            Console.WriteLine();
            Console.WriteLine("Exiting application.");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("invalid option.");

            Pause();
        }
    }
}

if (currentStudent != null)
{
    RunApplication(
        universityService,
        eligibilityService,
        savedProgrammeService,
        currentStudent);
}

static void SearchUniversities(
    UniversityService universityService,
    EligibilityService eligibilityService,
    SavedProgrammeService savedProgrammeService,
    Student currentStudent)

{
    Console.Clear();

    Console.WriteLine("======================================");
    Console.WriteLine("         SEARCH UNIVERSITIES");
    Console.WriteLine("======================================");
    Console.WriteLine();

    var provinces = universityService.GetProvince();

    for (int i = 0; i < provinces.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {provinces[i]}");
    }

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
    }

    string selectedProvince = provinces[selection - 1];

    var universities = universityService.GetUniversitiesByProvince(selectedProvince);

    Console.Clear();

    Console.WriteLine("==============================================");
    Console.WriteLine();
    Console.WriteLine($"            UNIVERSITIES IN {selectedProvince.ToUpper()}");
    Console.WriteLine();
    Console.WriteLine("==============================================");
    Console.WriteLine();

    for (int i = 0; i < universities.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {universities[i].Name}");
    }

    Console.WriteLine();
    Console.Write("Select a university: ");

    string? universityInput = Console.ReadLine();

    if (!int.TryParse(universityInput, out int universitySelection) ||
        universitySelection < 1 ||
        universitySelection > universities.Count)
    {
        Console.WriteLine("\nInvalid selection.");
        Pause();
        return;
    }

    var selectedUniversity = universities[universitySelection - 1];

    var programmes = universityService
        .GetProgrammesByUniversity(selectedUniversity.Id);

    Console.Clear();

    Console.WriteLine("===================================================");
    Console.WriteLine($"      PROGRAMMES AT {selectedUniversity.Name.ToUpper()}");
    Console.WriteLine("===================================================");
    Console.WriteLine();

    for (int i = 0; i < programmes.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {programmes[i].Name}");
    }

    Console.WriteLine();
    Console.Write("Select a programme: ");

    string? programmesInput = Console.ReadLine();

    if (!int.TryParse(programmesInput, out int programmesSelection) ||
        programmesSelection < 1 ||
        programmesSelection > programmes.Count)
    {
        Console.WriteLine("\nInvalid selection.");
        Pause();
        return;
    }

    Console.Clear();

    var selectedProgramme = programmes[programmesSelection - 1];

    var programmeDetails = universityService
        .GetProgrammeById(selectedProgramme.Id);

    Console.Clear();

    Console.WriteLine("==============================================");
    Console.WriteLine("             PROGRAMME DETAILS");
    Console.WriteLine("==============================================");
    Console.WriteLine();

    Console.WriteLine($"Programme: {programmeDetails?.Name}");
    Console.WriteLine($"University: {selectedUniversity.Name}");
    Console.WriteLine($"Field: {programmeDetails?.FieldOfStudy}");

    Console.WriteLine();
    Console.WriteLine("            Entry Requirements");
    Console.WriteLine("-------------------------------------------");
    Console.WriteLine($"Minimum APS: {programmeDetails?.RequiredAPS}");
    Console.WriteLine($"Mathematics: {programmeDetails?.RequiredMathematics:0}%");

    if (programmeDetails?.RequiredPhysicalScience == null)
    {
        Console.WriteLine("Physical Science: Not required");
    }
    else
    {
        Console.WriteLine($"Physical Science: {programmeDetails.RequiredPhysicalScience:0}%");
    }

    Console.WriteLine();
    Console.WriteLine($"Application Deadline: {programmeDetails?.ApplicationDeadline:dd MMMM yyyy}");
    Console.WriteLine();

    Console.WriteLine("------------------");
    Console.WriteLine("ELIGIBILITY RESULT");
    Console.WriteLine("------------------");

    var eligibilityIssues = eligibilityService.GetEligibilityIssues(
        currentStudent, programmeDetails!);

    Console.WriteLine();

    if (eligibilityIssues.Count == 0)
    {
        Console.WriteLine("You MEET the minimum requirements for this programme.");
    }
    else
    {
        Console.WriteLine();
        Console.WriteLine("You DO NOT meet the minimum requirements for this programme.");

        Console.WriteLine();
        Console.WriteLine("Requirements not met:");

        foreach (var issue in eligibilityIssues)
        {
            Console.WriteLine($"- {issue}");
        }
    }

    Console.WriteLine();
    Console.WriteLine("What would you like to do?");
    Console.WriteLine("1. Save this programme");
    Console.WriteLine("2. Return to Main Menu");

    Console.WriteLine();
    Console.WriteLine("Select an option");

    string? option = Console.ReadLine();
    
    if(option == "1")
    {
        bool programmeSaved = savedProgrammeService.SaveProgramme(
            currentStudent.Id, selectedProgramme.Id);

        Console.WriteLine();

        if(programmeSaved)
        {
            Console.WriteLine("Programme saved successfully.");
        }
        else
        {
            Console.WriteLine("This programme is already saved.");
        }
        Pause();
    }
    else if (option == "2")
    {
        return;
    }
    else
    {
        Console.WriteLine();
        Console.WriteLine("Invalid option.");

        Pause();
    }


    Pause();
}

static void ViewSavedProgrammes(SavedProgrammeService savedProgrammeService, Student currentStudent)
{
    var savedProgrammes = savedProgrammeService.GetSavedProgrammesByStudent(currentStudent.Id);

    Console.Clear();

    Console.WriteLine("==============================================");
    Console.WriteLine("              SAVED PROGRAMMES");
    Console.WriteLine("==============================================");
    Console.WriteLine();

    if (savedProgrammes.Count == 0)
    {
        Console.WriteLine("You have no saved programmes.");
    }
    else
    {
        for (int i = 0; i < savedProgrammes.Count; i++)
        {
            var savedProgramme = savedProgrammes[1];

            Console.WriteLine($"{i + 1}. {savedProgramme.Programme.Name}");
            Console.WriteLine($"{savedProgramme.Programme.University.Name}");

            Console.WriteLine();
        }
    }

    Pause();
}

static Student? SelectStudentProfile(StudentService studentService)
{
    Console.Clear();

    Console.WriteLine("=================================");
    Console.WriteLine("               MATRIC CONNECT");
    Console.WriteLine("=================================");
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

        Console.WriteLine("=====================================");
        Console.WriteLine("        CREATE STUDENT PROFILE");
        Console.WriteLine("=====================================");
        Console.WriteLine();

        Console.Write("Enter your first name: ");
        string? firstName = Console.ReadLine();

        Console.Write("Enter your last name: ");
        string? lastName = Console.ReadLine();

        Console.Write("Enter your province: ");
        string? province = Console.ReadLine();

        Console.Write("Enter your APS score: ");
        string? apsInput = Console.ReadLine();

        Console.Write("Enter your Mathematics marks: ");
        string? mathematicsInput = Console.ReadLine();

        Console.Write("Enter your Physical Science mark (leave blank if not applicable): ");
        string? physicalScienceInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(firstName) ||
            string.IsNullOrWhiteSpace(lastName) ||
            string.IsNullOrWhiteSpace(province))
        {
            Console.WriteLine();
            Console.WriteLine("First namr, last name and province are required.");

            Pause();
            return null;
        }

        if (!int.TryParse(apsInput, out int apsScore) ||
            !decimal.TryParse(mathematicsInput, out decimal mathematicsMark))
        {
            Console.WriteLine();
            Console.WriteLine("Invalid APS or Mathematics mark.");

            Pause();
            return null;
        }

        decimal? physicalScienceMark = null;

        if (!string.IsNullOrWhiteSpace(physicalScienceInput))
        {
            if (!decimal.TryParse(physicalScienceInput, out decimal physicalScienceValue))
            {
                Console.WriteLine();
                Console.WriteLine("Invalid Physical Science mark.");

                Pause();
                return null;
            }

            physicalScienceMark = physicalScienceValue;
        }

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
    }

    if (option == "2")
    {
        Console.WriteLine();
        Console.Write("Enter your Student ID: ");

        string? studentIdInput = Console.ReadLine();

        if (!int.TryParse(studentIdInput, out int studentId))
        {
            Console.WriteLine();
            Console.WriteLine("Invalid Student ID.");

            Pause();
            return null;
        }

        var student = studentService.GetStudentById(studentId);

        if (student == null)
        {
            Console.WriteLine();
            Console.WriteLine("No student profile was found with that ID.");

            Pause();
            return null;
        }

        Console.WriteLine();
        Console.WriteLine($"Welcome back, {student.FirstName} {student.LastName}.");

        Pause();
        return student;
    }

    if (option == "3")
    {
        return null;
    }

    Console.WriteLine();
    Console.WriteLine("Invalid option.");

    Pause();

    return null;
}


static void Pause()
{
    Console.WriteLine();
    Console.WriteLine("Press any key to continue....");
    Console.ReadKey();
}