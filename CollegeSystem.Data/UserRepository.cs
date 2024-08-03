using CollegeSystem.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace CollegeSystem.Data;

#region MyRegion
public class UserRepository
{
  private const string DoctorFilePath = "doctorsData.txt";
  private const string StudentFilePath = "studentsData.txt";
  private const string ManagerFilePath = "managerData.txt";

  public void SaveDoctorData(List<Doctor> users)
  {
    try
    {

      // Save Doctors
      using (StreamWriter writer = new StreamWriter(DoctorFilePath))
      {

        foreach (var user in users.OfType<Doctor>())
        {
          writer.WriteLine(
                            $"{user.NationalId}|" +
                            $"{user.Code}|" +
                            $"{user.Name}|" +
                            $"{user.Password}|" +
                            $"{user.Email}|" +
                            $"{user.Department}|" +
                            $"{user.Activate}|" +
                            $"{user.DateOfHire}|" +
                            $"{string.Join(",", user.CoursesTaughtCodes)}|" +
                            $"{string.Join(",", user.DoctorExamsCodes)}"
                          );
        }
      }

    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error saving data: {ex.Message}");
    }
  }
  public void SaveStudentData(List<Student> users)
  {
    try
    {

      // Save Students
      using (StreamWriter writer = new StreamWriter(StudentFilePath))
      {
        foreach (var user in users.OfType<Student>())
        {
          writer.WriteLine(
                            $"{user.NationalId}|" +
                            $"{user.Code}|" +
                            $"{user.Name}|" +
                            $"{user.Password}|" +
                            $"{user.Email}|" +
                            $"{user.Department}|" +
                            $"{user.Activate}|" +
                            $"{user.GPA}|" +
                            $"{user.Grade}|" +
                            $"{user.Gender}|" +
                            $"{user.YearOfStudy}|" +
                            $"{string.Join(",", user.EnrolledCourses)}"
                          );
        }
      }

    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error saving data: {ex.Message}");
    }
  }
  public void SaveManagerData(List<Manager> users)
  {
    try
    {
      // Save Managers
      using (StreamWriter writer = new StreamWriter(ManagerFilePath))
      {
        foreach (var user in users.OfType<Manager>())
        {
          writer.WriteLine(
                            $"{user.NationalId}|" +
                            $"{user.Code}|" +
                            $"{user.Name}|" +
                            $"{user.Password}|" +
                            $"{user.Email}|" +
                            $"{user.Department}|"+
                            $"{user.Activate}"
                          );
        }
      }

    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error saving data: {ex.Message}");
    }
  }

  public List<Doctor> LoadDoctors()
  {
    List<Doctor> users = new List<Doctor>();

    if (File.Exists(DoctorFilePath))
    {
      using (StreamReader reader = new StreamReader(DoctorFilePath))
      {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
          var fields = line.Split('|');
          if (fields.Length >= 10)  // Adjusted for correct length and to account for list parsing
          {
            var coursesTaughtCodes = fields[8].Split(',').ToList();
            var doctorExamsCodes = fields[9].Split(',').ToList();

            var doctor = new Doctor(
                                     fields[0],
                                     fields[1],
                                     fields[2],
                                     fields[3],
                                     fields[4],
                                     fields[5],
                                     bool.Parse(fields[6]),
                                     DateTime.Parse(fields[7]),
                                     coursesTaughtCodes,
                                     doctorExamsCodes
                                    );
            users.Add(doctor);
          }
        }
      }
    }
    return users;
  }

  public List<Student> LoadStudents()
  {
    List<Student> users = new List<Student>();

    if (File.Exists(StudentFilePath))
    {
      using (StreamReader reader = new StreamReader(StudentFilePath))
      {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
          var fields = line.Split('|');
          if (fields.Length >= 12)  // Adjusted for correct length and to account for list parsing
          {
            var enrolledCourses = fields[11].Split(',').ToList();


            var student = new Student(
                                       fields[0],
                                       fields[1],
                                       fields[2],
                                       fields[3],
                                       fields[4],
                                       fields[5],
                                       bool.Parse(fields[6]),
                                       double.Parse(fields[7]),
                                       fields[8],
                                       fields[9],
                                       fields[10],
                                       enrolledCourses
                                      );
            users.Add(student);
          }
        }
      }
    }
    return users;

  }

  public List<Manager> LoadManagers()
  {
    List<Manager> users = new List<Manager>();

    if (File.Exists(ManagerFilePath))
    {
      using (StreamReader reader = new StreamReader(ManagerFilePath))
      {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
          var fields = line.Split('|');
          if (fields.Length == 7)
          {
            var manager = new Manager(
                                       fields[0],
                                       fields[1],
                                       fields[2],
                                       fields[3],
                                       fields[4],
                                       fields[5],
                                       bool.Parse(fields[6])
                                     );
            users.Add(manager);
          }
        }
      }
    }

    return users;
  }
}
#endregion



