using System;
using System.Collections.Generic;

namespace CustomPaint
{
    class Program
    {
        /// <summary>
        /// Список фигур
        /// </summary>
        static List<Figure> figureList = new List<Figure>();

        static void Main(string[] args)
        {
            string name = Input.InputName();
            Console.WriteLine();

            string input = String.Empty;

            while (input != "5")
            {
                input = SelectAction(ref name);
            } 
        }

        /// <summary>
        /// Selection menu
        /// </summary>
        /// <param name="name">User name</param>
        /// <returns>Selection</returns>
        static string SelectAction(ref string name)
        {
            Console.WriteLine($"{name}, выберите действие:");
            Console.WriteLine("1. Добавить фигуру");
            Console.WriteLine("2. Вывести фигуры");
            Console.WriteLine("3. Очистить холст");
            Console.WriteLine("4. Сменить пользователя");
            Console.WriteLine("5. Выход");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.WriteLine();
                    Figure figure = Input.InputFigure();
                    if (figure != null)
                    {
                        figureList.Add(figure);
                    }
                    Console.WriteLine();
                    break;
                case "2":
                    Console.WriteLine();
                    DisplayFigureList();
                    Console.WriteLine();
                    break;
                case "3":
                    Console.WriteLine();
                    ClearCanvas();
                    Console.WriteLine();
                    break;
                case "4":
                    Console.WriteLine();
                    name = Input.InputName();
                    Console.WriteLine();
                    break;
                case "5":
                    break;
                default:
                    Console.WriteLine("Недопустимый ввод");
                    Console.WriteLine();
                    break;
            }

            return userInput;
        }

        /// <summary>
        /// Output list of created figures on the screen
        /// </summary>
        static void DisplayFigureList()
        {
            if (figureList.Count == 0)
            {
                Console.WriteLine("Список фигур пуст!");
            }
            else
            {
                foreach (Figure item in figureList)
                {
                    item.Display();
                }
            }
        }

