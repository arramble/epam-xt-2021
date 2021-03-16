using System;
using System.Text;
using System.Collections.Generic;

namespace Task2_2
{
    class Program
    {
        static char[,] board;
        static List<CellObject> cellObjectList = new List<CellObject>();

        static Random rnd = new Random();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            InitBoard();
            GenerateCellObjects();
            FillBoard();
            DisplayBoard();

            Console.WriteLine("Нажмите любую клавишу, чтобы начать...");

            ConsoleKeyInfo keyPressed;

            while (true)
            {
                keyPressed = Console.ReadKey(true);

                if ((ConsoleModifiers.Control & keyPressed.Modifiers) != 0)
                {
                    switch (keyPressed.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            break;
                        case ConsoleKey.UpArrow:
                            break;
                        case ConsoleKey.RightArrow:
                            break;
                        case ConsoleKey.DownArrow:
                            break;
                        default:
                            break;
                    }    
                }
                else if ((ConsoleModifiers.Shift & keyPressed.Modifiers) != 0)
                {
                    switch (keyPressed.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            break;
                        case ConsoleKey.UpArrow:
                            break;
                        case ConsoleKey.RightArrow:
                            break;
                        case ConsoleKey.DownArrow:
                            break;
                        default:
                            break;
                    }
                }
                else if ((ConsoleModifiers.Alt & keyPressed.Modifiers) != 0)
                {
                    switch (keyPressed.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            break;
                        case ConsoleKey.UpArrow:
                            break;
                        case ConsoleKey.RightArrow:
                            break;
                        case ConsoleKey.DownArrow:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (keyPressed.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            break;
                        case ConsoleKey.UpArrow:
                            break;
                        case ConsoleKey.RightArrow:
                            break;
                        case ConsoleKey.DownArrow:
                            break;
                        case ConsoleKey.G:
                            break;
                        default:
                            break;
                    }
                }

                foreach (CellObject item in cellObjectList)
                {
                    if (item is NPC)
                    {
                        (item as NPC).Move();
                    }
                }

                Console.Clear();
                DisplayBoard();
            }
        }

        static void DisplayBoard()
        {
            int rowCount = board.GetLength(0);
            int columnCount = board.GetLength(1);

            string frame = new string('=', columnCount + 2);
            Console.WriteLine(frame);

            for (int i = 0; i < rowCount; i++)
            {
                Console.Write('\u01c1');

                for (int j = 0; j < columnCount; j++)
                {
                    Console.Write(board[i, j]);
                }

                Console.WriteLine('\u01c1');
            }

            Console.WriteLine(frame);

            Console.Write($"Здоровье = {(cellObjectList[0] as Player).Life} ");
            Console.Write($"Монеты = {(cellObjectList[0] as Player).Coins} Бонусы = ");
            foreach (Bonus item in (cellObjectList[0] as Player).bonusList)
            {
                Console.Write(item.GetChar());
            }
            Console.WriteLine();
        }

        static void InitBoard()
        {
            Console.Write("Введите ширину игрового поля: ");
            int width = Int32.Parse(Console.ReadLine());
            Console.Write("Введите высоту игрового поля: ");
            int height = Int32.Parse(Console.ReadLine());

            board = new char[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    board[i, j] = ' ';
                }
            } 
        }

        static void FillBoard()
        {
            foreach (CellObject item in cellObjectList)
            {
                board[item.Y, item.X] = item.GetChar();
            }
        }

        static void GenerateCellObjects()
        {
            int rowCount = board.GetLength(0);
            int columnCount = board.GetLength(1);

            int cellNumber = rowCount * columnCount;

            int moneyNumber = cellNumber / 10;
            if (moneyNumber == 0)
            {
                moneyNumber = 1;
            }

            int obstacleNumber = cellNumber / 10;
            if (obstacleNumber == 0)
            {
                obstacleNumber = 1;
            }

            int bonusNumber = cellNumber / 50;
            if (bonusNumber == 0)
            {
                bonusNumber = 1;
            }

            int npcNumber = cellNumber / 100;
            if (npcNumber == 0)
            {
                npcNumber = 1;
            }

            Generate("player", 1, 1, 1);
            Generate("obstacle", obstacleNumber, rowCount, columnCount);
            Generate("money", moneyNumber, rowCount, columnCount);
            Generate("bonus", bonusNumber, rowCount, columnCount);
            Generate("npc", npcNumber, rowCount, columnCount);
        }

