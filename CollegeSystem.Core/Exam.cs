﻿using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CollegeSystem.Core;

public class Exam
{
  public string ExamCode { get; set; }
  public string ExamName { get; set; }
  public string CourseCode { get; set; }
  public string DoctorCode { get; set; }
  public DateTime ExamDate { get; set; }
  public int NoQuestions { get; set; }

  public Exam(
               string examCode,
               string examName,
               string courseCode,
               string doctorCode,
               DateTime examDate,
               int noQuestions
             )
  {
    ExamCode = examCode;
    ExamName = examName;
    CourseCode = courseCode;
    DoctorCode = doctorCode;
    ExamDate = examDate;
    NoQuestions = noQuestions;
  }


  public void PrintExam(string color)
  {
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Exam Code:[/] [{color}]{ExamCode}[/]\n");
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Exam Name:[/] [{color}]{ExamName}[/]\n");
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Course Code:[/] [{color}]{CourseCode}[/]\n");
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Doctor Code:[/] [{color}]{DoctorCode}[/]\n");
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]├─ Exam Date:[/] [{color}]{ExamDate}[/]\n");
    Thread.Sleep(60);
    AnsiConsole.Markup($"[bold]└─ Number Of Questions:[/] [{color}]{NoQuestions}[/]\n");

  }
}
