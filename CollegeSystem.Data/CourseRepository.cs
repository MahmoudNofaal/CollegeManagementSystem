using CollegeSystem.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.Data;

public class CourseRepository
{

  private const string CourseFilePath = "coursesData.txt";

  public void SaveCourseData(List<Course> courses)
  {
    try
    {
      // Save Courses
      using (StreamWriter writer = new StreamWriter(CourseFilePath))
      {
        foreach (var course in courses.OfType<Course>())
        {
          writer.WriteLine(
                            $"{course.CourseCode}|" +
                            $"{course.CourseName}|" +
                            $"{course.CourseDescription}|" +
                            $"{course.DoctorCode}|" +
                            $"{course.ExamCode}|" +
                            $"{course.Department}|" +
                            $"{course.NoStudents}|"+
                            $"{course.NoOfHours}"
                          );
        }
      }

    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error saving data: {ex.Message}");
    }
  }


  public List<Course> LoadCourses()
  {
    List<Course> courses = new List<Course>();

    if (File.Exists(CourseFilePath))
    {
      using (StreamReader reader = new StreamReader(CourseFilePath))
      {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
          var fields = line.Split('|');
          if (fields.Length >= 8)  // Adjusted for correct length and to account for list parsing
          {
            var course = new Course(
                                     fields[0],
                                     fields[1],
                                     fields[2],
                                     fields[3],
                                     fields[4],
                                     fields[5],
                                     int.Parse(fields[6]),
                                     int.Parse(fields[7])
                                    );
            courses.Add(course);
          }
        }
      }
    }
    return courses;
  }


}
