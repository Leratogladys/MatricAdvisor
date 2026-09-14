using MatricConnect.Models;

namespace MatricConnect.Services;

public class EligibilityService
{
    public List<string> GetEligibilityIssues(
        Student student,
        Programme programme)
    {
        var issues = new List<string>();

        if (student.APSScore < programme.RequiredAPS)
        {
            issues.Add($"Your APS score ({student.APSScore}) " +
                       $"is below the required minimum APS ({programme.RequiredAPS}).");
        }

        if (student.MathematicsMark < programme.RequiredMathematics)
        {
            issues.Add( $"Your Mathematics mark ({student.MathematicsMark:0}) " +
                        $"is below the required minimum mark ({programme.RequiredMathematics:0}).");
        }

        if (programme.RequiredPhysicalScience.HasValue)
        {
            if (student.PhysicalScienceMark == null)
            {
                issues.Add("A Physical Science mark is required for this programme.");
            }
            else if (student.PhysicalScienceMark <
                     programme.RequiredPhysicalScience.Value)
            {
                issues.Add($"Your Physical Science mark ({student.PhysicalScienceMark:0})" +
                           $" is below the required minimum mark ({programme.RequiredPhysicalScience.Value:0}).");
            }
        }

        return issues;
    }

    public bool IsEligible(Student student, Programme programme)
    {
        return GetEligibilityIssues(student, programme).Count == 0;
    }
}