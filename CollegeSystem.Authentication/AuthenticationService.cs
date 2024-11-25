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

}