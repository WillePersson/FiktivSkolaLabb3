using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using FiktivSkolaLabb._2.Data;
using FiktivSkolaLabb._2.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace FiktivSkolaLabb._2
{
    public static class Logic
    {


        public static MenuHandler.MenuOption ViewPersonal()
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuHandler.MenuOption>()
                    .Title("[Purple]Personal Menu[/]")
                    .AddChoices(
                        MenuHandler.MenuOption.ViewAllPersonal,
                        MenuHandler.MenuOption.ViewAdmins,
                        MenuHandler.MenuOption.ViewTeachers,
                        MenuHandler.MenuOption.ViewPrincipal,
                        MenuHandler.MenuOption.ViewJanitors,
                        MenuHandler.MenuOption.Back
                    )
            );

            return choice;
        }

        public static List<Personal> ViewAllPersonal()
        {
            using (var dbContext = new FiktivSkolaDatabaseContext())
            {
                try
                {
                    var allPersonals = dbContext.Personals.ToList();
                    if (allPersonals.Count > 0)
                    {
                        Console.WriteLine("All Personals:");
                        foreach (var personal in allPersonals)
                        {
                            string id = $"ID: {personal.PersonalId}";
                            string name = $"Name: {personal.Fname} {personal.Lname}";
                            string profession = $"Profession: {personal.Profession}";
                            string salary = $"Salary: {personal.Salary}";

                            string[] lines = { id, name, profession, salary };

                            int maxLineLength = lines.Max(line => line.Length);
                            for (int i = 0; i < lines.Length; i++)
                            {
                                Console.WriteLine(lines[i].PadRight(maxLineLength));
                            }
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No personals found.");
                    }
                    return allPersonals;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return new List<Personal>();
                }
            }
        }
        public static List<Admin> ViewAllAdmins()
        {
            using (var dbContext = new FiktivSkolaDatabaseContext())
            {
                try
                {
                    var allAdmins = dbContext.Admins.ToList();
                    if (allAdmins.Count > 0)
                    {
                        Console.WriteLine("All Admins:");
                        foreach (var admin in allAdmins)
                        {
                            Console.WriteLine($"ID: {admin.AdminId}, Name: {admin.Fname} {admin.Lname}, Personal ID: {admin.FkPersonalId}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No admins found.");
                    }
                    return allAdmins;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return new List<Admin>();
                }
            }
        }
        public static List<Principal> ViewAllPrincipals()
        {
            using (var dbContext = new FiktivSkolaDatabaseContext())
            {
                try
                {
                    var allPrincipals = dbContext.Principals.ToList();
                    if (allPrincipals.Count > 0)
                    {
                        Console.WriteLine("All Principals:");
                        foreach (var principal in allPrincipals)
                        {
                            Console.WriteLine($"ID: {principal.PrincipalId}, Name: {principal.Fname} {principal.Lname}, Personal ID: {principal.FkPersonalId}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No principals found.");
                    }
                    return allPrincipals;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return new List<Principal>();
                }
            }
        }
        public static List<Teacher> ViewAllTeachers()
        {
            using (var dbContext = new FiktivSkolaDatabaseContext())
            {
                try
                {
                    var allTeachers = dbContext.Teachers.ToList();
                    if (allTeachers.Count > 0)
                    {
                        Console.WriteLine("All Teachers:");
                        foreach (var teacher in allTeachers)
                        {
                            Console.WriteLine($"ID: {teacher.TeacherId}, Name: {teacher.Fname} {teacher.Lname}, Personal ID: {teacher.FkPersonalId}, Class ID: {teacher.FkClassId}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No teachers found.");
                    }
                    return allTeachers;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return new List<Teacher>();
                }
            }
        }
        public static List<Janitor> ViewAllJanitors()
        {
            using (var dbContext = new FiktivSkolaDatabaseContext())
            {
                try
                {
                    var allJanitors = dbContext.Janitors.ToList();
                    if (allJanitors.Count > 0)
                    {
                        Console.WriteLine("All Janitors:");
                        foreach (var janitor in allJanitors)
                        {
                            Console.WriteLine($"ID: {janitor.JanitorId}, Name: {janitor.Fname} {janitor.Lname}, Personal ID: {janitor.FkPersonalId}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No janitors found.");
                    }
                    return allJanitors;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return new List<Janitor>();
                }
            }
        }

        public static void ViewAllStudentsSorted(string sortBy, string sortOrder)
        {
            using (var dbContext = new FiktivSkolaDatabaseContext())
            {
                try
                {
                    IQueryable<Student> studentsQuery = dbContext.Students.AsQueryable();

                    studentsQuery = sortBy.ToLower() switch
                    {
                        "firstname" => sortOrder.ToLower() == "ascending"
                            ? studentsQuery.OrderBy(s => s.Fname)
                            : studentsQuery.OrderByDescending(s => s.Fname),
                        "lastname" => sortOrder.ToLower() == "ascending"
                            ? studentsQuery.OrderBy(s => s.Lname)
                            : studentsQuery.OrderByDescending(s => s.Lname),
                        _ => studentsQuery
                    };

                    var sortedStudents = studentsQuery.ToList();

                    if (sortedStudents.Count > 0)
                    {
                        Console.WriteLine("Sorted Students:");
                        foreach (var student in sortedStudents)
                        {
                            Console.WriteLine($"ID: {student.StudentId,-4} Name: {student.Fname} {student.Lname,-15} " +
                                $"Address: {student.Street} {student.Housenumber,-4}, " +
                                $"Personal Identity: {student.PersonalIdentityNumber,-10}, Class ID: {student.FkClassId}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No students found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public static void ViewStudentsInClass()
        {
            var dbContext = new FiktivSkolaDatabaseContext();
            try
            {
                var classes = dbContext.Classes.ToList();

                if (classes.Count > 0)
                {
                    var classTable = new Table().Border(TableBorder.Rounded).Title("Classes");
                    classTable.AddColumn("ID");
                    classTable.AddColumn("Name");

                    foreach (var classObj in classes)
                    {
                        classTable.AddRow(classObj.ClassId.ToString(), classObj.ClassName);
                    }

                    AnsiConsole.Render(classTable);

                    var classIdPrompt = new TextPrompt<int>("Enter the ID of the class to view students:")
                        .Validate(input =>
                        {
                            return classes.Any(c => c.ClassId == input) ? ValidationResult.Success() : ValidationResult.Error("Invalid class ID");
                        });

                    int classId = AnsiConsole.Prompt(classIdPrompt);

                    var studentsInClass = dbContext.Students.Where(s => s.FkClassId == classId).ToList();

                    if (studentsInClass.Count > 0)
                    {
                        var studentTable = new Table().Border(TableBorder.Rounded).Title($"Students in Class ID: {classId}");
                        studentTable.AddColumn("ID");
                        studentTable.AddColumn("Name");

                        foreach (var student in studentsInClass)
                        {
                            studentTable.AddRow(student.StudentId.ToString(), $"{student.Fname} {student.Lname}");
                        }

                        AnsiConsole.Render(studentTable);
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("No students found in the selected class.");
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("No classes found.");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
            }
            finally
            {
                dbContext.Dispose();
            }
        }

        public static void ViewGradesLast30Days()
        {
            using (var dbContext = new FiktivSkolaDatabaseContext())
            {
                try
                {
                    DateTime startDate = new DateTime(DateTime.Now.Year, 11, 1);
                    DateTime endDate = startDate.AddMonths(1).AddDays(-1); 

                    var gradesLast30Days = dbContext.CourseGrades
                        .Include(cg => cg.FkStudent)
                        .Include(cg => cg.FkCourse)
                        .Where(cg => cg.CourseDate >= startDate && cg.CourseDate <= endDate)
                        .ToList();

                    if (gradesLast30Days.Count > 0)
                    {
                        Console.WriteLine("Grades Set in November:");
                        Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
                        foreach (var grade in gradesLast30Days)
                        {
                            string studentName = grade.FkStudent != null ? $"{grade.FkStudent.Fname} {grade.FkStudent.Lname}" : "Unknown";
                            string courseName = grade.FkCourse != null ? grade.FkCourse.CourseName : "Unknown";
                            Console.WriteLine($"Student: {grade.FkStudent.Fname} {grade.FkStudent.Lname,-20} Course: {grade.FkCourse.CourseName,-40} Grade: {grade.CourseGrade1}");
                            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No grades found for the last 30 days (November).");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public static void ViewCourses()
        {
            using (var dbContext = new FiktivSkolaDatabaseContext())
            {
                try
                {
                    var courses = dbContext.Courses.ToList();

                    if (courses.Any())
                    {
                        Console.WriteLine("Courses Summary:");
                        Console.WriteLine("─────────────────────────────────────────────────────────────");

                        foreach (var course in courses)
                        {
                            var courseGrades = dbContext.CourseGrades
                                .Where(cg => cg.FkCourseId == course.CourseId)
                                .Select(cg => ConvertGradeToNumeric(cg.CourseGrade1))
                                .ToList();

                            if (courseGrades.Any())
                            {
                                var averageGrade = courseGrades.Average();
                                var highestGrade = courseGrades.Max();
                                var lowestGrade = courseGrades.Min();

                                Console.WriteLine($"Course: {course.CourseName}");
                                Console.WriteLine($"Average Grade: {ConvertNumericToGrade(averageGrade)}");
                                Console.WriteLine($"Highest Grade: {ConvertNumericToGrade(highestGrade)}");
                                Console.WriteLine($"Lowest Grade: {ConvertNumericToGrade(lowestGrade)}");
                                Console.WriteLine("-----------------------------------------------------------------------------");
                            }
                            else
                            {
                                Console.WriteLine($"Course: {course.CourseName}");
                                Console.WriteLine("No grades found for this course.");
                                Console.WriteLine();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No courses found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        private static double ConvertGradeToNumeric(string grade)
        {
            return grade switch
            {
                "A+" => 15,
                "A" => 14,
                "A-" => 13,
                "B+" => 12,
                "B" => 11,
                "B-" => 10,
                "C+" => 9,
                "C" => 8,
                "C-" => 7,
                "D+" => 6,
                "D" => 5,
                "D-" => 4,
                "E+" => 3, 
                "E" => 2,  
                "E-" => 1, 
                "F" => 0,
                _ => 0 
            };
        }

        private static string ConvertNumericToGrade(double numericGrade)
        {
            return numericGrade switch
            {
                15 => "A+",
                14 => "A",
                13 => "A-",
                12 => "B+",
                11 => "B",
                10 => "B-",
                9 => "C+",
                8 => "C",
                7 => "C-",
                6 => "D+",
                5 => "D",
                4 => "D-",
                3 => "E+", 
                2 => "E",  
                1 => "E-", 
                0.0 => "F",
                _ => "Cant Calculate that/To little data"
            };
        }

        public static void AddNewStudent()
        {
            Random random = new Random();
            int randomClassId = random.Next(1, 6);
            try
            {
                var minLength = 3;

                var firstName = AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter Student First Name:")
                        .Validate(input => input.Length >= minLength, $"Please enter at least {minLength} characters.")
                        .Validate(input => !string.IsNullOrWhiteSpace(input), "First name cannot be empty.")
                );

                var lastName = AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter Student Last Name:")
                        .Validate(input => input.Length >= minLength, $"Please enter at least {minLength} characters.")
                        .Validate(input => !string.IsNullOrWhiteSpace(input), "Last name cannot be empty.")
                );

                var personalIdentityNumber = AnsiConsole.Prompt(
                   new TextPrompt<string>("Enter Personal Identity Number:")
                       .Validate(input => input.Length == 10 && input.All(char.IsDigit),
                        "Personal Identity Number must be 10 digits and contain only digits.")
);

                var zipCode = AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter Zip Code:")
                          .Validate(input => input.Length == 5 && input.All(char.IsDigit),
                            "Zip Code must be 5 digits and contain only digits.")
);

                var street = AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter Street:")
                        .Validate(input => input.Length >= minLength, $"Please enter at least {minLength} characters.")
                        .Validate(input => !string.IsNullOrWhiteSpace(input), "Street name cannot be empty.")
                );

                var houseNumber = AnsiConsole.Prompt(
                    new TextPrompt<int>("Enter House Number:")
                        .Validate(input => input > 0, "Please enter a valid house number.")
                );
                var parsedPersonalIdentityNumber = long.Parse(personalIdentityNumber);
                var validatedZipCode = int.Parse(zipCode);

                var grid = new Grid();

                grid.AddColumn();
                grid.AddColumn();

                grid.AddRow("[bold]First Name[/]", firstName);
                grid.AddRow("[bold]Last Name[/]", lastName);
                grid.AddRow("[bold]Personal Identity Number[/]", personalIdentityNumber);
                grid.AddRow("[bold]Zip Code[/]", zipCode.ToString());
                grid.AddRow("[bold]Street[/]", street);
                grid.AddRow("[bold]House Number[/]", houseNumber.ToString());

                AnsiConsole.Render(new Panel(grid).Expand());

                var confirmation = AnsiConsole.Confirm("Is the information correct?");


                if (confirmation)
                {
                    using (var dbContext = new FiktivSkolaDatabaseContext())
                    {
                        var newStudent = new Student
                        {
                            Fname = firstName,
                            Lname = lastName,
                            PersonalIdentityNumber = (int)parsedPersonalIdentityNumber,
                            ZipCode = int.Parse(zipCode),
                            Street = street,
                            Housenumber = houseNumber,
                            FkClassId = randomClassId
                        };

                        dbContext.Students.Add(newStudent);
                        dbContext.SaveChanges();
                        Console.WriteLine("Student information saved successfully!");
                    }
                }
                else
                {
                    AddNewStudent();
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
            }
        }

        public static void AddNewPersonal()
        {
            try
            {
                var minLength = 3;

                var firstName = AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter Employee First Name:")
                        .Validate(input => input.Length >= minLength, $"Please enter at least {minLength} characters.")
                        .Validate(input => !string.IsNullOrWhiteSpace(input), "First name cannot be empty.")
                );

                var lastName = AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter Employee Last Name:")
                        .Validate(input => input.Length >= minLength, $"Please enter at least {minLength} characters.")
                        .Validate(input => !string.IsNullOrWhiteSpace(input), "Last name cannot be empty.")
                );

                var professionOptions = new SelectionPrompt<string>()
                    .Title("Choose Employee Profession/Role")
                    .PageSize(4)
                    .AddChoices(new[] { "Teacher", "Janitor", "Admin", "Principal" });
 
                var profession = AnsiConsole.Prompt(professionOptions);

                var salary = AnsiConsole.Prompt(
                    new TextPrompt<int>("Enter Employee Salary:")
                        .Validate(input => input > 0, "Please enter a valid salary.")
                );

                var employee = new Personal
                {
                    Fname = firstName,
                    Lname = lastName,
                    Profession = profession,
                    Salary = salary
                };

                
                using (var dbContext = new FiktivSkolaDatabaseContext())
                {
                    dbContext.Personals.Add(employee);

                    if (IsRole(profession, "Teacher"))
                    {
                        var teacher = new Teacher
                        {
                            Fname = firstName,
                            Lname = lastName,
                        };
                        employee.Teachers.Add(teacher);
                        dbContext.Teachers.Add(teacher);
                    }

                    if (IsRole(profession, "Admin"))
                    {
                        var admin = new Admin
                        {
                            Fname = firstName,
                            Lname = lastName,
                        };
                        employee.Admins.Add(admin); 
                        dbContext.Admins.Add(admin); 
                    }

                    if (IsRole(profession, "Principal"))
                    {
                        var principal = new Principal
                        {
                            Fname = firstName,
                            Lname = lastName,
                        };
                        employee.Principals.Add(principal);
                        dbContext.Principals.Add(principal);
                    }

                    if (IsRole(profession, "Janitor"))
                    {
                        var janitor = new Janitor
                        {
                            Fname = firstName,
                            Lname = lastName,
                        };
                        employee.Janitors.Add(janitor);
                        dbContext.Janitors.Add(janitor);
                    }

                    dbContext.SaveChanges();
                    Console.WriteLine("Employee information saved successfully!");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
            }
        }
        private static bool IsRole(string profession, string role)
        {
            return profession.Equals(role, StringComparison.OrdinalIgnoreCase);
        }

    }
}
