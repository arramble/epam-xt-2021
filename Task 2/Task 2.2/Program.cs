using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Task2_2
{
    enum CellType
    {
        Empty,
        Stone,
        Tree,
        Freeze,
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

    enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    interface IDisplayable
    {
        char GetChar();
    }

    interface IMovable
    {
        void Move(Direction dir, Board board, List<CellObject> objectList);
    }

    class Program
    {
        protected static int origRow;
        protected static int origCol;

        static readonly Random rnd = new();

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            InputSize(out int width, out int height);
            int difficulty = InputDifficulty();

            Board board = new (width, height);
            List<CellObject> cellObjectList = GenerateCellObjects(board, difficulty);
            Player player = cellObjectList[0] as Player;

            board.Update(cellObjectList);
            board.Display();

            DisplayDescription();

            Console.WriteLine("Нажмите любую клавишу, чтобы начать...");
            Console.ReadKey();
            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            while (NextMove(player, board, cellObjectList))
            { };
        }

        static void DisplayDescription()
        {
            Console.WriteLine();
            Console.WriteLine("Описание игрового поля");
            Console.WriteLine("@ - игрок");
            Console.WriteLine("Перемещается по игровому полю со скоростью 1, цель собрать все монетки * и не быть съеденным врагами");
            Console.WriteLine("Перемещение по полю осуществляется с помощью клавиш W/A/S/D");
            Console.WriteLine();
            Console.WriteLine("B - враг Медведь, скорость 1, урон 2");
            Console.WriteLine("W - враг Волк, скорость 2, урон 1");
            Console.WriteLine("После встречи с врагом у игрока отнимается здоровье равное урону от врага, а сам игрок переносится в левый верхний угол доски");
            Console.WriteLine("Бонусы, в том числе монетки, являются препятствием для врагов");
            Console.WriteLine();
            Console.WriteLine("S - препятствие Камень, игрок может перепрыгнуть через камень или врага используя бонус Прыжок (J)");
            Console.WriteLine("T - препятствие Дерево, игрок может срубить дерево используя бонус Топор (X)");
            Console.WriteLine();
            Console.WriteLine("J - бонус Прыжок, игрок может совершить прыжок на 2 клетки в любом направлении при условии что там нет препятствия");
            Console.WriteLine("Игрок может перепрыгнуть через камень, бонус или врага, но не может через дерево");
            Console.WriteLine("Сочетание клавиш для прыжка Shift+W/A/S/D, бонус при использовании расходуется");
            Console.WriteLine("X - бонус Топор, игрок может срубить дерево либо убить врага на соседней клетке, после чего переходит на эту клетку");
            Console.WriteLine("Сочетание клавиш для топора Alt+W/A/S/D, бонус при использовании расходуется");
            Console.WriteLine("F - бонус Лук, игрок может выстрелить и убить врага в любом направлении с радиусом 2 при условии что между ними нет препятствия или бонуса");
            Console.WriteLine("Сочетание клавиш для лука Ctrl+W/A/S/D, бонус при использовании расходуется");
            Console.WriteLine("G - бонус Граната, игрок может взорвать ее и убить всех врагов в примыкающих клетках, в том числе по диагонали");
            Console.WriteLine("Клавиша для взрыва гранаты G, бонус при использовании расходуется");
            Console.WriteLine("Z - бонус Заморозка, враги останавливаются на 10 ходов");
            Console.WriteLine("H - бонус Здоровье, игрок восстанавливает 1 единицу здоровья");
            Console.WriteLine();
        }

        static void InputSize(out int width, out int height)
        {
            do
            {
                Console.Write("Введите ширину игрового поля: ");
                Int32.TryParse(Console.ReadLine(), out width);
            }
            while (!(width > 0));

            do
            {
                Console.Write("Введите высоту игрового поля: ");
                Int32.TryParse(Console.ReadLine(), out height);
            }
            while (!(height > 0));
        }

        static int InputDifficulty()
        {
            int diff;

            do
            {
                Console.Write("Введите уровень сложности от 1 до 10: ");
                Int32.TryParse(Console.ReadLine(), out diff);
            }
            while ((diff < 1) || (diff > 10));

            return diff;
        }

        static List<CellObject> GenerateCellObjects(Board board, int difficulty)
        {
            List<CellObject> objectList = new ();

            int cellNumber = board.Rows * board.Columns;
            int moneyNumber = (cellNumber + 9) / 10;
            int obstacleNumber = (cellNumber + 9) / 10;
            int bonusNumber = (cellNumber + 49) / 50;
            int npcNumber = (cellNumber + 109 - difficulty * 10) / (110 - difficulty * 10);

            Generate(objectList, "player", 1, 1, 1);
            Generate(objectList, "obstacle", obstacleNumber, board.Rows, board.Columns);
            Generate(objectList, "money", moneyNumber, board.Rows, board.Columns);
            Generate(objectList, "bonus", bonusNumber, board.Rows, board.Columns);
            Generate(objectList, "npc", npcNumber, board.Rows, board.Columns);

            return objectList;
        }

        static void Generate(List<CellObject> objectList, string obj, int number, int rows, int columns)
        { 
            int x, y;
            CellType type;
            CellBuilder builder = new ();

            for (int i = 0; i < number; i++)
            {
                x = rnd.Next(columns);
                y = rnd.Next(rows);

                if (IsEmpty(objectList, x, y))
                {
                    type = obj switch
                    {
                        "obstacle" => (CellType)rnd.Next((int)CellType.Stone, (int)CellType.Tree + 1),
                        "bonus" => (CellType)rnd.Next((int)CellType.Freeze, (int)CellType.Health + 1),
                        "npc" => (CellType)rnd.Next((int)CellType.Bear, (int)CellType.Wolf + 1),
                        "money" => CellType.Money,
                        "player" => CellType.Player,
                        _ => CellType.Empty
                    };

                    objectList.Add(builder.CreateCellObject(x, y, type));
                }
            }
        }

        static bool IsEmpty(List<CellObject> objectList, int x, int y)
        {
            foreach (CellObject item in objectList)
            {
                if ((x == item.X) && (y == item.Y))
                {
                    return false;
                }
            }

            return true;
        }

        static bool NextMove(Player player, Board board, List<CellObject> objectList)
        {
            Console.SetCursorPosition(origCol, origRow);
            board.Update(objectList);
            board.Display();
            player.Status();

            if (player.Life == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Вас съели! Нажмите любую клавишу, чтобы выйти из игры" + new string(' ', 20));
                Console.ReadKey();
                return false;
            }
            else if (!objectList.Any(obj => obj.GetChar() == '*'))
            {
                Console.WriteLine();
                Console.WriteLine("Вы собрали все монеты и выиграли! Нажмите любую клавишу, чтобы выйти из игры" + new string(' ', 20));
                Console.ReadKey();
                return false;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Сделайте следующий ход / Нажмите q чтобы закончить игру / Нажмите F1 для вывода справки" + new string(' ', 20));
                ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                switch (keyPressed.Key)
                {
                    case ConsoleKey.Q:
                        return false;
                    case ConsoleKey.F1:
                        DisplayDescription();
                        Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
                        Console.ReadKey();
                        Console.Clear();
                        return true;
                    case ConsoleKey.W:
                        goto case ConsoleKey.G;
                    case ConsoleKey.A:
                        goto case ConsoleKey.G;
                    case ConsoleKey.S:
                        goto case ConsoleKey.G;
                    case ConsoleKey.D:
                        goto case ConsoleKey.G;
                    case ConsoleKey.G:
                        player.HandleKey(keyPressed, board, objectList);
                        board.Update(objectList);
                        if (player.FreezeTime == 0)
                        {
                            NPC.MoveNPC(board, objectList);
                        }
                        else
                        {
                            player.FreezeTime--;
                        }
                        return true;
                    default:
                        return true; 
                }
            }
        }
    }

    class Board
    {
        private char[,] cells;

        public int Rows { get; }

        public int Columns { get; }

        public char this[int index1, int index2]
        {
            get
            {
                return cells[index1, index2];
            }
            set
            {
                cells[index1, index2] = value;
            }
        }

        public Board(int width, int height)
        {
            Rows = height;
            Columns = width;

            cells = new char[height, width];
        }

        public void Display()
        {
            string frame = new string('═', Columns);
            Console.WriteLine('╔' + frame + '╗');

            for (int i = 0; i < Rows; i++)
            {
                Console.Write('║');

                for (int j = 0; j < Columns; j++)
                {
                    DisplayCell(cells[i, j]);
                }

                Console.ResetColor();
                Console.WriteLine('║');
            }

            Console.WriteLine('╚' + frame + '╝');
        }

        private static void DisplayCell(char c)
        {
            ConsoleColor color = c switch
            {
                'S' => ConsoleColor.DarkGray,
                'T' => ConsoleColor.DarkGreen,
                'Z' => ConsoleColor.DarkBlue,
                'J' => ConsoleColor.Blue,
                'X' => ConsoleColor.DarkCyan,
                'F' => ConsoleColor.Magenta,
                'G' => ConsoleColor.DarkMagenta,
                '*' => ConsoleColor.Yellow,
                'H' => ConsoleColor.Cyan,
                'B' => ConsoleColor.DarkRed,
                'W' => ConsoleColor.Red,
                '@' => ConsoleColor.White,
                ' ' => ConsoleColor.White,
                _ => ConsoleColor.White
            };

            Console.ForegroundColor = color;
            Console.Write(c);
        }

        public void Update(List<CellObject> objectList)
        {
            Clear();
            Fill(objectList);
        }

        private void Clear()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    cells[i, j] = ' ';
                }
            }
        }

        private void Fill(List<CellObject> objectList)
        {
            foreach (CellObject item in objectList)
            {
                cells[item.Y, item.X] = item.GetChar();
            }
        }
    }

    class CellBuilder
    {
        public CellObject CreateCellObject(int x, int y, CellType type)
        {
            return type switch
            {
                CellType.Stone => new Stone(x, y),
                CellType.Tree => new Tree(x, y),
                CellType.Freeze => new Freeze(x, y),
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
        public int X { get; set; }
        public int Y { get; set; }

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
        static readonly Random rnd = new();

        public int Harm { get; }
        public int Speed { get; }

        public Direction Orient { get; set; }

        public NPC(int x, int y, int harm, int speed)
            : base (x, y)
        {
            Harm = harm;
            Speed = speed;
            Orient = (Direction)rnd.Next((int)Direction.Up, (int)Direction.Right + 1);
        }

        public static void MoveNPC(Board board, List<CellObject> objectList)
        {
            NPC npc;

            foreach (CellObject item in objectList.Where(obj => obj is NPC))
            {
                npc = item as NPC;

                npc.Orient = SelectDirection(npc, board);

                if (npc.Orient != Direction.None)
                {
                    npc.Move(npc.Orient, board, objectList);
                }
            }
        }

        private static Direction SelectDirection(NPC npc, Board board)
        {
            List<Direction> directionList = new();

            if (npc.X != 0)
            {
                if (board[npc.Y, npc.X - 1] == '@')
                {
                    return Direction.Left;              //Attack a nearby player
                }
                else if (board[npc.Y, npc.X - 1] == ' ')
                {
                    directionList.Add(Direction.Left);
                }
            }

            if (npc.X != board.Columns - 1)
            {
                if (board[npc.Y, npc.X + 1] == '@')
                {
                    return Direction.Right;             //Attack a nearby player
                }
                else if (board[npc.Y, npc.X + 1] == ' ')
                {
                    directionList.Add(Direction.Right);
                }
            }

            if (npc.Y != 0)
            {
                if (board[npc.Y - 1, npc.X] == '@')
                {
                    return Direction.Up;                //Attack a nearby player
                }
                else if (board[npc.Y - 1, npc.X] == ' ')
                {
                    directionList.Add(Direction.Up);
                }
            }

            if (npc.Y != board.Rows - 1)
            {
                if (board[npc.Y + 1, npc.X] == '@')
                {
                    return Direction.Down;              //Attack a nearby player
                }
                else if (board[npc.Y + 1, npc.X] == ' ')
                {
                    directionList.Add(Direction.Down);
                }
            }

            if (directionList.Contains(npc.Orient))
            {
                directionList.Add(npc.Orient);      //Increased possibility to keep the direction
            }

            if (directionList.Count != 0)
            {
                return directionList[rnd.Next(directionList.Count)];
            }
            else
            {
                return Direction.None;
            }
        }

        public void Move(Direction dir, Board board, List<CellObject> objectList)
        {
            for (int i = 0; i < Speed; i++)
            {
                switch (dir)
                {
                    case Direction.Left:
                        if (X > 0)
                        {
                            MoveTo(board, objectList, X - 1, Y);
                        }
                        break;
                    case Direction.Up:
                        if (Y > 0)
                        {
                            MoveTo(board, objectList, X, Y - 1);
                        }
                        break;
                    case Direction.Right:
                        if (X < board.Columns - 1)
                        {
                            MoveTo(board, objectList, X + 1, Y);
                        }
                        break;
                    case Direction.Down:
                        if (Y < board.Rows - 1)
                        {
                            MoveTo(board, objectList, X, Y + 1);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void MoveTo(Board board, List<CellObject> objectList, int x, int y)
        {
            switch (board[y, x])
            {
                case '@':
                    if (objectList.FindIndex(obj => (obj.X == 0) && (obj.Y == 0) && (obj is NPC)) != -1)
                    {
                        objectList.Remove(objectList.Single(obj => (obj.X == 0) && (obj.Y == 0)));
                    }
                    Player player = objectList[0] as Player;
                    player.X = 0;
                    player.Y = 0;
                    player.Life -= Harm;
                    if (player.Life < 0)
                    {
                        player.Life = 0;
                    }
                    goto case ' ';
                case ' ':
                    X = x;
                    Y = y;
                    break;
                default:
                    break;
            }
        }
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

    class Freeze : Bonus
    {
        public Freeze(int x, int y)
            : base(x, y)
        { }

        public override char GetChar() => 'Z';
    }

    class Jump : Bonus
    {
        public Jump(int x, int y)
            : base(x, y)
        { }

        public override char GetChar() => 'J';
    }

    class Ax : Bonus
    {
        public Ax(int x, int y)
            : base(x, y)
        { }

        public override char GetChar() => 'X';
    }

    class Fire : Bonus
    {
        public Fire(int x, int y)
            : base(x, y)
        { }

        public override char GetChar() => 'F';
    }

    class Grenade : Bonus
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
    }

    class Wolf : NPC
    {
        public Wolf(int x, int y)
            : base(x, y, 1, 2)
        { }

        public override char GetChar() => 'W';
    }

    class Player : CellObject, IMovable
    {
        public int FreezeTime { get; set; }

        public int Life { get; set; }

        public int Coins { get; set; }

        public List<Bonus> bonusList;

        public Player(int x, int y)
            : base(x, y)
        {
            FreezeTime = 0;
            Life = 5;
            Coins = 0;

            bonusList = new List<Bonus>();
        }

        public override char GetChar() => '@';

        public void Status()
        {
            Console.Write($"Здоровье = {Life} ");
            Console.Write($"Монеты = {Coins} ");
            Console.Write($"Заморозка = {FreezeTime} ");
            Console.Write($"Бонусы = ");
            foreach (Bonus item in bonusList)
            {
                Console.Write(item.GetChar());
            }
            Console.WriteLine(new string(' ', 20));
        }

        public void HandleKey(ConsoleKeyInfo keyPressed, Board board, List<CellObject> objectList)
        {
            if (keyPressed.Key == ConsoleKey.G)
            {
                Detonate(objectList);
            }
            else
            {
                Direction dir = keyPressed.Key switch
                {
                    ConsoleKey.A => Direction.Left,
                    ConsoleKey.D => Direction.Right,
                    ConsoleKey.W => Direction.Up,
                    ConsoleKey.S => Direction.Down,
                    _ => Direction.None
                };

                if ((ConsoleModifiers.Control & keyPressed.Modifiers) != 0)
                {
                    if (bonusList.Any(obj => obj is Fire))
                    {
                        Shoot(dir, board, objectList);
                        bonusList.Remove(bonusList.Find(obj => obj is Fire));
                    }
                }
                else if ((ConsoleModifiers.Shift & keyPressed.Modifiers) != 0)
                {
                    if (bonusList.Any(obj => obj is Jump))
                    {
                        Hop(dir, board, objectList);
                        bonusList.Remove(bonusList.Find(obj => obj is Jump));
                    } 
                }
                else if ((ConsoleModifiers.Alt & keyPressed.Modifiers) != 0)
                {
                    if (bonusList.Any(obj => obj is Ax))
                    {
                        Chop(dir, board, objectList);
                        bonusList.Remove(bonusList.Find(obj => obj is Ax));
                    }
                }
                else
                {
                    Move(dir, board, objectList);
                }
            }
        }

        private void Shoot(Direction dir, Board board, List<CellObject> objectList)
        {
            switch (dir)
            {
                case Direction.Left:
                    if (X > 0)
                    {
                        if (board[Y, X - 1] == ' ')
                        {
                            KillNPC(objectList, X - 2, Y);
                        }
                        else
                        {
                            KillNPC(objectList, X - 1, Y);
                        }
                    }
                    break;
                case Direction.Up:
                    if (Y > 0)
                    {
                        if (board[Y - 1, X] == ' ')
                        {
                            KillNPC(objectList, X, Y - 2);
                        }
                        else
                        {
                            KillNPC(objectList, X, Y - 1);
                        }
                    }
                    break;
                case Direction.Right:
                    if (X < board.Columns - 1)
                    {
                        if (board[Y, X + 1] == ' ')
                        {
                            KillNPC(objectList, X + 2, Y);
                        }
                        else
                        {
                            KillNPC(objectList, X + 1, Y);
                        }
                    }
                    break;
                case Direction.Down:
                    if (Y < board.Rows - 1)
                    {
                        if (board[Y + 1, X] == ' ')
                        {
                            KillNPC(objectList, X, Y + 2);
                        }
                        else
                        {
                            KillNPC(objectList, X, Y + 1);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void Hop(Direction dir, Board board, List<CellObject> objectList)
        {
            switch (dir)
            {
                case Direction.Left:
                    if ((X > 1) && (board[Y, X - 1] != 'T'))
                    {
                        MoveTo(board, objectList, X - 2, Y);
                    }
                    break;
                case Direction.Up:
                    if ((Y > 1) && (board[Y - 1, X] != 'T'))
                    {
                        MoveTo(board, objectList, X, Y - 2);
                    }
                    break;
                case Direction.Right:
                    if ((X < board.Columns - 2) && (board[Y, X + 1] != 'T'))
                    {
                        MoveTo(board, objectList, X + 2, Y);
                    }
                    break;
                case Direction.Down:
                    if ((Y < board.Rows - 2) && (board[Y + 1, X] != 'T'))
                    {
                        MoveTo(board, objectList, X, Y + 2);
                    }
                    break;
                default:
                    break;
            }
        }

        private void Chop(Direction dir, Board board, List<CellObject> objectList)
        {
            switch (dir)
            {
                case Direction.Left:
                    if (X > 0)
                    {
                        ChopTo(board, objectList, X - 1, Y);
                    }
                    break;
                case Direction.Up:
                    if (Y > 0)
                    {
                        ChopTo(board, objectList, X, Y - 1);
                    }
                    break;
                case Direction.Right:
                    if (X < board.Columns - 1)
                    {
                        ChopTo(board, objectList, X + 1, Y);
                    }
                    break;
                case Direction.Down:
                    if (Y < board.Rows - 1)
                    {
                        ChopTo(board, objectList, X, Y + 1);
                    }
                    break;
                default:
                    break;
            }
        }

        private void ChopTo(Board board, List<CellObject> objectList, int x, int y)
        {
            if (objectList.FindIndex(obj => (obj.X == x) && (obj.Y == y) && ((obj is NPC) || (obj is Tree))) != -1)
            {
                ObjectRemove(objectList, x, y);
                board.Update(objectList);
                MoveTo(board, objectList, x, y);
            }
            else if (board[y, x] == ' ')
            {
                MoveTo(board, objectList, x, y);
            }
        }

        public void Move(Direction dir, Board board, List<CellObject> objectList)
        {
            switch (dir)
            {
                case Direction.Left:
                    if (X > 0)
                    {
                        MoveTo(board, objectList, X - 1, Y);
                    }
                    break;
                case Direction.Up:
                    if (Y > 0)
                    {
                        MoveTo(board, objectList, X, Y - 1);
                    }
                    break;
                case Direction.Right:
                    if (X < board.Columns - 1)
                    {
                        MoveTo(board, objectList, X + 1, Y);
                    }
                    break;
                case Direction.Down:
                    if (Y < board.Rows - 1)
                    {
                        MoveTo(board, objectList, X, Y + 1);
                    }
                    break;
                default:
                    break;
            }
        }

        private void MoveTo(Board board, List<CellObject> objectList, int x, int y)
        {
            switch (board[y, x])
            {
                case '*':
                    Coins++;
                    ObjectRemove(objectList, x, y);
                    goto case ' ';
                case 'H':
                    Life++;
                    ObjectRemove(objectList, x, y);
                    goto case ' ';
                case 'Z':
                    FreezeTime += 10;
                    ObjectRemove(objectList, x, y);
                    goto case ' ';
                case 'J':
                    bonusList.Add(new Jump(x, y));
                    ObjectRemove(objectList, x, y);
                    goto case ' ';
                case 'X':
                    bonusList.Add(new Ax(x, y));
                    ObjectRemove(objectList, x, y);
                    goto case ' ';
                case 'F':
                    bonusList.Add(new Fire(x, y));
                    ObjectRemove(objectList, x, y);
                    goto case ' ';
                case 'G':
                    bonusList.Add(new Grenade(x, y));
                    ObjectRemove(objectList, x, y);
                    goto case ' ';
                case 'B':
                    Life -= (objectList.Single(obj => (obj.X == x) && (obj.Y == y)) as Bear).Harm;
                    if (Life < 0)
                    {
                        Life = 0;
                    }
                    KillNPC(objectList, 0, 0);
                    X = 0;
                    Y = 0;
                    break;
                case 'W':
                    Life -= (objectList.Single(obj => (obj.X == x) && (obj.Y == y)) as Wolf).Harm;
                    if (Life < 0)
                    {
                        Life = 0;
                    }
                    KillNPC(objectList, 0, 0);
                    X = 0;
                    Y = 0;
                    break;
                case ' ':
                    X = x;
                    Y = y;
                    break;
                default:
                    break;
            }
        }

        private static void ObjectRemove(List<CellObject> objectList, int x, int y)
        {
            objectList.Remove(objectList.Single(obj => (obj.X == x) && (obj.Y == y)));
        }

        private void Detonate(List<CellObject> objectList)
        {
            KillNPC(objectList, X + 1, Y);
            KillNPC(objectList, X + 1, Y + 1);
            KillNPC(objectList, X, Y + 1);
            KillNPC(objectList, X - 1, Y + 1);
            KillNPC(objectList, X - 1, Y);
            KillNPC(objectList, X - 1, Y - 1);
            KillNPC(objectList, X, Y - 1);
            KillNPC(objectList, X + 1, Y - 1);

            bonusList.Remove(bonusList.Find(obj => obj is Grenade));
        }

        private void KillNPC(List<CellObject> objectList, int x, int y)
        {
            if (objectList.FindIndex(obj => (obj.X == x) && (obj.Y == y) && (obj is NPC)) != -1)
            {
                ObjectRemove(objectList, x, y);
            }
        }
    }
}
