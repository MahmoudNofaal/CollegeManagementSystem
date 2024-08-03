
using CollegeSystem.Core;
using CollegeSystem.Data;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.UI;


#region MyRegion
public static class StudentOperations
{

  public static Student sessionStudent = new();

  [Obsolete]
  public static void ViewPersonalProfile()
  {
    var panel01 = new Panel($"[thistle1]View Personal Info[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);

    AnsiConsole.MarkupLine("[silver]▶ View Personal Details Of Student[/]\n");


    var panel = new Panel($"[bold]  Student [thistle1]{sessionStudent.Name}[/]  [/]")
    .Border(BoxBorder.Rounded);
    AnsiConsole.Render(panel);

    sessionStudent.PrintUser("thistle1");

    Console.WriteLine();
    AnsiConsole.Markup($"[grey58]...Personal Info...[/]\n");

    Operation.FinishOption();
  }
  [Obsolete]
  public static void UpdatePersonalProfile()
  {
    var panel01 = new Panel($"[thistle1]Update Personal Info[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    string str = "Invalid input";

    string password = Operation.GetValidatedStringInput("● Set a new ", "password");
    string email = Operation.GetValidEmail("● Enter new ", "email");
    if (password == str || email == str)
    {
      Operation.OutputMessage("InValid Input");
      return;
    }

    Operation.LoadingOperation("▶ Updating Personal Profile...", 50);

    sessionStudent.Password = password;
    sessionStudent.Email = email;

    int sessionStudentIndex = Operation.GetUserIndex(sessionStudent.Code,2);

    //save student to students list
    MainMenu.students[sessionStudentIndex] = sessionStudent;
    MainMenu._userRepository.SaveStudentData(MainMenu.students);

    Console.WriteLine();
    AnsiConsole.Markup($"▶ Profile [green]Updated Successfully[/]\n");

    Operation.FinishOption();
  }

  [Obsolete]
  public static void RegisterCourses()
  {
    var panel01 = new Panel($"[thistle1]Register Courses[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    AnsiConsole.MarkupLine("[silver]▶ Show The UnRegistered Course In The System[/]\n");

    var doctorsCourses = MainMenu.courses.Where(r => r.DoctorCode != "D000");
    var unRegisteredCourses = doctorsCourses.Where(r => !sessionStudent.EnrolledCourses.Contains(r.CourseCode)).ToList();


    var root = new Tree($"[bold]▼ UnRegistered Courses [thistle1]{sessionStudent.Department}[/][/]");
    var node = root.AddNode($"[white]────────────────────────────────╯[/]");
        node = root.AddNode(
                            $"[white][thistle1]i. Course Name[/]" +
                            $"[gray] - [/]" +
                            $"[thistle1]Course Code[/]" +
                            $"[gray] - [/]" +
                            $"[thistle1]Number of Hours[/]" +
                            $"[/]"
                           );

    for (int i = 0; i < unRegisteredCourses.Count; i++)
    {
      node = root.AddNode($"[thistle1]{i + 1}.[/]" +
                          $" [white]{unRegisteredCourses[i].CourseName}" +
                          $"[gray] - [/]" +
                          $"{unRegisteredCourses[i].CourseCode}[/]" +
                          $"[gray] - [/]" +
                          $"[thistle1]{unRegisteredCourses[i].NoOfHours}[/]");
    }
    AnsiConsole.Render(root);

    Console.WriteLine();
    AnsiConsole.Markup($"[thistle1]Choose courses you want to register: [/]\n");
    bool isAgree = true;
    while (isAgree)
    {
      Console.WriteLine();
      string courseCode = Operation.GetCourseCodeInSystem("● Enter system course ", "code");
      if (courseCode == "Invalid input" || !unRegisteredCourses.Any(r => r.CourseCode == courseCode))
      {
        Operation.OutputMessage("Invalid input");
        return;
      }

      Operation.LoadingOperation("Registering Course to Student Courses...", 50);

      if (sessionStudent.EnrolledCourses.Contains(courseCode))
      {
        AnsiConsole.Markup($"[lightsteelblue1]Course is already registered![/]\n");

        string answer02 = Operation.GetValidatedStringInput("● Do you want to register another course ", "(y/n)").ToLower();
        if (answer02 == "Invalid input")
        {
          Operation.OutputMessage("InValid Input");
          return;
        }

        if (answer02.ToLower() != "y")
        {
          isAgree = false;
        continue;
        }
      }

      int sessionStudentIndex = Operation.GetUserIndex(sessionStudent.Code,2);

      //optmize student [enrolled courses] data
      sessionStudent.EnrolledCourses.Add(courseCode);
      MainMenu.students[sessionStudentIndex] = sessionStudent;
      MainMenu._userRepository.SaveStudentData(MainMenu.students);

      //optimzed course data
      int courseIndex = Operation.GetCourseIndex(courseCode);
      MainMenu.courses[courseIndex].NoStudents += 1;
      MainMenu._courseRepository.SaveCourseData(MainMenu.courses);

      Operation.LoadingOperation("Registered Complete", 30);

      string answer = Operation.GetValidatedStringInput("● Do you want to register another course ", "(y/n)").ToLower();
      if (answer == "Invalid input")
      {
        Operation.OutputMessage("InValid Input");
        return;
      }

      if (answer.ToLower() != "y")
      {
        isAgree = false;
      }
    }

    Console.WriteLine();
    AnsiConsole.Markup($"[green]Successfully registering course complete[/]\n");

    Operation.FinishOption();
  }
  [Obsolete]
  public static void ViewEnrolledCourses()
  {
    var panel01 = new Panel($"[thistle1]View Enrolled Courses[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ View Enrolled Courses By {sessionStudent.Name}[/]\n");



    var enrolledCourses = MainMenu.courses.Where(r => sessionStudent.EnrolledCourses.Contains(r.CourseCode)).ToList();

    // Create a table
    var table = new Table();
    // Add columns with different alignments and styles
    table.AddColumn(new TableColumn("[thistle1]I[/]").Centered());
    table.AddColumn(new TableColumn("[thistle1]Course Name[/]").Centered());
    table.AddColumn(new TableColumn("[thistle1]Course Code[/]").Centered());
    table.AddColumn(new TableColumn("[thistle1]Exam Code[/]").Centered());
    table.AddColumn(new TableColumn("[thistle1]No. Hours[/]").Centered());
    table.AddColumn(new TableColumn("[thistle1]Doctor Code[/]").Centered());

    // Set table border and title
    table.Border(TableBorder.Rounded);
    table.Title($"[bold]▼ [thistle1]Enrolled Courses[/] By Student [/]");

    AnsiConsole.WriteLine();
    for (int i = 0; i < enrolledCourses.Count; i++)
    {
      table.AddRow($"{i + 1}", $"[italic]{enrolledCourses[i].CourseName}[/]", $"{enrolledCourses[i].CourseCode}", $"{enrolledCourses[i].ExamCode}", $"[lightsteelblue1]{enrolledCourses[i].NoOfHours}[/]", $"[lightsteelblue1]{enrolledCourses[i].DoctorCode}[/]");
    }
    // Render the table to the console
    AnsiConsole.Write(table);

    AnsiConsole.Markup($"\n[grey58]...View Enrolled Courses...[/]\n");

    Operation.FinishOption();
  }
  [Obsolete]
  public static void ViewEnrolledCourseDetails()
  {
    var panel01 = new Panel($"[thistle1]View Enrolled Course Details[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ View Enrolled Course Details By {sessionStudent.Name}[/]\n");
    AnsiConsole.MarkupLine($"[thistle1]Enrolled Courses[/]: ({string.Join(",", sessionStudent.EnrolledCourses)})\n");


    Console.WriteLine();
    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    string codeInput = Operation.GetValidatedStringInput("● Enter a registered course ", "code");
    if (codeInput == "Invalid input" || !sessionStudent.EnrolledCourses.Any(c => c == codeInput))
    {
      Operation.OutputMessage("Invalid input");
      return;
    }

    var studentEnrolledCourses = MainMenu.courses.Where(r => sessionStudent.EnrolledCourses.Contains(r.CourseCode)).ToList();
    Course course = studentEnrolledCourses.First(c => c.CourseCode == codeInput);

    Console.WriteLine();
    AnsiConsole.Markup($"▼ [thistle1]Course {course.CourseName} Details[/]\n");
    AnsiConsole.Markup($"[bold]╭───────────────────╮[/]\n");
    AnsiConsole.Markup($"│  [thistle1]Enrolled Course  [/]│\n");
    AnsiConsole.Markup($"[bold]├───────────────────╯[/]\n");
    course.PrintCourse("thistle1");

    AnsiConsole.Markup($"\n[grey58]...View Enrolled Course Details...[/]\n");

    Operation.FinishOption();
  }

  [Obsolete]
  public static void ViewGrade()
  {
    var panel01 = new Panel($"[thistle1]View Student Grade[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.Markup($"▼ [thistle1]Show Student Grade[/]\n");
    Console.WriteLine();

    AnsiConsole.Markup($"[thistle1]▶ [/][white]Student Grade Report [/]\n");

    var table = new Table();

    table.AddColumn(new TableColumn("[thistle1]Student Name[/]").Centered());
    table.AddColumn(new TableColumn("[thistle1]Student Code[/]").Centered());
    table.AddColumn(new TableColumn("[thistle1]Student Grade[/]").Centered());
    table.AddColumn(new TableColumn("[thistle1]Student GPA[/]").Centered());

    table.AddRow($"{sessionStudent.Name}", $"{sessionStudent.Code}", $"{sessionStudent.Grade}", $"{sessionStudent.GPA}");

    table.Border(TableBorder.Rounded);

    AnsiConsole.Write(table);

    AnsiConsole.Markup($"\n[grey58]...View Grade...[/]\n");

    Operation.FinishOption();
  }
  [Obsolete]
  public static void StudentExams()
  {
    var panel01 = new Panel($"[thistle1]View Exams Of Student[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[thistle1]▼ [/][white]Show Student Exams This Year[/]\n");

    var studentExams = sessionStudent.EnrolledCourses.SelectMany(c => MainMenu.exams.Where(e => e.CourseCode == c)).ToList();

    //courses.Where(r => sessionStudent.EnrolledCourses.Contains(r.CourseCode)).ToList();
    //var studentExams = MainMenu.exams.Where(c => studentEnrolledCourses.Where(r => r.ExamCode == c.ExamCode)).ToList();


    var table = new Table();

    table.AddColumn(new TableColumn("[thistle1]Exam Name[/]").Centered());
    table.AddColumn(new TableColumn("[thistle1]Exam Code[/]").Centered());
    table.AddColumn(new TableColumn("[thistle1]Course Code[/]").Centered());
    table.AddColumn(new TableColumn("[thistle1]Exam Date[/]").Centered());
    table.AddColumn(new TableColumn("[thistle1]Number of Questions[/]").Centered());

    for (int i = 0; i < studentExams.Count; i++)
    {
      table.AddRow($"{studentExams[i].ExamName}", $"{studentExams[i].ExamCode}", $"{studentExams[i].CourseCode}", $"{studentExams[i].ExamDate}", $"{studentExams[i].NoQuestions}");
    }
    table.Border(TableBorder.Rounded);

    AnsiConsole.Write(table);

    AnsiConsole.Markup($"\n[grey58]...View Student Exam...[/]\n");

    Operation.FinishOption();
  }
  [Obsolete]
  public static void ViewExamDetails()
  {
    var panel01 = new Panel($"[thistle1]View Enrolled Course Details[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ View Exam Details By Code[/]\n");

    var studentExams = sessionStudent.EnrolledCourses.SelectMany(c => MainMenu.exams.Where(e => e.CourseCode == c)).ToList();
    var studentExamsCodes = new List<string>();
    for (int i = 0; i < studentExams.Count; i++)
    {
      studentExamsCodes.Add(studentExams[i].ExamCode);
    }
    AnsiConsole.MarkupLine($"[thistle1]Student Exams[/]: ({string.Join(",", studentExamsCodes)})\n");

    Console.WriteLine();
    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    string codeInput = Operation.GetValidatedStringInput("● Enter a student exam ", "code");
    if (codeInput == "Invalid input" || !studentExams.Any(c => c.ExamCode == codeInput))
    {
      Operation.OutputMessage("Invalid input");
      return;
    }

    Exam exam = MainMenu.exams.First(e => e.ExamCode == codeInput);

    Console.WriteLine();
    AnsiConsole.Markup($"▼ [thistle1]Course {exam.ExamName} Details[/]\n");
    AnsiConsole.Markup($"[bold]╭───────────────────╮[/]\n");
    AnsiConsole.Markup($"│  [thistle1]Exam Of Student  [/]│\n");
    AnsiConsole.Markup($"[bold]├───────────────────╯[/]\n");
    exam.PrintExam("thistle1");

    AnsiConsole.Markup($"\n[grey58]...View Exam Details...[/]\n");

    Operation.FinishOption();
  }

}
#endregion


