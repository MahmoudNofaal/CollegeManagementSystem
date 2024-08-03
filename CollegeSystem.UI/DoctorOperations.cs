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

  [Obsolete]
  public static void ViewPersonalProfile()
  {
    var panel01 = new Panel($"[lightcyan1]View Personal Info[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ View Personal Details[/]\n");

    var panel = new Panel($"[bold]  Doctor [lightcyan1]{sessionDoctor.Name}[/]  [/]")
    .Border(BoxBorder.Rounded);
    AnsiConsole.Render(panel);

    sessionDoctor.PrintUser("lightcyan1");

    Console.WriteLine();
    AnsiConsole.Markup($"[grey58]...Personal Info...[/]\n");

    Operation.FinishOption();
  }
  [Obsolete]
  public static void UpdatePersonalProfile()
  {
    var panel01 = new Panel($"[lightcyan1]Update Personal Info[/]")
   .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ Edit Password and Email[/]\n");

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    string password = Operation.GetValidatedStringInput("● Set a new ", "password");
    string email = Operation.GetValidEmail("● Enter new ", "email");
    string str = "Invalid input";
    if (password == str || email == str)
    {
      Operation.OutputMessage("Invalid input");
      return;
    }

    Operation.LoadingOperation("▶ Updating Personal Profile...", 50);

    sessionDoctor.Password = password;
    sessionDoctor.Email = email;

    int sessionDoctorIndex = Operation.GetUserIndex(sessionDoctor.Code, 1);

    //save doctor to doctors list
    MainMenu.doctors[sessionDoctorIndex] = sessionDoctor;
    MainMenu._userRepository.SaveDoctorData(MainMenu.doctors);

    Console.WriteLine();
    AnsiConsole.Markup($"▶ Profile [green]Updated Successfully[/]\n");

    Operation.FinishOption();
  }

  [Obsolete]
  public static void AssignCourses()
  {
    var panel01 = new Panel($"[lightcyan1]Assign Courses[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ View UnAssigned Courses In The System[/]\n");

    List<Course> unassignedCourses = MainMenu.courses.Where(r => r.DoctorCode == "D000").ToList();

    var root = new Tree($"▼ [lightcyan1]Unassigned Courses In The System[/]");
    var node = root.AddNode($"[white]──────────────────────────────╯[/]");
    node = root.AddNode($"[lightcyan1]i. Course Name[/][gray] - [/][lightcyan1]Course Code[/][gray] - [/][lightcyan1]Exam Code[/][gray] - [/][lightcyan1]Number of Hours[/]");
    for (int i = 0; i < unassignedCourses.Count; i++)
    {
      node = root.AddNode($"[lightcyan1]{i + 1}.[/] [white]{unassignedCourses[i].CourseName}[gray] - [/]{unassignedCourses[i].CourseCode}[/][gray] - [/][lightcyan1]{unassignedCourses[i].ExamCode}[/][gray] - [/][lightcyan1]{unassignedCourses[i].NoOfHours}[/]");
    }
    AnsiConsole.Render(root);

    Console.WriteLine();
    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    Console.WriteLine();
    AnsiConsole.Markup($"● [lightcyan1]Choose courses you want to assign: [/]\n");
    Console.WriteLine();
    bool isAgree = true;
    while (isAgree)
    {
      Console.WriteLine();
      string courseCode = Operation.GetCourseCodeInSystem("● Enter system course ", "code");
      if (courseCode == "Invalid input" || !unassignedCourses.Any(r => r.CourseCode == courseCode))
      {
        Operation.OutputMessage("InValid Code");
        return;
      }

      Operation.LoadingOperation("▶ Assigning Course to Doctor Courses", 50);

      int courseIndex = Operation.GetCourseIndex(courseCode);

      bool isCourseAssignedBefore = (MainMenu.courses[courseIndex].DoctorCode != "D000"); // true
      //check if the course is assigned already in doctor courses
      if (isCourseAssignedBefore)
      {
        AnsiConsole.Markup($"[lightcoral]Course is already assigned before![/]\n");
        Console.WriteLine();

        string answer02 = Operation.GetValidatedStringInput("● Do you want to add another course ", "(y/n)");
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

      int sessionDoctorIndex = Operation.GetUserIndex(sessionDoctor.Code, 1);
      //save doctor to doctors list
      sessionDoctor.CoursesTaughtCodes.Add(courseCode);
      MainMenu.doctors[sessionDoctorIndex] = sessionDoctor;
      MainMenu._userRepository.SaveDoctorData(MainMenu.doctors);

      //save assigneed course to courses
      MainMenu.courses[courseIndex].DoctorCode = sessionDoctor.Code;
      MainMenu._courseRepository.SaveCourseData(MainMenu.courses);

      Operation.LoadingOperation("Successfully Assigned", 40);

      Console.WriteLine();
      string answer = Operation.GetValidatedStringInput("● Do you want to add another course ", "(y/n)");
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
    AnsiConsole.Markup($"[green]Successfully assigned the course[/]\n");

    Operation.FinishOption();
  }
  [Obsolete]
  public static void UnAssignCourses()
  {
    var panel01 = new Panel($"[lightcyan1]UnAssign Courses[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);

    AnsiConsole.MarkupLine($"[silver]▶ Show Assigned Courses By Dr.{sessionDoctor.Name}[/]\n");

    var assignedCourses = MainMenu.courses.Where(r => sessionDoctor.CoursesTaughtCodes.Contains(r.CourseCode)).ToList();


    var root = new Tree($"▼ Assigned Courses By Dr [lightcyan1]{sessionDoctor.Name}[/]");
    var node = root.AddNode($"[lightcyan1]i. Course Name[/][gray] - [/][lightcyan1]Course Code[/][gray] - [/][lightcyan1]Exam Code[/][gray] - [/][lightcyan1]Number of Hours[/]");
    for (int i = 0; i < assignedCourses.Count; i++)
    {
      node = root.AddNode($"[lightcyan1]{i + 1}.[/] [white]{assignedCourses[i].CourseName}[gray] - [/]{assignedCourses[i].CourseCode}[/][gray] - [/][lightcyan1]{assignedCourses[i].ExamCode}[/][gray] - [/][lightcyan1]{assignedCourses[i].NoOfHours}[/]");
    }
    AnsiConsole.Render(root);
    Console.WriteLine();

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    AnsiConsole.Markup($"[lightcyan1]Choose courses you want to unassign: [/]\n");
    bool isAgree = true;
    while (isAgree)
    {
      Console.WriteLine();
      string courseCode = Operation.GetCourseCodeInSystem("● Enter assigned course ", "code");
      if (courseCode == "Invalid input" || !assignedCourses.Any(r => r.CourseCode == courseCode) || !sessionDoctor.CoursesTaughtCodes.Contains(courseCode))
      {
        Operation.OutputMessage("InValid Input");
        return;
      }

      Operation.LoadingOperation("UnAssigning course...", 40);

      //optimize student enrolledCourses data
      var enrolledStudents = MainMenu.students.Where(r => r.EnrolledCourses.Contains(courseCode)).ToList();
      for (int j = 0; j < enrolledStudents.Count; j++)
      {
        enrolledStudents[j].EnrolledCourses.Remove(courseCode);

        int studentIndex = Operation.GetUserIndex(enrolledStudents[j].Code, 2);
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
      sessionDoctor.CoursesTaughtCodes.Remove(courseCode);
      int sessionDoctorIndex = Operation.GetUserIndex(sessionDoctor.Code,1);
      MainMenu.doctors[sessionDoctorIndex] = sessionDoctor;
      MainMenu._userRepository.SaveDoctorData(MainMenu.doctors);

      //save course to courses list
      MainMenu.courses[courseIndex].DoctorCode = "D000";
      MainMenu.courses[courseIndex].ExamCode = "E000";
      MainMenu._courseRepository.SaveCourseData(MainMenu.courses);

      Console.WriteLine();
      string answer = Operation.GetValidatedStringInput("● Do you want to unassign another course ", "(y/n)");
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
    AnsiConsole.Markup($"[green]Courses unassigned successfully...[/]\n");

    Operation.FinishOption();
  }
  [Obsolete]
  public static void ViewAssignedCourses()
  {
    var panel01 = new Panel($"[lightcyan1]View Assigned Courses[/]")
   .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);

    AnsiConsole.MarkupLine($"[silver]▶ Show Assigned Courses By Dr. {sessionDoctor.Name}[/]\n");

    //var assignedCourses = GetAssignedCoursesByTheDoctor();
    var assignedCourses = MainMenu.courses.Where(r => sessionDoctor.CoursesTaughtCodes.Contains(r.CourseCode)).ToList();

    // Create a table
    var table = new Table();
    // Add columns with different alignments and styles
    table.AddColumn(new TableColumn("[lightcyan1]I[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Course Name[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Course Code[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Exam Code[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]No. Students[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]No. Hours[/]").Centered());

    // Set table border and title
    table.Border(TableBorder.Rounded);
    table.Title($"[bold]▼ Assigned Courses By [/][lightcyan1]Dr.{sessionDoctor.Name}[/]");

    AnsiConsole.WriteLine();
    for (int i = 0; i < assignedCourses.Count; i++)
    {
      table.AddRow($"{i + 1}",
                           $"[thistle1][italic]{assignedCourses[i].CourseName}[/][/]",
                           $"[thistle1]{assignedCourses[i].CourseCode}[/]",
                           $"[lightsteelblue1]{assignedCourses[i].ExamCode}[/]",
                           $"[lightsteelblue1]{assignedCourses[i].NoStudents}[/]",
                           $"[lightcoral]{assignedCourses[i].NoOfHours}[/]"
                  );
    }
    // Render the table to the console
    AnsiConsole.Write(table);

    AnsiConsole.Markup($"\n[grey58]...View Assigned Courses...[/]\n");

    Operation.FinishOption();
  }

  [Obsolete]
  public static void AddExams()
  {
    var panel01 = new Panel($"[lightcyan1]Add Exams[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[lightcyan1]Courses Taught[/]: ({string.Join(",", sessionDoctor.CoursesTaughtCodes)})\n");

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
        int doctorIndex = Operation.GetUserIndex(sessionDoctor.Code, 1);
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
  [Obsolete]
  public static void RemoveExams()
  {
    var panel01 = new Panel($"[lightcyan1]Remove Exams[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ Show Exams By Doctor[/]\n");


    var doctorExams = MainMenu.exams.Where(r => sessionDoctor.DoctorExamsCodes.Contains(r.ExamCode)).ToList();

    var root = new Tree($"▼ Exam By Dr [lightcyan1]{sessionDoctor.Name}[/]");
    var node = root.AddNode($"[lightcyan1]i. Exam Name[/][gray] - [/][lightcyan1]Exam Code[/][gray] - [/][lightcyan1]Course Code[/][gray] - [/][lightcyan1]Number of Questions[/]");
    for (int i = 0; i < doctorExams.Count; i++)
    {
      node = root.AddNode($"[lightcyan1]{i + 1}.[/] [white]{doctorExams[i].ExamName}[gray] - [/]{doctorExams[i].ExamCode}[/][gray] - [/][lightcyan1]{doctorExams[i].CourseCode}[/][gray] - [/][lightcyan1]{doctorExams[i].NoQuestions}[/]");
    }
    AnsiConsole.Render(root);

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    Console.WriteLine();
    AnsiConsole.Markup($"[lightcyan1]Choose exams you want to remove: [/]\n");
    bool isAgree = true;
    while (isAgree)
    {
      string examCode = Operation.GetExamCodeInSystem("● Enter doctor exam ", "code");
      if (examCode == "Invalid input" || !sessionDoctor.DoctorExamsCodes.Contains(examCode))
      {
        Operation.OutputMessage("InValid Code or exam does not exist in doctor exams");
        return;
      }

      Operation.LoadingOperation("Removing exam...", 40);

      int examIndex = Operation.GetExamIndex(examCode);
      //optmize course [examCode]
      int courseIndex = Operation.GetCourseIndex(MainMenu.exams[examIndex].CourseCode);
      MainMenu.courses[courseIndex].ExamCode = "E000";
      MainMenu._courseRepository.SaveCourseData(MainMenu.courses);

      //save doctor in doctors list
      sessionDoctor.DoctorExamsCodes.Remove(examCode);
      int sessionDoctorIndex = Operation.GetUserIndex(sessionDoctor.Code, 1);
      MainMenu.doctors[sessionDoctorIndex] = sessionDoctor;
      MainMenu._userRepository.SaveDoctorData(MainMenu.doctors);

      //save exam in exams list
      MainMenu.exams.Remove(MainMenu.exams[examIndex]);
      MainMenu._examRepository.SaveExamData(MainMenu.exams);


      string answer = Operation.GetValidatedStringInput("● Do you want to remove another exam ", "(y/n)");
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
    AnsiConsole.Markup($"[green]Exam removed successfully...[/]\n");

    Operation.FinishOption();

  }
  [Obsolete]
  public static void ViewExams()
  {
    var panel01 = new Panel($"[lightcyan1]View Assigned Courses[/]")
   .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ Show Exams By Dr. {sessionDoctor.Name}[/]\n");


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
    table.Title($"[bold]▼ Exams By [/][lightcyan1]Dr.{sessionDoctor.Name}[/]");

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

  [Obsolete]
  public static void ViewStudentsOfSpecificCourse()
  {
    var panel01 = new Panel($"[lightcyan1]View Enrolled Students[/]")
   .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine($"[silver]▶ Show Student Enrolled Doctor Courses By Course-Code[/]\n");
    AnsiConsole.MarkupLine($"[lightcyan1]Courses Taught[/]: ({string.Join(",", sessionDoctor.CoursesTaughtCodes)})\n");

    Console.WriteLine();
    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    var assignedCourses = MainMenu.courses.Where(r => sessionDoctor.CoursesTaughtCodes.Contains(r.CourseCode)).ToList();

    string courseCode = Operation.GetCourseCodeInSystem("● Enter assigned course ", "code");
    if (courseCode == "Invalid input" || !sessionDoctor.CoursesTaughtCodes.Contains(courseCode))
    {
      Operation.OutputMessage("Course code not exist");
      return;
    }

    var enrolledStudents = MainMenu.students.Where(r => r.EnrolledCourses.Contains(courseCode)).ToList();

    // Create a table
    var table = new Table();
    // Add columns with different alignments and styles
    table.AddColumn(new TableColumn("[lightcyan1]I[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Student Name[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Student Code[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Student Email[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Student GPA[/]").Centered());
    table.AddColumn(new TableColumn("[lightcyan1]Student EnrolledCourses[/]").Centered());

    // Set table border and title
    table.Border(TableBorder.Rounded);
    table.Title($"[bold]▼ Enrolled [/][lightcyan1]Students[/]");

    AnsiConsole.WriteLine();
    for (int i = 0; i < enrolledStudents.Count; i++)
    {
      table.AddRow($"{i + 1}",
                           $"[thistle1][italic]{enrolledStudents[i].Name}[/][/]",
                           $"[thistle1]{enrolledStudents[i].Code}[/]",
                           $"[lightsteelblue1]{enrolledStudents[i].Email}[/]",
                           $"[lightsteelblue1]{enrolledStudents[i].GPA}[/]",
                           $"[lightcoral]{string.Join(",", enrolledStudents[i].EnrolledCourses)}[/]"
                  );
    }
    // Render the table to the console
    AnsiConsole.Write(table);

    AnsiConsole.Markup($"\n[grey58]...View Students of The Course...[/]\n");

    Operation.FinishOption();
  }
  [Obsolete]

  private static Exam GetExam()
  {
    string str = "Invalid input";

    string courseCode = Operation.GetCourseCodeInSystem("● Enter system course ", "code");
    int courseIndex = Operation.GetCourseIndex(courseCode);
    if (courseCode == "Invalid input")
    {
      Console.WriteLine("InValid Code or course does not exist in doctor assigned courses");
      return null;
    }

    if (!sessionDoctor.CoursesTaughtCodes.Contains(courseCode) || MainMenu.courses[courseIndex].ExamCode != "E000")
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
    int noQuestions = Operation.GetValidIntInput("● Enter Exam ", "No. Questions", 50, 100);

    return new Exam(examCode, examName, courseCode, examDate, noQuestions);
  }
  private static int GetAssignedCourseIndex(string code)
  {
    for (int i = 0; i <= sessionDoctor.CoursesTaughtCodes.Count - 1; i++)
    {
      if (sessionDoctor.CoursesTaughtCodes[i] == code)
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
      if (sessionDoctor.CoursesTaughtCodes.Any(u => u == code))
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





