﻿using System;
using System.Data.SqlClient;
using System.Linq;

namespace SYSA_Project_2
{
    class Program
    {
        public static string connectString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\wrike\\source\\repos\\SYSA-Project-2\\Project2Db.mdf;Integrated Security=True";
        public static int loginCounter = 0;

        public static string iD;
        public static string pass;
        public static string code;

        static void Main(string[] args)
        {
            Login();
        }

        public static void Login()
        {
            // Header and login input
            Console.WriteLine("            WELCOME TO SYSA PROJECT 2");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine();
            Console.Write("Enter Login ID: ");
            iD = Console.ReadLine().Trim();
            bool hasDigit = iD.Any(char.IsDigit);
            bool isValid = false;

            while(isValid == false)
            {
                // Empty login validation
                if (iD == string.Empty)
                {
                    Console.WriteLine("Login ID must not be empty, please try again.");
                    Console.Write("Enter Login ID: ");
                    iD = Console.ReadLine().Trim();
                    hasDigit = iD.Any(char.IsDigit);
                }

                // Login ID length validation
                else if (iD.Length > 15)
                {
                    Console.WriteLine("Login ID must not greater than 15 characters, please try again.");
                    Console.Write("Enter Login ID: ");
                    iD = Console.ReadLine().Trim();
                    hasDigit = iD.Any(char.IsDigit);
                }

                // No numerics login validation
                else if (hasDigit)
                {
                    Console.WriteLine("Login ID must not contain numeric digits, please try again.");
                    Console.Write("Enter Login ID: ");
                    iD = Console.ReadLine().Trim();
                    hasDigit = iD.Any(char.IsDigit);
                }
                else
                {
                    isValid = true;
                }

            }

            isValid = false;            

            // Password input
            Console.Write("Enter Password: ");
            pass = Console.ReadLine().Trim();

            while(isValid == false)
            {
                
                // Empty password validation
                if (pass == string.Empty)
                {
                    Console.WriteLine("Password must not be empty, please try again.");
                    Console.Write("Enter Password: ");
                    pass = Console.ReadLine().Trim();
                }
                
                // Password length validation
                else if (pass.Length > 8)
                {
                    Console.WriteLine("Password must not be greater than 8 characters, please try again.");
                    Console.Write("Enter Password: ");
                    pass = Console.ReadLine().Trim();
                }
                else
                {
                    bool firstUpper = char.IsUpper(pass[0]);

                    // Password first character uppercase validation
                    if (firstUpper == false)
                    {
                        Console.WriteLine("The first character of the password must be uppercase, please try again.");
                        Console.Write("Enter Password: ");
                        pass = Console.ReadLine().Trim();
                        firstUpper = char.IsUpper(pass[0]);
                    }
                    else
                    {
                        isValid = true;
                    }
                }    
            }
            isValid = false;
            

            // Security Code input
            Console.Write("Enter Security Code: ");
            code = Console.ReadLine().Trim();

            while(isValid == false)
            {
                bool isNumbers = code.All(char.IsDigit);
                // Security code empty validation
                if (code == string.Empty)
                {
                    Console.WriteLine("Security Code must not be empty, please try again.");
                    Console.Write("Enter Security Code: ");
                    code = Console.ReadLine().Trim();
                }

                else if (code.Length > 3)
                {
                    Console.WriteLine("Security Code must not be greater than 3 characters, please try again.");
                    Console.Write("Enter Security Code: ");
                    code = Console.ReadLine().Trim();
                }
                
                else if (isNumbers == false)
                {
                    Console.WriteLine("Security Code be numeric, please try again.");
                    Console.Write("Enter Security Code: ");
                    code = Console.ReadLine().Trim();
                    isNumbers = code.All(char.IsDigit);
                }
                else
                {
                    isValid = true;
                }
            }

            ValidateLogin();
        }

        public static void ValidateLogin()
        {
            int codeParse = int.Parse(code);

            SqlConnection cn = new SqlConnection(connectString);
            cn.Open();
            string selectQuery = "SELECT * FROM Users WHERE LoginID='" + iD + "' AND Password='" + pass + "' AND SecurityCode=" + codeParse + ";";
            SqlCommand selectCommand = new SqlCommand(selectQuery, cn);
            SqlDataReader dataReader;

            while (loginCounter < 3)
            {
                try
                {
                    dataReader = selectCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Console.WriteLine("------------------------------");
                        Console.WriteLine("Welcome " + dataReader["LoginID"]);
                        Console.Write("Press any key to continue to the Menu...");
                        Console.ReadKey();
                        Console.Clear();
                        DisplayMenu();
                        loginCounter = 3;
                    }                                     
                    
                }
                catch
                {
                    loginCounter++;

                    if (loginCounter == 3)
                    {
                        Console.Write("You have failed to login three times! Press any key to exit the program...");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }


                    Console.WriteLine("User not found! Login attempt " + loginCounter + " failed Please Try Again!");
                    Console.Write("Press any key to attempt another login...");
                    Console.ReadKey();
                    Console.Clear();
                    Login();

                }
            }

        }

