using CollegeSystem.Core;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.UI;


public class StudentPage
{
  public static Student _sessionStudent = new Student();

  public StudentPage(Student sessionStudent)
  {
    StudentOperations.sessionStudent = sessionStudent;

    _sessionStudent = sessionStudent;

    PrintMainMenuOptions();
  }


  private void PrintMainMenuOptions()
  {
    while (true)
    {
      Console.Clear();
      Console.OutputEncoding = Encoding.UTF8;

      var rule = new Rule($"[thistle1]Students {Manager.CollegeName} System[/]");
      rule.Justification = Justify.Left;
      AnsiConsole.Write(rule);

      var panel = new Panel($"[white]Student System Of College [/][thistle1]{Manager.CollegeName}[/]")
      .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
      AnsiConsole.Write(panel);

      AnsiConsole.Markup($"[bold]● Welcome Student: [thistle1]{_sessionStudent.FullName}[/] To College System\n\n[/]");

      string[] mainMenuOptions =
      {
        "Profile Page",
        "Courses Section",
        "View Grade",
        "View Exams Schedule",
        "Exit Student System"
      };

      Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━┓");
      AnsiConsole.MarkupLine("┃  [thistle1][italic]Main Menu Option[/][/]  ┃");
      Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━┛");



      var selection = AnsiConsole.Prompt(
      new SelectionPrompt<string>()
     .Title("[gold3_1]▶ [/]Please select an option:")
     .PageSize(10)
     .AddChoices(mainMenuOptions));

      switch (selection)
      {
        case "Profile Page":
          ProfilePage();
          break;
        case "Courses Section":
          CoursesSection();
          break;
        case "View Grade":
          Operation.StartOption("Processing..");
          var rule02 = new Rule($"[thistle1]Students {Manager.CollegeName} System[/]");
          rule02.Justification = Justify.Left;
          AnsiConsole.Write(rule02);
          StudentOperations.ViewGrade();
          break;
        case "View Exams Schedule":
          ViewExamsSchedule();
          break;
        case "Exit Student System":
          return;
      }
    }
  }


  private void ProfilePage()
  {
    Console.Clear();

    var rule = new Rule($"[thistle1]Students {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[thistle1]View Profile Page[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel);

    string[] userMenuOptions =
    {
      "Profile Info",
      "Update Personal Profile",
      "Notify Section",
      "Exit To Main Menu"
    };

    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [thistle1][italic]Student Info Menu Option[/][/]  ┃");
    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");



    var selection = AnsiConsole.Prompt(
  new SelectionPrompt<string>()
  .Title("[gold3_1]▶ [/]Please select an option:")
  .PageSize(10)
  .AddChoices(userMenuOptions));

    Operation.StartOption("Processing..");
    var rule02 = new Rule($"[thistle1]Students {Manager.CollegeName} System[/]");
    rule02.Justification = Justify.Left;
    AnsiConsole.Write(rule02);
    switch (selection)
    {
      case "Profile Info":
        StudentOperations.ProfileInfo();
        break;
      case "Update Personal Profile":
        StudentOperations.UpdatePersonalProfile();
        break;
      case "Notify Section":
        StudentOperations.NotifySection();
        break;
      case "Exit To Main Menu":
        return;
    }

    ProfilePage();
  }

  private void CoursesSection()
  {
    Console.Clear();

    var rule = new Rule($"[thistle1]Students {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[thistle1]Course Section[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel);

    string[] courseMenuOptions =
    {
      "Enroll Course",
      "Unenroll Course",
      "View Enrolled Courses",
      "View Enrolled Course Details",
      "Exit To Main Menu"
    };

    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [thistle1][italic]Courses Section Menu Option[/][/]  ┃");
    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");



    var selection = AnsiConsole.Prompt(
  new SelectionPrompt<string>()
  .Title("[gold3_1]▶ [/]Please select an option:")
  .PageSize(10)
  .AddChoices(courseMenuOptions));

    Operation.StartOption("Processing..");
    var rule02 = new Rule($"[thistle1]Students {Manager.CollegeName} System[/]");
    rule02.Justification = Justify.Left;
    AnsiConsole.Write(rule02);
    switch (selection)
    {
      case "Enroll Course":
        StudentOperations.EnrollCourse();
        break;
      case "Unenroll Course":
        StudentOperations.UnenrollCourse();
        break;
      case "View Enrolled Courses":
        StudentOperations.ViewEnrolledCourses();
        break;
      case "View Enrolled Course Details":
        StudentOperations.ViewEnrolledCourseDetails();
        break;
      case "Exit To Main Menu":
        return;
    }

    CoursesSection();
  }

  private void ViewExamsSchedule()
  {
    Console.Clear();
    var rule = new Rule($"[thistle1]Students {Manager.CollegeName} System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[thistle1]View Exams Schedule[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel);

    string[] userMenuOptions =
    {
                    "Student Exams",
                    "View Exam Details",
                    "Exit To Main Menu"
                };

    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
    AnsiConsole.MarkupLine("┃  [thistle1][italic]View Exams Schedule Menu Option[/][/]  ┃");
    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");


    var selection = AnsiConsole.Prompt(
  new SelectionPrompt<string>()
  .Title("[gold3_1]▶ [/]Please select an option:")
  .PageSize(10)
  .AddChoices(userMenuOptions));

    Operation.StartOption("Processing..");
    var rule02 = new Rule($"[thistle1]Students {Manager.CollegeName} System[/]");
    rule02.Justification = Justify.Left;
    AnsiConsole.Write(rule02);
    switch (selection)
    {
      case "Student Exams":
        StudentOperations.StudentExams();
        break;
      case "View Exam Details":
        StudentOperations.ViewExamDetails();
        break;
      case "Exit To Main Menu":
        return;
    }

    ViewExamsSchedule();
  }
}