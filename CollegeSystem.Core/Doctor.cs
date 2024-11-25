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
  public List<string> RegisteredCoursesCodes { get; set; } = new List<string>();
  public List<string> DoctorExamsCodes { get; set; } = new List<string>();

  public Doctor() : base()
  { }

  public Doctor(
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
    : base(code, name, password, email, department,activate)
  {
    DateOfHire = dateOfHire;
    RegisteredCoursesCodes = coursesTaughtCodes;
    DoctorExamsCodes = doctorExamsCodes;
  }

  public override string ToString()
  {
    return base.ToString() +
           $"Date Of Hire: {DateOfHire}\n" +
           $"Courses Taught: {string.Join(",", RegisteredCoursesCodes)}\n" +
           $"Exams of Doctor: {string.Join(",", DoctorExamsCodes)}\n";
  }

  public override void PrintUser(string color)
  {
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Full Name:[/] [{color}]{FullName}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Code:[/] [{color}]{Code}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Department:[/] [{color}]{Department}[/]\n");
   
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Email:[/] [{color}]{Email}[/]\n");
    
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Password:[/] [{color}]{Password}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]└─ Date Of Hire:[/] [{color}]{DateOfHire}[/]\n");
  }
}