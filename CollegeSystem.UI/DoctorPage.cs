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

  public DoctorPage(Doctor sessionDoctor)
  {
    DoctorOperations.sessionDoctor = sessionDoctor;
    _sessionDoctor = sessionDoctor;

    PrintMainMenuOptions();
  }

  private void PrintMainMenuOptions()
  {
    Console.Clear();
    Console.OutputEncoding = Encoding.UTF8;

    var rule = new Rule($"[lightcyan1]Doctors {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[white]Doctor System Of College [/][lightcyan1]{Manager.CollegeName}[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel);

    AnsiConsole.Markup($"[bold]● Welcome Doctor: [lightcyan1]{_sessionDoctor.FullName}[/] To College System\n\n[/]");

    string[] mainMenuOptions =
    {
      "Profile Page",
      "Courses Management",
      "Exams Management",
      "Students Management",
      "Exist Doctor System"
    };

    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [lightcyan1][italic]Main Menu Option[/][/]  ┃");
    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━┛");

    var selection = AnsiConsole.Prompt(
  new SelectionPrompt<string>()
  .Title("[gold3_1]▶ [/]Please Select An Option:")
  .PageSize(10)
  .AddChoices(mainMenuOptions));

    switch (selection)
    {
      case "Profile Page":
        ProfilePage();
        break;
      case "Courses Management":
        CoursesManagement();
        break;
      case "Exams Management":
        ExamsManagement();
        break;
      case "Students Management":
        StudentsManagement();
        break;
      case "Exist Doctor System":
        return;
    }

    PrintMainMenuOptions();

  }

  private void ProfilePage()
  {
    Console.Clear();

    var rule = new Rule($"[lightcyan1]Doctors {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[lightcyan1]View Profile[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel);

    string[] userMenuOptions =
    {
      "Profile Info",
      "Update Personal Info",
      "Notify Section",
      "Exit To Main Menu"
    };

    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [lightcyan1][italic]Doctor Info Menu Option[/][/]  ┃");
    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");

    var selection = AnsiConsole.Prompt(
  new SelectionPrompt<string>()
  .Title("[gold3_1]▶ [/]Please Select An Option:")
  .PageSize(10)
  .AddChoices(userMenuOptions));

    Operation.StartOption("Processing..");
    var rule02 = new Rule($"[lightcyan1]Doctors {Manager.CollegeName} System[/]");
    rule02.Justification = Justify.Left;
    AnsiConsole.Write(rule02);
    switch (selection)
    {
      case "Profile Info":
        DoctorOperations.ProfileInfo();
        break;
      case "Update Personal Info":
        DoctorOperations.UpdatePersonalProfile();
        break;
      case "Notify Section":
        DoctorOperations.NotifySection();
        break;
      case "Exit To Main Menu":
        return;
    }

    ProfilePage();
  }

  private void CoursesManagement()
  {
    Console.Clear();

    var rule = new Rule($"[lightcyan1]Doctor {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[lightcyan1]Manage Courses[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel);

    string[] userMenuOptions =
    {
      "Assign Course",
      "Unassign Course",
      "View Assigned Courses",
      "Exist To Main Menu"
    };


    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [lightcyan1][italic]Manage Courses Menu Option[/][/]  ┃");
    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");

    var selection = AnsiConsole.Prompt(
  new SelectionPrompt<string>()
  .Title("[gold3_1]▶ [/]Please Select An Option:")
  .PageSize(10)
  .AddChoices(userMenuOptions));

    Operation.StartOption("Processing..");
    var rule02 = new Rule($"[lightcyan1]Doctors {Manager.CollegeName} System[/]");
    rule02.Justification = Justify.Left;
    AnsiConsole.Write(rule02);
    switch (selection)
    {
      case "Assign Course":
        DoctorOperations.AssignCourse();
        break;
      case "Unassign Course":
        DoctorOperations.UnassignCourse();
        break;
      case "View Assigned Courses":
        DoctorOperations.ViewAssignedCourses();
        break;
      case "Exist To Main Menu":
        return;
    }

    CoursesManagement();
  }

  private void ExamsManagement()
  {
    Console.Clear();

    var rule = new Rule($"[lightcyan1]Doctors {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[lightcyan1]Exam Management[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel);

    string[] userMenuOptions =
    {
      "Create New Exam",
      "Remove Exam",
      "View Exams",
      "Exist To Main Menu"
    };

    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [lightcyan1][italic]Manage Exams Menu Option[/][/]  ┃");
    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");

    var selection = AnsiConsole.Prompt(
  new SelectionPrompt<string>()
  .Title("[gold3_1]▶ [/]Please Select An Option:")
  .PageSize(10)
  .AddChoices(userMenuOptions));

    Operation.StartOption("Processing..");
    var rule02 = new Rule($"[lightcyan1]Doctors {Manager.CollegeName} System[/]");
    rule02.Justification = Justify.Left;
    AnsiConsole.Write(rule02);
    switch (selection)
    {
      case "Create New Exam":
        DoctorOperations.CreateNewExam();
        break;
      case "Remove Exam":
        DoctorOperations.RemoveExam();
        break;
      case "View Exams":
        DoctorOperations.ViewExams();
        break;
      case "Exist To Main Menu":
        return;
    }

    ExamsManagement();
  }

  private void StudentsManagement()
  {
    Console.Clear();

    var rule = new Rule($"[lightcyan1]Doctors {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[lightcyan1]Students Management[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel);

    string[] userMenuOptions =
    {
      "Show Students Of Specific Course",
      "Give Students Grade",
      "Exist To Main Menu"
    };

    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [lightcyan1][italic]Manage Students Menu Option[/][/]  ┃");
    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");

    var selection = AnsiConsole.Prompt(
  new SelectionPrompt<string>()
  .Title("[gold3_1]▶ [/]Please Select An Option:")
  .PageSize(10)
  .AddChoices(userMenuOptions));

    Operation.StartOption("Processing..");
    var rule02 = new Rule($"[lightcyan1]Doctors {Manager.CollegeName} System[/]");
    rule02.Justification = Justify.Left;
    AnsiConsole.Write(rule02);
    switch (selection)
    {
      case "Show Students Of Specific Course":
        DoctorOperations.ShowStudentsOfSpecificCourse();
        break;
      case "Give Students Grade":
        DoctorOperations.GiveStudentsMarks();
        break;
      case "Exist To Main Menu":
        return;
    }

    StudentsManagement();
  }


}