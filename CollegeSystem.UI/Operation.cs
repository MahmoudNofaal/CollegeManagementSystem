using CollegeSystem.Core;
using CollegeSystem.Data;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.UI;

#region MyRegion
public static class Operation
{
  public static string GeneratePassword()
  {
    Random _random = new Random();

    const string chars = "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM1234567890";
    return new string(Enumerable.Repeat(chars, 5).Select(s => s[_random.Next(s.Length)]).ToArray());
  }
  public static void LoadingOperation(string str, int turns)
  {
    AnsiConsole.Status().Start($"{str}...", ctx =>
    {
      // Simulate some work
      for (int i = 0; i < turns; i++)
      {
        Thread.Sleep(2); // Simulate work
      }
    });
  }
  public static void Loading(int time)
  {
    var animation = new[] { ".", "..", "..." };
    for (int i = 0; i < 10; i++)
    {
      Console.Write("\rLoading" + animation[i % animation.Length]);
      Thread.Sleep(time);
    }
  }
  ////////////////////////////
  public static string GetValidInputArray(string prompt, string something, string[] arr)
  {
    int i = 0;
    while (i < 3)
    {
      AnsiConsole.Markup($"{prompt}[gold3_1]{something}[/]: ");

      string input = Console.ReadLine().ToUpper();

      // Check if the entered department code is valid
      if (arr.Contains(input))
      {
        return input;
      }
      else
      {
        AnsiConsole.Markup($"[red]Invalid department code.[/] Please enter right data\n");
      }
      i++;
    }
    return "Invalid input";
  }
  public static DateTime GetValidatedDateInput(string prompt, string something)
  {
    DateTime date;
    int attempts = 0;

    while (attempts < 3)
    {
      AnsiConsole.Markup($"{prompt}[gold3_1]{something}[/]: ");

      if (DateTime.TryParse(Console.ReadLine(), out date))
      {
        if (date != DateTime.Now)
          return date;
      }
      attempts++;
      AnsiConsole.Markup($"[red]Invalid input  [/][maroon]Please try again.[/]\n");
    }
    return default;
  }
  public static int GetValidIntInput(string prompt, string something,int minValue, int maxValue)
  {
    int result;
    int attempts = 0;

    while (attempts < 3)
    {
      AnsiConsole.Markup($"{prompt}[gold3_1]{something}[/]: ");

      if (int.TryParse(Console.ReadLine(), out result) && result >= minValue && result <= maxValue)
      {
        return result;
      }
      attempts++;

      AnsiConsole.Markup($"[red]Invalid input  [/][maroon]Please try again.[/]\n");
    }
    return -1;
  }
  public static string GetValidatedStringInput(string prompt, string something)
  {
    string input;
    int attempts = 0;

    while (attempts < 3)
    {
      AnsiConsole.Markup($"{prompt}[gold3_1]{something}[/]: ");

      input = Console.ReadLine();
      if (!string.IsNullOrEmpty(input))
      {
        return input;
      }
      attempts++;

      AnsiConsole.Markup($"[red]Invalid input  [/][maroon]Please try again.[/]\n");
    }
    return "Invalid input";
  }
  public static string GetValidEmail(string prompt, string something)
  {
    string? input;
    int attempts = 0;

    while (attempts < 3)
    {
      AnsiConsole.Markup($"{prompt}[gold3_1]{something}[/]: ");

      input = Console.ReadLine();
      if (!string.IsNullOrEmpty(input) && IsValidEmail(input))
      {
        return input;
      }
      attempts++;

      AnsiConsole.Markup($"[red]Invalid input, [/][maroon]Please try again.[/]\n");
    }
    return "Invalid input";
  }
  public static bool IsValidEmail(string email)
  {
    try
    {
      var addr = new MailAddress(email);
      return true;
    }
    catch
    {
      return false;
    }
  }
  //////////////////////////
  public static void StartOption(string msg)
  {
    Console.Clear();
  }
  public static void FinishOption()
  {
    Console.WriteLine();
    AnsiConsole.Markup($"▶ [lightcyan1]You have finished this option. [/]Press <Enter> to return to the menu.");

    Console.ReadLine();
    Console.Clear();
  }
  public static void OutputMessage(string str)
  {
    if (string.IsNullOrEmpty(str))
    {
      AnsiConsole.Markup($"\n[steelblue]... Press any button to return to the menu ...[/]\n");
    }
    else
    {
      AnsiConsole.Markup($"\n[red]... {str}[/] Press <[yellow]Enter[/]> to try again ...\n");
    }
    Console.ReadLine();
    Console.Clear();
  }
  public static bool ConfirmAction(string message)
  {
    AnsiConsole.Markup($"[yellow3_1]{message}[/]");
    string input = Console.ReadLine().ToLower();

    return input == "y" || input == "yes";
  }

