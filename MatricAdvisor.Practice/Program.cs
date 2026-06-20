using System;
using static System.Console;
using MatricAdvisor.Practice.Models;
using System.Collections.Generic;


Student student = new Student
{
    Name = "Thabo",
    Surname = "Molefe",
    MatricYear = 2024,
    Subjects = new List<SubjectMark>
    {
        new SubjectMark {SubjectName = "Mathematics", Mark = 78},
        new SubjectMark {SubjectName = "English Home Language", Mark = 65},
        new SubjectMark {SubjectName = "Physical Sciences", Mark = 72},
        new SubjectMark {SubjectName = "Life Sciences", Mark = 55},
        new SubjectMark {SubjectName = "History", Mark = 60},
        new SubjectMark {SubjectName = "Accounting", Mark = 70},
        new SubjectMark {SubjectName = "Life Orientation", Mark = 80},
    }
};

//Call methods on the student object
WriteLine(student.GetSummary());

foreach(SubjectMark subject in student.Subjects)
{
    if(subject.SubjectName == "Life Orientation") continue;
    WriteLine($" {subject.SubjectName}: {subject.Mark}% = {subject.ApsPoints} points");
}



//Creating a university with a programme
University uct = new University
{
    Name = "University of Cape Town",
    City = "Cape Town",
    Province = "Western Cape",
    Website = "https://www.uct.ac.za",
    Programmes = new List<Programme>
    {
        new Programme
        {
            Name = "BSc Engineering",
            Faculty = "Engineering & the Built Environment",
            MinimumAps = 37,
            SubjectRequirements = new List<SubjectRequirement>
            {
                new SubjectRequirement
                {
                    SubjectName = "Mathematics",
                    MinimumMark = 70,
                    isCompulsory = true
                },

                new SubjectRequirement
                {
                    SubjectName = "Physical Sciences",
                    MinimumMark = 60,
                    isCompulsory = true,
                }
            }
        }
    }

    
};

//Check if Thabo qualifies for UCT Engineering
Programme engineeringProgramme = uct.Programmes[0];  //Gets the first item in list

bool meetsAps = student.TotalAps >= engineeringProgramme.MinimumAps;

WriteLine($"\nChecking qualification for: {uct.Name} - {engineeringProgramme.Name}");
WriteLine($"APS Required: {engineeringProgramme.MinimumAps} | Thabo's APS: {student.TotalAps}");
WriteLine($"Meets APS: {meetsAps}");


Dictionary<string, int> studentSubjects = new Dictionary<string, int>
{
  {"Mathematics", 88},
  {"English Home Language", 85},
  {"Physical Sciences", 92},
  {"Life Sciences", 95},
  {"History", 80},
  {"Accounting", 90},
  {"Life Orientation", 80}, //Won't be counted for most unis
};

//Calculate total APS
int totalApps = 0;
int subjectCount = 0;
int mathMark = 0;

foreach (KeyValuePair<string, int> subject in studentSubjects)
{
    //Skip Life Orientation - most Unis exclude it
    if (subject.Key == "Life Orientation")
    {
        WriteLine("Skipping Life Orientation (not counted for APS)");
        continue; //continue skips this loop iteration
    }

    int points = GetApsPoints(subject.Value); //subject.Value is the mark
    totalApps = points + mathMark;
    subjectCount++;

    WriteLine($"{subject.Key}: {subject.Value}% → {points} APS points");
}

WriteLine("---------------------");
WriteLine($"Total APS Score: {totalApps}");
WriteLine($"Subjects Counted: {subjectCount}");

int GetApsPoints(int mark)
{
    if (mark >= 80) return 7;
    if (mark >= 70) return 6;
    if (mark >= 60) return 5;
    if (mark >= 50) return 4;
    if (mark >= 40) return 3;
    if (mark >= 30) return 2;
    return 1;
}