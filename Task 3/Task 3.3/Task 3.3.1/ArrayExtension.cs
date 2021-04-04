using System;
using System.Linq;

namespace Task3_3_1
{
    public static class ArrayExtension
    {
        public static void MemberwiseAction<T>(this T[] array, Func<T, T> function)
        {
            if (function != null)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = function(array[i]);
                }
            }
        }

        public static T MostFrequent<T>(this T[] array)
        {
            return array.GroupBy(x => x)
                        .OrderBy(num => num.Count())
                        .Last().Key;
        }

        public static sbyte Sum(this sbyte[] array)
        {
            sbyte sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        public static byte Sum(this byte[] array)
        {
            byte sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        public static short Sum(this short[] array)
        {
            short sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        public static ushort Sum(this ushort[] array)
        {
            ushort sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        public static int Sum(this int[] array)
        {
            int sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        public static uint Sum(this uint[] array)
        {
            uint sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        public static long Sum(this long[] array)
        {
            long sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        public static ulong Sum(this ulong[] array)
        {
            ulong sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        public static float Sum(this float[] array)
        {
            float sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        public static double Sum(this double[] array)
        {
            double sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        public static decimal Sum(this decimal[] array)
        {
            decimal sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        public static sbyte Average(this sbyte[] array)
        {
            return (sbyte)(array.Sum() / array.Length);
        }

        public static byte Average(this byte[] array)
        {
            return (byte)(array.Sum() / array.Length);
        }

        public static short Average(this short[] array)
        {
            return (short)(array.Sum() / array.Length);
        }

        public static ushort Average(this ushort[] array)
        {
            return (ushort)(array.Sum() / array.Length);
        }

        public static int Average(this int[] array)
        {
            return array.Sum() / array.Length;
        }

        public static uint Average(this uint[] array)
        {
            return (uint)(array.Sum() / array.Length);
        }

        public static long Average(this long[] array)
        {
            return array.Sum() / array.Length;
        }

        public static ulong Average(this ulong[] array)
        {
            return (ulong)((uint)array.Sum() / array.Length);
        }

        public static float Average(this float[] array)
        {
            return array.Sum() / array.Length;
        }

        public static double Average(this double[] array)
        {
            return array.Sum() / array.Length;
        }

        public static decimal Average(this decimal[] array)
        {
            return array.Sum() / array.Length;
        }
    }
}
