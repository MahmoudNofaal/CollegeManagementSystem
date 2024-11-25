
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


public static class StudentOperations
{
  public static Student sessionStudent = new();

  public static void ProfileInfo()
  {
    var panel01 = new Panel($"[thistle1]Profile Info[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);

    AnsiConsole.MarkupLine("[silver]▶ View Personal Details Of Student[/]\n");


    var panel = new Panel($"[bold]  Student [thistle1]{sessionStudent.FullName}[/]  [/]")
    .Border(BoxBorder.Rounded);
    AnsiConsole.Write(panel);

    sessionStudent.PrintUser("thistle1");

    Console.WriteLine();
    AnsiConsole.Markup($"[grey58]...Personal Info...[/]\n");

    Operation.FinishOption();
  }
  public static void UpdatePersonalProfile()
  {
    var panel01 = new Panel($"[lightcyan1]Update Personal Info[/]")
   .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    var root = new Tree($"[bold]▼ What Do You Want To Edit?[/]");
    var node = root.AddNode($"[white]───────────────────────╯[/]");
    node = root.AddNode($"[white]1) Password[/]");
    node = root.AddNode($"[white]2) Email[/]");
    node = root.AddNode($"[white]3) Department[/]");
    AnsiConsole.Write(root);
    Console.WriteLine();

    AnsiConsole.Markup("● Choose a [green]Number[/] Or Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    Console.WriteLine();

    if (int.TryParse(option, out int number) && (number == 1 || number == 2 || number == 3))
    {
      if (number == 1)
      {
        Operation.LoadingOperation("▶ Editting User Password...", 70);
        Console.WriteLine($"● User Password: {sessionStudent.Password}");
        string password = Operation.GetValidatedStringInput("●─> Set New ", "Password");
        if (password == "Invalid input")
        {
          AnsiConsole.MarkupLine("▶ [red]Password Is Wrong Format[/]");
          Console.WriteLine();
          return;
        }

        sessionStudent.Password = password;
      }
      else if (number == 2)
      {
        Operation.LoadingOperation("▶ Editting User Email...", 70);
        Console.WriteLine($"● User Email: {sessionStudent.Email}");
        string email = Operation.GetValidatedStringInput("●─> Set New ", "Email");

        if (email == "Invalid input")
        {
          AnsiConsole.MarkupLine("▶ [red]Email is wrong format[/]");
          Console.WriteLine();
          return;
        }

        sessionStudent.Email = email;
      }
      else if (number == 3)
      {
        Operation.LoadingOperation("▶ Editting User Department...", 70);
        Console.WriteLine($"● User Department: {sessionStudent.Department}");
        string department = Operation.GetValidatedStringInput("●─> Set New ", "Department");

        if (department == "Invalid input")
        {
          AnsiConsole.MarkupLine("▶ [red]Department is wrong format[/]");
          Console.WriteLine();
          return;
        }

        sessionStudent.Department = department;
      }

      int sessionStudentIndex = Operation.GetUserIndex(sessionStudent.Code);
      //save data
      MainMenu.students[sessionStudentIndex] = sessionStudent;
      MainMenu._userRepository.SaveStudentData(MainMenu.students);

      Console.WriteLine();
      AnsiConsole.Markup($"▶ Profile [green]Updated Successfully[/]\n");
      Operation.FinishOption();
    } //endOf if(tryParse)
    else if (option.ToLower() == "q")
    {
      return;
    }
    else
    {
      Console.WriteLine("Wrong Input!!");
      Console.ReadLine();
      Console.Clear();
      UpdatePersonalProfile();
    }
  }
  public static void NotifySection()
  {
    var panel01 = new Panel($"[gold3]Notify Section[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);

    AnsiConsole.WriteLine("[bold]Showing The Notifies By Manager[/]");

    var root = new Tree($"[bold]▼ Notifies By Manager[/]");
    var node = root.AddNode($"[white]───────────────────╯[/]");
    node = root.AddNode(
                        $"[bold]#[/]" +
                        $"[gray] Notify Content [/]"
                       );

    for (int i = 0; i < Manager.Notifies.Count; i++)
    {
      node = root.AddNode(
                          $"[bold]{i + 1}.[/]" +
                          $"[white]{Manager.Notifies[i]}[/]"
                         );
    }
    AnsiConsole.Write(root);

    Operation.FinishOption();
  }

  public static void EnrollCourse()
  {
    var panel01 = new Panel($"[thistle1]Enroll Course[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    AnsiConsole.MarkupLine("[silver]▶ Show Courses In The System[/]\n");

    var doctorsCourses = MainMenu.courses.Where(r => r.DoctorCode != "D0000");
    var unRegisteredCourses = doctorsCourses.Where(r => !sessionStudent.EnrolledCoursesCodes.Contains(r.CourseCode)).ToList();

    // Create a new table
    var table = new Table()
        .AddColumn("[thistle1]#[/]")             // Column for numbering
        .AddColumn("[thistle1]Course Name[/]")   // Column for Course Name
        .AddColumn("[thistle1]Course Code[/]")   // Column for Course Code
        .AddColumn("[thistle1]Doctor Code[/]")   // Column for Course Code
        .AddColumn("[thistle1]Department[/]")   // Column for Course Code
        .AddColumn("[thistle1]Number of Hours[/]"); // Column for Number of Hours

    // Add rows for each unregistered course
    for (int i = 0; i < unRegisteredCourses.Count; i++)
    {
      table.AddRow(
          $"[yellow]{i + 1}[/]",                           // Row number
          $"[orangered1]{unRegisteredCourses[i].CourseName}[/]",  // Course Name
          $"[yellow2]{unRegisteredCourses[i].CourseCode}[/]",   // Course Code
          $"[yellow2]{unRegisteredCourses[i].DoctorCode}[/]",   // Course Code
          $"[bold]{unRegisteredCourses[i].Department}[/]",   // Course Code
          $"[thistle1]{unRegisteredCourses[i].NoOfHours}[/]" // Number of Hours
      );
    }

    // Customize table appearance
    table.Border = TableBorder.Rounded;
    table.Title = new TableTitle($"[bold]Unregistered Courses in [thistle1]{sessionStudent.Department}[/][/]");
    // Render the table
    AnsiConsole.Write(table);

    if (sessionStudent.NumberOfCoursesHours >= Manager.NumberOfCoursesHoursAccepted)
    {
      Console.WriteLine("The Number Of Courses Hours Is Accepted");
      Operation.FinishOption();
      return;
    }

    Console.WriteLine();
    AnsiConsole.Markup($"[thistle1]Choose courses you want to enroll: [/]\n");
    bool isAgree = true;
    while (isAgree)
    {
      Console.WriteLine();
      string courseCode = Operation.GetCourseCodeInSystem("● Enter course ", "code");
      if (courseCode == "Invalid input" || !unRegisteredCourses.Any(r => r.CourseCode == courseCode))
      {
        Operation.OutputMessage("Invalid input");
        return;
      }

      Operation.LoadingOperation("Registering Course to Student Courses...", 50);

      if (sessionStudent.EnrolledCoursesCodes.Contains(courseCode))
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

      int sessionStudentIndex = Operation.GetUserIndex(sessionStudent.Code);
      int courseIndex = Operation.GetCourseIndex(courseCode);

      //optmize student [enrolled courses] data
      sessionStudent.EnrolledCoursesCodes.Add(courseCode);
      sessionStudent.NoOfCreditHours += MainMenu.courses[courseIndex].NoOfHours;
      sessionStudent.NumberOfCoursesHours += MainMenu.courses[courseIndex].NoOfHours;
      MainMenu.students[sessionStudentIndex] = sessionStudent;
      MainMenu._userRepository.SaveStudentData(MainMenu.students);

      //optimzed course data
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
  public static void UnenrollCourse()
  {
    var panel01 = new Panel($"[thistle1]Unenroll Course[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    AnsiConsole.MarkupLine("[silver]▶ Show Student Courses[/]\n");

    var enrolledCourses = MainMenu.courses.Where(r => sessionStudent.EnrolledCoursesCodes.Contains(r.CourseCode)).ToList();

    // Create a new table
    var table = new Table()
        .AddColumn("[thistle1]#[/]")             // Column for numbering
        .AddColumn("[thistle1]Course Name[/]")   // Column for Course Name
        .AddColumn("[thistle1]Course Code[/]")   // Column for Course Code
        .AddColumn("[thistle1]Doctor Code[/]")   // Column for Course Code
        .AddColumn("[thistle1]Department[/]")   // Column for Course Code
        .AddColumn("[thistle1]Number of Hours[/]"); // Column for Number of Hours

    // Add rows for each unregistered course
    for (int i = 0; i < enrolledCourses.Count; i++)
    {
      table.AddRow(
          $"[thistle1]{i + 1}[/]",                           // Row number
          $"[white]{enrolledCourses[i].CourseName}[/]",  // Course Name
          $"[gray]{enrolledCourses[i].CourseCode}[/]",   // Course Code
          $"[gray]{enrolledCourses[i].DoctorCode}[/]",   // Course Code
          $"[gray]{enrolledCourses[i].Department}[/]",   // Course Code
          $"[thistle1]{enrolledCourses[i].NoOfHours}[/]" // Number of Hours
      );
    }

    // Customize table appearance
    table.Border = TableBorder.Rounded;
    table.Title = new TableTitle($"[bold]Enrolled Courses in [thistle1]{sessionStudent.Department}[/][/]");
    // Render the table
    AnsiConsole.Write(table);

    Console.WriteLine();
    AnsiConsole.Markup($"[thistle1]Choose Courses You Want To Unenroll: [/]\n");
    bool isAgree = true;
    while (isAgree)
    {
      Console.WriteLine();
      string courseCode = Operation.GetCourseCodeInSystem("● Enter Course ", "Code");
      if (courseCode == "Invalid input" || !enrolledCourses.Any(r => r.CourseCode == courseCode))
      {
        Operation.OutputMessage("Invalid input");
        return;
      }

      Operation.LoadingOperation("Unenrolled Course From Student Courses...", 50);

      if (!sessionStudent.EnrolledCoursesCodes.Contains(courseCode))
      {
        AnsiConsole.Markup($"[lightsteelblue1]Course Is Already Unenrolled![/]\n");

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

      int sessionStudentIndex = Operation.GetUserIndex(sessionStudent.Code);
      int courseIndex = Operation.GetCourseIndex(courseCode);

      //optmize student [unenrolled courses] data
      sessionStudent.EnrolledCoursesCodes.Remove(courseCode);
      sessionStudent.NoOfCreditHours -= MainMenu.courses[courseIndex].NoOfHours;
      sessionStudent.NumberOfCoursesHours -= MainMenu.courses[courseIndex].NoOfHours;
      MainMenu.students[sessionStudentIndex] = sessionStudent;
      MainMenu._userRepository.SaveStudentData(MainMenu.students);

      //optimzed course data
      MainMenu.courses[courseIndex].NoStudents -= 1;
      MainMenu._courseRepository.SaveCourseData(MainMenu.courses);

      Operation.LoadingOperation("Unenrolled Complete", 30);

      string answer = Operation.GetValidatedStringInput("● Do you want to unenroll another course ", "(y/n)").ToLower();
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
    AnsiConsole.Markup($"[green]Successfully Unenrolling Courses Complete[/]\n");

    Operation.FinishOption();
  }
  public static void ViewEnrolledCourses()
  {
    var panel01 = new Panel($"[thistle1]View Enrolled Courses[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ View Enrolled Courses By {sessionStudent.FullName}[/]\n");

    var enrolledCourses = MainMenu.courses.Where(r => sessionStudent.EnrolledCoursesCodes.Contains(r.CourseCode)).ToList();

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
      table.AddRow($"{i + 1}",
                          $"[yellow3_1]{enrolledCourses[i].CourseName}[/]",
                          $"[yellow]{enrolledCourses[i].CourseCode}[/]",
                          $"[yellow]{enrolledCourses[i].ExamCode}[/]",
                          $"[lightsteelblue1]{enrolledCourses[i].NoOfHours}[/]",
                          $"[lightsteelblue1]{enrolledCourses[i].DoctorCode}[/]");
    }
    // Render the table to the console
    AnsiConsole.Write(table);

    AnsiConsole.Markup($"\n[grey58]...View Enrolled Courses...[/]\n");

    Operation.FinishOption();
  }
  public static void ViewEnrolledCourseDetails()
  {
    var panel01 = new Panel($"[thistle1]View Enrolled Course Details[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ View Enrolled Course Details By {sessionStudent.FullName}[/]\n");
    AnsiConsole.MarkupLine($"[thistle1]Enrolled Courses[/]: ({string.Join(",", sessionStudent.EnrolledCoursesCodes)})\n");

    Console.WriteLine();
    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    string codeInput = Operation.GetValidatedStringInput("● Enter a registered course ", "code");
    if (codeInput == "Invalid input" || !sessionStudent.EnrolledCoursesCodes.Any(c => c == codeInput))
    {
      Operation.OutputMessage("Invalid input");
      return;
    }

    var studentEnrolledCourses = MainMenu.courses.Where(r => sessionStudent.EnrolledCoursesCodes.Contains(r.CourseCode)).ToList();
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

  public static void ViewGrade()
  {
    var panel01 = new Panel($"[thistle1]View Student Grade[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.Markup($"▼ [thistle1]Show Student Grade[/]\n");
    Console.WriteLine();

    AnsiConsole.Markup($"[thistle1]▶ [/][white]Student Grade Report [/]\n");

    var table = new Table();

    table.AddColumn(new TableColumn("[thistle1]Student Name[/]").Centered());
    table.AddColumn(new TableColumn("[thistle1]Student Code[/]").Centered());
    table.AddColumn(new TableColumn("[thistle1]Student Marks[/]").Centered());
    table.AddColumn(new TableColumn("[thistle1]Student GPA[/]").Centered());
    table.AddColumn(new TableColumn("[thistle1]Student Grade[/]").Centered());

    table.AddRow($"{sessionStudent.FullName}",
                        $"{sessionStudent.Code}",
                        $"{sessionStudent.Marks}",
                        $"{sessionStudent.GPA}",
                        $"{sessionStudent.Grade}");

    table.Border(TableBorder.Rounded);

    AnsiConsole.Write(table);

    AnsiConsole.Markup($"\n[grey58]...View Grade...[/]\n");

    Operation.FinishOption();
  }

  public static void StudentExams()
  {
    var panel01 = new Panel($"[thistle1]View Exams Of Student[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[thistle1]▼ [/][white]Show Student Exams This Year[/]\n");

    var studentExams = sessionStudent.EnrolledCoursesCodes.SelectMany(c => MainMenu.exams.Where(e => e.CourseCode == c)).ToList();

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
      table.AddRow($"[orangered1]{studentExams[i].ExamName}[/]",
                          $"[yellow1]{studentExams[i].ExamCode}[/]",
                          $"[yellow1]{studentExams[i].CourseCode}[/]",
                          $"[orangered1]{studentExams[i].ExamDate}[/]",
                          $"[red1]{studentExams[i].NoQuestions}[/]");
    }
    table.Border(TableBorder.Rounded);

    AnsiConsole.Write(table);

    AnsiConsole.Markup($"\n[grey58]...View Student Exam...[/]\n");

    Operation.FinishOption();
  }
  public static void ViewExamDetails()
  {
    var panel01 = new Panel($"[thistle1]View Enrolled Course Details[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ View Exam Details By Code[/]\n");

    var studentExams = sessionStudent.EnrolledCoursesCodes.SelectMany(c => MainMenu.exams.Where(e => e.CourseCode == c)).ToList();
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


