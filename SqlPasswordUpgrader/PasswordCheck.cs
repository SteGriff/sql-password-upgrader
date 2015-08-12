using System;
using System.Linq;

namespace SqlPasswordUpgrader
{
    public class PasswordCheck
    {
        
        private const string lowercaseCharacters = "abcdefghijklmnopqrstuvwxyz";
        private const string uppercaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string specialCharacters = "!£$%^&*()-=_+,./<>?;'#:@~[]{}";
        private const string numbers = "0123456789";

        private string[] characterSets = { lowercaseCharacters, uppercaseCharacters, specialCharacters, numbers };

        // Used for padding passwords where username cant be used
        // Product/database name is a good idea
        private string _specialString;

        public PasswordCheck()
        {
            _specialString = "LOGIN123";
        }

        public PasswordCheck(string specialString)
        {
            _specialString = specialString;
        }

        private bool ComplexEnough(string password)
        {
            int points = 0;

            foreach (var charSet in characterSets)
            {
                if (password.Any(ch => charSet.Contains(ch)))
                {
                    points++;
                }

                if (points > 2)
                {
                    return true;
                }
            }

            return points > 2;
        }

        private bool LongEnough(string password)
        {
            // 8 or more chars
            return password.Length > 7;
        }

        public string CheckAndFixLength(string password, string username)
        {
            if (!LongEnough(password))
            {
                password = MakeLonger(password, username);
            }

            return password;
        }

        /// <summary>
        /// Take the first x characters of username
        /// and add them to the end of the password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        private string MakeLonger(string password, string username)
        {
            int deficit = 8 - password.Length;
            string padding = "";

            try
            {
                padding = username.Substring(0, deficit);
            }
            catch (Exception)
            {
                padding = _specialString;
            }

            return password + padding;
        }

        public string CheckAndFixComplexity(string password)
        {
            if (!ComplexEnough(password))
            {
                password = password + "+1";
            }

            if (!ComplexEnough(password))
            {
                password = password + "x";
            }

            return password;
        }

    }
}
