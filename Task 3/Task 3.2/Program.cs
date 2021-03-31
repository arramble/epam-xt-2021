using System;
using System.Collections.Generic;

namespace CustomTypes
{
    class Program
    {
        static void Main()
        {
            TestDynamicArray();
            TestCycledDynamicArray();

            Console.ReadKey(true);
        }

        static void TestDynamicArray()
        {
            List<Person> list = new(15) { new Person("Tom"), new Person("Bob"), new Person("Kim") };
            DynamicArray<Person> arr = new(list);
            DynamicArray<Person> arr2 = arr.Clone() as DynamicArray<Person>;

            arr[1] = new Person("Mary");
            arr.Add(new Person("Kate"));
            arr.AddRange(list);
            arr.Remove(new Person("Tom"));
            arr.Insert(0, new Person("Jack"));
            arr.Capacity = 2;

            foreach (Person item in arr)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            Console.WriteLine(arr2[1]);
            Console.WriteLine(arr2[-3]);
            Console.WriteLine();

            Person[] arr3 = arr2.ToArray();

            foreach (Person item in arr3)
            {
                Console.WriteLine(item);
            }
        }

        static void TestCycledDynamicArray()
        {
            List<Person> list = new(4) { new Person("Kim"), new Person("Jane"), new Person("Mary"), new Person("Liz") };
            CycledDynamicArray<Person> arr = new(list);

            arr[1] = new Person("Eve");
            arr.Add(new Person("Kate"));
            arr.Remove(new Person("Kim"));
            arr.Insert(0, new Person("Sara"));
            arr.Capacity = 4;

            Console.WriteLine();

            int i = 0;
            foreach (Person item in arr)
            {
                Console.WriteLine(item);

                if (++i == 20)
                {
                    break;
                }
            }
        }
    }
}
