using System;
using System.Text;

namespace Task1_1
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
        }

        static void SelectTask(string userInput)
        {
            if (Int32.TryParse(userInput, out int taskNumber))
            {
                Console.WriteLine();

                switch (taskNumber)
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

    class DrawTriangles
    {
        public static void DrawRightTriangle()
        {
            int lineNumber = InputNumber();

            if (lineNumber > 0)
            {
                Console.WriteLine();

                StringBuilder s = new StringBuilder(lineNumber);

                for (int line = 0; line < lineNumber; line++)
                {
                    s.Append('*');
                    Console.WriteLine(s);
                }  
            }

            Console.ReadKey();
        }

        public static void DrawEqualTriangle()
        {
            int lineNumber = InputNumber();

            if (lineNumber > 0)
            {
                Console.WriteLine();

                DrawArbitraryEqualTriangle(lineNumber, 0); 
            }

            Console.ReadKey();
        }

        public static void DrawXmasTree()
        {
            int triangleNumber = InputNumber();

            if (triangleNumber > 0)
            {
                Console.WriteLine();

                for (int i = 0; i <= triangleNumber; i++)
                {
                    DrawArbitraryEqualTriangle(i + 1, triangleNumber - i); 
                }
            }

            Console.ReadKey();
        }

        private static void DrawArbitraryEqualTriangle(int num, int offset)
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

        private static int InputNumber()
        {
            Console.Write("Введите целое число N: ");

            if (Int32.TryParse(Console.ReadLine(), out int num))
            {
                if (num > 0)
                {
                    return num;
                }
                else Console.WriteLine("Число должно быть больше нуля");
            }
            else Console.WriteLine("Неверный формат числа");

            return -1;
        }
    }

    class CalcMath
    {
        public static void CalcRectangleSquare()
        {
            Console.Write("Введите длину стороны прямоугольника a: ");
            string inputA = Console.ReadLine();

            Console.Write("Введите длину стороны прямоугольника b: ");
            string inputB = Console.ReadLine();

            if (Int32.TryParse(inputA, out int a) && 
                Int32.TryParse(inputB, out int b))
            {
                if ((a > 0) && (b > 0))
                {
                    Console.WriteLine($"Площадь прямоугольника равна {a * b}");
                }
                else Console.WriteLine("Длина стороны прямоугольника должна быть больше нуля");
            }
            
            Console.ReadKey();
        }

        public static void CalcSumMultipleNumbers(int number, int multiplier_1, int multiplier_2)
        {
            int sum = SumMultiple(number, multiplier_1) + SumMultiple(number, multiplier_2) - SumMultiple(number, multiplier_1 * multiplier_2);

            Console.WriteLine($"Сумма натуральных чисел меньше {number}, кратных {multiplier_1} и {multiplier_2}, равна {sum}");

            Console.ReadKey();
        }

        private static int SumMultiple(int num, int mult)
        {
            int memberNum = (num - 1) / mult;                     

            return (mult * 2 + mult * (memberNum - 1)) * memberNum / 2;   //Arithmetic progression sum
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

        private static Font font = new Font { bold = false, italic = false, underline = false };

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

        private static void OutputFont()
        {
            Console.Write("Параметры надписи: ");

            string type;

            if (!font.bold && !font.italic && !font.underline)
            {
                type = "None";
            }
            else
            {
                type = String.Empty;

                if (font.bold)
                {
                    type += "Bold, ";
                }

                if (font.italic)
                {
                    type += "Italic, ";
                }

                if (font.underline)
                {
                    type += "Underline";
                }

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
            FillRandomArrayWithSort(arrSize, out int[] arrUnsorted, out int[] arrSorted);

            DisplayArraysWithSort(arrUnsorted, arrSorted);

            Console.ReadKey();
        }

        private static void FillRandomArrayWithSort(int size, out int[] arrUnsorted, out int[] arrSorted)
        {
            arrUnsorted = new int[size];
            arrSorted = new int[size];

            int num;
            int index;

            for (int i = 0; i < size; i++)
            {
                num = rnd.Next(-100, 101);

                arrUnsorted[i] = num;

                //Insertion sort
                index = i;

                while ((index > 0) && (num < arrSorted[index - 1]))
                {
                    arrSorted[index] = arrSorted[index - 1];
                    index--;
                }

                arrSorted[index] = num;
            }
        }

        private static void DisplayArraysWithSort(int[] arrUnsorted, int[] arrSorted)
        {
            Console.WriteLine("Исходный массив случайных целых чисел от -100 до 100:");
            Console.WriteLine(string.Join(' ', arrUnsorted));
            Console.WriteLine();

            Console.WriteLine("Отсортированный массив случайных целых чисел от -100 до 100:");
            Console.WriteLine(string.Join(' ', arrSorted));
            Console.WriteLine();

            Console.WriteLine("Максимальное значение в массиве: {0}", arrSorted[arrSorted.Length - 1]);
            Console.WriteLine("Минимальное значение в массиве: {0}", arrSorted[0]);
        }

        public static void TestNoNegative3D(int arrSize1, int arrSize2, int arrSize3)
        {
            FillRandomArray3D(arrSize1, arrSize2, arrSize3, out int[,,] arr);

            Console.WriteLine("Исходный трехмерный массив случайных целых чисел от -100 до 100:");
            DisplayArray3D(arr);

            NoNegative3D(arr);

            Console.WriteLine("Трехмерный массив после замены положительных чисел на нули:");
            DisplayArray3D(arr);

            Console.ReadKey();
        }

        private static void FillRandomArray3D(int size1, int size2, int size3, out int[,,] arr)
        {
            arr = new int[size1, size2, size3];

            for (int i3 = 0; i3 < size3; i3++)
            {
                for (int i1 = 0; i1 < size1; i1++)
                {
                    for (int i2 = 0; i2 < size2; i2++)
                    {
                        arr[i1, i2, i3] = rnd.Next(-100, 101);
                    }
                }
            }
        }

        private static void NoNegative3D(int[,,] arr)
        {
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
                    }
                }
            }
        }

        private static void DisplayArray3D(int[,,] arr)
        {
            for (int i3 = 0; i3 < arr.GetLength(2); i3++)
            {
                for (int i1 = 0; i1 < arr.GetLength(0); i1++)
                {
                    for (int i2 = 0; i2 < arr.GetLength(1); i2++)
                    {
                        Console.Write("{0}\t", arr[i1, i2, i3]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        public static void TestCalcSumNonnegative(int arrSize)
        {
            FillRandomArray(arrSize, out int[] arr);

            DisplayArray(arr);

            CalcSumNonnegative(arr);

            Console.ReadKey();
        }

        private static void FillRandomArray(int size, out int[] arr)
        {
            arr = new int[size];

            for (int i = 0; i < size; i++)
            {
                arr[i] = rnd.Next(-100, 101);
            }
        }

        private static void DisplayArray(int[] arr)
        {
            Console.WriteLine("Исходный одномерный массив случайных целых чисел от -100 до 100:");
            Console.WriteLine(string.Join(' ', arr));
            Console.WriteLine();
        }

        private static void CalcSumNonnegative(int[] arr)
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
            FillRandomArray2D(arrSize1, arrSize2, out int[,] arr);

            DisplayArray2D(arr);

            CalcSumEvenPosition(arr);

            Console.ReadKey();
        }

        private static void FillRandomArray2D(int size1, int size2, out int[,] arr)
        {
            arr = new int[size1, size2];

            for (int i1 = 0; i1 < size1; i1++)
            {
                for (int i2 = 0; i2 < size2; i2++)
                {
                    arr[i1, i2] = rnd.Next(-100, 101);
                }
            }
        }

        private static void DisplayArray2D(int[,] arr)
        {
            Console.WriteLine("Исходный двумерный массив случайных целых чисел от -100 до 100:");

            for (int i1 = 0; i1 < arr.GetLength(0); i1++)
            {
                for (int i2 = 0; i2 < arr.GetLength(1); i2++)
                {
                    Console.Write($"{arr[i1, i2]}\t");
                }
                Console.WriteLine();
            }
        }

        private static void CalcSumEvenPosition(int[,] arr)
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
