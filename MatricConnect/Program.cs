using MatricConnect.Data;
using Microsoft.EntityFrameworkCore;
using MatricConnect.Services;
using MatricConnect.Models;

var databasePath = Path.Combine(
    AppContext.BaseDirectory,
    "MatricConnect.db");

var options = new DbContextOptionsBuilder<MatricConnectContext>()
    .UseSqlite($"Data Source={databasePath}")
    .Options;

using var context = new MatricConnectContext(options);

DbInitializer.Seed(context);

var universityService = new UniversityService(context);
var eligibilityService = new EligibilityService();

RunApplication(universityService, eligibilityService);

static void RunApplication(UniversityService universityService,EligibilityService eligibilityService)
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

    foreach (var university in universities)
    {
        Console.WriteLine($"{university.Id}. {university.Name}");
    }

    Console.WriteLine();
    Console.Write("Select a university: ");
    
    string? universityInput = Console.ReadLine();

    if(!int.TryParse(universityInput, out int universitySelection) || 
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
        Console.WriteLine($"{ i + 1}. {programmes[i].Name}");
    }

    Console.WriteLine();
    Console.WriteLine("Select a programme: ");

    string? programmesInput = Console.ReadLine();

    if(!int.TryParse(programmesInput, out int programmesSelection) 
            || programmesSelection < 1
            || programmesSelection > programmes.Count)
    {
        Console.WriteLine("\nInvalid selection.");
        Pause();
        return;
    }

    var selectedProgramme = programmes[programmesSelection - 1];
    var programmeDetails = universityService.GetProgrammeById(selectedProgramme.Id);

    Console.Clear();

    Console.WriteLine("==============================================");
    Console.WriteLine("             STUDENT INFORMATION ");
    Console.WriteLine("==============================================");
    Console.WriteLine();


    Console.Write("Enter your APS score: ");
    string? apsInput = Console.ReadLine();

    Console.Write("Enter your Mathematics mark: ");
    string? mathematicsInput = Console.ReadLine();


    Console.Write("Enter your Physical Science mark: ");
    string? physicalScienceInput = Console.ReadLine();

    if(!int.TryParse(apsInput, out int apsScore) ||
       !decimal.TryParse(mathematicsInput, out decimal mathematicsMark) ||
       !decimal.TryParse(physicalScienceInput, out decimal physicalScienceMark))
    {
        Console.WriteLine();
        Console.WriteLine("Invalid input. Please enter numeric values.");
        Pause();
        return;
    }

    var student = new Student
    {
        APSScore = apsScore,
        MathematicsMark = mathematicsMark,
        PhysicalScienceMark = physicalScienceMark
    };


    Console.Clear();

    Console.WriteLine("==============================================");
    Console.WriteLine("             PROGRAMME DETAILS ");
    Console.WriteLine("==============================================");
    Console.WriteLine();


    Console.WriteLine($"Programme: {programmeDetails?.Name}");
    Console.WriteLine($"University: {selectedUniversity.Name}");
    Console.WriteLine($"Field: {programmeDetails?.FieldOfStudy}");

    Console.WriteLine();
    Console.WriteLine("Entry Requirements");
    Console.WriteLine("-------------------");
    Console.WriteLine($"Minimum APS: {programmeDetails?.RequiredAPS}");
    Console.WriteLine($"Mathematics: {programmeDetails?.RequiredMathematics:0}%");

    if (programmeDetails?.RequiredPhysicalScience == null)
    {
        Console.WriteLine("Physical Science: Not required");
    }
    else
    {
        Console.WriteLine($"Physical Science: {programmeDetails.RequiredPhysicalScience}%");
    }

    Console.WriteLine();
    Console.WriteLine($"Application Deadline: {programmeDetails?.ApplicationDeadline:dd MMMM yyyy}");

    Pause();
}

static void Pause()
{
    Console.WriteLine();
    Console.WriteLine("Press any key to continue....");
    Console.ReadKey();
}