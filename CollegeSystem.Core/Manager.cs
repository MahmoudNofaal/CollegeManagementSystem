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
  public const string CollegeName = "FCAI";
  public Manager()
  {

  }
  // departments, noStudents, noDoctors, courses, exams
  public Manager(
                  string nationalId,
                  string code,
                  string name,
                  string password,
                  string email,
                  string department,
                  bool activate
                )
    : base(nationalId, code, name, password,  email, department, activate)
  {
  }

  public override string ToString()
  {
    return base.ToString();
  }

  [Obsolete]
  public override void PrintUser(string color)
  {
    Thread.Sleep(40);
    AnsiConsole.Markup($"[bold]├─ National Id:[/] [{color}]{NationalId}[/]\n");
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Code:[/] [{color}]{Code}[/]\n");
    Thread.Sleep(40);
    AnsiConsole.Markup($"[bold]├─ Name:[/] [{color}]{Name}[/]\n");
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Password:[/] [{color}]{Password}[/]\n");
    Thread.Sleep(40);
    AnsiConsole.Markup($"[bold]├─ Email:[/] [{color}]{Email}[/]\n");
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Department:[/] [{color}]{Department}[/]\n");
    Thread.Sleep(40);
    AnsiConsole.Markup($"[bold]└─ College Name:[/] [{color}]{CollegeName}[/]\n");

  }
}