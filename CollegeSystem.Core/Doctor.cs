using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace CollegeSystem.Core;


public class Doctor : Person
{
  public DateTime DateOfHire { get; set; }

  public List<string> CoursesTaughtCodes { get; set; } = new List<string>();
  public List<string> DoctorExamsCodes { get; set; } = new List<string>();

  public Doctor() : base()
  { }

  public Doctor(
                 string nationalId,
                 string code,
                 string name,
                 string password, 
                 string email,
                 string department,
                 bool activate,
                 DateTime dateOfHire,
                 List<string> coursesTaughtCodes,
                 List<string> doctorExamsCodes
               )
    : base(nationalId, code, name, password, email, department,activate)
  {
    DateOfHire = dateOfHire;
    CoursesTaughtCodes = coursesTaughtCodes;
    DoctorExamsCodes = doctorExamsCodes;
  }


  public override string ToString()
  {
    return base.ToString() +
           $"Date Of Hire: {DateOfHire}\n" +
           $"Courses Taught: {string.Join(",", CoursesTaughtCodes)}\n" +
           $"Exams of Doctor: {string.Join(",", DoctorExamsCodes)}\n";
  }

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
    AnsiConsole.Markup($"[bold]├─ Date Of Hire:[/] [{color}]{DateOfHire}[/]\n");
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Course Taught Codes:[/] [{color}]{string.Join(",", CoursesTaughtCodes)}[/]\n");
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]└─ Doctor Exams Codes:[/] [{color}]{string.Join(",", DoctorExamsCodes)}[/]\n");

  }
}