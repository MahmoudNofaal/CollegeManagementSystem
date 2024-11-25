using CollegeSystem.Authentication;
using CollegeSystem.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Rendering;
using CollegeSystem.Data;

namespace CollegeSystem.UI;

public class MainMenu
{
  public static UserRepository _userRepository = new UserRepository();
  public static CourseRepository _courseRepository = new CourseRepository();
  public static ExamRepository _examRepository = new ExamRepository();
  public static List<Doctor> doctors = _userRepository.LoadDoctors();
  public static List<Student> students = _userRepository.LoadStudents();
  public static List<Manager> managers = _userRepository.LoadManagers();
  public static List<Course> courses = _courseRepository.LoadCourses();
  public static List<Exam> exams = _examRepository.LoadExams();

  private readonly AuthenticationService _authService;

  public MainMenu(AuthenticationService authService)
  {
    _authService = authService;

    Display();
  }


  public void Display()
  {
    var rule = new Rule("[lightcyan1]College Management System[/]");
    rule.Justification = Justify.Left;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[white] Welcome to the [lightcyan1]{Manager.CollegeName}[/] Management System  [/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel);

    Console.WriteLine("╔═════════════════════════════════════╗");
    AnsiConsole.MarkupLine("║ ★ [white][italic]Get To [lightcyan1]College Management System[/][/][/]  ║");
    Console.WriteLine("┣═════════════════════════════════════╝");

    Console.WriteLine("┃  ╭────────────╮");
    AnsiConsole.MarkupLine("┣━━┃ [lightcyan1][italic]1) SIGN IN[/][/] ┃");
    Console.WriteLine("┃  ╰────────────╯");

    Console.WriteLine("┃  ╭─────────────────────────╮");
    AnsiConsole.MarkupLine("┗━━┃ [lightcyan1][italic]2) Exit From The System[/][/] ┃");
    Console.WriteLine("   ╰─────────────────────────╯");

    Console.WriteLine();

    var option = Operation.GetValidatedStringInput("● Select an ", "option");
    if (option == "Invalid input")
    {
      Operation.OutputMessage("Invalid input");
      return;
    }

    if(option.ToLower() == "q")
    {
      return;
    }

    switch (option)
    {
      case "1":
        Operation.LoadingOperation("Signing In..",30);
        SignIn();
        break;
      case "2":
        return;
      default:
        AnsiConsole.MarkupLine("[red]Invalid Option. [/][yellow]Please try again.[/]");
        return;
    }
    Operation.FinishOption();
    Display();
  }

  private void SignIn()
  {
    Console.Clear();

    var rule = new Rule("[lightcyan1]Sign In Page[/]");
    rule.Justification = Justify.Left;
    rule.Border = BoxBorder.Double;
    AnsiConsole.Write(rule);

    var panel = new Panel($"[white] Welcome to the [lightcyan1]{Manager.CollegeName}[/] Management System  [/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel);

    var panel02 = new Panel($"[bold] [lightcyan1]SIGN IN[/]  [/]")
    .Border(BoxBorder.Rounded).BorderColor(Color.Silver);
    AnsiConsole.Write(panel02);

    AnsiConsole.Markup("● Tap [red]'q'[/] To Quit: ");
    var option = Console.ReadLine();
    if (option.ToLower() == "q")
    {
      return;
    }

    Console.WriteLine();
    var code = Operation.GetValidatedStringInput("● Please Enter Your ", "Code");
    if (code == "Invalid input")
    { return; }

    var password = Operation.GetValidatedStringInput("● Please Enter Your ", "Password");
    if (password == "Invalid input")
    { return; }

    Operation.LoadingOperation("Signing in the system...", 50);

    Person user = _authService.AuthenticateUser(code, password);
    if (user == null)
    {
      Console.WriteLine();
      AnsiConsole.MarkupLine("[red]Invalid code or password. Or Account not Activated[/]");
      return;
    }

    Console.WriteLine();
    AnsiConsole.Markup("[green]> Successfully Signed In. [/]");
    AnsiConsole.Markup("[white]Press <Enter> To Go To Your Page.[/]");
    Console.ReadLine();
    Operation.Loading(250);

    switch (user)
    {
      case Manager manager:
        new ManagerPage(manager);
        break;
      case Doctor doctor:
        new DoctorPage(doctor);
        break;
      case Student student:
        new StudentPage(student);
        break;
      default:
        Console.WriteLine();
        AnsiConsole.MarkupLine("[magenta1]Unknown user type.[/]");
        break;
    }
  }

}