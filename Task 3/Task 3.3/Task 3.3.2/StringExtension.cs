using System;
using System.Linq;

namespace Task3_3_2
{
    public enum Language
    {
        Mixed,
        Russian,
        English,
        Number
    }

    public static class StringExtension
    {
        public static Language GetLanguage(this string word)
        {
            char[] chars = word.ToCharArray();

            if (chars.All(c => Char.IsDigit(c)))
            {
                return Language.Number;
            }
            else if (chars.All(c => (c >= 'а' && c <= 'я') || (c >= 'А' && c <= 'Я')))
            {
                return Language.Russian;
            }
            else if (chars.All(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')))
            {
                return Language.English;
            }
            else
            {
                return Language.Mixed;
            }
        }
    }
}
