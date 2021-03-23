using System;
using System.Collections.Generic;

namespace Task3_1_1
{
    class Program
    {
        static void Main()
        {
            int totalNum = InputNum("Введите N: ");

            List<Person> personCircle = new List<Person>(totalNum);
            FillList(personCircle, totalNum);
            DisplayList(personCircle);
            Console.WriteLine();

            int decimationNum = InputNum("Введите, какой по счету человек будет вычеркнут каждый раунд: ");

            int roundCount = 0;
            int decimationCount = decimationNum - 1;
            while (personCircle.Count >= decimationNum)
            {
                Console.WriteLine();
                decimationCount = Decimation(personCircle, decimationNum, decimationCount);
                Console.WriteLine($"Раунд {++roundCount}. Вычеркнут человек. Людей осталось: {personCircle.Count}");
                DisplayList(personCircle);
                Console.ReadKey(true);
            }

            Console.WriteLine();
            Console.WriteLine("Игра окончена. Невозможно вычеркнуть больше людей.");
            Console.ReadKey();
        }

        static int InputNum(string prompt)
        {
            int num;

            do
            {
                Console.Write(prompt);
                Int32.TryParse(Console.ReadLine(), out num);
            }
            while (num <= 0);

            return num;
        }

        static void FillList(List<Person> list, int num)
        {
            for (int i = 0; i < num; i++)
            {
                list.Add(new Person(i + 1));
            }
        }

        static void DisplayList(List<Person> list)
        {
            foreach (Person item in list)
            {
                item.Display();
            }

            Console.WriteLine();
        }

        static int Decimation(List<Person> list, int inc, int count)
        {
            list.RemoveAt(count--);
            
            count += inc;

            if (count >= list.Count)
            {
                count -= list.Count;
            }

            return count;
        }
    }

    class Person
    { 
        public int Num { get; }

        public Person (int num)
        {
            Num = num;
        }

        public void Display()
        {
            Console.Write($"{Num} ");
        }
    }
}
