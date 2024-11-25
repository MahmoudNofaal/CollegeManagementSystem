using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.Core;

public class Manager : Person
{
  public static string CollegeName = "FCAI";
  public static List<string> Notifies = new List<string>();
  public static int NumberOfCoursesHoursAccepted = 18;
  public Manager()
  {

  }
  // departments, noStudents, noDoctors, courses, exams
  public Manager(
                  string code,
                  string name,
                  string password,
                  string email,
                  string department,
                  bool activate,
                  string collegeName,
                  int numberOfCoursesHours,
                  List<string> notifies
                )
    : base(code, name, password,  email, department, activate)
  {
    CollegeName = collegeName;
    NumberOfCoursesHoursAccepted = numberOfCoursesHours;
    Notifies = notifies;
  }


  public override void PrintUser(string color)
  {
    Thread.Sleep(40);
    AnsiConsole.Markup($"[bold]├─ College :[/] [{color}]{CollegeName}[/]\n");
    
    Thread.Sleep(40);
    AnsiConsole.Markup($"[bold]├─ Full Name:[/] [{color}]{FullName}[/]\n");
    
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Code:[/] [{color}]{Code}[/]\n");

    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Password:[/] [{color}]{Password}[/]\n");

    Thread.Sleep(40);
    AnsiConsole.Markup($"[bold]└─ Email:[/] [{color}]{Email}[/]\n");

  }
}