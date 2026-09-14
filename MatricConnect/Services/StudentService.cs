using MatricConnect.Data;
using MatricConnect.Models;

namespace MatricConnect.Services;

public class StudentService(MatricConnectContext context)
{
    public Student AddStudent(Student student)
    {
        context.Students.Add(student);
        context.SaveChanges();

        return student;
    }
}