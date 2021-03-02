using System;
using System.Text;

namespace Task1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите номер подзадачи или q чтобы закрыть программу:");
                Console.WriteLine("1 Rectangle");
                Console.WriteLine("2 Triangle");
                Console.WriteLine("3 Another triangle");
                Console.WriteLine("4 X-mas tree");
                Console.WriteLine("5 Sum of numbers");
                Console.WriteLine("6 Font adjustment");
                Console.WriteLine("7 Array processing");
                Console.WriteLine("8 No positive");
                Console.WriteLine("9 Non-negative sum");
                Console.WriteLine("10 2D array");
                Console.WriteLine();

                string input = Console.ReadLine();

                if (input == "q")   break;
                else if (Int32.TryParse(input, out int num))
                {
                    Console.WriteLine();

                    switch (num)
                    {
                        case 1:
                            CalcMath.CalcRectangleSquare();
                            Console.WriteLine();
                            break;
                        case 2:
                            DrawTriangles.DrawRightTriangle();
                            Console.WriteLine();
                            break;
                        case 3:
                            DrawTriangles.DrawEqualTriangle();
                            Console.WriteLine();
                            break;
                        case 4:
                            DrawTriangles.DrawXmasTree();
                            Console.WriteLine();
                            break;
                        case 5:
                            CalcMath.CalcSumMultipleNumbers(1000, 3, 5);
                            Console.WriteLine();
                            break;
                        case 6:
                            ProcessFont.FontAdjustment();
                            Console.WriteLine();
                            break;
                        case 7:
                            CalcArray.ArrayProcessing(10);
                            Console.WriteLine();
                            break;
                        case 8:
                            CalcArray.TestNoNegative3D(3, 4, 5);
                            Console.WriteLine();
                            break;
                        case 9:
                            CalcArray.TestCalcSumNonnegative(10);
                            Console.WriteLine();
                            break;
                        case 10:
                            CalcArray.TestCalcSumEvenPosition(4, 5);
                            Console.WriteLine();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    class DrawTriangles
    {
        public static void DrawRightTriangle()
        {
            Console.Write("Введите целое число N: ");

            if (Int32.TryParse(Console.ReadLine(), out int num))
            {
                if (num > 0)
                {
                    Console.WriteLine();

                    StringBuilder s = new StringBuilder(num);

                    for (int line = 1; line <= num; line++)
                    {
                        s.Append('*');
                        Console.WriteLine(s);
                    }
                }
                else Console.WriteLine("Число должно быть больше нуля");
            }
            else Console.WriteLine("Неверный формат числа");

            Console.ReadKey();
        }

        public static void DrawEqualTriangle()
        {
            Console.Write("Введите целое число N: ");

            if (Int32.TryParse(Console.ReadLine(), out int num))
            {
                if (num > 0)
                {
                    Console.WriteLine();

                    DrawArbitraryEqualTriangle(num, 0);
                }
                else Console.WriteLine("Число должно быть больше нуля");
            }
            else Console.WriteLine("Неверный формат числа");

            Console.ReadKey();
        }

        public static void DrawXmasTree()
        {
            Console.Write("Введите целое число N: ");

            if (Int32.TryParse(Console.ReadLine(), out int num))
            {
                if (num > 0)
                {
                    Console.WriteLine();

                    for (int i = 0; i <= num; i++)
                    {
                        DrawArbitraryEqualTriangle(i + 1, num - i);
                    }
                }
                else Console.WriteLine("Число должно быть больше нуля");
            }
            else Console.WriteLine("Неверный формат числа");

            Console.ReadKey();
        }

        static void DrawArbitraryEqualTriangle(int num, int offset)
        {
            StringBuilder s = new StringBuilder(offset + num * 2 - 1);

            s.Append(' ', offset + num - 1);
            s.Append('*');
            Console.WriteLine(s);

            for (int line = 2; line <= num; line++)
            {
                Console.WriteLine(s.Remove(0, 1).Append('*', 2));
            }
        }
    }

    class CalcMath
    {
        public static void CalcRectangleSquare()
        {
            Console.Write("Введите длину стороны прямоугольника a: ");
            int a = Int32.Parse(Console.ReadLine());

            Console.Write("Введите длину стороны прямоугольника b: ");
            int b = Int32.Parse(Console.ReadLine());

            if ((a > 0) && (b > 0))
            {
                int s = a * b;
                Console.WriteLine("Площадь прямоугольника равна {0}", s);
            }
            else Console.WriteLine("Длина стороны прямоугольника должна быть больше нуля");

            Console.ReadKey();
        }

        public static void CalcSumMultipleNumbers(int num, int m1, int m2)
        {
            int sum = SumMultiple(num, m1) + SumMultiple(num, m2) - SumMultiple(num, m1 * m2);

            Console.WriteLine("Сумма натуральных чисел меньше {0}, кратных {1} и {2}, равна {3}", num, m1, m2, sum);

            Console.ReadKey();
        }

        static int SumMultiple(int num, int m)
        {
            int n = (num - 1) / m;

            return (m * 2 + m * (n - 1)) * n / 2;   //Arithmetic progression sum
        }
    }

    class ProcessFont
    {
        struct Font
        {
            public bool bold;
            public bool italic;
            public bool underline;
        }

        static Font font = new Font { bold = false, italic = false, underline = false };

        public static void FontAdjustment()
        {
            Console.WriteLine("Для сохранения начертания и выхода нажмите q");
            Console.WriteLine();

            string input;

            do
            {
                OutputFont();

                input = Console.ReadLine();

                if (Int32.TryParse(input, out int num))
                {
                    switch (num)
                    {
                        case 1:
                            font.bold = !font.bold;
                            break;
                        case 2:
                            font.italic = !font.italic;
                            break;
                        case 3:
                            font.underline = !font.underline;
                            break;
                        default:
                            break;
                    }
                }
            }
            while (input != "q");
        }

        static void OutputFont()
        {
            Console.Write("Параметры надписи: ");

            string type;

            if ((!font.bold)&&(!font.italic)&&(!font.underline))
            {
                type = "None";
            }
            else
            {
                type = "";

                if (font.bold)  type += "Bold, ";
                if (font.italic) type += "Italic, ";
                if (font.underline) type += "Underline";

                char[] charsToTrim = { ',', ' ' };
                type = type.Trim(charsToTrim);
            }

            Console.WriteLine(type);

            Console.WriteLine("Введите:");
            Console.WriteLine("\t1: bold");
            Console.WriteLine("\t2: italic");
            Console.WriteLine("\t3: underline");
        }
    }

    class CalcArray
    {
        static Random rnd = new Random();

        public static void ArrayProcessing(int arrSize)
        {
            int[] arrUnsorted = new int[arrSize];
            int[] arrSorted = new int[arrSize];

            int num;
            int k;

            for (int i = 0; i < arrSize; i++)
            {
                num = rnd.Next(-100, 101);

                arrUnsorted[i] = num;

                //Insertion sort
                k = i;

                while ((k > 0) && (num < arrSorted[k - 1]))
                {
                    arrSorted[k] = arrSorted[k - 1];
                    k--;
                }

                arrSorted[k] = num;
            }

            int arrMin = arrSorted[0];
            int arrMax = arrSorted[arrSize - 1];

            Console.WriteLine("Исходный массив случайных целых чисел от -100 до 100:");
            Console.WriteLine(string.Join(' ', arrUnsorted));
            Console.WriteLine();

            Console.WriteLine("Отсортированный массив случайных целых чисел от -100 до 100:");
            Console.WriteLine(string.Join(' ', arrSorted));
            Console.WriteLine();

            Console.WriteLine("Максимальное значение в массиве: {0}", arrMax);
            Console.WriteLine("Минимальное значение в массиве: {0}", arrMin);

            Console.ReadKey();
        }

        public static void TestNoNegative3D(int arrSize1, int arrSize2, int arrSize3)
        {
            int[,,] arr = new int[arrSize1, arrSize2, arrSize3];

            Console.WriteLine("Исходный трехмерный массив случайных целых чисел от -100 до 100:");

            for (int i3 = 0; i3 < arrSize3; i3++)
            {
                for (int i1 = 0; i1 < arrSize1; i1++)
                {
                    for (int i2 = 0; i2 < arrSize2; i2++)
                    {
                        arr[i1, i2, i3] = rnd.Next(-100, 101);
                        Console.Write("{0}\t", arr[i1, i2, i3]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            NoNegative3D(arr);

            Console.ReadKey();
        }

        static void NoNegative3D(int[,,] arr)
        {
            Console.WriteLine("Трехмерный массив после замены положительных чисел на нули:");

            for (int i3 = 0; i3 < arr.GetLength(2); i3++)
            {
                for (int i1 = 0; i1 < arr.GetLength(0); i1++)
                {
                    for (int i2 = 0; i2 < arr.GetLength(1); i2++)
                    {
                        if (arr[i1, i2, i3] > 0)
                        {
                            arr[i1, i2, i3] = 0;
                        }
                        Console.Write("{0}\t", arr[i1, i2, i3]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        public static void TestCalcSumNonnegative(int arrSize)
        {
            int[] arr = new int[arrSize];

            Console.WriteLine("Исходный одномерный массив случайных целых чисел от -100 до 100:");

            for (int i = 0; i < arrSize; i++)
            {
                arr[i] = rnd.Next(-100, 101);
                Console.Write("{0} ", arr[i]);
            }
            Console.WriteLine();
            Console.WriteLine();

            CalcSumNonnegative(arr);

            Console.ReadKey();
        }

        static void CalcSumNonnegative(int[] arr)
        {
            int sum = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > 0)
                {
                    sum += arr[i];
                }
            }

            Console.WriteLine("Сумма неотрицательных элементов в массиве равна {0}", sum);
        }

        public static void TestCalcSumEvenPosition(int arrSize1, int arrSize2)
        {
            int[,] arr = new int[arrSize1, arrSize2];

            Console.WriteLine("Исходный двумерный массив случайных целых чисел от -100 до 100:");

            for (int i1 = 0; i1 < arrSize1; i1++)
            {
                for (int i2 = 0; i2 < arrSize2; i2++)
                {
                    arr[i1, i2] = rnd.Next(-100, 101);
                    Console.Write("{0}\t", arr[i1, i2]);
                }
                Console.WriteLine();
            }

            CalcSumEvenPosition(arr);

            Console.ReadKey();
        }
        static void CalcSumEvenPosition(int[,] arr)
        {
            int sum = 0;

            for (int i1 = 0; i1 < arr.GetLength(0); i1++)
            {
                for (int i2 = 0; i2 < arr.GetLength(1); i2++)
                {
                    if ((i1 + i2) % 2 == 0)
                    {
                        sum += arr[i1, i2];
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Сумма элементов массива, стоящих на четной позиции, равна {0}", sum);
        }
    }
}