        static void Generate(string obj, int number, int rows, int columns)
        { 
            int x, y;
            bool isEmpty;
            CellBuilder builder = new CellBuilder();
            CellType type;

            int count = 0;
            while (count < number)
            {
                x = rnd.Next(0, columns);
                y = rnd.Next(0, rows);

                isEmpty = true;
                foreach (CellObject item in cellObjectList)
                {
                    if ((x == item.X) && (y == item.Y))
                    {
                        isEmpty = false;
                        break;
                    }
                }

                if (isEmpty)
                {
                    type = obj switch
                    {
                        "obstacle" => (CellType)rnd.Next((int)CellType.Stone, (int)CellType.Tree + 1),
                        "bonus" => (CellType)rnd.Next((int)CellType.Cycle, (int)CellType.Health + 1),
                        "npc" => (CellType)rnd.Next((int)CellType.Bear, (int)CellType.Wolf + 1),
                        "money" => CellType.Money,
                        "player" => CellType.Player,
                        _ => CellType.None
                    };

                    cellObjectList.Add(builder.CreateCellObject(x, y, type));

                    count++;
                }
            }
        }
    }

    public enum CellType
    {
        None,
        Stone,
        Tree,
        Cycle,
        Jump,
        Ax,
        Fire,
        Grenade,
        Health,
        Money,
        Bear,
        Wolf,
        Player
    }

    interface IDisplayable
    {
        char GetChar();
    }

    interface IMovable
    {
        void Move();
    }

    class CellBuilder
    {
        public CellObject CreateCellObject(int x, int y, CellType type)
        {
            return type switch
            {
                CellType.Stone => new Stone(x, y),
                CellType.Tree => new Tree(x, y),
                CellType.Cycle => new Cycle(x, y),
                CellType.Jump => new Jump(x, y),
                CellType.Ax => new Ax(x, y),
                CellType.Fire => new Fire(x, y),
                CellType.Grenade => new Grenade(x, y),
                CellType.Money => new Money(x, y),
                CellType.Health => new Health(x, y),
                CellType.Bear => new Bear(x, y),
                CellType.Wolf => new Wolf(x, y),
                CellType.Player => new Player(x, y),
                _ => null
            };
        }
    }

    abstract class CellObject : IDisplayable
    {
        public int X { get; }
        public int Y { get; }

        public CellObject(int x, int y)
        {
            X = x;
            Y = y;
        }

        public abstract char GetChar();
    }

    abstract class Bonus : CellObject
    {
        public Bonus(int x, int y)
            : base(x, y)
        { }
    }

    abstract class NPC : CellObject, IMovable
    {
        public int Harm { get; }
        public int Speed { get; }

        public NPC(int x, int y, int harm, int speed)
            : base (x, y)
        {
            Harm = harm;
            Speed = speed;
        }

        public abstract void Move();
    }

    class Stone : CellObject
    {
        public Stone(int x, int y)
            : base(x, y)
        { }

        public override char GetChar() => 'S';
    }

    class Tree : CellObject
    {
        public Tree(int x, int y)
            : base(x, y)
        { }

        public override char GetChar() => 'T';
    }

    class Cycle : CellObject
    {
        public Cycle(int x, int y)
            : base(x, y)
        { }

        public override char GetChar() => 'C';
    }

    class Jump : CellObject
    {
        public Jump(int x, int y)
            : base(x, y)
        { }

        public override char GetChar() => 'J';
    }

    class Ax : CellObject
    {
        public Ax(int x, int y)
            : base(x, y)
        { }

        public override char GetChar() => 'X';
    }

    class Fire : CellObject
    {
        public Fire(int x, int y)
            : base(x, y)
        { }

        public override char GetChar() => 'F';
    }

    class Grenade : CellObject
    {
        public Grenade(int x, int y)
            : base(x, y)
        { }

        public override char GetChar() => 'G';
    }

    class Money : CellObject
    {
        public Money(int x, int y)
            : base(x, y)
        { }

        public override char GetChar() => '*';
    }

    class Health : CellObject
    {
        public Health(int x, int y)
            : base(x, y)
        { }

        public override char GetChar() => 'H';
    }

    class Bear : NPC
    {
        public Bear(int x, int y)
            : base(x, y, 2, 1)
        { }

        public override char GetChar() => 'B';
        
        public override void Move()
        {

        }
    }

    class Wolf : NPC
    {
        public Wolf(int x, int y)
            : base(x, y, 2, 1)
        { }

        public override char GetChar() => 'W';

        override public void Move()
        {

        }
    }

    class Player : CellObject, IMovable
    {
        public int Speed { get; set; }

        public int Life { get; set; }

        public int Coins { get; set; }

        public List<Bonus> bonusList;

        public Player(int x, int y)
            : base(x, y)
        {
            Speed = 1;
            Life = 3;
            Coins = 0;

            bonusList = new List<Bonus>();
        }

        public override char GetChar() => '@';

        public void Move()
        {

        }
    }
}
