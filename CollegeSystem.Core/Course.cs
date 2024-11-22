using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.Core;

public class Course
{
  public string CourseCode { get; set; }
  public string CourseName { get; set; }
  public string CourseDescription { get; set; }
  public string DoctorCode { get; set; }
  public string ExamCode { get; set; }
  public string Department { get; set; }
  public int NoStudents { get; set; } = 0;
  public int NoOfHours { get; set; } = 1;


  public Course(
                 string courseCode,
                 string courseName,
                 string courseDescription,
                 string doctorCode,
                 string examCode,
                 string department,
                 int noStudents,
                 int noOfHours
               )
  {
    CourseCode = courseCode;
    CourseName = courseName;
    CourseDescription = courseDescription;
    DoctorCode = doctorCode;
    ExamCode = examCode;
    Department = department;
    NoStudents = noStudents;
    NoOfHours = noOfHours;
  }



  public void PrintCourse(string color)
  {
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Course Code:[/] [{color}]{CourseCode}[/]\n");
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Course Name:[/] [{color}]{CourseName}[/]\n");
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Course Description:[/] [{color}]{CourseDescription}[/]\n");
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Doctor Code:[/] [{color}]{DoctorCode}[/]\n");
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Exam Code:[/] [{color}]{ExamCode}[/]\n");
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Department:[/] [{color}]{Department}[/]\n");
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]└─ Number Of Students:[/] [{color}]{NoStudents}[/]\n");
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]└─ Number Of Hours:[/] [{color}]{NoOfHours}[/]\n");

  }
}
