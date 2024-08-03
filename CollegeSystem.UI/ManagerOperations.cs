using CollegeSystem.Core;
using CollegeSystem.Data;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.UI;

public static class ManagerOperations
{

  public static Manager sessionManager = new();

  [Obsolete]
  public static void ViewManagerInfo()
  {
    var panel01 = new Panel($"[gold3]View Manager Info[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);

    AnsiConsole.Markup("[silver]● The Personal Info of Manager[/]");

    Console.WriteLine();
    var panel = new Panel($"[bold]  Manager [gold3]{sessionManager.Name}[/]  [/]")
    .Border(BoxBorder.Rounded);
    AnsiConsole.Render(panel);

    sessionManager.PrintUser("gold3");

    AnsiConsole.Markup($"\n[grey58]...Personal Info...[/]\n");

    Operation.FinishOption();
  }
  [Obsolete]
  public static void EditManagerInfo()
  {
    Console.WriteLine();
    var panel01 = new Panel($"[gold3]Update Personal Info[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine("[silver]▶ Edit Password and Email[/]\n");

    string str = "Invalid input";

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    string password = Operation.GetValidatedStringInput("● ╭─Set a new ", "password");
    string email = Operation.GetValidEmail("● ╰─Enter new ", "email");
    if (password == str || email == str)
    {
      AnsiConsole.MarkupLine("▶ [red]password or email is wrong format[/]");
      Console.WriteLine();
      return;
    }

    Operation.LoadingOperation("▶ Updating Personal Profile...", 50);

    sessionManager.Password = password;
    sessionManager.Email = email;

    int sessionManagerIndex = Operation.GetUserIndex(sessionManager.Code, 3);

    //save data
    MainMenu.managers[sessionManagerIndex] = sessionManager;
    MainMenu._userRepository.SaveManagerData(MainMenu.managers);

    Console.WriteLine();
    AnsiConsole.Markup($"▶ Profile [green]Updated Successfully[/]\n");

    Operation.FinishOption();
  }
  [Obsolete]

  public static void AddUser()
  {
    Console.WriteLine();
    var panel01 = new Panel($"[gold3]Add User[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    AnsiConsole.MarkupLine($"[gold3]▶ [/][grey93]Add Doctor(1) Or Student(2) (1-2) [/]\n");

    int userChoice = Operation.GetValidIntInput("● Please Enter a ", "Choice", 1, 2);
    if (userChoice == -1)
    {
      Operation.OutputMessage("Invalid input, User not added!");
      return;
    }

    try
    {
      AnsiConsole.MarkupLine("[silver]▶ Add User[/]\n");


      Person user = GetUser(userChoice);

      Operation.LoadingOperation("▶ Adding User To System", 40);
      if (user != null)
      {
        if (userChoice == 1)
        {
          MainMenu.doctors.Add((Doctor)user);
          MainMenu._userRepository.SaveDoctorData(MainMenu.doctors);
        }
        else
        {
          MainMenu.students.Add((Student)user);
          MainMenu._userRepository.SaveStudentData(MainMenu.students);
        }

        Console.WriteLine();
        AnsiConsole.Markup($"▶ [green]Successfully added user[/]\n");

        Operation.FinishOption();
      }
      else
      {
        Operation.OutputMessage("Invalid input, User not added!");
      }

    }
    catch (Exception ex)
    {
      Operation.OutputMessage($"Something went wrong: {ex.Message}");
    }

  }
  [Obsolete]
  public static void EditUserPassword()
  {
    var panel01 = new Panel($"[gold3]Edit User Password[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine("[silver]▶ Edit The Password[/]\n");

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    AnsiConsole.MarkupLine ($"[gold3]▶ [/] [grey93]Edit Doctor(1) Or Student(2) Password (1-2) [/]\n");
    int userChoice = Operation.GetValidIntInput("● Please Enter a ", "Choice", 1, 2);
    if (userChoice == -1)
    {
      Operation.OutputMessage("Invalid input");
      return;
    }

    Console.WriteLine();
    string codeInput = Operation.GetUserCodeInSystem("● Enter a user ", "code", userChoice);
    if (codeInput == "Invalid input")
    {
      Operation.OutputMessage("Invalid input");
      return;
    }

    Console.WriteLine();
    if (Operation.ConfirmAction("● Are you sure you want to edit the password of this user? (y/n): "))
    {
      Console.WriteLine();
      Operation.LoadingOperation("▶ Editting password user...", 70);

      int userIndex;

      string userPassword;

      if (userChoice == 1)
      {
        userIndex = Operation.GetUserIndex(codeInput, 1);

        userPassword = GetUserPassword(userIndex, codeInput, 1);
        if (userPassword == "Invalid input")
        {
          Operation.OutputMessage("Invalid input");
          return;
        }

        //save doctor data to doctors list
        MainMenu.doctors[userIndex].Password = userPassword;
        MainMenu._userRepository.SaveDoctorData(MainMenu.doctors);
      }
      else
      {
        userIndex = Operation.GetUserIndex(codeInput, 2);
        userPassword = GetUserPassword(userIndex, codeInput, 2);

        //save student data to students list
        MainMenu.students[userIndex].Password = userPassword;
        MainMenu._userRepository.SaveStudentData(MainMenu.students);
      }

      Console.WriteLine();
      AnsiConsole.Markup($"▶ [green]Successfully edited user password[/]\n");

      Operation.FinishOption();
    }
    else
    {
      Operation.OutputMessage("Editting Canceled!");
    }
  }
  [Obsolete]
  public static void RemoveUser()
  {
    var panel01 = new Panel($"[gold3]Remove User[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine("[silver]▶ Remove User[/]\n");

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    AnsiConsole.MarkupLine($"[gold3]▶ [/] [grey93]Remove Doctor(1) Or Student(2) Password (1-2) [/]\n");
    int userChoice = Operation.GetValidIntInput("● Please User ", "Choice", 1, 2);
    if (userChoice == -1)
    {
      Operation.OutputMessage("Invalid input");
      return;
    }

    Console.WriteLine();
    string codeInput = Operation.GetUserCodeInSystem("● Enter a user ", "code", userChoice);
    if (codeInput == "Invalid input")
    {
      Operation.OutputMessage("Invalid input");
      return;
    }

    Console.WriteLine();
    if (Operation.ConfirmAction("● Are you sure you want to remove this user (y/n)? "))
    {
      Operation.LoadingOperation("▶ Removing user...", 70);

      int userIndex;

      if (userChoice == 1)
      {
        userIndex = Operation.GetUserIndex(codeInput, 1);

        int doctorIndex = Operation.GetUserIndex(codeInput, 1);

        //optimize student.enrolledCourses data
        for (int i = 0; i < MainMenu.doctors[doctorIndex].CoursesTaughtCodes.Count; i++)
        {
          var enrolledStudents = MainMenu.students.Where(r => r.EnrolledCourses.Contains(MainMenu.doctors[doctorIndex].CoursesTaughtCodes[i])).ToList();
          for (int j = 0; j < enrolledStudents.Count; j++)
          {
            enrolledStudents[j].EnrolledCourses.Remove(MainMenu.doctors[doctorIndex].CoursesTaughtCodes[i]);

            int studentIndex = Operation.GetUserIndex(enrolledStudents[j].Code, 2);
            MainMenu.students[studentIndex] = enrolledStudents[j];
          }
        }
        MainMenu._userRepository.SaveStudentData(MainMenu.students);

        //optimize exams of system and exams of doctor
        for (int i = 0; i < MainMenu.doctors[doctorIndex].DoctorExamsCodes.Count; i++)
        {
          if (MainMenu.exams.Any(r => r.ExamCode == MainMenu.doctors[doctorIndex].DoctorExamsCodes[i]))
          {
            int examIndex = Operation.GetExamIndex(MainMenu.doctors[doctorIndex].DoctorExamsCodes[i]);
            MainMenu.exams.Remove(MainMenu.exams[examIndex]);
          }
        }
        MainMenu._examRepository.SaveExamData(MainMenu.exams);
        MainMenu.doctors[doctorIndex].DoctorExamsCodes.Clear();

        //optimize courses of system and courses of doctor
        for (int i = 0; i < MainMenu.doctors[doctorIndex].CoursesTaughtCodes.Count; i++)
        {
          if (MainMenu.courses.Any(r => r.CourseCode == MainMenu.doctors[doctorIndex].CoursesTaughtCodes[i]))
          {
            int courseIndex = Operation.GetCourseIndex(MainMenu.doctors[doctorIndex].CoursesTaughtCodes[i]);
            MainMenu.courses[courseIndex].DoctorCode = "D000";
          }
        }
        MainMenu._courseRepository.SaveCourseData(MainMenu.courses);
        MainMenu.doctors[doctorIndex].CoursesTaughtCodes.Clear();

        //remove doctor from doctors list
        MainMenu.doctors.Remove(MainMenu.doctors[userIndex]);
        MainMenu._userRepository.SaveDoctorData(MainMenu.doctors);

      }
      else if (userChoice == 2)
      {
        userIndex = Operation.GetUserIndex(codeInput, 2);

        int studentIndex = Operation.GetUserIndex(codeInput, 2);

        for (int i = 0; i < MainMenu.students[studentIndex].EnrolledCourses.Count; i++)
        {
          if (MainMenu.courses.Any(r => r.CourseCode == MainMenu.students[studentIndex].EnrolledCourses[i]))
          {
            int courseIndex = Operation.GetCourseIndex(MainMenu.students[studentIndex].EnrolledCourses[i]);
            MainMenu.courses[courseIndex].NoStudents--;
          }
        }
        MainMenu._courseRepository.SaveCourseData(MainMenu.courses);
        MainMenu.students[studentIndex].EnrolledCourses.Clear();

        //remove student from students list
        MainMenu.students.Remove(MainMenu.students[userIndex]);
        MainMenu._userRepository.SaveStudentData(MainMenu.students);
      }

      Console.WriteLine();
      AnsiConsole.Markup($"▶ [green]Successfully removed user[/]\n");

      Operation.FinishOption();
    }
    else
    {
      Operation.OutputMessage("Removal Canceled!");
    }
  }
  [Obsolete]
  public static void ViewUserDetails()
  {
    var panel01 = new Panel($"[gold3]View User Details[/]")
   .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine("[silver]▶ View User Details[/]\n");

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    AnsiConsole.MarkupLine($"[gold3]▶ [/] [grey93]View Doctor(1) Or Student(2) Details (1-2) [/]\n");
    int userChoice = Operation.GetValidIntInput("Please User ", "Choice", 1, 2);
    if (userChoice == -1)
    {
      Operation.OutputMessage("Invalid input");
      return;
    }

    Console.WriteLine();
    string codeInput = Operation.GetUserCodeInSystem("● Enter a user ", "code", userChoice);
    if (codeInput == "Invalid input")
    {
      Operation.OutputMessage("Invalid input");
      return;
    }

    Operation.LoadingOperation("▶ Viewing User Details", 50);

    int userIndex;

    if (userChoice == 1)
    {
      userIndex = Operation.GetUserIndex(codeInput, 1);

      //get doctor from doctors list
      Doctor doctor = MainMenu.doctors[userIndex];

      Console.WriteLine();
      AnsiConsole.Markup($"▶ Doctor [gold3]{doctor.Name}[/]\n");
      AnsiConsole.Markup($"[white]╭───────────────╯[/]\n");
      doctor.PrintUser("gold3");
    }
    else if (userChoice == 2)
    {
      userIndex = Operation.GetUserIndex(codeInput, 2);

      //get student from students list
      Student student = MainMenu.students[userIndex];

      Console.WriteLine();
      AnsiConsole.Markup($"▶ Student [gold3]{student.Name}[/]\n");
      Console.WriteLine();
      student.PrintUser("gold3");
    }

    Console.WriteLine();
    AnsiConsole.Markup($"[grey58]...User Details...[/]\n");

    Operation.FinishOption();

  }
  [Obsolete]
  public static void ViewUsers()
  {
    var panel01 = new Panel($"[gold3]View Users In System[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine("[silver]▶ View Users[/]\n");

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    AnsiConsole.MarkupLine($"[gold3]▶ [/][grey93]View Doctors(1) Or Students(2) (1-2) [/]\n");
    int userChoice = Operation.GetValidIntInput("● Please Enter a ", "Choice", 1, 2);
    if (userChoice == -1)
    {
      Operation.OutputMessage("Invalid input, User not added!");
      return;
    }

    Operation.LoadingOperation("View All Users In The System", 90);

    if (userChoice == 1)
    {

      // Create a table
      var table = new Table();
      // Add columns with different alignments and styles
      table.AddColumn(new TableColumn("[gold3]I[/]").Centered());
      table.AddColumn(new TableColumn("[gold3]Name[/]").Centered());
      table.AddColumn(new TableColumn("[gold3]Code[/]").Centered());
      table.AddColumn(new TableColumn("[gold3]Department[/]").Centered());
      table.AddColumn(new TableColumn("[gold3]Email[/]").Centered());

      // Set table border and title
      table.Border(TableBorder.Rounded);
      table.Title($"[white]▼ Doctors In The System Of [yellow3_1]{Manager.CollegeName}[/] College[/]");

      AnsiConsole.WriteLine();
      for (int i = 0; i < MainMenu.doctors.Count; i++)
      {
        table.AddRow($"[gold3]{i + 1}[/]",
                            $"Dr.{MainMenu.doctors[i].Name}",
                            $"[yellow3]{MainMenu.doctors[i].Code}[/]",
                            $"[yellow3]{MainMenu.doctors[i].Department}[/]",
                            $"[white]{MainMenu.doctors[i].Email}[/]"
                    );
      }
      // Render the table to the console
      AnsiConsole.Write(table);

    }
    else if (userChoice == 2)
    {
      // Create a table
      var table = new Table();
      // Add columns with different alignments and styles
      table.AddColumn(new TableColumn("[gold3]I[/]").Centered());
      table.AddColumn(new TableColumn("[gold3]Name[/]").Centered());
      table.AddColumn(new TableColumn("[gold3]Code[/]").Centered());
      table.AddColumn(new TableColumn("[gold3]Department[/]").Centered());
      table.AddColumn(new TableColumn("[gold3]Year Of Study[/]").Centered());
      table.AddColumn(new TableColumn("[gold3]Email[/]").Centered());
      table.AddColumn(new TableColumn("[gold3]Gender[/]").Centered());

      // Set table border and title
      table.Border(TableBorder.Rounded);
      table.Title($"[bold]▼ Students In The System Of [yellow3_1]{Manager.CollegeName}[/] College[/]");

      AnsiConsole.WriteLine();
      for (int i = 0; i < MainMenu.students.Count; i++)
      {
        table.AddRow($"{i + 1}",
                            $"{MainMenu.students[i].Name}",
                            $"[yellow3]{MainMenu.students[i].Code}[/]",
                            $"[yellow3]{MainMenu.students[i].Department}[/]",
                            $"[white]{MainMenu.students[i].YearOfStudy}[/]",
                            $"{MainMenu.students[i].Email}",
                            $"[yellow3]{MainMenu.students[i].Gender}[/]"
                    );
      }
      // Render the table to the console
      AnsiConsole.Write(table);
    }

    Console.WriteLine();
    AnsiConsole.Markup($"[grey58]...Users in the system...[/]\n");

    Operation.FinishOption();
  }

  [Obsolete]
  public static void AddCourse()
  {
    var panel01 = new Panel($"[gold3]Add Course[/]")
   .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine("[silver]▶ Get Course Details[/]\n");

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    try
    {
      Course course = GetCourse();

      Operation.LoadingOperation("▶ Adding Course To The System", 40);

      if (course != null)
      {
        MainMenu.courses.Add(course);

        //save course to courses list
        MainMenu._courseRepository.SaveCourseData(MainMenu.courses);

        Console.WriteLine();
        AnsiConsole.Markup($"▶ [green]Successfully added course[/]\n");

        Operation.FinishOption();
      }
      else
      {
        Operation.OutputMessage("Invalid input, Course not added!");
      }
    }
    catch (Exception ex)
    {
      Operation.OutputMessage($"Something went wrong: {ex.Message}");
    }
  }
  [Obsolete]
  public static void RemoveCourse()
  {
    var panel01 = new Panel($"[gold3]Remove Course[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

      AnsiConsole.MarkupLine("[silver]▶ Remove Course By Code[/]\n");

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    string codeInput = Operation.GetCourseCodeInSystem("● Enter a course ", "Code");
    if (codeInput == "Invalid input")
    {
      Console.WriteLine("Invalid input");
      return;
    }

    Console.WriteLine();
    if (Operation.ConfirmAction("● Are you sure you want to remove this course? (y/n) "))
    {
      Operation.LoadingOperation("▶ Removing course...", 70);

      int courseIndex = Operation.GetCourseIndex(codeInput);

      Course course = MainMenu.courses[courseIndex];


      //optimize student enrolledCourses data
      var enrolledStudents = MainMenu.students.Where(r => r.EnrolledCourses.Contains(course.CourseCode)).ToList();
      for (int j = 0; j < enrolledStudents.Count; j++)
      {
        enrolledStudents[j].EnrolledCourses.Remove(course.CourseCode);

        int studentIndex = Operation.GetUserIndex(enrolledStudents[j].Code, 2);
        MainMenu.students[studentIndex] = enrolledStudents[j];
      }
      MainMenu._userRepository.SaveStudentData(MainMenu.students);

      //optimize exams of system
      if (MainMenu.exams.Any(r => r.CourseCode == course.CourseCode))
      {
        int examIndex = Operation.GetExamIndex(course.ExamCode);
        MainMenu.exams.Remove(MainMenu.exams[examIndex]);
      }
      MainMenu._examRepository.SaveExamData(MainMenu.exams);

      //optimize doctor who assigned the course
      if (MainMenu.doctors.Any(r => r.CoursesTaughtCodes.Contains(course.CourseCode)))
      {
        int doctorIndex = Operation.GetUserIndex(course.DoctorCode, 1);
        MainMenu.doctors[doctorIndex].CoursesTaughtCodes.Remove(course.CourseCode);
      }
      if (MainMenu.doctors.Any(r => r.DoctorExamsCodes.Contains(course.ExamCode)))
      {
        int doctorIndex = Operation.GetUserIndex(course.DoctorCode, 1);
        MainMenu.doctors[doctorIndex].DoctorExamsCodes.Remove(course.ExamCode);
      }
      MainMenu._userRepository.SaveDoctorData(MainMenu.doctors);

      //remove course from courses list
      MainMenu.courses.Remove(course);
      MainMenu._courseRepository.SaveCourseData(MainMenu.courses);

      Console.WriteLine();
      AnsiConsole.Markup($"▶ [green]Successfully removed course[/]\n");

      Operation.FinishOption();
    }
    else
    {
      Operation.OutputMessage("Removal Canceled!");
    }
  }
  [Obsolete]
  public static void ViewCourses()
  {
    var panel01 = new Panel($"[gold3]View Courses[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);

    AnsiConsole.MarkupLine("[silver]▶ View All Courses of System[/]\n");



    // Create a table
    var table = new Table();
    // Add columns with different alignments and styles
    table.AddColumn(new TableColumn("[gold3]I[/]").Centered());
    table.AddColumn(new TableColumn("[gold3]Course Code[/]").Centered());
    table.AddColumn(new TableColumn("[gold3]Course Name[/]").Centered());
    table.AddColumn(new TableColumn("[gold3]Exam Code[/]").Centered());
    table.AddColumn(new TableColumn("[gold3]Doctor Code[/]").Centered());
    table.AddColumn(new TableColumn("[gold3]Department[/]").Centered());
    table.AddColumn(new TableColumn("[gold3]NoStudents[/]").Centered());
    table.AddColumn(new TableColumn("[gold3]NoOfHours[/]").Centered());

    // Set table border and title
    table.Border(TableBorder.Rounded);
    table.Title($"[bold]▼ [gold3]Courses[/] In [gold3]{Manager.CollegeName}[/] System College[/]");

    AnsiConsole.WriteLine();
    for (int i = 0; i < MainMenu.courses.Count; i++)
    {
      table.AddRow($"{i + 1}",
                          $"{MainMenu.courses[i].CourseCode}",
                          $"[italic]{MainMenu.courses[i].CourseName}[/]",
                          $"[italic]{MainMenu.courses[i].ExamCode}[/]",
                          $"[lightsteelblue1]{MainMenu.courses[i].DoctorCode}[/]",
                          $"[lightsteelblue1]{MainMenu.courses[i].Department}[/]",
                          $"[lightsteelblue1]{MainMenu.courses[i].NoStudents}[/]",
                          $"[lightsteelblue1]{MainMenu.courses[i].NoOfHours}[/]"
                  );
    }
    // Render the table to the console
    AnsiConsole.Write(table);

    AnsiConsole.Markup($"\n[grey58]...View Courses...[/]\n");

    Operation.FinishOption();
  }
  [Obsolete]
  public static void ViewCourseDetails()
  {
    var panel01 = new Panel($"[gold3] View Course Details[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine("[silver]▶ View Course Details By Code[/]\n");

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    string codeInput = Operation.GetCourseCodeInSystem("● Enter A Course ", "Code");
    if (codeInput == "Invalid input")
    {
      Operation.OutputMessage("Invalid input");
      return;
    }

    Operation.LoadingOperation("▶ Viewing Course Details..", 50);

    int courseIndex = Operation.GetCourseIndex(codeInput);

    Course course = MainMenu.courses[courseIndex];

    Console.WriteLine();
    AnsiConsole.Markup($"▼ [gold3]Course {course.CourseName} Details[/]\n");
    AnsiConsole.Markup($"[bold]╭────────────────────╮[/]\n");
    AnsiConsole.Markup($"│  [gold3]Course In System  [/]│\n");
    AnsiConsole.Markup($"[bold]├────────────────────╯[/]\n");
    course.PrintCourse("gold3");

    AnsiConsole.Markup($"\n[grey58]...View Course Details...[/]\n");

    Operation.FinishOption();

  }

  [Obsolete]
  public static void ScheduleExams()
  {
    var panel01 = new Panel($"[gold3]Schedule Exams[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine("[silver]▶ Show And Schedule Exams In The System[/]\n");


    // Create a table
    var table = new Table();
    // Add columns with different alignments and styles
    table.AddColumn(new TableColumn("[gold3]I[/]").Centered());
    table.AddColumn(new TableColumn("[gold3]Exam Code[/]").Centered());
    table.AddColumn(new TableColumn("[gold3]Exam Name[/]").Centered());
    table.AddColumn(new TableColumn("[gold3]Course Code[/]").Centered());
    table.AddColumn(new TableColumn("[gold3]Exam Date[/]").Centered());
    table.AddColumn(new TableColumn("[gold3]NoQuestions[/]").Centered());

    // Set table border and title
    table.Border(TableBorder.Rounded);
    table.Title($"[bold]▼ [gold3]Exams[/] In The System Of [gold3]{Manager.CollegeName}[/] College[/]");

    AnsiConsole.WriteLine();
    for (int i = 0; i < MainMenu.exams.Count; i++)
    {
      table.AddRow($"{i + 1}",
                          $"{MainMenu.exams[i].ExamCode}",
                          $"[italic]{MainMenu.exams[i].ExamName}[/]",
                          $"[italic]{MainMenu.exams[i].CourseCode}[/]",
                          $"[lightsteelblue1]{MainMenu.exams[i].ExamDate}[/]",
                          $"[lightsteelblue1]{MainMenu.exams[i].NoQuestions}[/]"
                  );
    }
    // Render the table to the console
    AnsiConsole.Write(table);

    AnsiConsole.Markup($"[gold3]▶ [/] [grey93]Exams In Systems[/]\n\n");
    int userChoice = Operation.GetValidIntInput("● Press 1(Y)/2(N) Are You Want To ", "Schedule Exam", 1, 2);
    if (userChoice == -1)
    {
      Operation.OutputMessage("Invalid input");
      return;
    }

    if (userChoice == 1)
    {
      string codeInput = Operation.GetExamCodeInSystem("● Enter a exam ", "code");

      int examIndex = Operation.GetExamIndex(codeInput);
      if (examIndex == -1)
      {
        Operation.OutputMessage("Invalid input");
        return;
      }

      DateTime examDate = Operation.GetValidatedDateInput("▶ Enter Exam ", "Date");
      if (examDate == default)
      {
        Operation.OutputMessage("Invalid input");
        return;
      }

      Console.WriteLine();
      if (Operation.ConfirmAction("● Are you sure you want to schedule this exam? "))
      {
        Operation.LoadingOperation("▶ Scheduling exam...", 40);

        //save exam to exams list
        MainMenu.exams[examIndex].ExamDate = examDate;
        MainMenu._examRepository.SaveExamData(MainMenu.exams);

        Console.WriteLine();
        AnsiConsole.Markup($"▶ [green]Successfully removed course[/]\n");

        Operation.FinishOption();

      }
      else
      {
        Operation.OutputMessage("Shceduling Canceled!");
      }
    }
    else
    {
      AnsiConsole.Markup($"▶ [yellow]Show Ended[/]\n");
      Operation.FinishOption();
    }
  }

  [Obsolete]
  public static void SystemReport()
  {
    var panel01 = new Panel($"[gold3]System Report[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Render(panel01);

    AnsiConsole.MarkupLine("[silver]▶ Get System Report[/]\n");

    Operation.LoadingOperation("▶ Reporting The System", 50);

    // Create a table
    var table = new Table();
    // Add columns with different alignments and styles
    table.AddColumn(new TableColumn("[gold3]I[/]").Centered());
    table.AddColumn(new TableColumn("[gold3]Department Code[/]").Centered());
    table.AddColumn(new TableColumn("[gold3]Department Name[/]").Centered());

    // Set table border and title
    table.Border(TableBorder.Rounded);
    table.Title($"[gold3]★ [/]Departments");

    string[] departsCodes = { "CS", "IT", "IS", "MM" };
    string[] departsNames = { "Computer Science", "Information Technology", "Information System", "Multi Media" };

    AnsiConsole.WriteLine();
    for (int i = 0; i < departsCodes.Length; i++)
    {
      table.AddRow($"[gold3]{i + 1}[/]",
                          $"{departsCodes[i]}",
                          $"{departsNames[i]}"
                  );
    }
    // Render the table to the console
    AnsiConsole.Write(table);


    Console.WriteLine();
    AnsiConsole.MarkupLine($"About...");
    AnsiConsole.MarkupLine($"● [gold3]College {Manager.CollegeName} Of Beni-Suef University[/]");
    AnsiConsole.MarkupLine($"▶ [gold3]College seeks to develop and enrich practical and educational level[/]");
    AnsiConsole.MarkupLine($"  [gold3]of Computer Science,Information Technology, and Mulit-Media fields.[/]");

    Console.WriteLine();
    AnsiConsole.Markup($"[grey58]...System Report...[/]\n");

    Operation.FinishOption();
  }


  private static string GetUserPassword(int userIndex, string code, int i)
  {
    if (i == 1)
    {
      Console.WriteLine($"● User old password: {MainMenu.doctors[userIndex].Password}");
    }
    else if (i == 2)
    {
      Console.WriteLine($"● User old password: {MainMenu.students[userIndex].Password}");
    }
    string password = Operation.GetValidatedStringInput("● Enter a user new ", "password");

    return password;
  }
  private static Person GetUser(int userChoice)
  {
    Console.WriteLine();

    // usesd for validating checks
    string str = "Invalid input";

    string password = "00000";
    Thread.Sleep(100);
    AnsiConsole.Markup($"[bold]● ╭─ Default Password: [/][gold3_1]{password}[/]\n");

    string name = Operation.GetValidatedStringInput("● ├─ Enter User ", "Name");
    if (name == str)
      return null;

    string email = "entity@gmail.com";
    Thread.Sleep(100);
    AnsiConsole.Markup($"[bold]● ├─ Default Email: [/][gold3_1]{email}[/]\n");

    Thread.Sleep(100);
    string[] departs = { "CS", "IS", "IT", "MM", "COMPUTER SCIENCE", "INFORMATION SYSTEM", "INFORMATION TECHNOLOGY", "MULTI MEDIA" };
    string department = Operation.GetValidInputArray("● ├─ Enter User ", "Department", departs);
    if (department == str)
      return null;

    bool activate = false;

    if (userChoice == 1)
    {
      // Generated automatically on that appotach [D000]
      string nationalId = Generator.GenerateDoctorNationalId(MainMenu.doctors);
      Thread.Sleep(100);
      AnsiConsole.Markup($"[bold]● ├─ Generated National ID : [/][gold3_1]{nationalId}[/]\n");

      string code = Generator.GenerateDoctorCode(MainMenu.doctors);
      Thread.Sleep(100);
      AnsiConsole.Markup($"[bold]● ├─ Generated Code : [/][gold3_1]{code}[/]\n");

      DateTime dateOfHire = Operation.GetValidatedDateInput("● ╰─ Enter ", "Date Of Hire");
      if (dateOfHire == default)
        return null;

      var coursesTaughtCodes = new List<string>();
      var doctorExamsCodes = new List<string>();

      return new Doctor(nationalId, code, name, password, email, department, activate, dateOfHire, coursesTaughtCodes, doctorExamsCodes);
    }
    else if (userChoice == 2)
    {
      string nationalId = Generator.GenerateStudentNationalId(MainMenu.students);
      Thread.Sleep(100);
      AnsiConsole.Markup($"[bold]● ├─ Generated National Id : [/][gold3_1]{nationalId}[/]\n");

      string code = Generator.GenerateStudentCode(MainMenu.students);
      Thread.Sleep(100);
      AnsiConsole.Markup($"[bold]● ├─ Generated Code : [/][gold3]{code}[/]\n");


      string[] grades = { "A", "A+", "A-", "B", "B+", "B-", "D", "D+", "D-" };
      var rnd = new Random().Next(grades.Length);
      string grade = grades[rnd]; // default
      Thread.Sleep(100);
      AnsiConsole.Markup($"[bold]● ├Default Grade : [/][gold3_1]{grade}[/]\n");

      double gpa = 0.0; // default
      switch (grades[rnd])
      {
        case "A+":
          gpa = 3.7;
          break;
        case "A":
          gpa = 3.5;
          break;
        case "A-":
          gpa = 3.3;
          break;
        case "B+":
          gpa = 3.1;
          break;
        case "B":
          gpa = 2.9;
          break;
        case "B-":
          gpa = 2.8;
          break;
        case "D+":
          gpa = 2.7;
          break;
        case "D":
          gpa = 2.6;
          break;
        case "D-":
          gpa = 2.5;
          break;
      }
      Thread.Sleep(100);
      AnsiConsole.Markup($"[bold]● ├─ Default GPA : [/][gold3]{gpa}[/]\n");

      string[] arr02 = { "M", "F", "MALE", "FEMALE" };
      string gender = Operation.GetValidInputArray("● ├─ Enter a ", "gender", arr02);
      if (gender == str)
        return null;

      string[] arr03 = { "1", "2", "3", "4", "FIRST", "SECOND", "THIRD", "FOURTH" };
      string yearOfStudy = Operation.GetValidInputArray("● ╰─ Enter a ", "year of study", arr03);
      if (yearOfStudy == str)
        return null;

      List<string> enrolledCourses = new();

      return new Student(nationalId, code, name, password, email, department, activate, gpa, grade, gender, yearOfStudy, enrolledCourses);
    }

    return null;
  }
  private static Course GetCourse()
  {
    string str = "Invalid input";

    string code = Generator.GenerateCourseCode(MainMenu.courses);
    AnsiConsole.Markup($"● ╭─ Generated Course Code: [gold3]{code}[/]\n");

    string name = Operation.GetValidatedStringInput("● ├─ Enter Course ", "Name");
    if (name == str)
      return null;
    string description = Operation.GetValidatedStringInput("● ├─ Enter Course ", "Description");
    if (description == str)
      return null;
    string doctorCode = "D000";
    string examCode = "E000";

    string[] arr01 = { "CS", "IS", "IT", "MM", "COMPUTER SCIENCE", "INFORMATION SYSTEM", "INFORMATION TECHNOLOGY", "MULTI MEDIA" };
    string department = Operation.GetValidInputArray("● ├─ Enter Course ", "Department", arr01);
    if (department == str)
      return null;
    int noStudents = 0;

    int noOfHours = Operation.GetValidIntInput("● ╰─ Enter Course ", "Number of Hours", 1, 4);
    if (noOfHours == -1)
      return null;

    return new Course(code, name, description, doctorCode, examCode, department, noStudents, noOfHours);
  }

}

