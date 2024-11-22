using CollegeSystem.Core;
using CollegeSystem.Data;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.Authentication;

public class AuthenticationService
{
  private readonly UserRepository _userRepository = new();

  public AuthenticationService(UserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public Person AuthenticateUser(string code, string password)
  {
    // Check in Doctor repository
    var doctor = _userRepository.LoadDoctors().FirstOrDefault(d => d.Code == code && d.Password == password);
    if (doctor != null && doctor.IsEmailActivate)
    {
      return doctor;
    }

    // Check in Student repository
    var student = _userRepository.LoadStudents().FirstOrDefault(s => s.Code == code && s.Password == password);
    if (student != null && student.IsEmailActivate)
    {
      return student;
    }

    // Check in Manager repository
    var manager = _userRepository.LoadManagers().FirstOrDefault(s => s.Code == code && s.Password == password);
    if (manager != null && manager.IsEmailActivate)
    {
      return manager;
    }

    return null;
  }

  public bool RegisterManager(string nationalId, string password)
  {
    var managers = _userRepository.LoadManagers();
    var manager = managers.FirstOrDefault(d => d.NationalId == nationalId);

    if (manager != null && !manager.IsEmailActivate)
    {
      manager.IsEmailActivate = true;
      manager.Password = password;
      _userRepository.SaveManagerData(managers);
      return true;
    }
    return false;
  }
  public bool RegisterDoctor(string nationalId, string password)
  {
    var doctors = _userRepository.LoadDoctors();
    var doctor = doctors.FirstOrDefault(d => (d.NationalId == nationalId));

    if (doctor != null && !doctor.IsEmailActivate)
    {
      doctor.IsEmailActivate = true;
      AnsiConsole.MarkupLine($"● Doctor Code for Sign In: [lightcyan1]{doctor.Code}[/]");
      doctor.Password = password;
      _userRepository.SaveDoctorData(doctors);
      return true;
    }
    return false;
  }
  public bool RegisterStudent(string nationalId, string password)
  {
    var students = _userRepository.LoadStudents();
    var student = students.FirstOrDefault(s => s.NationalId == nationalId);

    if (student != null && !student.IsEmailActivate)
    {
      student.IsEmailActivate = true;
      AnsiConsole.MarkupLine($"● Student Code for Sign In: [lightcyan1]{student.Code}[/]");
      student.Password = password;
      _userRepository.SaveStudentData(students);
      return true;
    }
    return false;
  }
}