using CollegeSystem.Core;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.UI;

public class DoctorPage
{
  public static Doctor _sessionDoctor = new Doctor();

  [Obsolete]
  public DoctorPage(Doctor sessionDoctor)
  {
    DoctorOperations.sessionDoctor = sessionDoctor;
    _sessionDoctor = sessionDoctor;

    PrintMainMenuOptions();
  }

  [Obsolete]
  private void PrintMainMenuOptions()
  {
    Console.Clear();
    Console.OutputEncoding = Encoding.UTF8;

    var rule = new Rule($"[lightcyan1]Doctors {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[white]Doctor System Of College [/][lightcyan1]{Manager.CollegeName}[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel);

    AnsiConsole.Markup($"[bold]● Welcome Doctor: [lightcyan1]{_sessionDoctor.Name}[/] To College System\n\n[/]");


    string[] mainMenuOptions =
    {
      "Personal Info",
      "Manage Courses",
      "Manage Exams",
      "View Students Of Specific Course",
      "Exist Doctor System"
    };

    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [lightcyan1][italic]Main Menu Option[/][/]  ┃");
    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━┛");


    //for (int i = 0; i < mainMenuOptions.Length - 1; i++)
    //{
    //  Thread.Sleep(40);
    //  //Console.WriteLine($"╠ {i + 1}. {mainMenuOptions[i]}");
    //  AnsiConsole.MarkupLine($"┣━━ [lightcyan1]{i + 1}.[/] [grey89]{mainMenuOptions[i]}[/]");

    //}
    //AnsiConsole.MarkupLine($"┗━━ [lightcyan1]{mainMenuOptions.Length}.[/] [grey89]{mainMenuOptions[mainMenuOptions.Length - 1]}[/]");
    //Console.WriteLine();

    //int index = Operation.GetValidIntInput("▶ Please enter ", "index", 1, mainMenuOptions.Length);


    var selection = AnsiConsole.Prompt(
  new SelectionPrompt<string>()
  .Title("[gold3_1]▶ [/]Please select an option:")
  .PageSize(10)
  .AddChoices(mainMenuOptions));

    switch (selection)
    {
      case "Personal Info":
        ViewPersonalInfo();
        break;
      case "Manage Courses":
        ManageCourses();
        break;
      case "Manage Exams":
        ManageExams();
        break;
      case "View Students Of Specific Course":
        Operation.StartOption("Processing..");
        var rule02 = new Rule($"[lightcyan1]Doctors {Manager.CollegeName} System[/]");
        rule02.Justification = Justify.Left;
        AnsiConsole.Write(rule02);
        DoctorOperations.ViewStudentsOfSpecificCourse();
        break;
      case "Exist Doctor System":
        return;
    }

    PrintMainMenuOptions();

  }

  [Obsolete]
  private void ViewPersonalInfo()
  {
    Console.Clear();

    var rule = new Rule($"[lightcyan1]Doctors {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[lightcyan1]View Personal Info[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel);

    string[] userMenuOptions =
    {
      "View Personal Info",
      "Update Personal Info",
      "Exit To Main Menu"
    };

    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [lightcyan1][italic]Doctor Info Menu Option[/][/]  ┃");
    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");

    //for (int i = 0; i < userMenuOptions.Length - 1; i++)
    //{
    //  Thread.Sleep(50);
    //  //Console.WriteLine($"╠ {i + 1}. {mainMenuOptions[i]}");
    //  AnsiConsole.MarkupLine($"┣━━ [lightcyan1]{i + 1}.[/] [grey89]{userMenuOptions[i]}[/]");
    //}
    //AnsiConsole.MarkupLine($"┗━━ [lightcyan1]{userMenuOptions.Length}.[/] [grey89]{userMenuOptions[userMenuOptions.Length - 1]}[/]");
    //Console.WriteLine();

    //int index = Operation.GetValidIntInput("Please enter ", "index", 1, userMenuOptions.Length);


    var selection = AnsiConsole.Prompt(
  new SelectionPrompt<string>()
  .Title("[gold3_1]▶ [/]Please select an option:")
  .PageSize(10)
  .AddChoices(userMenuOptions));

    Operation.StartOption("Processing..");
    var rule02 = new Rule($"[lightcyan1]Doctors {Manager.CollegeName} System[/]");
    rule02.Justification = Justify.Left;
    AnsiConsole.Write(rule02);
    switch (selection)
    {
      case "View Personal Info":
        DoctorOperations.ViewPersonalProfile();
        break;
      case "Update Personal Info":
        DoctorOperations.UpdatePersonalProfile();
        break;
      case "Exit To Main Menu":
        return;
    }

    ViewPersonalInfo();
  }

  [Obsolete]
  private void ManageCourses()
  {
    Console.Clear();

    var rule = new Rule($"[lightcyan1]Doctor {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[lightcyan1]Manage Courses[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel);

    string[] userMenuOptions =
    {
      "Assign Courses",
      "UnAssign Courses",
      "View Assigned Courses",
      "Exist To Main Menu"
    };


    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [lightcyan1][italic]Manage Courses Menu Option[/][/]  ┃");
    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");

    //for (int i = 0; i < userMenuOptions.Length - 1; i++)
    //{
    //  Thread.Sleep(50);
    //  //Console.WriteLine($"╠ {i + 1}. {mainMenuOptions[i]}");
    //  AnsiConsole.MarkupLine($"┣━━ [lightcyan1]{i + 1}.[/] [grey89]{userMenuOptions[i]}[/]");
    //}
    //AnsiConsole.MarkupLine($"┗━━ [lightcyan1]{userMenuOptions.Length}.[/] [grey89]{userMenuOptions[userMenuOptions.Length - 1]}[/]");
    //Console.WriteLine();

    //int index = Operation.GetValidIntInput("Please enter ", "index", 1, userMenuOptions.Length);


    var selection = AnsiConsole.Prompt(
  new SelectionPrompt<string>()
  .Title("[gold3_1]▶ [/]Please select an option:")
  .PageSize(10)
  .AddChoices(userMenuOptions));

    Operation.StartOption("Processing..");
    var rule02 = new Rule($"[lightcyan1]Doctors {Manager.CollegeName} System[/]");
    rule02.Justification = Justify.Left;
    AnsiConsole.Write(rule02);
    switch (selection)
    {
      case "Assign Courses":
        DoctorOperations.AssignCourses();
        break;
      case "UnAssign Courses":
        DoctorOperations.UnAssignCourses();
        break;
      case "View Assigned Courses":
        DoctorOperations.ViewAssignedCourses();
        break;
      case "Exist To Main Menu":
        return;
    }

    ManageCourses();
  }

  [Obsolete]
  private void ManageExams()
  {
    Console.Clear();

    var rule = new Rule($"[lightcyan1]Doctors {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[lightcyan1]View Personal Info[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel);

    string[] userMenuOptions =
    {
      "Add Exams",
      "Remove Exams",
      "View Exams",
      "Exist To Main Menu"
    };

    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [lightcyan1][italic]Manage Exams Menu Option[/][/]  ┃");
    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");

    //for (int i = 0; i < userMenuOptions.Length - 1; i++)
    //{
    //  Thread.Sleep(50);
    //  //Console.WriteLine($"╠ {i + 1}. {mainMenuOptions[i]}");
    //  AnsiConsole.MarkupLine($"┣━━ [lightcyan1]{i + 1}.[/] [grey89]{userMenuOptions[i]}[/]");
    //}
    //AnsiConsole.MarkupLine($"┗━━ [lightcyan1]{userMenuOptions.Length}.[/] [grey89]{userMenuOptions[userMenuOptions.Length - 1]}[/]");
    //Console.WriteLine();

    //int index = Operation.GetValidIntInput("Please enter ", "index", 1, userMenuOptions.Length);



    var selection = AnsiConsole.Prompt(
  new SelectionPrompt<string>()
  .Title("[gold3_1]▶ [/]Please select an option:")
  .PageSize(10)
  .AddChoices(userMenuOptions));

    Operation.StartOption("Processing..");
    var rule02 = new Rule($"[lightcyan1]Doctors {Manager.CollegeName} System[/]");
    rule02.Justification = Justify.Left;
    AnsiConsole.Write(rule02);
    switch (selection)
    {
      case "Add Exams":
        DoctorOperations.AddExams();
        break;
      case "Remove Exams":
        DoctorOperations.RemoveExams();
        break;
      case "View Exams":
        DoctorOperations.ViewExams();
        break;
      case "Exist To Main Menu":
        return;
    }

    ManageExams();
  }

}