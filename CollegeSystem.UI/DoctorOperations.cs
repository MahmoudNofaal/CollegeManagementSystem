using CollegeSystem.Core;
using CollegeSystem.Data;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.UI;


public static class DoctorOperations
{

  public static Doctor sessionDoctor = new();

  public static void ProfileInfo()
  {
    var panel01 = new Panel($"[lightcyan1]Profile Info[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ View Personal Details[/]\n");

    var panel = new Panel($"[bold]  Doctor [lightcyan1]{sessionDoctor.FullName}[/]  [/]")
    .Border(BoxBorder.Rounded);
    AnsiConsole.Write(panel);

    sessionDoctor.PrintUser("lightcyan1");

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
        Console.WriteLine($"● User Password: {sessionDoctor.Password}");
        string password = Operation.GetValidatedStringInput("●─> Set New ", "Password");
        if (password == "Invalid input")
        {
          AnsiConsole.MarkupLine("▶ [red]Password Is Wrong Format[/]");
          Console.WriteLine();
          return;
        }

        sessionDoctor.Password = password;
      }
      else if (number == 2)
      {
        Operation.LoadingOperation("▶ Editting User Email...", 70);
        Console.WriteLine($"● User Email: {sessionDoctor.Email}");
        string email = Operation.GetValidatedStringInput("●─> Set New ", "Email");

        if (email == "Invalid input")
        {
          AnsiConsole.MarkupLine("▶ [red]Email is wrong format[/]");
          Console.WriteLine();
          return;
        }

        sessionDoctor.Email = email;
      }
      else if (number == 3)
      {
        Operation.LoadingOperation("▶ Editting User Department...", 70);
        Console.WriteLine($"● User Department: {sessionDoctor.Department}");
        string department = Operation.GetValidatedStringInput("●─> Set New ", "Department");

        if (department == "Invalid input")
        {
          AnsiConsole.MarkupLine("▶ [red]Department is wrong format[/]");
          Console.WriteLine();
          return;
        }

        sessionDoctor.Department = department;
      }

      int sessionDoctorIndex = Operation.GetUserIndex(sessionDoctor.Code);
      //save data
      MainMenu.doctors[sessionDoctorIndex] = sessionDoctor;
      MainMenu._userRepository.SaveDoctorData(MainMenu.doctors);

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

    var root = new Tree($"[bold]▼ What Action Do You Want?[/]");
    var node = root.AddNode($"[white]───────────────────────╯[/]");
    node = root.AddNode($"[white]1) Make New Notify[/]");
    node = root.AddNode($"[white]2) Show Notifies[/]");
    AnsiConsole.Write(root);
    Console.WriteLine();

    AnsiConsole.Markup("● Choose a [green]Number[/] Or Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    Console.WriteLine();

    if (int.TryParse(option, out int number) && (number == 1 || number == 2))
    {
      Operation.LoadingOperation("▶ Loading Action...", 50);

      if (number == 1)
      {
        string notify = Operation.GetValidatedStringInput("●─> Create New ", "Notify");
        if (notify == "Invalid input")
        {
          AnsiConsole.MarkupLine("▶ [red]Notify Is In Wrong Format[/]");
          Console.WriteLine();
          return;
        }

        string result = $"[blue](Doctor){sessionDoctor.FullName} - {sessionDoctor.Code}[/]: {notify}. [grey]Created At[/] [red]{DateTime.Now}[/]";
        Manager.Notifies.Add(result);
      }
      else if (number == 2)
      {
        // Create a table
        var table = new Table();
        // Add columns with different alignments and styles
        table.AddColumn("[gold3]#[/]");
        table.AddColumn("[gold3]Notify Content[/]");

        Console.WriteLine();
        for (int i = 0; i < Manager.Notifies.Count; i++)
        {
          table.AddRow($"{i + 1}",
                              $"[bold]{Manager.Notifies[i]}[/]"
                      );
        }

        // Set table border and title
        table.Border(TableBorder.Rounded);
        table.Title($"[bold]▼ Notifies By Doctor[/]");
        // Render the table to the console
        AnsiConsole.Write(table);
      }

      int sessionManagerIndex = Operation.GetUserIndex(sessionDoctor.Code);
      //save data
      MainMenu.managers[0] = MainMenu.managers[0];
      MainMenu._userRepository.SaveManagerData(MainMenu.managers);

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
      NotifySection();
    }
  }


