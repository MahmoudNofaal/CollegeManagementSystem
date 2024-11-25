using CollegeSystem.Authentication;
using CollegeSystem.Core;
using CollegeSystem.Data;
using CollegeSystem.UI;
using System.Text;

namespace CollegeSystem;

internal class Program
{

  static void Main(string[] args)
  {
    Console.OutputEncoding = Encoding.UTF8;

    #region Rung Manager System

    //ManagerPage managerPage = new ManagerPage(new Manager("M001", "Nofaal", "12345", "nofaal@manager.com", "CS", true, "FCAI", new List<string>()));

    #endregion

    #region Run Doctor System

    //Session sessionEntity = new();
    //sessionEntity.CurrentUser = MainMenu.doctors[0];
    //DoctorPage doctorPage = new((Doctor)sessionEntity.CurrentUser);

    #endregion

    #region Run Student System

    //Session sessionEntity = new();
    //sessionEntity.CurrentUser = MainMenu.students[0];
    //StudentPage studentPage = new((Student)sessionEntity.CurrentUser);

    #endregion

    #region Run The Program

    // this is will be the adminstrator
    List<Manager> managers = new();
    managers.Add(new Manager("M0001", "Mahmoud Nofaal", "12345", "mahmoudNof0001@gmail.com", "NULL", true, "FACI", 18, new List<string>()));

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
