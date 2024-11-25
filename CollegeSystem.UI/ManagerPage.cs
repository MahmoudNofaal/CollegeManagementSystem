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
  public static Manager _sessionManager = new Manager();

  public ManagerPage(Manager sessionUser)
  {
    ManagerOperations.sessionManager = sessionUser;
    _sessionManager = sessionUser;

    PrintMainMenuOptions();
  }

  private void PrintMainMenuOptions()
  {
    Console.Clear();
    Console.OutputEncoding = Encoding.UTF8;

    var rule = new Rule($"[gold3]Management {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[white]Management System Of College [/][gold3]{Manager.CollegeName}[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel);

    AnsiConsole.Markup($"[bold]● Welcome Manager: [gold3]{_sessionManager.FullName}[/] To College System\n\n[/]");

    string[] mainMenuOptions =
    {
      "Adminstration Page",
      "Users Management", //add-edit-remove(Doctors,Students) | View User Details
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
      .Title("[gold3_1]▶ [/]Please Select An Option:")
      .PageSize(10)
      .AddChoices(mainMenuOptions));

    switch (selection)
    {
      case "Adminstration Page":
        AdminstrationPage();
        break;
      case "Users Management":
        UsersManagement();
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


  private void AdminstrationPage()
  {
    Console.Clear();

    var rule = new Rule($"[gold3]Manager {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[gold3]Manager Information[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel);

    string[] userMenuOptions =
    {
      "Admin Information",
      "Edit Admin Info",
      "Notify Section",
      "Exit To Main Menu"
    };

    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [gold3][italic]Manager Info Menu Option[/][/]  ┃");
    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");

    var selection = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
    .Title("[gold3_1]▶ [/]Please Select An Option:")
    .PageSize(10)
    .AddChoices(userMenuOptions));

    Operation.StartOption("Processing..");
    var rule02 = new Rule($"[gold3]Manager {Manager.CollegeName} System[/]");
    rule02.Justification = Justify.Left;
    AnsiConsole.Write(rule02);
    switch (selection)
    {
      case "Admin Information":
        ManagerOperations.AdminInformation();
        break;
      case "Edit Admin Info":
        ManagerOperations.EditAdminInfo();
        break;
      case "Notify Section":
        ManagerOperations.NotifySection();
        break;
      case "Exit To Main Menu":
        return;
    }

    AdminstrationPage();
  }

  private void UsersManagement()
  {
    Console.Clear();

    var rule = new Rule($"[gold3]Manager {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[gold3]User Management[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel);

    string[] userMenuOptions =
    {
      "Create New User",
      "Edit User Details",
      "Remove User From System",
      "View User Full Details",
      "View Users In System",
      "Exit To Main Menu"
    };

    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [gold3][italic]User Management Menu Option[/][/]  ┃");
    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");



    var selection = AnsiConsole.Prompt(
  new SelectionPrompt<string>()
  .Title("[gold3_1]▶ [/]Please Select An Option:")
  .PageSize(10)
  .AddChoices(userMenuOptions));

    Operation.StartOption("Processing..");
    var rule02 = new Rule($"[gold3]Manager {Manager.CollegeName} System[/]");
    rule02.Justification = Justify.Left;
    AnsiConsole.Write(rule02);
    switch (selection)
    {
      case "Create New User":
        ManagerOperations.CreateNewUser();
        break;
      case "Edit User Details":
        ManagerOperations.EditUserDetails();
        break;
      case "Remove User From System":
        ManagerOperations.RemoveUser();
        break;
      case "View User Full Details":
        ManagerOperations.ViewUserDetails();
        break;
      case "View Users In System":
        ManagerOperations.ViewUsers();
        break;
      case "Exit To Main Menu":
        return;
    }

    UsersManagement();
  }

  private void CourseManagement()
  {
    Console.Clear();

    var rule = new Rule($"[gold3]Manager {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[gold3]Course Management[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel);

    string[] courseMenuOptions =
    {
      "Create New Course",
      "Edit Course Details",
      "Remove Course From System",
      "View Course Details",
      "View Courses In System",
      "Exit To Main Menu"
    };

    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [gold3][italic]Course Management Menu Option[/][/]  ┃");
    Console.WriteLine("┣━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");



    var selection = AnsiConsole.Prompt(
  new SelectionPrompt<string>()
  .Title("[gold3_1]▶ [/]Please Select An Option:")
  .PageSize(10)
  .AddChoices(courseMenuOptions));

    Operation.StartOption("Processing..");
    var rule02 = new Rule($"[gold3]Manager {Manager.CollegeName} System[/]");
    rule02.Justification = Justify.Left;
    AnsiConsole.Write(rule02);
    switch (selection)
    {
      case "Create New Course":
        ManagerOperations.CreateNewCourse();
        break;
      case "Edit Course Details":
        ManagerOperations.EditCourseDetails();
        break;
      case "Remove Course From System":
        ManagerOperations.RemoveCourse();
        break;
      case "View Course Details":
        ManagerOperations.ViewCourseDetails();
        break;
      case "View Courses In System":
        ManagerOperations.ViewCourses();
        break;
      case "Exit To Main Menu":
        return;
    }

    CourseManagement();
  }

}





