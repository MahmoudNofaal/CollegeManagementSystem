using CollegeSystem.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.UI;

public static class Generator
{
  public static string GenerateStudentCode(List<Student> students)
  {
    if (students == null || students.Count == 0)
    {
      return "S001";
    }

    var highestCode = students.Max(s => s.Code);
    int numericPart = int.Parse(highestCode.Substring(1));
    return $"S{(numericPart + 1).ToString("D3")}";
  }

  public static string GenerateDoctorCode(List<Doctor> doctors)
  {
    if (doctors == null || doctors.Count == 0)
    {
      return "D001";
    }

    var highestCode = doctors.Max(d => d.Code);
    int numericPart = int.Parse(highestCode.Substring(1));
    return $"D{(numericPart + 1).ToString("D3")}";
  }

  public static string GenerateStudentNationalId(List<Student> students)
  {
    if (students == null || students.Count == 0)
    {
      return "20200001";
    }

    var highestId = students.Max(s => s.NationalId);
    int numericPart = int.Parse(highestId.Substring(3));
    return $"202{(numericPart + 1).ToString("D5")}";
  }

  public static string GenerateDoctorNationalId(List<Doctor> doctors)
  {
    if (doctors == null || doctors.Count == 0)
    {
      return "30300001";
    }

    var highestId = doctors.Max(d => d.NationalId);
    int numericPart = int.Parse(highestId.Substring(3));
    return $"303{(numericPart + 1).ToString("D5")}";
  }

  public static string GenerateCourseCode(List<Course> courses)
  {
    if (courses == null || courses.Count == 0)
    {
      return "C001";
    }

    var highestCode = courses.Max(d => d.CourseCode);
    int numericPart = int.Parse(highestCode.Substring(1));
    return $"C{(numericPart + 1).ToString("D3")}";
  }

  public static string GenerateExamCode(List<Exam> exams)
  {
    if (exams == null || exams.Count == 0)
    {
      return "E001";
    }

    var highestCode = exams.Max(d => d.ExamCode);
    int numericPart = int.Parse(highestCode.Substring(1));
    return $"E{(numericPart + 1).ToString("D3")}";
  }

}
