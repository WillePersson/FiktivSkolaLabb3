using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiktivSkolaLabb._2.Data;
using FiktivSkolaLabb._2.Models;
using Spectre.Console;
using static FiktivSkolaLabb._2.Logic;

namespace FiktivSkolaLabb._2
{
    public class MenuHandler
    {
        public enum MenuOption
        {
            ViewPersonal = 1,
            ViewAllStudents,
            ViewStudentsInClass,
            ViewGradesLast30Days,
            ViewCourses,
            AddNewStudent,
            AddNewPersonal,
            ViewAllPersonal,
            ViewAdmins,
            ViewTeachers,
            ViewPrincipal,
            ViewJanitors,
            Back,
            Exit,
            Invalid
        }

        public MenuOption GetSelection()
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOption>()
                    .Title("[Purple]Main Menu[/]")
                    .AddChoices(
                        MenuOption.ViewPersonal,
                        MenuOption.ViewAllStudents,
                        MenuOption.ViewStudentsInClass,
                        MenuOption.ViewGradesLast30Days,
                        MenuOption.ViewCourses,
                        MenuOption.AddNewStudent,
                        MenuOption.AddNewPersonal,
                        MenuOption.Exit
                    )
            );

            return choice;
        }

        public void DisplayMenu()
        {
            bool showMenu = true;

            while (showMenu)
            {
                Console.Clear();
                var choice = GetSelection();

                switch (choice)
                {
                    case MenuOption.ViewPersonal:
                        DisplayPersonalMenu(); 
                        break;
                    case MenuOption.ViewAllStudents:
                        DisplayStudentSortingMenu();
                        break;
                    case MenuOption.ViewStudentsInClass:
                        Logic.ViewStudentsInClass();
                        WaitForEnter();
                        break;
                    case MenuOption.ViewGradesLast30Days:
                        Logic.ViewGradesLast30Days();
                        WaitForEnter();
                        break;
                    case MenuOption.ViewCourses:
                        Logic.ViewCourses();
                        WaitForEnter();
                        break;
                    case MenuOption.AddNewStudent:
                        Logic.AddNewStudent();
                        WaitForEnter();
                        break;
                    case MenuOption.AddNewPersonal:
                        Logic.AddNewPersonal();
                        WaitForEnter();
                        break;
                    case MenuOption.Exit:
                        Console.WriteLine("Exiting...");
                        showMenu = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        WaitForEnter();
                        break;
                }
            }
        }

        private void WaitForEnter()
        {
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        private void DisplayPersonalMenu()
        {
            bool showPersonalMenu = true;

            while (showPersonalMenu)
            {
                Console.Clear();
                var choice = Logic.ViewPersonal();

                switch (choice)
                {
                    case MenuOption.ViewAllPersonal:
                        Logic.ViewAllPersonal();
                        WaitForEnter();
                        break;
                    case MenuOption.ViewAdmins:
                        Logic.ViewAllAdmins();
                        WaitForEnter();
                        break;
                    case MenuOption.ViewTeachers:
                        Logic.ViewAllTeachers();
                        WaitForEnter();
                        break;
                    case MenuOption.ViewPrincipal:
                        Logic.ViewAllPrincipals();
                        WaitForEnter();
                        break;
                    case MenuOption.ViewJanitors:
                        Logic.ViewAllJanitors();
                        WaitForEnter();
                        break;
                    case MenuOption.Back:
                        showPersonalMenu = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        WaitForEnter();
                        break;
                }
            }
        }
        private void DisplayStudentSortingMenu()
        {
            var sortBy = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Sort Students by:")
                    .PageSize(5)
                    .AddChoices("First Name", "Last Name")
            );

            var sortOrder = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Sort Order:")
                    .PageSize(3)
                    .AddChoices("Ascending", "Descending")
            );

            string sortCriteria = sortBy.ToLowerInvariant().Contains("first") ? "firstname" : "lastname";
            string order = sortOrder.ToLowerInvariant().Contains("asc") ? "ascending" : "descending";

            Logic.ViewAllStudentsSorted(sortCriteria, order);
            WaitForEnter();
        }
    }
}

