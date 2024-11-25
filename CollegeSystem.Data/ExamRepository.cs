using CollegeSystem.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.Data;

public class ExamRepository
{

  private const string ExamFilePath = "examsData.txt";

  public void SaveExamData(List<Exam> exams)
  {
    try
    {
      // Save Courses
      using (StreamWriter writer = new StreamWriter(ExamFilePath))
      {
        foreach (var exam in exams.OfType<Exam>())
        {
          writer.WriteLine(
                            $"{exam.ExamCode}|" +
                            $"{exam.ExamName}|" +
                            $"{exam.CourseCode}|" +
                            $"{exam.DoctorCode}|" +
                            $"{exam.ExamDate}|" +
                            $"{exam.NoQuestions}"
                          );
        }
      }

    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error saving data: {ex.Message}");
    }
  }


  public List<Exam> LoadExams()
  {
    List<Exam> exams = new List<Exam>();

    if (File.Exists(ExamFilePath))
    {
      using (StreamReader reader = new StreamReader(ExamFilePath))
      {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
          var fields = line.Split('|');
          if (fields.Length >= 6)  // Adjusted for correct length and to account for list parsing
          {
            var exam = new Exam(
                                     fields[0],
                                     fields[1],
                                     fields[2],
                                     fields[3],
                                     DateTime.Parse(fields[4]),
                                     int.Parse(fields[5])
                                    );
            exams.Add(exam);
          }
        }
      }
    }
    return exams;
  }


}
