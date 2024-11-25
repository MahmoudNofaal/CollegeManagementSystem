# College Management System 🎓

## Overview
The **College Management System** is a **C# console application** designed to streamline and manage various operations within a college environment. This system caters to three types of users: Managers, Doctors, and Students, each with unique roles and functionalities.

---

## Features 🚀

### Authentication System 🔐
- **Sign-Up**: 
  - Doctors and Students register using their National ID to retrieve their system code and set a password.
- **Login**:
  - Credentials are validated to determine the user's role (Manager, Doctor, or Student).
  - Redirects users to role-specific menus based on their credentials.

### Role-Based Operations 🎭
- **Manager**:
  - Has full control over the system's operations, including managing datasets and overseeing all users.
- **Doctor**:
  - Can manage courses, interact with student grades, and administer exams.
- **Student**:
  - Can view grades, enroll in courses, and access course materials.

### Data Management 📂
- Pre-recorded datasets are used to simulate real-world scenarios:
  - `Doctors.txt`: Stores information about all doctors.
  - `Students.txt`: Contains details about students and their academic progress.
  - `Manager.txt`: Holds data for the administrator.

### Interactive UI 🎨
- Designed with a **modern console interface** using the **Spectre.Console** library:
  - Clean and organized tables.
  - Colored text and animations for enhanced user experience.
  - Responsive menus tailored for each user role.

---

## How to Run 🛠

### Prerequisites
- **.NET SDK** (version 6.0 or higher)
- **Visual Studio** (with .NET Core support)

### Steps
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/MahmoudNofaal/CollegeManagementSystem.git