  ////////////////////////////
  public static string GetExamCodeInSystem(string prompt, string something)
  {
    int i = 0;
    while (i < 3)
    {
      AnsiConsole.Markup($"{prompt}[gold3_1]{something}[/]: ");

      string code = Console.ReadLine();

      // Check if the entered code is in the system
      if (MainMenu.exams.Any(u => u.ExamCode == code))
      {
        return code;
      }
      else
      {
        Console.WriteLine("This code does not exist. Please enter a code in the system.");

      }
      i++;
    }
    return "Invalid input";

  }
  public static string GetUserCodeInSystem(string prompt, string something, int c)
  {
    int i = 0;
    while (i < 3)
    {
      AnsiConsole.Markup($"{prompt}[gold3_1]{something}[/]: ");

      string? code = Console.ReadLine();

      // Check if the entered code is in the system
      if (c==1 && MainMenu.doctors.Any(u => u.Code == code))
      {
        return code;
      }
      else if (c==2 && MainMenu.students.Any(u => u.Code == code))
      {
        return code;
      }
      else
      {
        AnsiConsole.Markup($"[red]This code does not exist.[/] Please enter a code in the system.\n");
      }
      i++;
    }
    return "Invalid input";
  }
  public static string GetCourseCodeInSystem(string prompt, string something)
  {
    int i = 0;
    while (i < 3)
    {
      AnsiConsole.Markup($"{prompt}[gold3_1]{something}[/]: ");

      string code = Console.ReadLine();

      // Check if the entered code is in the system
      if (!string.IsNullOrEmpty(code) && MainMenu.courses.Any(u => u.CourseCode == code))
      {
        return code;
      }
      else
      {
        AnsiConsole.Markup($"[red]This code does not exist.[/] Please enter a code in the system.\n");
      }
      i++;
    }
    return "Invalid input";

  }
  ///////////////////////////////

  public static int GetUserIndex(string code, int c) // 3-manager, 1-doctor, 2-student
  {
    int index = 0;
    if (c == 1)
    {
      for (int i = 0; i < MainMenu.doctors.Count; i++)
      {
        if (MainMenu.doctors[i].Code == code)
        {
          index = i;
          return index;
        }
      }
    }
    else if (c == 2)
    {
      for (int i = 0; i < MainMenu.students.Count; i++)
      {
        if (MainMenu.students[i].Code == code)
        {
          index = i;
          return index;
        }
      }
    }
    else if (c == 3)
    {
      for (int i = 0; i < MainMenu.managers.Count; i++)
      {
        if (MainMenu.managers[i].Code == code)
        {
          index = i;
          return index;
        }
      }
    }

    return -1;
  }
  public static int GetCourseIndex(string code)
  {
    int index = 0;
    for (int i = 0; i < MainMenu.courses.Count; i++)
    {
      if (MainMenu.courses[i].CourseCode == code)
      {
        index = i;
        return index;
      }
    }
    return -1;
  }
  public static int GetExamIndex(string code)
  {
    int index = 0;
    for (int i = 0; i < MainMenu.exams.Count; i++)
    {
      if (MainMenu.exams[i].ExamCode == code)
      {
        index = i;
        return index;
      }
    }
    return -1;
  }
}
#endregion

