using System;

namespace CustomTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Тестирование класса CustomString");
            Console.WriteLine();

            char[] testchars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            CustomString str1 = new CustomString(testchars);
            Console.WriteLine("Тестирование конструктора объекта CustomString из массива символов");
            Console.WriteLine("Исходный массив: testchars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }");
            Console.WriteLine("Объект CustomString str1 = new CustomString(testchars): {0}", str1);
            Console.WriteLine();

            string teststring = "ABCDEFGHijklmnopqrSTUVWXYZ";
            CustomString str2 = new CustomString(teststring);
            Console.WriteLine("Тестирование конструктора объекта CustomString из строки");
            Console.WriteLine("Исходная строка: teststring = {0}", teststring);
            Console.WriteLine("Объект CustomString str2 = new CustomString(teststring): {0}", str2);
            Console.WriteLine();

            Console.WriteLine("Тестирование свойства число символов в объекте CustomString: str2.Length = {0}", str2.Length);
            Console.WriteLine();

            CustomString str3 = new CustomString(teststring);
            Console.WriteLine("Тестирование функций сравнения объектов CustomString");
            Console.WriteLine("Новый объект CustomString str3 = new CustomString(teststring): {0}", str3);
            Console.WriteLine("Функция CustomString.Compare(str1,str2) = {0}", CustomString.Compare(str1,str2));
            Console.WriteLine("Функция CustomString.Compare(str2,str3) = {0}", CustomString.Compare(str2, str3));
            Console.WriteLine("Переопределенная функция str2.Equals(str3) = {0}", str2.Equals(str3));
            Console.WriteLine("Переопределенная функция object.GetHashCode(): (str2.GetHashCode() == str3.GetHashCode()) = {0}", str2.GetHashCode() == str3.GetHashCode());
            Console.WriteLine("Переопределенный оператор (str2 == str3) = {0}", str2 == str3);
            Console.WriteLine("Переопределенный оператор (str2 != str3) = {0}", str2 != str3);
            Console.WriteLine("Функция object.ReferenceEquals(str2,str3) = {0}", object.ReferenceEquals(str2,str3));
            Console.WriteLine();

            Console.WriteLine("Тестирование функций конкатенации");
            Console.WriteLine("Функция конкатенации двух объектов CustomString: CustomString.Concat(str1,str2) = {0}", CustomString.Concat(str1,str2));
            Console.WriteLine("Функция конкатенации объекта CustomString и символа: CustomString.Concat(str1,'A') = {0}", CustomString.Concat(str1,'A'));
            Console.WriteLine("Функция конкатенации объекта CustomString и строки: CustomString.Concat(str1,\"XYZ\") = {0}", CustomString.Concat(str1,"XYZ"));
            Console.WriteLine("Функция конкатенации объекта CustomString и массива символов: CustomString.Concat(str2,testchars) = {0}", CustomString.Concat(str2,testchars));
            Console.WriteLine("Перегруженный оператор \"+\" конкатенации двух объектов CustomString: str3 + str1 = {0}", str3 + str1);
            Console.WriteLine();

            Console.WriteLine("Тестирование функций символьного поиска в объекте CustomString");
            Console.WriteLine("Функция поиска индекса первого вхождения символа: str2.IndexOf('D') = {0}", str2.IndexOf('D'));
            Console.WriteLine("Функция поиска индекса первого вхождения символа, начиная с произвольного стартового индекса: CustomString.Concat(str2,str3).IndexOf('A', 4) = {0}", CustomString.Concat(str2,str3).IndexOf('A', 4));
            Console.WriteLine("Функция проверки вхождения символа в объект CustomString: str3.Contains('f') = {0}", str3.Contains('f'));
            Console.WriteLine();

            Console.WriteLine("Тестирование дополнительных функций объекта CustomString");
            Console.WriteLine("Функция разворота объекта CustomString: str2.Reverse() = {0}", str2.Reverse());
            Console.WriteLine("Функция приведения всех символов объекта CustomString к верхнему регистру: str2.ToUpper() = {0}", str2.ToUpper());
            Console.WriteLine("Функция приведения произвольного символа объекта CustomString к верхнему регистру: str2.ToUpper(14) = {0}", str2.ToUpper(14));
            Console.WriteLine();

            Console.WriteLine("Тестирование индексатора");
            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] == '5')
                {
                    str1[i] = 'x';
                }
            }
            Console.WriteLine("Замена каждого символа '5' объекта str1 на символ 'x': {0}", str1);
            Console.WriteLine();

            Console.WriteLine("Переопределенная функция object.ToString() вызывается неявно при каждом выводе объекта CustomString на консоль");

            Console.ReadKey();
        }
    }

    /// <summary>
    /// Custom string class as char array
    /// </summary>
    public class CustomString
    {
        /// <summary>
        /// Internal char array
        /// </summary>
        private char[] data;

        /// <summary>
        /// The length of internal char array
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Class constructor
        /// </summary>
        public CustomString()
            : this("")
        { }
        
        /// <summary>
        /// Class constructor from char array
        /// </summary>
        /// <param name="chars">Input char array</param>
        public CustomString(char[] chars)
        {
            Length = chars.Length;

            data = new char[Length];

            Array.Copy(chars, data, Length);
        }

        /// <summary>
        /// Class constructor from string
        /// </summary>
        /// <param name="str">Input string</param>
        public CustomString(string str)
        {
            Length = str.Length;

            data = str.ToCharArray();
        }

        /// <summary>
        /// Class indexer
        /// </summary>
        /// <param name="index">Char index</param>
        /// <returns>Char at index</returns>
        public char this[int index]
        {
            get
            {
                return data[index];
            }
            set
            {
                data[index] = value;
            }
        }

        /// <summary>
        /// Overridden object.ToString() method 
        /// </summary>
        /// <returns>String presentation of class instance</returns>
        public override string ToString()
        {
            return new string(data);
        }

        /// <summary>
        /// Compare method
        /// </summary>
        /// <param name="str1">First CustomString object</param>
        /// <param name="str2">Second CustomString object</param>
        /// <returns>Integer result of compare { -1, 0, 1 }</returns>
        public static int Compare(CustomString str1, CustomString str2)
        {
            if (str1.Length > str2.Length)
            {
                return 1;
            }
            else if (str1.Length < str2.Length)
            {
                return -1;
            }
            else
            {
                for (int i = 0; i < str1.Length; i++)
                {
                    if (str1[i] > str2[i])
                    {
                        return 1;
                    }
                    else if (str1[i] < str2[i])
                    {
                        return -1;
                    }
                }

                return 0;
            }
                
        }

        /// <summary>
        /// Overridden object.Equals(object o) method
        /// </summary>
        /// <param name="str">CustomString object to compare</param>
        /// <returns>Boolean result of compare { true, false }</returns>
        public override bool Equals(object str)
        {
            return Compare(this, (CustomString)str) == 0;
        }

        /// <summary>
        /// Overridden object.GetHashCode() method
        /// </summary>
        /// <returns>Hash code for CustomString object</returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Overloaded == operator
        /// </summary>
        /// <param name="str1">First CustomString object</param>
        /// <param name="str2">Second CustomString object</param>
        /// <returns>Boolean result of compare { true, false }</returns>
        public static bool operator ==(CustomString str1, CustomString str2)
        {
            return Compare(str1, str2) == 0;
        }

        /// <summary>
        /// Overloaded != operator
        /// </summary>
        /// <param name="str1">First CustomString object</param>
        /// <param name="str2">Second CustomString object</param>
        /// <returns>Boolean result of compare { false, true }</returns>
        public static bool operator !=(CustomString str1, CustomString str2)
        {
            return Compare(str1, str2) != 0;
        }

        /// <summary>
        /// Conversion to char array
        /// </summary>
        /// <returns>Char array from CustomString object</returns>
        public char[] ToCharArray()
        {
            char[] result = new char[Length];

            Array.Copy(data, result, Length);

            return data;
        }

        /// <summary>
        /// Concatenation of CustomString object and char array
        /// </summary>
        /// <param name="str">Input CustomString object</param>
        /// <param name="chars">Input char array</param>
        /// <returns>CustomString object</returns>
        public static CustomString Concat(CustomString str, char[] chars)
        {
            char[] result = new char[str.Length + chars.Length];

            Array.Copy(str.ToCharArray(), result, str.Length);
            Array.Copy(chars, 0, result, str.Length, chars.Length);

            return new CustomString(result);
        }

        /// <summary>
        /// Concatenation of two CustomString objects
        /// </summary>
        /// <param name="str1">Input CustomString object 1</param>
        /// <param name="str2">Input CustomString object 2</param>
        /// <returns>CustomString object</returns>
        public static CustomString Concat(CustomString str1, CustomString str2)
        {
            return Concat(str1, str2.ToCharArray());
        }

        /// <summary>
        /// Concatenation of CustomString object and string
        /// </summary>
        /// <param name="str1">Input CustomString object</param>
        /// <param name="str2">Input string</param>
        /// <returns>CustomString object</returns>
        public static CustomString Concat(CustomString str1, string str2)
        {
            return Concat(str1, str2.ToCharArray());
        }

        /// <summary>
        /// Concatenation of CustomString object and char
        /// </summary>
        /// <param name="str">Input CustomString object</param>
        /// <param name="c">Input char</param>
        /// <returns>CustomString object</returns>
        public static CustomString Concat(CustomString str, char c)
        {
            return Concat(str, new char[] { c });
        }

        /// <summary>
        /// Overloaded operator + for concatenation of CustomString objects
        /// </summary>
        /// <param name="str1">Input CustomString object 1</param>
        /// <param name="str2">Input CustomString object 2</param>
        /// <returns>CustomString object</returns>
        public static CustomString operator +(CustomString str1, CustomString str2)
        {
            return Concat(str1, str2);
        }

        /// <summary>
        /// Symbol search from start index
        /// </summary>
        /// <param name="c">Input symbol</param>
        /// <param name="startIndex">Start index</param>
        /// <returns>First index of symbol</returns>
        public int IndexOf(char c, int startIndex)
        {
            return Array.FindIndex(data, startIndex, x => x == c);
        }

        /// <summary>
        /// Symbol search
        /// </summary>
        /// <param name="c">>Input symbol</param>
        /// <returns>First index of symbol</returns>
        public int IndexOf(char c)
        {
            return IndexOf(c, 0);
        }

        /// <summary>
        /// Check whether CustomString object contains symbol c
        /// </summary>
        /// <param name="c">Input symbol</param>
        /// <returns>CustomString object contains symbol c</returns>
        public bool Contains(char c)
        {
            return IndexOf(c) != -1;
        }

        /// <summary>
        /// Reverse CustomString object
        /// </summary>
        /// <returns>New reversed CustomString object</returns>
        public CustomString Reverse()
        {
            char[] result = new char[Length];

            Array.Copy(this.ToCharArray(),result,Length);
            Array.Reverse(result);

            return new CustomString(result);
        }

        /// <summary>
        /// Capitalize all symbols of CustomString object
        /// </summary>
        /// <returns>New capitalized CustomString object</returns>
        public CustomString ToUpper()
        {
            char[] result = new char[Length];

            for (int i = 0; i < Length; i++)
            {
                result[i] = Char.ToUpper(data[i]);
            }

            return new CustomString(result);
        }

        /// <summary>
        /// Capitalize symbol at index 
        /// </summary>
        /// <param name="index">Input index</param>
        /// <returns>New CustomString object with capitalized symbol at index</returns>
        public CustomString ToUpper(int index)
        {
            char[] result = new char[Length];

            Array.Copy(data, result, Length);
            result[index] = Char.ToUpper(data[index]);

            return new CustomString(result);
        }
    }
}
