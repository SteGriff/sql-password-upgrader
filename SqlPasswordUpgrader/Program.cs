using System;
using System.IO;

namespace SqlPasswordUpgrader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started");

            var pc = new PasswordCheck("DefaultPassword#951");

            string filename = "C:\\temp\\users.txt";
            string[] lines = File.ReadAllLines(filename);

            Console.WriteLine("Read " + filename);

            string statement = "";
            string report = "Users whose passwords were changed:" + Environment.NewLine;

            int passwordsChanged = 0;

            foreach (var l in lines)
            {
                string[] parts = l.Split(new char[] { '\t' });
                string username = parts[0];
                string password = parts[1];

                string originalPassword = password;

                password = pc.CheckAndFixLength(password, username);
                password = pc.CheckAndFixComplexity(password);

                statement += String.Format("CREATE LOGIN [{0}] WITH PASSWORD = '{1}'{2}GO{2}",
                    username,
                    password,
                    Environment.NewLine);

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

    }
}

