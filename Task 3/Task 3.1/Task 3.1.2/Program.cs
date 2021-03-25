using System;
using System.Linq;

namespace Task3_1_2
{
    class Program
    {
        static void Main()
        {
            string input = InputText();
            string[] words = SplitText(input);
            DisplayStatistics(words);

            Console.ReadKey(true);
        }

        static string InputText()
        {
            Console.Write("Введите текст: ");

            return Console.ReadLine();
        }

        static string[] SplitText(string text)
        {
            char[] separators = new char[] { ' ', ':', '.', ',', ';', '!', '?', '(', ')', '-', '"' };

            return text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        static void DisplayStatistics(string[] words)
        {
            var wordGroups = words.GroupBy(w => w.ToLower())
                                  .Select(g => new { Word = g.Key, Count = g.Count() })
                                  .OrderByDescending(g => g.Count);

            Console.WriteLine();
            Console.WriteLine("Частота использования слов в тексте:");

            foreach (var group in wordGroups)
            {
                Console.WriteLine($"{group.Word}: {group.Count}");
            }
        }
    }
}
