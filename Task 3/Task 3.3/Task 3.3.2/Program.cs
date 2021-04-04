using System;

namespace Task3_3_2
{
    class Program
    {
        static void Main()
        {
            TestSuperString();

            Console.ReadKey(true);
        }

        static void TestSuperString()
        {
            Console.Write("Введите слово: ");

            string input = Console.ReadLine();

            Console.WriteLine("Язык слова: " + input.GetLanguage());
        }
    }
}
