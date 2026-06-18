using System;
using static System.Console;

Dictionary<string, int> studentSubjects = new Dictionary<string, int>
{
  {"Mathematics", 78},  
  {"English Home Language", 65},
  {"Physical Sciences", 72},
  {"Life Sciences", 55},
  {"History", 60},
  {"Accounting", 70},
  {"Life Orientation", 80}, //Won't be counted for most unis
};

//Calculate total APS
int totalApps = 0;
int subjectCount = 0;


foreach(KeyValuePair<string, int> subject in studentSubjects)
{
    //Skip Life Orientation - most Unis exclude it
    if(subject.Key == "Life Orientation")
    {
        WriteLine("Skipping Life Orientation (not counted for APS)");
        continue; //continue skips this loop iteration
    }

    int points = GetApsPoints(subject.Value); //subject.Value is the mark
    totalApps += points;                      
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