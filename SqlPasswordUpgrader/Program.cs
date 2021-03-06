﻿using System;
using System.IO;

namespace SqlPasswordUpgrader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started");

            var pc = new PasswordCheck("DefaultPassword#951");

            //The input file of usernames/passwords
            string filename = "C:\\temp\\users.txt";
            string[] lines = File.ReadAllLines(filename);

            Console.WriteLine("Opened " + filename);

            //The template (SQL statement probably) to insert parsed values into
            string templateFilename = "template.txt";
            string template = File.ReadAllText(templateFilename);

            Console.WriteLine("Opened template " + templateFilename);

            //Prepare final outputs
            string statement = "";
            string report = "Users whose passwords were changed:" + Environment.NewLine;

            int passwordsChanged = 0;

            foreach (var l in lines)
            {
                string[] parts = l.Split(new char[] { '\t' });
                string username = parts[0];
                string password = parts[1];

                string originalPassword = password;

                password = pc.CheckAndFix(password, username);
                parts[1] = password;

                statement += String.Format(template, parts);

                if (originalPassword != password)
                {
                    report += username + Environment.NewLine;
                    Console.WriteLine("Changed password for " + username);
                    passwordsChanged += 1;
                }
            }

            Console.WriteLine(passwordsChanged + " passwords changed; saving results.");

            string dateField = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ssf");
            string sqlFileName = "C:\\temp\\create-logins-" + dateField + ".sql";

            var ss = File.CreateText(sqlFileName);
            ss.Write(statement);
            ss.Close();

            string reportFileName = "C:\\temp\\report-" + dateField + ".txt";

            var rs = File.CreateText(reportFileName);
            rs.Write(report);
            rs.Close();

            Console.WriteLine("Saved SQL to " + sqlFileName);
            Console.WriteLine("Saved report to " + reportFileName);
            Console.ReadLine();

        }

        static void Error(string message)
        {
            Console.WriteLine("Error:");
            Console.WriteLine(message);
            Console.WriteLine("Press Return to quit");
            Console.ReadLine();
        }

    }
}

