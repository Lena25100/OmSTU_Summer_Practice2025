using System;

namespace task01
{
    public static class StringExtensions
    {
        public static bool IsPalindrome(this string input)
        {
            if (input == null || input == "")
            {
                return false;
            }

            string low = input.ToLower();
            string str = "";

            foreach (char ch in low)
            {
                if (char.IsPunctuation(ch) == false && char.IsWhiteSpace(ch) == false)
                {
                    str += ch;
                }
            }

            for (int i = 0, j = str.Length - 1; i < j; i++, j--)
            {
                if (str[i] != str[j])
                {
                    return false;
                }
            }
            return true;


        }
    }
}

