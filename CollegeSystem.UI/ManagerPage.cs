using CollegeSystem.Core;
using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.UI;

public class ManagerPage
{
  public static List<Person> users = new List<Person>();
  public static List<Course> courses = new List<Course>();
  public static List<Exam> exams = new List<Exam>();

  public static Manager _sessionManager = new Manager();

  [Obsolete]
  public ManagerPage(Manager sessionUser)
  {
    ManagerOperations.sessionManager = sessionUser;

    _sessionManager = sessionUser;

    PrintMainMenuOptions();
  }

  [Obsolete]
  private void PrintMainMenuOptions()
  {
    Console.Clear();
    Console.OutputEncoding = Encoding.UTF8;

    var rule = new Rule($"[gold3]Manager {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[white]Manager System Of College [/][gold3]{Manager.CollegeName}[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel);

    AnsiConsole.Markup($"[bold]● Welcome Manager: [gold3]{_sessionManager.Name}[/] To College System\n\n[/]");

    string[] mainMenuOptions =
    {
      "Manager Information",
      "User Management", //add-edit-remove(Doctors,Students) | View User Details
      "Course Management", //add-edit-remove(Course) | Assign Course To Doctor
      "Schedule Exams", // Shedule Exam Date
      "System Report", // Give a Report about system
      "Exit Manager System" // return
    };

    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [gold3][italic]Main Menu Option[/][/]  ┃");
    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━┛");
    Console.WriteLine();



    var selection = AnsiConsole.Prompt(
      new SelectionPrompt<string>()
      .Title("[gold3_1]▶ [/]Please select an option:")
      .PageSize(10)
      .AddChoices(mainMenuOptions));

    switch (selection)
    {
      case "Manager Information":
        ManagerInformation();
        break;
      case "User Management":
        UserManagement();
        break;
      case "Course Management":
        CourseManagement();
        break;
      case "Schedule Exams":
        Console.Clear();
        var rule02 = new Rule($"[gold3]Manager {Manager.CollegeName} System[/]");
        rule02.Justification = Justify.Left;
        AnsiConsole.Write(rule02);
        ManagerOperations.ScheduleExams();
        break;
      case "System Report":
        Console.Clear();
        var rule03 = new Rule($"[gold3]Manager {Manager.CollegeName} System[/]");
        rule03.Justification = Justify.Left;
        AnsiConsole.Write(rule03);
        ManagerOperations.SystemReport();
        break;
      case "Exit Manager System":
        return;
      default:
        AnsiConsole.Markup("[grey]Invalid Selection.[/]");
        break;
    }

    PrintMainMenuOptions();

  } //endOf PrintMainMenuOptions

  [Obsolete]
  private void ManagerInformation()
  {
    Console.Clear();

    var rule = new Rule($"[gold3]Manager {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[gold3]Manager Information[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel);

    string[] userMenuOptions =
    {
      "View Manager Info",
      "Edit Manager Info",
      "Exit To Main Menu"
    };

    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [gold3][italic]Manager Info Menu Option[/][/]  ┃");
    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");



    var selection = AnsiConsole.Prompt(
  new SelectionPrompt<string>()
  .Title("[gold3_1]▶ [/]Please select an option:")
  .PageSize(10)
  .AddChoices(userMenuOptions));

    Operation.StartOption("Processing..");
    var rule02 = new Rule($"[gold3]Manager {Manager.CollegeName} System[/]");
    rule02.Justification = Justify.Left;
    AnsiConsole.Write(rule02);
    switch (selection)
    {
      case "View Manager Info":
        ManagerOperations.ViewManagerInfo();
        break;
      case "Edit Manager Info":
        ManagerOperations.EditManagerInfo();
        break;
      case "Exit To Main Menu":
        return;
    }

    ManagerInformation();
  }

  [Obsolete]
  private void UserManagement()
  {
    Console.Clear();

    var rule = new Rule($"[gold3]Manager {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[gold3]User Management[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel);

    string[] userMenuOptions =
    {
      "Add User",
      "Edit User Password",
      "View Users",
      "Remove User",
      "View User Details",
      "Exit To Main Menu"
    };

    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [gold3][italic]User Management Menu Option[/][/]  ┃");
    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");



    var selection = AnsiConsole.Prompt(
  new SelectionPrompt<string>()
  .Title("[gold3_1]▶ [/]Please select an option:")
  .PageSize(10)
  .AddChoices(userMenuOptions));

    Operation.StartOption("Processing..");
    var rule02 = new Rule($"[gold3]Manager {Manager.CollegeName} System[/]");
    rule02.Justification = Justify.Left;
    AnsiConsole.Write(rule02);
    switch (selection)
    {
      case "Add User":
        ManagerOperations.AddUser();
        break;
      case "Edit User Password":
        ManagerOperations.EditUserPassword();
        break;
      case "View Users":
        ManagerOperations.ViewUsers();
        break;
      case "Remove User":
        ManagerOperations.RemoveUser();
        break;
      case "View User Details":
        ManagerOperations.ViewUserDetails();
        break;
      case "Exit To Main Menu":
        return;
    }

    UserManagement();
  }

  [Obsolete]
  private void CourseManagement()
  {
    Console.Clear();

    var rule = new Rule($"[gold3]Manager {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[gold3]Course Management[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel);

    string[] courseMenuOptions =
    {
      "Add Course",
      "Remove Course",
      "View Courses",
      "View Course Details",
      "Exit To Main Menu"
    };

    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [gold3][italic]Course Management Menu Option[/][/]  ┃");
    Console.WriteLine("┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");



    var selection = AnsiConsole.Prompt(
  new SelectionPrompt<string>()
  .Title("[gold3_1]▶ [/]Please select an option:")
  .PageSize(10)
  .AddChoices(courseMenuOptions));

    Operation.StartOption("Processing..");
    var rule02 = new Rule($"[gold3]Manager {Manager.CollegeName} System[/]");
    rule02.Justification = Justify.Left;
    AnsiConsole.Write(rule02);
    switch (selection)
    {
      case "Add Course":
        ManagerOperations.AddCourse();
        break;
      //case 2:
      //  ManagerOperations.EditCourse();
      //  break;
      case "Remove Course":
        ManagerOperations.RemoveCourse();
        break;
      case "View Courses":
        ManagerOperations.ViewCourses();
        break;
      case "View Course Details":
        ManagerOperations.ViewCourseDetails();
        break;
      case "Exit To Main Menu":
        return;
    }

    CourseManagement();
  }

}





