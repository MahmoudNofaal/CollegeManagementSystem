using CollegeSystem.Authentication;
using CollegeSystem.Core;
using CollegeSystem.Data;
using CollegeSystem.UI;
using Spectre.Console;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace CollegeSystem;

internal class Program
{
  [Obsolete]
  static void Main(string[] args)
  {
    Console.OutputEncoding = Encoding.UTF8;

    #region Rung Manager System

    //ManagerPage managerPage = new ManagerPage(new Manager("50500001", "M001", "Nofaal", "12345", "nofaal@manager.com", "CS", true));
    #endregion

    #region Run Doctor System

    //Session sessionEntity = new();
    //sessionEntity.CurrentUser = MainMenu.doctors[7];
    //DoctorPage doctorPage = new((Doctor)sessionEntity.CurrentUser);

    #endregion

    #region Run Student System

    //Session sessionEntity = new();
    //sessionEntity.CurrentUser = MainMenu.students[24];
    //StudentPage studentPage = new((Student)sessionEntity.CurrentUser);

    #endregion

    #region Run The Program

    //// this is will be the adminstrator
    //List<Manager> managers = new();
    //managers.Add(new Manager("50500001", "M001", "Nofaal", "12345", "nofaal@manager.com", "CS", true));


    //Manager Info:
    //  CODE: M001
    //  Password : 12345


    UserRepository _userRepository = new UserRepository();

    List<Doctor> doctors = _userRepository.LoadDoctors();
    List<Student> students = _userRepository.LoadStudents();
    _userRepository.SaveManagerData(MainMenu.managers);
    _userRepository.LoadManagers();

    AuthenticationService authenticationService = new(_userRepository);

    MainMenu mainMenu = new(authenticationService);

    #endregion

  }
}
