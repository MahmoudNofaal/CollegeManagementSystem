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
          writer.WriteLine($"{user.Code}|" +
                           $"{user.FullName}|" +
                           $"{user.Password}|" +
                           $"{user.Email}|" +
                           $"{user.Department}|" +
                           $"{user.IsEmailActivate}|" +
                           $"{user.DateOfHire}|" +
                           $"{string.Join(",", user.RegisteredCoursesCodes)}|" +
                           $"{string.Join(",", user.DoctorExamsCodes)}"
                          );
        }
      }

    }//end Of Try
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
          writer.WriteLine( $"{user.Code}|" +
                            $"{user.FullName}|" +
                            $"{user.Password}|" +
                            $"{user.Email}|" +
                            $"{user.Department}|" +
                            $"{user.IsEmailActivate}|" +
                            $"{user.GPA}|" +
                            $"{user.Marks}|" +
                            $"{user.Grade}|" +
                            $"{user.Gender}|" +
                            $"{user.YearOfStudy}|" +
                            $"{user.NoOfCreditHours}|" +
                            $"{user.NumberOfCoursesHours}|"+
                            $"{string.Join(",", user.EnrolledCoursesCodes)}"
                          );
        }
      }

    }//endOf Try
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
          writer.WriteLine( $"{user.Code}|" +
                            $"{user.FullName}|" +
                            $"{user.Password}|" +
                            $"{user.Email}|" +
                            $"{user.Department}|"+
                            $"{user.IsEmailActivate}|"+
                            $"{Manager.CollegeName}|"+
                            $"{Manager.NumberOfCoursesHoursAccepted}|"+
                            $"{string.Join("~", Manager.Notifies)}"
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
          if (fields.Length >= 9)  // Adjusted for correct length and to account for list parsing
          {
            var coursesTaughtCodes = fields[7].Split(',').ToList();
            var doctorExamsCodes = fields[8].Split(',').ToList();

            var doctor = new Doctor(
                                     fields[0],
                                     fields[1],
                                     fields[2],
                                     fields[3],
                                     fields[4],
                                     bool.Parse(fields[5]),
                                     DateTime.Parse(fields[6]),
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
          if (fields.Length >= 14)  // Adjusted for correct length and to account for list parsing
          {
            var enrolledCourses = fields[13].Split(',').ToList();

            var student = new Student(
                                       fields[0],
                                       fields[1],
                                       fields[2],
                                       fields[3],
                                       fields[4],
                                       bool.Parse(fields[5]),
                                       double.Parse(fields[6]),
                                       double.Parse(fields[7]),
                                       fields[8],
                                       fields[9],
                                       fields[10],
                                       int.Parse(fields[11]),
                                       int.Parse(fields[12]),
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
          if (fields.Length >= 9)
          {
            var notifies = fields[8].Split('~').ToList();
            var manager = new Manager(
                                       fields[0],
                                       fields[1],
                                       fields[2],
                                       fields[3],
                                       fields[4],
                                       bool.Parse(fields[5]),
                                       fields[6],
                                       int.Parse(fields[7]),
                                       notifies
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



