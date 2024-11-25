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
  public double Marks { get; set; }
  public string Grade { get; set; }
  public string Gender { get; set; }
  public string YearOfStudy { get; set; }
  public int NoOfCreditHours { get; set; }
  public int NumberOfCoursesHours { get; set; }
  public List<string> EnrolledCoursesCodes { get; set; } = new List<string>();

  public Student() : base()
  {

  }
  public Student(
                  string code,
                  string name,
                  string password,
                  string email,
                  string department,
                  bool activate,
                  double gpa,
                  double marks,
                  string grade,
                  string gender,
                  string yearOfStudy,
                  int noOfCreditHours,
                  int numberOfCoursesHoursAccepted,
                  List<string> enrolledCourses
                )
    : base(code, name, password, email, department, activate)
  {
    GPA = gpa;
    Gender = gender;
    Marks = marks;
    Grade = grade;
    YearOfStudy = yearOfStudy;
    NoOfCreditHours = noOfCreditHours;
    NumberOfCoursesHours = numberOfCoursesHoursAccepted;
    EnrolledCoursesCodes = enrolledCourses;
  }

  public override string ToString()
  {
    return base.ToString() +
           $"GPA: {GPA}\n" +
           $"Grade: {Marks}" +
           $"Gender: {Gender}\n" +
           $"Year Of Study: {YearOfStudy}\n" +
           $"Courses: {string.Join(",", EnrolledCoursesCodes)}";
  }


  public override void PrintUser(string color)
  {
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Code:[/] [{color}]{Code}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Full Name:[/] [{color}]{FullName}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Password:[/] [{color}]{Password}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Email:[/] [{color}]{Email}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Department:[/] [{color}]{Department}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Gender:[/] [{color}]{Gender}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Year Of Study:[/] [{color}]{YearOfStudy}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]└─ No. Of Credit Hours:[/] [{color}]{NoOfCreditHours}[/]\n");
  }
}