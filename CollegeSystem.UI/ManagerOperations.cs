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

  public static void AdminInformation()
  {
    var panel01 = new Panel($"[gold3]Admin Information[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);

    AnsiConsole.Markup("[silver]● Manager Information[/]");

    Console.WriteLine();
    var panel = new Panel($"[bold]  Manager [gold3]{sessionManager.FullName}[/]  [/]")
    .Border(BoxBorder.Rounded);
    AnsiConsole.Write(panel);

    sessionManager.PrintUser("gold3");

    AnsiConsole.Markup($"\n[grey58]...Adminstration Info...[/]\n");

    Operation.FinishOption();
  }
  public static void EditAdminInfo()
  {
    Console.WriteLine();
    var panel01 = new Panel($"[gold3]Edit Admin Info[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    var root = new Tree($"[bold]▼ What Do You Want To Edit?[/]");
    var node = root.AddNode($"[white]───────────────────────╯[/]");
    node = root.AddNode($"[white]1) Full Name[/]");
    node = root.AddNode($"[white]2) Password[/]");
    node = root.AddNode($"[white]3) Email[/]");
    AnsiConsole.Write(root);
    Console.WriteLine();


    AnsiConsole.Markup("● Choose a [green]Number[/] Or Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    Console.WriteLine();

    if (int.TryParse(option, out int number) && (number == 1 || number == 2 || number == 3))
    {
      Operation.LoadingOperation("▶ Updating Personal Profile...", 50);

      if (number == 1)
      {
        string fullName = Operation.GetValidatedStringInput("●─> Set New ", "Full Name");

        if (fullName == "Invalid input")
        {
          AnsiConsole.MarkupLine("▶ [red]Full Name Is Wrong Format[/]");
          Console.WriteLine();
          return;
        }

        sessionManager.FullName = fullName;
      }
      else if (number == 2)
      {
        string password = Operation.GetValidatedStringInput("●─> Set New ", "Password");

        if (password == "Invalid input")
        {
          AnsiConsole.MarkupLine("▶ [red]Password Is Wrong Format[/]");
          Console.WriteLine();
          return;
        }

        sessionManager.Password = password;
      }
      else if (number == 3)
      {
        string email = Operation.GetValidEmail("●─> Set New ", "Email");

        if (email == "Invalid input")
        {
          AnsiConsole.MarkupLine("▶ [red]Email Is Wrong Format[/]");
          Console.WriteLine();
          return;
        }

        sessionManager.Email = email;
      }

      int sessionManagerIndex = Operation.GetUserIndex(sessionManager.Code);
      //save data
      MainMenu.managers[sessionManagerIndex] = sessionManager;
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
      EditAdminInfo();
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

        string result = $"[blue](Manager){sessionManager.FullName} - {sessionManager.Code}[/]: {notify}. [grey]Created At[/] [red]{DateTime.Now}[/]";
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
        table.Title($"[bold]▼ Notifies By Manager[/]");
        // Render the table to the console
        AnsiConsole.Write(table);
      }

      int sessionManagerIndex = Operation.GetUserIndex(sessionManager.Code);
      //save data
      MainMenu.managers[sessionManagerIndex] = sessionManager;
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

  public static void CreateNewUser()
  {
    Console.WriteLine();
    var panel01 = new Panel($"[gold3]Create New User Email[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    var root = new Tree($"[bold]▼ Creating Email For:[/]");
    var node = root.AddNode($"[white]1) Doctor[/]");
    node = root.AddNode($"[white]2) Student[/]");
    AnsiConsole.Write(root);
    Console.WriteLine();

    AnsiConsole.Markup("● Choose a [green]Number[/] Or Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();

    if (int.TryParse(option, out int number) && (number == 1 || number == 2))
    {
      try
      {
        AnsiConsole.MarkupLine("[silver]▶ Create User Email[/]\n");

        Person user = GetUser(number);

        Operation.LoadingOperation("▶ Creating User Email In System", 40);
        if (user != null)
        {
          if (number == 1)
          {
            MainMenu.doctors.Add((Doctor)user);
            MainMenu._userRepository.SaveDoctorData(MainMenu.doctors);
          }
          else if (number == 2)
          {
            MainMenu.students.Add((Student)user);
            MainMenu._userRepository.SaveStudentData(MainMenu.students);
          }

          Console.WriteLine();
          AnsiConsole.Markup($"▶ [green]Email Created Successfully[/]\n");

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
      CreateNewUser();
    }
  }
  public static void EditUserDetails()
  {
    var panel01 = new Panel($"[gold3]Edit User Details[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    var userCode = Operation.GetUserCodeInSystem("Enter User ", "Code");
    if (userCode == "Invalid input")
    {
      Operation.OutputMessage("Invalid input");
      return;
    }

    var root = new Tree($"[bold]▼ Editting User Details:[/]");
    var node = root.AddNode($"[white]1) Full Name[/]");
    node = root.AddNode($"[white]2) Password[/]");
    node = root.AddNode($"[white]3) Department[/]");
    node = root.AddNode($"[white]4) Email Activate[/]");

    AnsiConsole.Write(root);
    Console.WriteLine();

    AnsiConsole.Markup("● Choose a [green]Number[/] Or Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (int.TryParse(option, out int number))
    {
      if (Operation.ConfirmAction("● Are you sure you want to edit this option? (y/n): "))
      {
        Console.WriteLine();
        int userIndex = Operation.GetUserIndex(userCode);
        if (number == 1)
        {
          Operation.LoadingOperation("▶ Editting User Full Name...", 70);
          Console.WriteLine($"● User Full Name: {MainMenu.doctors[userIndex].FullName}");
          string userFullName = Operation.GetValidatedStringInput("● Enter User New ", "Full Name");
          if (userFullName == "Invalid input")
          {
            Operation.OutputMessage("Invalid input");
            return;
          }

          //saving the change of option
          if (userCode[0] == 'D')
          {
            //save doctor data to doctors list
            MainMenu.doctors[userIndex].FullName = userFullName;
          }
          else if (userCode[0] == 'S')
          {
            //save student data to doctors list
            MainMenu.students[userIndex].FullName = userFullName;
          }
        } // Change Full Name
        else if (number == 2)
        {
          Operation.LoadingOperation("▶ Editting User Password...", 70);
          Console.WriteLine($"● User Old Password: {MainMenu.doctors[userIndex].Password}");
          string userPassword = Operation.GetValidatedStringInput("● Enter New ", "Password");
          if (userPassword == "Invalid input")
          {
            Operation.OutputMessage("Invalid input");
            return;
          }

          //saving the change of option
          if (userCode[0] == 'D')
          {
            //save doctor data to doctors list
            MainMenu.doctors[userIndex].Password = userPassword;
          }
          else if (userCode[0] == 'S')
          {
            //save student data to doctors list
            MainMenu.students[userIndex].Password = userPassword;
          }
        } // Change Password
        else if (number == 3)
        {
          Operation.LoadingOperation("▶ Editting User Department...", 70);
          Console.WriteLine($"● User Department: {MainMenu.doctors[userIndex].Department}");
          string department = Operation.GetValidatedStringInput("● Enter User New ", "Department");
          if (department == "Invalid input")
          {
            Operation.OutputMessage("Invalid input");
            return;
          }

          //saving the change of option
          if (userCode[0] == 'D')
          {
            //save doctor data to doctors list
            MainMenu.doctors[userIndex].Department = department;
          }
          else if (userCode[0] == 'S')
          {
            //save student data to doctors list
            MainMenu.students[userIndex].Department = department;
          }
        } // Change Department
        else if (number == 4)
        {
          Operation.LoadingOperation("▶ Activating User Email...", 70);
          Console.WriteLine($"● Email Is Activate: {MainMenu.doctors[userIndex].IsEmailActivate}");
          Console.Write("Enter 1'true' Or 0'false': ");
          int activation = Convert.ToInt32(Console.ReadLine());
          if (activation is not 0 && activation is not 1)
          {
            Operation.OutputMessage("Invalid input");
            return;
          }

          //saving the change of option
          if (userCode[0] == 'D')
          {
            //save doctor data to doctors list
            MainMenu.doctors[userIndex].IsEmailActivate = (activation == 1) ? true : false;
          }
          else if (userCode[0] == 'S')
          {
            //save student data to doctors list
            MainMenu.students[userIndex].IsEmailActivate = (activation == 1) ? true : false;
          }
        } // Change Activation

        MainMenu._userRepository.SaveDoctorData(MainMenu.doctors);
        MainMenu._userRepository.SaveStudentData(MainMenu.students);

        Console.WriteLine();
        AnsiConsole.Markup($"▶ [green]Successfully Edited User Password[/]\n");
        Operation.FinishOption();
      }//endOf if(confirm)
      else
      {
        Operation.OutputMessage("Editting Canceled!");
      }
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
      EditAdminInfo();
    }
  }
  public static void RemoveUser()
  {
    var panel01 = new Panel($"[gold3]Remove User From System[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine("[silver]▶ Remove User From System[/]\n");

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    Console.WriteLine();
    var userCode = Operation.GetUserCodeInSystem("Enter User ", "Code");
    if (userCode == "Invalid input")
    {
      Operation.OutputMessage("Invalid input");
      return;
    }

    Console.WriteLine();
    if (Operation.ConfirmAction("● Are You Sure You Want To Remove This User (y/n)? "))
    {
      Operation.LoadingOperation("▶ Removing user...", 70);

      int userIndex = Operation.GetUserIndex(userCode);

      if (userCode[0] == 'D')
      {
        //optimize exams of system and exams of doctor
        for (int i = 0; i < MainMenu.doctors[userIndex].DoctorExamsCodes.Count; i++)
        {
          if (MainMenu.exams.Any(r => r.ExamCode == MainMenu.doctors[userIndex].DoctorExamsCodes[i]))
          {
            int examIndex = Operation.GetExamIndex(MainMenu.doctors[userIndex].DoctorExamsCodes[i]);
            MainMenu.exams.Remove(MainMenu.exams[examIndex]);
          }
        }
        MainMenu._examRepository.SaveExamData(MainMenu.exams);
        MainMenu.doctors[userIndex].DoctorExamsCodes.Clear();

        //optimize courses of system and courses of doctor
        for (int i = 0; i < MainMenu.doctors[userIndex].RegisteredCoursesCodes.Count; i++)
        {
          if (MainMenu.courses.Any(r => r.CourseCode == MainMenu.doctors[userIndex].RegisteredCoursesCodes[i]))
          {
            int courseIndex = Operation.GetCourseIndex(MainMenu.doctors[userIndex].RegisteredCoursesCodes[i]);
            MainMenu.courses[courseIndex].DoctorCode = "D000";
          }
        }
        MainMenu._courseRepository.SaveCourseData(MainMenu.courses);
        MainMenu.doctors[userIndex].RegisteredCoursesCodes.Clear();

        //remove doctor from doctors list
        MainMenu.doctors.Remove(MainMenu.doctors[userIndex]);
        MainMenu._userRepository.SaveDoctorData(MainMenu.doctors);

      }
      else if (userCode[0] == 'S')
      {

        for (int i = 0; i < MainMenu.students[userIndex].EnrolledCoursesCodes.Count; i++)
        {
          if (MainMenu.courses.Any(r => r.CourseCode == MainMenu.students[userIndex].EnrolledCoursesCodes[i]))
          {
            int courseIndex = Operation.GetCourseIndex(MainMenu.students[userIndex].EnrolledCoursesCodes[i]);
            MainMenu.courses[courseIndex].NoStudents--;
          }
        }
        MainMenu._courseRepository.SaveCourseData(MainMenu.courses);
        MainMenu.students[userIndex].EnrolledCoursesCodes.Clear();

        //remove student from students list
        MainMenu.students.Remove(MainMenu.students[userIndex]);
        MainMenu._userRepository.SaveStudentData(MainMenu.students);
      }

      Console.WriteLine();
      AnsiConsole.Markup($"▶ [green]Successfully Removed User[/]\n");

      Operation.FinishOption();
    }
    else
    {
      Operation.OutputMessage("Removal Canceled!");
    }
  }
  public static void ViewUserDetails()
  {
    var panel01 = new Panel($"[gold3]View User Details[/]")
   .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine("[silver]▶ View User Full Details[/]\n");

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    Console.WriteLine();
    var userCode = Operation.GetUserCodeInSystem("Enter User ", "Code");
    if (userCode == "Invalid input")
    {
      Operation.OutputMessage("Invalid input");
      return;
    }

    Operation.LoadingOperation("▶ Viewing User Details", 50);

    int userIndex = Operation.GetUserIndex(userCode);

    if (userCode[0] == 'D')
    {
      //get doctor from doctors list
      Doctor doctor = MainMenu.doctors[userIndex];

      Console.WriteLine();
      AnsiConsole.Markup($"▶ Doctor [gold3]{doctor.FullName}[/]\n");
      doctor.PrintUser("gold3");
    }
    else if (userCode[0] == 'S')
    {
      //get student from students list
      Student student = MainMenu.students[userIndex];

      Console.WriteLine();
      AnsiConsole.Markup($"▶ Student [gold3]{student.FullName}[/]\n");
      student.PrintUser("gold3");
    }

    Console.WriteLine();
    AnsiConsole.Markup($"[grey58]...User Details...[/]\n");

    Operation.FinishOption();
  }
  public static void ViewUsers()
  {
    var panel01 = new Panel($"[gold3]View Users In System[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine("[silver]▶ View Users[/]\n");

    AnsiConsole.Markup("● Choose a [green]Number[/] To View [lightcyan1](1)Doctors[/],[thistle1](2)Students[/] Or Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (int.TryParse(option, out int number))
    {
      Console.WriteLine();
      Operation.LoadingOperation("View All Users In The System", 90);

      if (number == 1)
      {
        // Create a table
        var table = new Table();
        // Add columns with different alignments and styles
        table.AddColumn(new TableColumn("[gold3]#[/]").Centered());
        table.AddColumn(new TableColumn("[gold3]Full Name[/]").Centered());
        table.AddColumn(new TableColumn("[gold3]Code[/]").Centered());
        table.AddColumn(new TableColumn("[gold3]Department[/]").Centered());
        table.AddColumn(new TableColumn("[gold3]Date Of Hire[/]").Centered());
        table.AddColumn(new TableColumn("[gold3]Activate[/]").Centered());

        // Set table border and title
        table.Border(TableBorder.Rounded);
        table.Title($"[white]▼ Doctors In System Of [yellow3_1]{Manager.CollegeName}[/] College[/]");

        AnsiConsole.WriteLine();
        for (int i = 0; i < MainMenu.doctors.Count; i++)
        {
          table.AddRow($"[gold3]{i + 1}[/]",
                              $"Dr.{MainMenu.doctors[i].FullName}",
                              $"[yellow3]{MainMenu.doctors[i].Code}[/]",
                              $"[yellow3]{MainMenu.doctors[i].Department}[/]",
                              $"[yellow3]{MainMenu.doctors[i].DateOfHire}[/]",
                              $"[white]{MainMenu.doctors[i].IsEmailActivate}[/]"
                      );
        }
        // Render the table to the console
        AnsiConsole.Write(table);
      }
      else if (number == 2)
      {
        // Create a table
        var table = new Table();
        // Add columns with different alignments and styles
        table.AddColumn(new TableColumn("[gold3]#[/]").Centered());
        table.AddColumn(new TableColumn("[gold3]Full Name[/]").Centered());
        table.AddColumn(new TableColumn("[gold3]Code[/]").Centered());
        table.AddColumn(new TableColumn("[gold3]Gender[/]").Centered());
        table.AddColumn(new TableColumn("[gold3]Department[/]").Centered());
        table.AddColumn(new TableColumn("[gold3]Year Of Study[/]").Centered());
        table.AddColumn(new TableColumn("[gold3]Activate[/]").Centered());

        // Set table border and title
        table.Border(TableBorder.Rounded);
        table.Title($"[bold]▼ Students In The System Of [yellow3_1]{Manager.CollegeName}[/] College[/]");

        AnsiConsole.WriteLine();
        for (int i = 0; i < MainMenu.students.Count; i++)
        {
          table.AddRow($"{i + 1}",
                              $"{MainMenu.students[i].FullName}",
                              $"[yellow3]{MainMenu.students[i].Code}[/]",
                              $"[yellow3]{MainMenu.students[i].Gender}[/]",
                              $"[yellow3]{MainMenu.students[i].Department}[/]",
                              $"[white]{MainMenu.students[i].YearOfStudy}[/]",
                              $"[Green]{MainMenu.students[i].IsEmailActivate}[/]"
                      );
        }
        // Render the table to the console
        AnsiConsole.Write(table);
      }
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
      EditAdminInfo();
    }

    Console.WriteLine();
    AnsiConsole.Markup($"[grey58]...Users in the system...[/]\n");

    Operation.FinishOption();
  }


  public static void CreateNewCourse()
  {
    var panel01 = new Panel($"[gold3]Create New Course[/]")
   .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine("[silver]▶ Get The New Course Details[/]\n");

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    try
    {
      Course course = GetCourse();

      Operation.LoadingOperation("▶ Creating New Course In System", 40);

      if (course != null)
      {
        MainMenu.courses.Add(course);

        //save course to courses list
        MainMenu._courseRepository.SaveCourseData(MainMenu.courses);

        Console.WriteLine();
        AnsiConsole.Markup($"▶ [green]Successfully Created The Course[/]\n");

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
  public static void EditCourseDetails()
  {
    var panel01 = new Panel($"[gold3]Edit Course Details[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine("[silver]▶ Edit Course Details By Code[/]\n");

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    Console.WriteLine();
    string courseCode = Operation.GetCourseCodeInSystem("● Enter The Course ", "Code");
    if (courseCode == "Invalid input")
    {
      Console.WriteLine("Invalid input");
      return;
    }

    int courseIndex = Operation.GetCourseIndex(courseCode);
    Course course = MainMenu.courses[courseIndex];

    Console.WriteLine();

    var root = new Tree($"[bold]▼ What Do You Want To Edit?[/]");
    var node = root.AddNode($"[white]───────────────────────╯[/]");
    node = root.AddNode($"[white]1) Course Name[/]");
    node = root.AddNode($"[white]2) Description[/]");
    node = root.AddNode($"[white]3) No. Of Hours[/]");
    AnsiConsole.Write(root);
    Console.WriteLine();

    AnsiConsole.Markup("● Choose a [green]Number[/] Or Tap [red]'q'[/] To Quit: ");
    var option2 = Console.ReadLine();
    Console.WriteLine();

    if (int.TryParse(option2, out int number) && (number == 1 || number == 2 || number == 3))
    {
      Operation.LoadingOperation("▶ Editting Course Details...", 50);

      if (number == 1)
      {
        string courseName = Operation.GetValidatedStringInput("●─> Set New ", "Course Name");

        if (courseName == "Invalid input")
        {
          AnsiConsole.MarkupLine("▶ [red]Course Name is Wrong Format[/]");
          Console.WriteLine();
          return;
        }

        course.CourseName = courseName;
      }
      else if (number == 2)
      {
        string description = Operation.GetValidatedStringInput("●─> Set New ", "Description");

        if (description == "Invalid input")
        {
          AnsiConsole.MarkupLine("▶ [red]Description Is Wrong Format[/]");
          Console.WriteLine();
          return;
        }

        course.CourseDescription = description;
      }
      else if (number == 3)
      {
        int noOfHours = Operation.GetValidIntInput("●─> Set The New ", "No. Of Hours", 1, 3);

        if (noOfHours == -1)
        {
          AnsiConsole.MarkupLine("▶ [red]No. Of Hours Is Wrong Format[/]");
          Console.WriteLine();
          return;
        }

        course.NoOfHours = noOfHours;
      }

      int sessionManagerIndex = Operation.GetUserIndex(sessionManager.Code);
      //save data
      MainMenu.courses[courseIndex] = course;
      MainMenu._courseRepository.SaveCourseData(MainMenu.courses);

      Console.WriteLine();
      AnsiConsole.Markup($"▶ Course [green]Updated Successfully[/]\n");
      Operation.FinishOption();
    } //endOf if(tryParse)
    else if (option2.ToLower() == "q")
    {
      return;
    }
    else
    {
      Console.WriteLine("Wrong Input!!");
      Console.ReadLine();
      Console.Clear();
      EditAdminInfo();
    }
  }
  public static void RemoveCourse()
  {
    var panel01 = new Panel($"[gold3]Remove Course From System[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine("[silver]▶ Remove Course By Code[/]\n");

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    Console.WriteLine();
    string codeInput = Operation.GetCourseCodeInSystem("● Enter The Course ", "Code");
    if (codeInput == "Invalid input")
    {
      Console.WriteLine("Invalid input");
      return;
    }

    Console.WriteLine();
    if (Operation.ConfirmAction("● Are You Sure You Want To Remove This Course? (y/n) "))
    {
      Operation.LoadingOperation("▶ Removing course...", 70);

      int courseIndex = Operation.GetCourseIndex(codeInput);
      Course course = MainMenu.courses[courseIndex];

      //optimize student enrolledCourses data
      var enrolledStudents = MainMenu.students.Where(r => r.EnrolledCoursesCodes.Contains(course.CourseCode)).ToList();
      for (int j = 0; j < enrolledStudents.Count; j++)
      {
        enrolledStudents[j].EnrolledCoursesCodes.Remove(course.CourseCode);

        int studentIndex = Operation.GetUserIndex(enrolledStudents[j].Code);
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
      if (MainMenu.doctors.Any(r => r.RegisteredCoursesCodes.Contains(course.CourseCode)))
      {
        int doctorIndex = Operation.GetUserIndex(course.DoctorCode);
        MainMenu.doctors[doctorIndex].RegisteredCoursesCodes.Remove(course.CourseCode);
      }
      if (MainMenu.doctors.Any(r => r.DoctorExamsCodes.Contains(course.ExamCode)))
      {
        int doctorIndex = Operation.GetUserIndex(course.DoctorCode);
        MainMenu.doctors[doctorIndex].DoctorExamsCodes.Remove(course.ExamCode);
      }
      MainMenu._userRepository.SaveDoctorData(MainMenu.doctors);

      //remove course from courses list
      MainMenu.courses.Remove(course);
      MainMenu._courseRepository.SaveCourseData(MainMenu.courses);

      Console.WriteLine();
      AnsiConsole.Markup($"▶ [green]Successfully Removed Course[/]\n");

      Operation.FinishOption();
    }
    else
    {
      Operation.OutputMessage("Removal Canceled!");
    }
  }
  public static void ViewCourseDetails()
  {
    var panel01 = new Panel($"[gold3] View Course Details[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine("[silver]▶ View Course Details By Code[/]\n");

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    Console.WriteLine();
    string codeInput = Operation.GetCourseCodeInSystem("● Enter The Course ", "Code");
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
  public static void ViewCourses()
  {
    var panel01 = new Panel($"[gold3]View Courses[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);

    AnsiConsole.MarkupLine("[silver]▶ View All Courses In System[/]\n");

    // Create a table
    var table = new Table();
    // Add columns with different alignments and styles
    table.AddColumn(new TableColumn("[gold3]#[/]").Centered());
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

    AnsiConsole.Markup($"\n[grey58]...Courses In System...[/]\n");

    Operation.FinishOption();
  }

  public static void ScheduleExams()
  {
    var panel01 = new Panel($"[gold3]Schedule Exams[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);
    Console.WriteLine();

    AnsiConsole.MarkupLine("[silver]▶ Show Exams And Schedule Them In System[/]\n");

    // Create a table
    var table = new Table();
    // Add columns with different alignments and styles
    table.AddColumn(new TableColumn("[gold3]#[/]").Centered());
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
      string examInput = Operation.GetExamCodeInSystem("● Enter The Exam ", "Code");

      int examIndex = Operation.GetExamIndex(examInput);
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
      if (Operation.ConfirmAction("● Are You Sure You Want To Schedule This Exam? "))
      {
        Operation.LoadingOperation("▶ Scheduling Exam...", 40);

        //save exam to exams list
        MainMenu.exams[examIndex].ExamDate = examDate;
        MainMenu._examRepository.SaveExamData(MainMenu.exams);

        Console.WriteLine();
        AnsiConsole.Markup($"▶ [green]Successfully Removed Course[/]\n");

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
  public static void SystemReport()
  {
    var panel01 = new Panel($"[gold3]System Report[/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel01);

    AnsiConsole.MarkupLine("[silver]▶ Get System Report[/]\n");

    Operation.LoadingOperation("▶ Reporting The System", 50);

    // Create a table
    var table = new Table();
    // Add columns with different alignments and styles
    table.AddColumn(new TableColumn("[gold3]#[/]").Centered());
    table.AddColumn(new TableColumn("[gold3]Department Code[/]").Centered());
    table.AddColumn(new TableColumn("[gold3]Department Name[/]").Centered());

    // Set table border and title
    table.Border(TableBorder.Rounded);
    table.Title($"[gold3]★ [/]Departments");

    string[] departsCodes = { "CS", "IT", "IS", "MM" };
    string[] departsNames = { "Computer Science", "Information Technology", "Information System", "Multi Media" };

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
    AnsiConsole.MarkupLine($"● [bold]College {Manager.CollegeName} Of Beni-Suef University[/]");
    AnsiConsole.MarkupLine($"  [bold]College seeks to develop and enrich practical and educational level[/]");
    AnsiConsole.MarkupLine($"  [bold]of Computer Science,Information Technology, and Mulit-Media fields.[/]");
    Console.WriteLine();

    var notifyTable = new Table();
    notifyTable.AddColumn("[gold3]#[/]");
    notifyTable.AddColumn("[gold3]Latest Notify[/]");
    // Set table border and title
    notifyTable.Title($"[gold3]Latest Notifies ★ [/]");
    notifyTable.AddRow($"[gold3]0[/]",
                        $"{Manager.Notifies.Last()}");
    AnsiConsole.Write(notifyTable);

    Console.WriteLine();
    AnsiConsole.Markup($"[bold]...System Report...[/]\n");

    Operation.FinishOption();
  }

  private static Person GetUser(int userChoice)
  {
    Console.WriteLine();

    string code = "";

    string name = Operation.GetValidatedStringInput("● ╭─ Enter User ", "Full Name");
    if (name == "Invalid input")
      return null;

    string password = "00000";
    Thread.Sleep(100);
    AnsiConsole.Markup($"[bold]● ├─ Default password: [/][gold3_1]{password}[/]\n");
    string department = "NULL";
    Thread.Sleep(100);
    AnsiConsole.Markup($"[bold]● ├─ Department: [/][gold3_1]{department}[/]\n");
    bool activate = false;
    Thread.Sleep(100);
    AnsiConsole.Markup($"[bold]● ├─ Email Activated: [/][gold3_1]{activate}[/]\n");

    if (userChoice == 1)
    {
      code = Generator.GenerateDoctorCode(MainMenu.doctors);
      Thread.Sleep(100);
      AnsiConsole.Markup($"[bold]● ├─ Generated Doctor Code : [/][gold3_1]{code}[/]\n");

      DateTime dateOfHire = Operation.GetValidatedDateInput("● ├─ Enter ", "Date Of Hire");
      if (dateOfHire == default)
        return null;

      var coursesTaughtCodes = new List<string>();
      var doctorExamsCodes = new List<string>();

      string email = Generator.GenerateEmail(name, code);
      AnsiConsole.Markup($"[bold]● ╰─ Default Email: [/][gold3_1]{email}[/]\n");

      return new Doctor(code, name, password, email, department, activate, dateOfHire, coursesTaughtCodes, doctorExamsCodes);
    }
    else if (userChoice == 2)
    {
      code = Generator.GenerateStudentCode(MainMenu.students);
      Thread.Sleep(100);
      AnsiConsole.Markup($"[bold]● ├─ Generated Student Code : [/][gold3]{code}[/]\n");

      double marks = 0.0;
      Thread.Sleep(100);
      AnsiConsole.Markup($"[bold]● ├─ Default Marks : [/][gold3]{marks}[/]\n");

      string grade ="A";
      Thread.Sleep(100);
      AnsiConsole.Markup($"[bold]● ├─ Default Grade : [/][gold3]{grade}[/]\n");

      double gpa = 0.0;
      Thread.Sleep(100);
      AnsiConsole.Markup($"[bold]● ├─ Default GPA : [/][gold3]{gpa}[/]\n");

      string[] arr02 = { "M", "F", "MALE", "FEMALE" };
      string gender = Operation.GetValidInputArray("● ├─ Enter a Student ", "Gender", arr02);
      if (gender == "Invalid input")
        return null;

      string[] arr03 = { "1", "2", "3", "4", "FIRST", "SECOND", "THIRD", "FOURTH" };
      string yearOfStudy = Operation.GetValidInputArray("● ├─ Enter a Student ", "Year of Study", arr03);
      if (yearOfStudy == "Invalid input")
        return null;

      int noOfCreditHours = 0;

      List<string> enrolledCourses = new();

      string email = Generator.GenerateEmail(name, code);
      AnsiConsole.Markup($"[bold]● ╰─ Default Email: [/][gold3_1]{email}[/]\n");

      int isCoursesHoursAccepted = 0;
      AnsiConsole.Markup($"[bold]● ╰─ Number Of Courses Hours: [/][gold3_1]{isCoursesHoursAccepted}[/]\n");

      return new Student(code, name, password, email, department, activate, gpa, marks, grade, gender, yearOfStudy, noOfCreditHours, isCoursesHoursAccepted, enrolledCourses);
    }

    return null;
  }
  private static Course GetCourse()
  {
    Console.WriteLine();

    string str = "Invalid input";

    Thread.Sleep(40);
    string name = Operation.GetValidatedStringInput("● ╭─ Enter Course ", "Name");
    if (name == str)
      return null;

    Thread.Sleep(40);
    string description = Operation.GetValidatedStringInput("● ├─ Enter Course ", "Description");
    if (description == str)
      return null;

    Thread.Sleep(40);
    string code = Generator.GenerateCourseCode(MainMenu.courses);
    AnsiConsole.Markup($"● ├─ Generated Course Code: [gold3]{code}[/]\n");

    Thread.Sleep(40);
    string doctorCode = "D0000";
    AnsiConsole.Markup($"● ├─ Doctor Code: [gold3]{doctorCode}[/]\n");
    Thread.Sleep(40);
    string examCode = "E0000";
    AnsiConsole.Markup($"● ├─ Exam Code: [gold3]{examCode}[/]\n");

    Thread.Sleep(40);
    string[] arr01 = { "CS", "IS", "IT", "MM", "COMPUTER SCIENCE", "INFORMATION SYSTEM", "INFORMATION TECHNOLOGY", "MULTI MEDIA" };
    string department = Operation.GetValidInputArray("● ├─ Enter Course ", "Department", arr01);
    if (department == str)
      return null;
    int noStudents = 0;

    Thread.Sleep(40);
    int noOfHours = Operation.GetValidIntInput("● ╰─ Enter Course ", "Number of Hours (1 -> 4)", 1, 4);
    if (noOfHours == -1)
      return null;

    return new Course(code, name, description, doctorCode, examCode, department, noStudents, noOfHours);
  }

}

