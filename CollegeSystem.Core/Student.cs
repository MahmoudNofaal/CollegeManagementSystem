using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.Core;

public class Student : Person
{

  public double GPA { get; set; }
  public string Grade { get; set; }
  public string Gender { get; set; }
  public string YearOfStudy { get; set; }
  public List<string> EnrolledCourses { get; set; } = new List<string>();

  public Student() : base()
  {

  }

  public Student(
                  string nationalId,
                  string code,
                  string name,
                  string password,
                  string email,
                  string department,
                  bool activate,
                  double gpa,
                  string grade,
                  string gender,
                  string yearOfStudy,
                  List<string> enrolledCourses
                )
    : base(nationalId, code, name, password, email, department, activate)
  {
    GPA = gpa;
    Gender = gender;
    Grade = grade;
    YearOfStudy = yearOfStudy;
    EnrolledCourses = enrolledCourses;
  }

  public override string ToString()
  {
    return base.ToString() +
           $"GPA: {GPA}\n" +
           $"Grade: {Grade}" +
           $"Gender: {Gender}\n" +
           $"Year Of Study: {YearOfStudy}\n" +
           $"Courses: {string.Join(",", EnrolledCourses)}";
  }

  [Obsolete]
  public override void PrintUser(string color)
  {
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ National Id:[/] [{color}]{NationalId}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Code:[/] [{color}]{Code}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Name:[/] [{color}]{Name}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Password:[/] [{color}]{Password}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Email:[/] [{color}]{Email}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Department:[/] [{color}]{Department}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Gpa:[/] [{color}]{GPA}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Grade:[/] [{color}]{Grade}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Gender:[/] [{color}]{Gender}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Year Of Study:[/] [{color}]{YearOfStudy}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]└─ Enrolled Courses :[/] [{color}]{string.Join(",", EnrolledCourses)}[/]\n");

  }
}