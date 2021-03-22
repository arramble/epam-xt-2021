using System;
using System.Text;
using System.Collections.Generic;

namespace Task1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = String.Empty;

            while (input != "q")
            {
                DisplayTaskList();

                input = Console.ReadLine();
                SelectTask(input);
            }
        }

        static void DisplayTaskList()
        {
            Console.WriteLine("Введите номер подзадачи или q чтобы закрыть программу:");
            Console.WriteLine("1 Averages");
            Console.WriteLine("2 Doubler");
            Console.WriteLine("3 Lowercase");
            Console.WriteLine("4 Validator");
            Console.WriteLine();
        }

        static void SelectTask(string userInput)
        {
            if (Int32.TryParse(userInput, out int taskNumber))
            {
                Console.WriteLine();

                switch (taskNumber)
                {
                    case 1:
                        ProcessString.CalcWordAverage();
                        Console.WriteLine();
                        break;
                    case 2:
                        ProcessString.DoubleSymbols();
                        Console.WriteLine();
                        break;
                    case 3:
                        ProcessString.CountLowercaseWords();
                        Console.WriteLine();
                        break;
                    case 4:
                        ProcessString.TypeUppercase();
                        Console.WriteLine();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    class ProcessString
    {
        public static void CalcWordAverage()
        {
            Console.Write("Введите строку: ");

            string input = Console.ReadLine();

            char[] separators = new char[] { ' ', ':', '.', ',', ';', '!', '?', '(', ')', '-', '"' };
            string[] words = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            int sum = 0;

            foreach (string word in words)
            {
                sum += word.Length;
            }

            int avg = sum / words.Length;  //Discard the fractional part 

            Console.WriteLine("Средняя длина слова в введенной строке равна {0}", avg);

            Console.ReadKey();
        }

        public static void DoubleSymbols()
        {
            Console.Write("Введите первую строку: ");
            string input1 = Console.ReadLine();

            Console.Write("Введите вторую строку: ");
            string input2 = Console.ReadLine();

            StringBuilder output = new StringBuilder(input1.Length * 2);

            foreach (char c in input1)
            {
                if (input2.Contains(c))
                {
                    output.Append(c, 2);
                }
                else
                {
                    output.Append(c, 1);
                }
            }

            Console.WriteLine("Итоговая строка: {0}", output);

            Console.ReadKey();
        }

        public static void CountLowercaseWords()
        {
            Console.Write("Введите строку: ");

            string input = Console.ReadLine();

            char[] separators = new char[] { ' ', ':', '.', ',', ';', '!', '?', '(', ')', '-', '"' };
            string[] words = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            int count = 0;

            foreach (string word in words)
            {
                if (Char.IsLower(word[0]))
                {
                    count++;
                }
            }

            Console.WriteLine("Количество слов с маленькой буквы в введенной строке равно {0}", count);

            Console.ReadKey();
        }

        public static void TypeUppercase()
        {
            List<int> indices = new List<int> ();

            string[] endSign = new string[3] { ". ", "? ", "! " };

            Console.WriteLine("Введите строку:");

            string input = Console.ReadLine();

            int index = 0;

            if (Char.IsLower(input[index]))
            {
                indices.Add(index);
            }

            foreach (string s in endSign)
            {
                index = input.IndexOf(s);

                while (index != -1)
                {
                    if (Char.IsLower(input[index + 2]))
                    {
                        indices.Add(index + 2);
                    }

                    index = input.IndexOf(s, index + 2);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Строка с исправленными первыми словами предложений:");
            Console.WriteLine(Capitalize(input, indices));

            Console.ReadKey();
        }

        private static string Capitalize(string s, List<int> ind)
        {
            char[] chars = s.ToCharArray();

            foreach (int i in ind)
            {
                chars[i] = Char.ToUpper(chars[i]);
            }

            return new string(chars);
        }
    }
}