        /// <summary>
        /// Clear list of the created figures
        /// </summary>
        static void ClearCanvas()
        {
            figureList.Clear();
            Console.WriteLine("Холст очищен!");
        }
    }

    /// <summary>
    /// Abstract class of geometric figure
    /// </summary>
    abstract class Figure
    {
        /// <summary>
        /// Abstract method for output the figure on the screen
        /// </summary>
        public abstract void Display();
    }

    /// <summary>
    /// Class of Line figure
    /// </summary>
    class Line : Figure
    {
        public double X_start { get; }
        public double Y_start { get; }
        public double X_end { get; }
        public double Y_end { get; }
        public double Length { get; }

        public Line(double x_start, double y_start, double x_end, double y_end)
        {
            X_start = x_start;
            Y_start = y_start;
            X_end = x_end;
            Y_end = y_end;

            Length = GetLength(x_start, y_start, x_end, y_end);
        }

        public static double GetLength(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        public override void Display()
        {
            Console.WriteLine($"Линия с координатами ({X_start},{Y_start}) - ({X_end},{Y_end})");
        }
    }

    /// <summary>
    /// Class of Circle figure
    /// </summary>
    class Circle : Figure
    {
        public double X_center { get; }
        public double Y_center { get; }
        public double Radius { get; }
        public double Circum { get; }

        public Circle(double x_center, double y_center, double radius)
        {
            X_center = x_center;
            Y_center = y_center;

            if (radius > 0)
            {
                Radius = radius;
                Circum = GetCircum(radius);
            }
            else throw new ArgumentException("Радиуc должен быть положительным");
        }

        public static double GetCircum(double r) => 2 * Math.PI * r;

        public static double GetArea(double r) => Math.PI * r * r;

        public override void Display()
        {
            Console.WriteLine($"Окружность с центром ({X_center},{Y_center}) и радиусом {Radius}");
        }
    }

    /// <summary>
    /// Class of Disc figure
    /// </summary>
    class Disc : Circle
    {
        public double Area { get; }

        public Disc(double x_center, double y_center, double radius) 
            : base(x_center, y_center, radius)
        {
            Area = Circle.GetArea(radius);
        }

        public override void Display()
        {
            Console.WriteLine($"Круг с центром ({X_center},{Y_center}) и радиусом {Radius}");
        }
    }

    /// <summary>
    /// Class of Ring figure
    /// </summary>
    class Ring : Circle
    {
        public double Inner_radius { get; }
        public double Area { get; }
        public double SumCircum { get; }

        public Ring(double x_center, double y_center, double radius, double inner_radius)
            : base(x_center, y_center, radius)
        {
            if (inner_radius > 0)
            {
                if (inner_radius < radius)
                {
                    Inner_radius = inner_radius;
                    Area = Circle.GetArea(radius) - Circle.GetArea(inner_radius);
                    SumCircum = Circle.GetCircum(radius) + Circle.GetCircum(inner_radius);
                }
                else throw new ArgumentException("Внутренний радиус должен быть меньше внешнего");
            }
            else throw new ArgumentException("Внутренний радиус должен быть положительным");
        }

        public override void Display()
        {
            Console.WriteLine($"Кольцо с центром ({X_center},{Y_center}), внешним радиусом {Radius} и внутренним радиусом {Inner_radius}");
        }
    }

    /// <summary>
    /// Class of Triangle figure
    /// </summary>
    class Triangle : Figure
    {
        public double X1 { get; }
        public double Y1 { get; }
        public double X2 { get; }
        public double Y2 { get; }
        public double X3 { get; }
        public double Y3 { get; }
        public double A { get; }
        public double B { get; }
        public double C { get; }
        public double Perimeter { get; }
        public double Area { get; }

        public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            X3 = x3;
            Y3 = y3;

            A = Line.GetLength(x1, y1, x2, y2);
            B = Line.GetLength(x2, y2, x3, y3);
            C = Line.GetLength(x1, y1, x3, y3);

            Perimeter = A + B + C;

            double p = Perimeter / 2;
            Area = Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }

        public override void Display()
        {
            Console.WriteLine($"Треугольник с координатами ({X1},{Y1}) - ({X2},{Y2}) - ({X3},{Y3})");
        }
    }

    /// <summary>
    /// Class of Rectangle figure
    /// </summary>
    class Rectangle : Figure
    {
        public double X0 { get; }
        public double Y0 { get; }
        public double A { get; }
        public double B { get; }
        public double Perimeter { get; }
        public double Area { get; }

        public Rectangle(double x0, double y0, double a, double b)
        {
            X0 = x0;
            Y0 = y0;

            A = a;
            B = b;

            if ((a > 0) && (b > 0))
            {
                Perimeter = (a + b) * 2;
                Area = a * b;
            }
            else throw new ArgumentException("Значения длин сторон должны быть положительными");
        }

        public override void Display()
        {
            Console.WriteLine($"Прямоугольник с координатами левого нижнего угла ({X0},{Y0}) и длиной сторон ({A},{B})");
        }
    }

    /// <summary>
    /// Class of Square figure
    /// </summary>
    class Square : Rectangle
    {
        public Square(double x0, double y0, double a)
            : base (x0, y0, a, a)
        { }

        public override void Display()
        {
            Console.WriteLine($"Квадрат с координатами левого нижнего угла ({X0},{Y0}) и длиной стороны {A}");
        }
    }

    /// <summary>
    /// Class for static input methods
    /// </summary>
    class Input
    {
        /// <summary>
        /// Input method for User name
        /// </summary>
        /// <returns>User name</returns>
        public static string InputName()
        {
            Console.Write("Введите имя пользователя: ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Common input method for Figure object
        /// </summary>
        /// <returns>Figure object or null</returns>
        public static Figure InputFigure()
        {
            Console.WriteLine("Выберите тип фигуры:");
            Console.WriteLine("1. Линия");
            Console.WriteLine("2. Окружность");
            Console.WriteLine("3. Круг");
            Console.WriteLine("4. Кольцо");
            Console.WriteLine("5. Треугольник");
            Console.WriteLine("6. Прямоугольник");
            Console.WriteLine("7. Квадрат");

            switch (Console.ReadLine())
            {
                case "1": return InputLine();
                case "2": return InputCircle();
                case "3": return InputDisc();
                case "4": return InputRing();
                case "5": return InputTriangle();
                case "6": return InputRectangle();
                case "7": return InputSquare();
                default:
                    Console.WriteLine("Недопустимый ввод");
                    Console.WriteLine();
                    return null;
            }
        }

        /// <summary>
        /// Input method for Line figure
        /// </summary>
        /// <returns>Line instance or null</returns>
        static Line InputLine()
        {
            Console.WriteLine();
            Console.WriteLine("Введите параметры фигуры Линия");
            Console.WriteLine("Введите координаты начальной точки через пробел:");
            string[] start = Console.ReadLine().Split(' ');
            Console.WriteLine("Введите координаты конечной точки через пробел:");
            string[] end = Console.ReadLine().Split(' ');

            if ((start.Length == 2) && (end.Length == 2))
            {
                if (Double.TryParse(start[0], out double x_start) &&
                    Double.TryParse(start[1], out double y_start) &&
                    Double.TryParse(end[0], out double x_end) &&
                    Double.TryParse(end[1], out double y_end))
                {
                    Line result = new Line(x_start, y_start, x_end, y_end);

                    if (result != null)
                    {
                        Console.WriteLine("Фигура Линия создана!");
                        return result;
                    }
                }
                else Console.WriteLine("Неверный формат значений");
            }
            else Console.WriteLine("Неверное число координат");

            return null;
        }

        /// <summary>
        /// Input method for Circle figure
        /// </summary>
        /// <returns>Circle object or null</returns>
        static Circle InputCircle()
        {
            Console.WriteLine();
            Console.WriteLine("Введите параметры фигуры Окружность");
            Console.WriteLine("Введите координаты центра окружности через пробел:");
            string[] center = Console.ReadLine().Split(' ');
            Console.WriteLine("Введите радиус окружности:");
            string input_radius = Console.ReadLine();

            if (center.Length == 2)
            {
                if (Double.TryParse(center[0], out double x_center) &&
                    Double.TryParse(center[1], out double y_center) &&
                    Double.TryParse(input_radius, out double radius))
                {
                    try
                    {
                        Circle result = new Circle(x_center, y_center, radius);
                        Console.WriteLine("Фигура Окружность создана!");
                        return result;
                    }
                    catch (ArgumentException ex) 
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else Console.WriteLine("Неверный формат значений");
            }
            else Console.WriteLine("Неверное число координат центра окружности");

            return null;
        }

        /// <summary>
        /// Input method for Disc figure
        /// </summary>
        /// <returns>Disc object or null</returns>
        static Disc InputDisc()
        {
            Console.WriteLine();
            Console.WriteLine("Введите параметры фигуры Круг");
            Console.WriteLine("Введите координаты центра круга через пробел:");
            string[] center = Console.ReadLine().Split(' ');
            Console.WriteLine("Введите радиус круга:");
            string input_radius = Console.ReadLine();

            if (center.Length == 2)
            {
                if (Double.TryParse(center[0], out double x_center) &&
                    Double.TryParse(center[1], out double y_center) &&
                    Double.TryParse(input_radius, out double radius))
                {
                    try
                    {
                        Disc result = new Disc(x_center, y_center, radius);
                        Console.WriteLine("Фигура Круг создана!");
                        return result;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else Console.WriteLine("Неверный формат значений");
            }
            else Console.WriteLine("Неверное число координат центра круга");

            return null;
        }

        /// <summary>
        /// Input method for Ring figure
        /// </summary>
        /// <returns>Ring object or null</returns>
        static Ring InputRing()
        {
            Console.WriteLine();
            Console.WriteLine("Введите параметры фигуры Кольцо");
            Console.WriteLine("Введите координаты центра кольца через пробел:");
            string[] center = Console.ReadLine().Split(' ');
            Console.WriteLine("Введите внешний радиус кольца:");
            string input_ext_radius = Console.ReadLine();
            Console.WriteLine("Введите внутренний радиус кольца:");
            string input_int_radius = Console.ReadLine();

            if (center.Length == 2)
            {
                if (Double.TryParse(center[0], out double x_center) &&
                    Double.TryParse(center[1], out double y_center) &&
                    Double.TryParse(input_ext_radius, out double ext_radius) &&
                    Double.TryParse(input_int_radius, out double int_radius))
                {
                    try
                    {
                        Ring result = new Ring(x_center, y_center, ext_radius, int_radius);
                        Console.WriteLine("Фигура Кольцо создана!");
                        return result;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else Console.WriteLine("Неверный формат значений");
            }
            else Console.WriteLine("Неверное число координат центра кольца");

            return null;
        }

        /// <summary>
        /// Input method for Triangle figure
        /// </summary>
        /// <returns>Triangle object or null</returns>
        static Triangle InputTriangle()
        {
            Console.WriteLine();
            Console.WriteLine("Введите параметры фигуры Треугольник");
            Console.WriteLine("Введите координаты первой точки через пробел:");
            string[] point_1 = Console.ReadLine().Split(' ');
            Console.WriteLine("Введите координаты второй точки через пробел:");
            string[] point_2 = Console.ReadLine().Split(' ');
            Console.WriteLine("Введите координаты третьей точки через пробел:");
            string[] point_3 = Console.ReadLine().Split(' ');

            if ((point_1.Length == 2) && (point_2.Length == 2) && (point_3.Length == 2))
            {
                if (Double.TryParse(point_1[0], out double x1) &&
                    Double.TryParse(point_1[1], out double y1) &&
                    Double.TryParse(point_2[0], out double x2) &&
                    Double.TryParse(point_2[1], out double y2) &&
                    Double.TryParse(point_3[0], out double x3) &&
                    Double.TryParse(point_3[1], out double y3))
                {
                    Triangle result = new Triangle(x1, y1, x2, y2, x3, y3);

                    if (result != null)
                    {
                        Console.WriteLine("Фигура Треугольник создана!");
                        return result;
                    }
                }
                else Console.WriteLine("Неверный формат значений");
            }
            else Console.WriteLine("Неверное число координат");

            return null;
        }

        /// <summary>
        /// Input method for Rectangle figure
        /// </summary>
        /// <returns>Rectangle object or null</returns>
        static Rectangle InputRectangle()
        {
            Console.WriteLine();
            Console.WriteLine("Введите параметры фигуры Прямоугольник");
            Console.WriteLine("Введите координаты левого нижнего угла через пробел:");
            string[] start = Console.ReadLine().Split(' ');
            Console.WriteLine("Введите стороны прямоугольника через пробел:");
            string[] sides = Console.ReadLine().Split(' ');

            if ((start.Length == 2) && (sides.Length == 2))
            {
                if (Double.TryParse(start[0], out double x0) &&
                    Double.TryParse(start[1], out double y0) &&
                    Double.TryParse(sides[0], out double a) &&
                    Double.TryParse(sides[1], out double b))
                {
                    try
                    {
                        Rectangle result = new Rectangle(x0, y0, a, b);
                        Console.WriteLine("Фигура Прямоугольник создана!");
                        return result;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else Console.WriteLine("Неверный формат значений");
            }
            else Console.WriteLine("Неверное число координат левого нижнего угла");

            return null;
        }

        /// <summary>
        /// Input method for Square figure
        /// </summary>
        /// <returns>Square object or null</returns>
        static Square InputSquare()
        {
            Console.WriteLine();
            Console.WriteLine("Введите параметры фигуры Квадрат");
            Console.WriteLine("Введите координаты левого нижнего угла через пробел:");
            string[] start = Console.ReadLine().Split(' ');
            Console.WriteLine("Введите сторону квадрата:");
            string side = Console.ReadLine();

            if (start.Length == 2)
            {
                if (Double.TryParse(start[0], out double x0) &&
                    Double.TryParse(start[1], out double y0) &&
                    Double.TryParse(side, out double a))
                {
                    try
                    {
                        Square result = new Square(x0, y0, a);
                        Console.WriteLine("Фигура Квадрат создана!");
                        return result;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else Console.WriteLine("Неверный формат значений");
            }
            else Console.WriteLine("Неверное число координат левого нижнего угла");

            return null;
        }
    }
}