        public static void DisplayMenu()
        {
            string selection;

            Console.WriteLine("     Project 2 - Student Progress Information");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("           - Enter Student Grade Sheet Details");
            Console.WriteLine("           - Display Module");
            Console.WriteLine("           - Exit from the menu");
            Console.Write("          Your Selection (S, D, X): ");
            selection = Console.ReadLine();

            if(selection == "s" || selection == "S")
            {
                Console.Clear();
                DisplayGradeSheet();
            }
            else if (selection == "d" || selection == "D")
            {
                DisplayReport();
            }
            else if (selection == "x" || selection == "X")
            {
                Console.WriteLine();
                Console.Write("Bye for now! Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else 
            {
                Console.WriteLine();
                Console.WriteLine("\"" + selection + "\" is and invalid selection!");
                Console.Write("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                DisplayMenu();
            }
        }

        public static void DisplayGradeSheet()
        {
            bool isValid = false;
            string temp;
            int studentID;
            string name;
            int gned;
            int itce;
            int netd;
            int oop;
            int syde;
            int sysa;
            int webd;
            float gpa;
            string comments;

            Console.WriteLine("   Enter Student Grade Sheet Details");
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter the Student ID: ");
            temp = Console.ReadLine().Trim();

            while (isValid == false)
            {
                bool isNumbers = temp.All(char.IsDigit);
                // Student ID empty validation
                if (temp == string.Empty)
                {
                    Console.WriteLine("Student ID must not be empty, please try again!");
                    Console.Write("Enter the Student ID: ");
                    temp = Console.ReadLine().Trim();
                }

                // Student ID numeric validation                
                else if (isNumbers == false)
                {
                    Console.WriteLine("Student ID must be numeric characters, please try again!");
                    Console.Write("Enter the Student ID: ");
                    temp = Console.ReadLine().Trim();
                    isNumbers = temp.All(char.IsDigit);
                }
                // Student ID length validation
                else if (temp.Length != 9)
                {
                    Console.WriteLine("Student ID must be exactly 9 characters long, please try again!");
                    Console.Write("Enter the Student ID: ");
                    temp = Console.ReadLine().Trim();
                }
                else
                {
                    isValid = true;
                    studentID = Int32.Parse(temp);
                }
            }
            isValid = false;
           

            Console.Write("Enter the Student Name: ");
            temp = Console.ReadLine().Trim();

            while (isValid == false)
            {
                // Student name empty validation
                if (temp == string.Empty)
                {
                    Console.WriteLine("Student Name must not be empty, please try again!");
                    Console.Write("Enter the Student Name: ");
                    temp = Console.ReadLine().Trim();
                }

                // Students name length validation
                else if (temp.Length > 25)
                {
                    Console.WriteLine("Student Name must be 25 characters or less, please try again!");
                    Console.Write("Enter the Student Name: ");
                    temp = Console.ReadLine().Trim();
                }
                else
                {
                    isValid = true;
                    name = temp;
                }
            }
            isValid = false;

            gned = GradeInput("GNED000");

            itce = GradeInput("ITCE3200");

            netd = GradeInput("NETD3202");

            oop = GradeInput("OOP3200");

            syde = GradeInput("SYDE3203");

            sysa = GradeInput("SYSA3204");

            webd = GradeInput("WEBD3201");

            gpa = (gned + itce + netd + oop + syde + sysa + webd) / 7;


        }

        public static void DisplayReport()
        {

        }

        public static int GradeInput(string course)
        {
            string temp;
            int grade = 0;
            bool isValid = false;

            Console.Write("Enter the " + course + " Grade: ");
            temp = Console.ReadLine().Trim();

            while (isValid == false)
            {
                bool isNumbers = temp.All(char.IsDigit);

                // ITCE3200 empty validation
                if (temp == string.Empty)
                {
                    Console.WriteLine("Grades must not be empty, please try again!");
                    Console.Write("Enter the " + course + " Grade: ");
                    temp = Console.ReadLine().Trim();
                }

                //ITCE3200 numeric validation
                else if (isNumbers == false)
                {
                    Console.WriteLine("Grades must be numeric characters, please try again!");
                    Console.Write("Enter the " + course + " Grade: ");
                    temp = Console.ReadLine().Trim();
                    isNumbers = temp.All(char.IsDigit);
                }
                else
                {
                    grade = Int32.Parse(temp);
                    // ITCE3200 range validation
                    if (grade < 0 || grade > 100)
                    {
                        Console.WriteLine("Grades must be between 0 and 100, please try again!");
                        Console.Write("Enter the " + course + " Grade: ");
                        temp = Console.ReadLine();
                    }
                    else
                    {
                        isValid = true;
                    }
                }
            }
            isValid = false;

            return grade;
        }
    }
}