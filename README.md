# College Management System 🎓

A **console-based application** built with **C# and .NET** for managing a college system. This project provides functionalities for administrators, doctors, and students to interact with various aspects of the college's operations in a user-friendly and visually appealing console interface.

---

## ✨ Features

### 1. **Login and Sign-Up System** 🔐
- **Sign-Up**:
  - **Doctors** and **Students** can sign up using their National ID to retrieve their code and set a password.
- **Login**:
  - Credentials are validated to identify the user's role: Manager, Doctor, or Student.
  - Redirects users to their respective menus based on their role.

### 2. **Role-Specific Functionalities**
- **Manager**:
  - Full control of the system, including managing users and overseeing the entire dataset.
- **Doctors**:
  - View and manage assigned courses.
  - Interact with student grades and assignments.
- **Students**:
  - Enroll in courses, view grades, and manage personal information.

### 3. **Dataset Management** 🗂
- Pre-loaded datasets for Doctors, Students, and Manager to simulate real-world usage.
- Data is stored in text files for persistence, using a `DataManager`-like approach to read/write files.

### 4. **Enhanced Console UI** 🎨
- Modern and engaging UI design using **Spectre.Console** for visually appealing elements:
  - Tables for structured data.
  - Colored text for better readability.
  - Animations and visual effects for a dynamic user experience.

---

## 🛠 Project Structure

### Solution Components:
1. **Core**: Contains shared models and business logic.
2. **Data**: Manages data access and storage, including dataset handling.
3. **UI**: Implements a console-based interface for user interactions.
4. **Authentication**: Handles login and sign-up functionalities.

### Roles and Details:

#### 📌 **Doctors**
- **Properties**:
  - Code, National ID, Name, Department, Email, Courses, Password.
- **Functionalities**:
  - Manage courses and interact with students.

#### 📌 **Students**
- **Properties**:
  - Code, National ID, Name, Department, GPA, Courses, Password.
- **Functionalities**:
  - Enroll in courses and view academic progress.

---

## 🚀 How to Run

1. Clone this repository:
   ```bash
   git clone https://github.com/MahmoudNofaal/CollegeManagementSystem.git
