  using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.Core;

public abstract class Person
{
  public string NationalId { get; set; }
  public string Code { get; set; }
  public string Name { get; set; }
  public string Password { get; set; }
  public string Email { get; set; }
  public string Department { get; set; }
  public bool IsEmailActivate { get; set; } = false;

  // Default constructor for deserialization or use cases where initialization is not needed
  public Person() { }

  // Constructor for initialization with parameters
  public Person(string nationalId, string code,  string name, string password, string email, string department, bool activate)
  {
    NationalId = nationalId;
    Code = code;
    Name = name;
    Password = password;
    Email = email;
    Department = department;
    IsEmailActivate = activate;
  }

  public abstract void PrintUser(string color);

  public override string ToString()
  {
     return $"National ID: {NationalId},\n" +
            $"Code: {Code},\n" +
            $"Name: {Name},\n" +
            $"Password: {Password}\n" +
            $"Email: {Email}\n" +
            $"Department: {Department}\n";
  }
}