  public static void AssignCourse()
  {
    var panel01 = new Panel($"[lightcyan1]Assign Course Page[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ View Courses In The System Of The College[/]\n");

    List<Course> systemCourses = MainMenu.courses.Where(r => r.DoctorCode == "D0000").ToList();

    /////////////////////////////////

    // Create a new table
    var table = new Table()
        .AddColumn("[yellow]#[/]")       // Column for numbering
        .AddColumn("[lightcyan1]Course Name[/]")  // Column for Course Name
        .AddColumn("[lightcyan1]Course Code[/]")  // Column for Course Code
        .AddColumn("[lightcyan1]Department[/]")  // Column for Course Code
        .AddColumn("[lightcyan1]Number of Hours[/]"); // Column for Number of Hours

    // Add rows for each unassigned course
    for (int i = 0; i < systemCourses.Count; i++)
    {
      table.AddRow(
          $"[yellow]{i + 1}[/]",                             // Row number
          $"[cyan1]{systemCourses[i].CourseName}[/]",   // Course Name
          $"[gold3]{systemCourses[i].CourseCode}[/]",    // Course Code
          $"[lightcyan1]{systemCourses[i].Department}[/]",// Exam Code
          $"[lightcyan1]{systemCourses[i].NoOfHours}[/]"// Number of Hours
      );
    }

    // Customize table appearance
    table.Border = TableBorder.Rounded;
    table.Title = new TableTitle("[lightcyan1]Courses In The System[/]");

    // Render the table
    AnsiConsole.Write(table);

    ////////////////////////////////

    Console.WriteLine();
    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    Console.WriteLine();
    AnsiConsole.Markup($"● [lightcyan1]Choose Course You Want To Assign: [/]\n");

    bool isAgree = true;
    while (isAgree)
    {
      Console.WriteLine();
      string courseCode = Operation.GetCourseCodeInSystem("● Enter System Course ", "Code");
      if (courseCode == "Invalid input" || !systemCourses.Any(r => r.CourseCode == courseCode))
      {
        Operation.OutputMessage("Invalid Code");
        return;
      }

      Operation.LoadingOperation("▶ Assigning Course to Doctor Courses", 50);

      int courseIndex = Operation.GetCourseIndex(courseCode);

      bool isCourseAssignedBefore = (MainMenu.courses[courseIndex].DoctorCode != "D0000"); // true
      //check if the course is assigned already in doctor courses
      if (isCourseAssignedBefore)
      {
        AnsiConsole.Markup($"[lightcoral]Course Is Already Assigned Before![/]\n");

        string answer02 = Operation.GetValidatedStringInput("● Do You Want To Add Another Course ", "(y/n)");
        if (answer02 == "Invalid input")
        {
          Operation.OutputMessage("Invalid input");
          return;
        }

        if (answer02.ToLower() != "y")
        {
          isAgree = false;
          continue;
        }
      }

      int sessionDoctorIndex = Operation.GetUserIndex(sessionDoctor.Code);
      //save doctor to doctors list
      sessionDoctor.RegisteredCoursesCodes.Add(courseCode);
      MainMenu.doctors[sessionDoctorIndex] = sessionDoctor;
      MainMenu._userRepository.SaveDoctorData(MainMenu.doctors);

      //save assigneed course to courses
      MainMenu.courses[courseIndex].DoctorCode = sessionDoctor.Code;
      MainMenu._courseRepository.SaveCourseData(MainMenu.courses);

      Operation.LoadingOperation("Successfully Assigned", 40);

      Console.WriteLine();
      string answer = Operation.GetValidatedStringInput("● Do You Want To Add Another Course ", "(y/n)");
      if (answer == "Invalid input")
      {
        Operation.OutputMessage("Invalid Input");
        return;
      }

      if (answer.ToLower() != "y")
      {
        isAgree = false;
      }
    }

    Console.WriteLine();
    AnsiConsole.Markup($"[green]Successfully Assigned The Course[/]\n");

    Operation.FinishOption();
  }
  public static void UnassignCourse()
  {
    var panel01 = new Panel($"[lightcyan1]Unassign Course[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);

    AnsiConsole.MarkupLine($"[silver]▶ Show Assigned Courses By Dr.{sessionDoctor.FullName}[/]\n");

    var assignedCourses = MainMenu.courses.Where(r => sessionDoctor.RegisteredCoursesCodes.Contains(r.CourseCode)).ToList();

    // Create a new table
    var table = new Table()
        .AddColumn("[lightcyan1]#[/]")           // Column for numbering
        .AddColumn("[lightcyan1]Course Name[/]")  // Column for Course Name
        .AddColumn("[lightcyan1]Course Code[/]")  // Column for Course Code
        .AddColumn("[lightcyan1]Department[/]")  // Column for Course Code
        .AddColumn("[lightcyan1]Number of Hours[/]"); // Column for Number of Hours

    // Add rows for each assigned course
    for (int i = 0; i < assignedCourses.Count; i++)
    {
      table.AddRow(
          $"[white]{i + 1}[/]",                             // Row number
          $"[white]{assignedCourses[i].CourseName}[/]",    // Course Name
          $"[gray]{assignedCourses[i].CourseCode}[/]",     // Course Code
          $"[lightcyan1]{assignedCourses[i].Department}[/]", // department
          $"[lightcyan1]{assignedCourses[i].NoOfHours}[/]" // Number of Hours
      );
    }

    // Customize table appearance
    table.Border = TableBorder.Rounded;
    table.Title = new TableTitle($"[lightcyan1]..Assigned Courses By Dr {sessionDoctor.FullName}..[/]");

    // Render the table
    AnsiConsole.Write(table);

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    AnsiConsole.Markup($"[lightcyan1]Choose Courses You Want to unassign: [/]\n");
    bool isAgree = true;
    while (isAgree)
    {
      Console.WriteLine();
      string courseCode = Operation.GetCourseCodeInSystem("● Enter assigned course ", "code");
      if (courseCode == "Invalid input" || !assignedCourses.Any(r => r.CourseCode == courseCode) || !sessionDoctor.RegisteredCoursesCodes.Contains(courseCode))
      {
        Operation.OutputMessage("Invalid Input");
        return;
      }

      Operation.LoadingOperation("UnAssigning course...", 40);

      //optimize student enrolledCourses data
      var enrolledStudents = MainMenu.students.Where(r => r.EnrolledCoursesCodes.Contains(courseCode)).ToList();
      for (int j = 0; j < enrolledStudents.Count; j++)
      {
        enrolledStudents[j].EnrolledCoursesCodes.Remove(courseCode);

        int studentIndex = Operation.GetUserIndex(enrolledStudents[j].Code);
        MainMenu.students[studentIndex] = enrolledStudents[j];
      }
      MainMenu._userRepository.SaveStudentData(MainMenu.students);

      //optimize exams of system
      int courseIndex = Operation.GetCourseIndex(courseCode);
      if (MainMenu.exams.Any(r => r.CourseCode == courseCode))
      {
        int examIndex = Operation.GetExamIndex(MainMenu.courses[courseIndex].ExamCode);
        MainMenu.exams.Remove(MainMenu.exams[examIndex]);
      }
      MainMenu._examRepository.SaveExamData(MainMenu.exams);

      //optimize doctor who assigned the course
      sessionDoctor.RegisteredCoursesCodes.Remove(courseCode);
      int sessionDoctorIndex = Operation.GetUserIndex(sessionDoctor.Code);
      MainMenu.doctors[sessionDoctorIndex] = sessionDoctor;
      MainMenu._userRepository.SaveDoctorData(MainMenu.doctors);

      //save course to courses list
      MainMenu.courses[courseIndex].DoctorCode = "D0000";
      MainMenu.courses[courseIndex].ExamCode = "E0000";
      MainMenu._courseRepository.SaveCourseData(MainMenu.courses);

      Console.WriteLine();
      string answer = Operation.GetValidatedStringInput("● Do You Want To Unassign Another Course ", "(y/n)");
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
    AnsiConsole.Markup($"[green]Courses Unassigned Successfully...[/]\n");

    Operation.FinishOption();
  }
  public static void ViewAssignedCourses()
  {
    var panel01 = new Panel($"[lightcyan1]View Assigned Courses[/]")
   .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);

    AnsiConsole.MarkupLine($"[silver]▶ Show Assigned Courses By Dr. {sessionDoctor.FullName}[/]\n");

    //var assignedCourses = GetAssignedCoursesByTheDoctor();
    var assignedCourses = MainMenu.courses.Where(r => sessionDoctor.RegisteredCoursesCodes.Contains(r.CourseCode)).ToList();

    // Create a table
    var table = new Table();
    // Add columns with different alignments and styles
    table.AddColumn(new TableColumn("[lightcyan1]I[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Course Name[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Course Code[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Exam Code[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Department[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]No. Students[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]No. Hours[/]").Centered());

    // Set table border and title
    table.Border(TableBorder.Rounded);
    table.Title($"[bold]▼ Assigned Courses By [/][lightcyan1]Dr.{sessionDoctor.FullName}[/]");

    AnsiConsole.WriteLine();
    for (int i = 0; i < assignedCourses.Count; i++)
    {
      table.AddRow($"{i + 1}",
                           $"[orangered1][italic]{assignedCourses[i].CourseName}[/][/]",
                           $"[yellow]{assignedCourses[i].CourseCode}[/]",
                           $"[yellow]{assignedCourses[i].ExamCode}[/]",
                           $"[bold]{assignedCourses[i].Department}[/]",
                           $"[yellow2]{assignedCourses[i].NoStudents}[/]",
                           $"[orangered1]{assignedCourses[i].NoOfHours}[/]"
                  );
    }
    // Render the table to the console
    AnsiConsole.Write(table);

    AnsiConsole.Markup($"\n[grey58]...View Assigned Courses...[/]\n");

    Operation.FinishOption();
  }

  public static void CreateNewExam()
  {
    var panel01 = new Panel($"[lightcyan1]Create New Exam[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[lightcyan1]Registered Courses Codes[/]: ({string.Join(",", sessionDoctor.RegisteredCoursesCodes)})\n");

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    try
    {
      Console.WriteLine();
      AnsiConsole.MarkupLine($"[silver]▶ Input Exam Details[/]\n");

      Exam exam = GetExam();

      if (exam != null)
      {
        Operation.LoadingOperation("Adding Exam To System And Doctor Exams ...", 90);

        //save exams data
        MainMenu.exams.Add(exam);
        MainMenu._examRepository.SaveExamData(MainMenu.exams);

        //optimize courses of system
        int courseIndex = Operation.GetCourseIndex(exam.CourseCode);
        MainMenu.courses[courseIndex].ExamCode = exam.ExamCode;
        MainMenu._courseRepository.SaveCourseData(MainMenu.courses);

        //optimize doctor data
        int doctorIndex = Operation.GetUserIndex(sessionDoctor.Code);
        MainMenu.doctors[doctorIndex].DoctorExamsCodes.Add(exam.ExamCode);
        MainMenu.doctors[doctorIndex] = sessionDoctor;
        MainMenu._userRepository.SaveDoctorData(MainMenu.doctors);

        Console.WriteLine();
        AnsiConsole.Markup($"[green]Successfully added the exam[/]\n");

        Operation.FinishOption();
      }
      else
      {
        Operation.OutputMessage("Invalid input, Exam not added!");
      }
    }
    catch (Exception ex)
    {
      Operation.OutputMessage($"Something went wrong: {ex.Message}");
    }
  }
  public static void RemoveExam()
  {
    var panel01 = new Panel($"[lightcyan1]Remove Exams[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ Show Exams By Doctor[/]\n");

    var doctorExams = MainMenu.exams.Where(r => sessionDoctor.DoctorExamsCodes.Contains(r.ExamCode)).ToList();

    // Create a new table
    var table = new Table()
        .AddColumn("[lightcyan1]#[/]")                    // Column for numbering
        .AddColumn("[lightcyan1]Exam Name[/]")            // Column for Exam Name
        .AddColumn("[lightcyan1]Exam Code[/]")            // Column for Exam Code
        .AddColumn("[lightcyan1]Course Code[/]");         // Column for Course Code

    // Add rows for each exam
    for (int i = 0; i < doctorExams.Count; i++)
    {
      table.AddRow(
   $"[white]{i + 1}[/]",                       // Row number
          $"[white]{doctorExams[i].ExamName}[/]",           // Exam Name
          $"[gray]{doctorExams[i].ExamCode}[/]",            // Exam Code
          $"[lightcyan1]{doctorExams[i].CourseCode}[/]"    // Course Code
      );
    }

    // Customize table appearance
    table.Border = TableBorder.Rounded;
    table.Title = new TableTitle($"[lightcyan1]Exams By Dr {sessionDoctor.FullName}[/]");

    // Render the table
    AnsiConsole.Write(table);

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    Console.WriteLine();
    AnsiConsole.Markup($"[lightcyan1]Choose Exam Code To Remove: [/]\n");
    bool isAgree = true;
    while (isAgree)
    {
      string examCode = Operation.GetExamCodeInSystem("● Enter The Exam ", "Code");
      if (examCode == "Invalid input" || !sessionDoctor.DoctorExamsCodes.Contains(examCode))
      {
        Operation.OutputMessage("Invalid Code or exam does not exist in doctor exams");
        return;
      }

      Operation.LoadingOperation("Removing exam...", 40);

      int examIndex = Operation.GetExamIndex(examCode);
      //optmize course [examCode]
      int courseIndex = Operation.GetCourseIndex(MainMenu.exams[examIndex].CourseCode);
      MainMenu.courses[courseIndex].ExamCode = "E0000";
      MainMenu._courseRepository.SaveCourseData(MainMenu.courses);

      //save doctor in doctors list
      sessionDoctor.DoctorExamsCodes.Remove(examCode);
      int sessionDoctorIndex = Operation.GetUserIndex(sessionDoctor.Code);
      MainMenu.doctors[sessionDoctorIndex] = sessionDoctor;
      MainMenu._userRepository.SaveDoctorData(MainMenu.doctors);

      //save exam in exams list
      MainMenu.exams.Remove(MainMenu.exams[examIndex]);
      MainMenu._examRepository.SaveExamData(MainMenu.exams);


      string answer = Operation.GetValidatedStringInput("● Do You Want To Remove Another Exam ", "(y/n)");
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
    AnsiConsole.Markup($"[green]Exam Removed Successfully...[/]\n");

    Operation.FinishOption();
  }
  public static void ViewExams()
  {
    var panel01 = new Panel($"[lightcyan1]View All Exams[/]")
   .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ Show All Exams By Dr. {sessionDoctor.FullName}[/]\n");

    var doctorExams = MainMenu.exams.Where(r => sessionDoctor.DoctorExamsCodes.Contains(r.ExamCode)).ToList();

    // Create a table
    var table = new Table();
    // Add columns with different alignments and styles
    table.AddColumn(new TableColumn("[lightcyan1]I[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Exam Name[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]EXam Code[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Course Code[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Exam Date[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]No. Questions[/]").Centered());

    // Set table border and title
    table.Border(TableBorder.Rounded);
    table.Title($"[bold]▼ Exams By [/][lightcyan1]Dr.{sessionDoctor.FullName}[/]");

    AnsiConsole.WriteLine();
    for (int i = 0; i < doctorExams.Count; i++)
    {
      table.AddRow($"{i + 1}",
                           $"[thistle1][italic]{doctorExams[i].ExamName}[/][/]",
                           $"[thistle1]{doctorExams[i].ExamCode}[/]",
                           $"[lightsteelblue1]{doctorExams[i].CourseCode}[/]",
                           $"[lightsteelblue1]{doctorExams[i].ExamDate}[/]",
                           $"[lightcoral]{doctorExams[i].NoQuestions}[/]"
                  );
    }

    // Render the table to the console
    AnsiConsole.Write(table);

    AnsiConsole.Markup($"\n[grey58]...View Exams...[/]\n");

    Operation.FinishOption();
  }

  public static void ShowStudentsOfSpecificCourse()
  {
    var panel01 = new Panel($"[lightcyan1]View Enrolled Students[/]")
   .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ Show Student Enrolled Doctor Courses By Course-Code[/]\n");
    AnsiConsole.MarkupLine($"[lightcyan1]Courses Taught[/]: ({string.Join(",", sessionDoctor.RegisteredCoursesCodes)})\n");

    Console.WriteLine();
    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    var assignedCourses = MainMenu.courses.Where(r => sessionDoctor.RegisteredCoursesCodes.Contains(r.CourseCode)).ToList();

    Console.WriteLine();
    string courseCode = Operation.GetCourseCodeInSystem("● Enter assigned course ", "code");
    if (courseCode == "Invalid input" || !sessionDoctor.RegisteredCoursesCodes.Contains(courseCode))
    {
      Operation.OutputMessage("Course code not exist");
      return;
    }

    var enrolledStudents = MainMenu.students.Where(r => r.EnrolledCoursesCodes.Contains(courseCode)).ToList();

    // Create a table
    var table = new Table();
    // Add columns with different alignments and styles
    table.AddColumn(new TableColumn("[lightcyan1]I[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Student Name[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Student Code[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Marks[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]GPA[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Grade[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Department[/]").Centered());

    // Set table border and title
    table.Border(TableBorder.Rounded);
    table.Title($"[bold]▼ Enrolled [/][lightcyan1]Students[/]");

    AnsiConsole.WriteLine();
    for (int i = 0; i < enrolledStudents.Count; i++)
    {
      table.AddRow($"{i + 1}",
                           $"[thistle1][italic]{enrolledStudents[i].FullName}[/][/]",
                           $"[thistle1]{enrolledStudents[i].Code}[/]",
                           $"[lightsteelblue1]{enrolledStudents[i].Marks}[/]",
                           $"[lightsteelblue1]{enrolledStudents[i].GPA}[/]",
                           $"[lightsteelblue1]{enrolledStudents[i].Grade}[/]",
                           $"[lightsteelblue1]{enrolledStudents[i].Department}[/]"
                  );
    }
    // Render the table to the console
    AnsiConsole.Write(table);

    AnsiConsole.Markup($"\n[grey58]...View Students of The Course...[/]\n");

    Operation.FinishOption();
  }
  public static void GiveStudentsMarks()
  {
    var panel01 = new Panel($"[lightcyan1]Search Enrolled Student[/]")
   .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ Show Doctor Courses By Course-Code[/]\n");
    AnsiConsole.MarkupLine($"[lightcyan1]Courses Taught[/]: ({string.Join(",", sessionDoctor.RegisteredCoursesCodes)})\n");

    Console.WriteLine();
    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    var registeredCourses = MainMenu.courses.Where(r => sessionDoctor.RegisteredCoursesCodes.Contains(r.CourseCode)).ToList();

    string courseCode = Operation.GetCourseCodeInSystem("● Enter Registered Course ", "Code");
    if (courseCode == "Invalid input" || !sessionDoctor.RegisteredCoursesCodes.Contains(courseCode))
    {
      Operation.OutputMessage("Course code not exist");
      return;
    }

    Console.WriteLine();
    var enrolledStudents = MainMenu.students.Where(r => r.EnrolledCoursesCodes.Contains(courseCode)).ToList();
    AnsiConsole.Markup($"● [lightcyan1]Search Student Code To Give Grade: [/]\n");
    Console.WriteLine();

    bool isAgree = true;
    while (isAgree)
    {
      Console.WriteLine();
      string studentCode = Operation.GetUserCodeInSystem("● Enter Student ", "Code");
      if (studentCode == "Invalid input" || !enrolledStudents.Any(r => r.Code == studentCode))
      {
        Operation.OutputMessage("Invalid Code");
        return;
      }

      AnsiConsole.Markup($"Enter [gold3_1]Mark[/]: ");
      double marks = Convert.ToDouble(Console.ReadLine());

      Operation.LoadingOperation("▶ Giving A Marks", 50);

      int studentIndex = Operation.GetUserIndex(studentCode);
      //save student to doctors list
      MainMenu.students[studentIndex].Marks += marks;
      MainMenu.students[studentIndex].GPA = Operation.CalculateGPA(MainMenu.students[studentIndex]);
      Operation.CalculateGrade(MainMenu.students[studentIndex]);

      MainMenu._userRepository.SaveStudentData(MainMenu.students);

      Operation.LoadingOperation("Successfully Assigned", 40);

      Console.WriteLine();
      string answer = Operation.GetValidatedStringInput("● Do You Want To Give Marks To Another Student ", "(y/n)");
      if (answer == "Invalid input")
      {
        Operation.OutputMessage("Invalid Input");
        return;
      }

      if (answer.ToLower() != "y")
      {
        isAgree = false;
      }
    }

    Console.WriteLine();
    AnsiConsole.Markup($"[green]Successfully Gived Marks To The Students[/]\n");

    Operation.FinishOption();
  }

  private static Exam GetExam()
  {
    string str = "Invalid input";

    string courseCode = Operation.GetCourseCodeInSystem("● Enter System Course ", "Code");
    int courseIndex = Operation.GetCourseIndex(courseCode);
    if (courseCode == "Invalid input")
    {
      Console.WriteLine("Invalid Code or course does not exist in doctor assigned courses");
      return null;
    }

    if (!sessionDoctor.RegisteredCoursesCodes.Contains(courseCode) || MainMenu.courses[courseIndex].ExamCode != "E0000")
    {
      Console.WriteLine("InValid Code or exam already added!");
      return null;
    }

    string examCode = Generator.GenerateExamCode(MainMenu.exams);
    Thread.Sleep(50);
    Console.WriteLine($"● Generated Exam Code: {examCode}");

    int indexOfCourse = Operation.GetCourseIndex(courseCode);

    string examName = MainMenu.courses[indexOfCourse].CourseName;
    Thread.Sleep(50);
    Console.WriteLine($"● Get Exam Name: {examName}");

    string doctorCode = sessionDoctor.Code;
    Thread.Sleep(50);
    Console.WriteLine($"● Doctor Code: {sessionDoctor.Code}");

    DateTime examDate = default;

    Thread.Sleep(50);
    int noQuestions = Operation.GetValidIntInput("● Enter Exam ", "No. Questions (50 -> 100)", 50, 100);

    return new Exam(examCode, examName, courseCode, doctorCode, examDate, noQuestions);
  }
  private static int GetAssignedCourseIndex(string code)
  {
    for (int i = 0; i <= sessionDoctor.RegisteredCoursesCodes.Count - 1; i++)
    {
      if (sessionDoctor.RegisteredCoursesCodes[i] == code)
      {
        return i;
      }
    }
    return 0;
  }
  private static int GetDoctorExamsCodesIndex(string code)
  {
    for (int i = 0; i <= sessionDoctor.DoctorExamsCodes.Count - 1; i++)
    {
      if (sessionDoctor.DoctorExamsCodes[i] == code)
      {
        return i;
      }
    }
    return 0;
  }

  private static string GetAssignedCourseCode(string prompt, string something)
  {
    int i = 0;
    while (i < 3)
    {
      AnsiConsole.Markup($"{prompt}[gold3_1]{something}[/]: ");
      string code = Console.ReadLine();

      // Check if the entered code is in the system
      if (sessionDoctor.RegisteredCoursesCodes.Any(u => u == code))
      {
        return code;
      }
      else
      {
        Console.WriteLine("This code does not exist. Please enter a code in the assigned.");
      }
      i++;
    }
    return "Invalid input";

  }
  private static string GetDoctorExamCode(string prompt, string something)
  {
    int i = 0;
    while (i < 3)
    {
      AnsiConsole.Markup($"{prompt}[gold3_1]{something}[/]: ");
      string code = Console.ReadLine();

      // Check if the entered code is in the system
      if (sessionDoctor.DoctorExamsCodes.Any(u => u == code))
      {
        return code;
      }
      else
      {
        Console.WriteLine("This code does not exist. Please enter a code in the assigned.");
      }
      i++;
    }
    return "Invalid input";

  }

}





