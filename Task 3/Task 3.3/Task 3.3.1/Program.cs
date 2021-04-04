using System;
using System.Linq;

namespace Task3_3_1
{
    class Program
    {
        static void Main()
        {
            TestSuperArray();

            Console.ReadKey(true);
        }

        static void TestSuperArray()
        {
            float[] arrayFloat = new float[6] { 0.1f, 1.2f, 2.3f, 3.4f, 4.5f, 2.3f };
            Console.WriteLine(String.Join(',', arrayFloat));

            arrayFloat.MemberwiseAction(Square);
            Console.WriteLine(String.Join(',', arrayFloat));

            Console.WriteLine();

            byte[] arrayByte = new byte[6] { 3, 1, 2, 3, 4, 5 };
            Console.WriteLine(String.Join(',', arrayByte));

            arrayByte.MemberwiseAction(Square);
            Console.WriteLine(String.Join(',', arrayByte));

            Console.WriteLine();

            Console.WriteLine(arrayFloat.Sum());
            Console.WriteLine(arrayFloat.Average());
            Console.WriteLine(arrayFloat.MostFrequent());
            Console.WriteLine(arrayByte.MostFrequent());
        }

        static float Square (float x)
        {
            return (float)Math.Round(x * x, 2);
        }

        static byte Square (byte x)
        {
            return (byte)(x * x);
        }
    }
}
