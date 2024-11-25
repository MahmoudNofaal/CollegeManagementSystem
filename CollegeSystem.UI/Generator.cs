using CollegeSystem.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.UI;

public static class Generator
{
  public static string GenerateEmail(string name, string code)
  {
    string[] nameParts = name.Split(' ');
    string firstName = nameParts[0].ToLower();
    string lastNamePart = nameParts[1].Substring(0, 3).ToLower();
    string numericCode = code.Substring(1);
    return $"{firstName}{char.ToUpper(lastNamePart[0]) + lastNamePart.Substring(1)}{numericCode}@gmail.com";
  }

  public static string GenerateStudentCode(List<Student> students)
  {
    if (students == null || students.Count == 0)
    {
      return "S0001";
    }

    var highestCode = students.Max(s => s.Code);
    int numericPart = int.Parse(highestCode.Substring(1));
    return $"S{(numericPart + 1).ToString("D4")}";
  }

  public static string GenerateDoctorCode(List<Doctor> doctors)
  {
    if (doctors == null || doctors.Count == 0)
    {
      return "D0001";
    }

    var highestCode = doctors.Max(d => d.Code);
    int numericPart = int.Parse(highestCode.Substring(1));
    return $"D{(numericPart + 1).ToString("D4")}";
  }

  public static string GenerateCourseCode(List<Course> courses)
  {
    if (courses == null || courses.Count == 0)
    {
      return "C0001";
    }

    var highestCode = courses.Max(d => d.CourseCode);
    int numericPart = int.Parse(highestCode.Substring(1));
    return $"C{(numericPart + 1).ToString("D4")}";
  }

  public static string GenerateExamCode(List<Exam> exams)
  {
    if (exams == null || exams.Count == 0)
    {
      return "E0001";
    }

    var highestCode = exams.Max(d => d.ExamCode);
    int numericPart = int.Parse(highestCode.Substring(1));
    return $"E{(numericPart + 1).ToString("D4")}";
  }

}
