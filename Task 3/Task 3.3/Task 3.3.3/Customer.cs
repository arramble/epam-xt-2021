using System;
using System.Collections.Generic;

namespace Task3_3_3
{
    public class Customer
    {
        public string Name { get; }

        public Customer(string name)
        {
            Name = name;
        }

        public List<Position> CheckPositions()
        {
            List<Position> positions = new List<Position>();

            switch (Name)
            {
                case "Александр":
                    positions.Add(new Position("Пицца Маргарита", 349m, 1));
                    positions.Add(new Position("Соус", 30m, 2));
                    positions.Add(new Position("Латте", 120m, 1));
                    break;
                case "Сергей":
                    positions.Add(new Position("Пончик малиновый", 80m, 3));
                    positions.Add(new Position("Американо", 99m, 1));
                    break;
                case "Дмитрий":
                    positions.Add(new Position("Пицца 4 сыра", 420m, 1));
                    positions.Add(new Position("Соус", 30m, 2));
                    positions.Add(new Position("Капучино ", 150m, 1));
                    break;
            default:
                    break;
            }

            return positions;
        }

        public void Subscribe(Order order)
        {
            order.OnReady += GetOrder;
        }

        private void GetOrder(Order order)
        {
            Console.WriteLine($"{Name} забрал заказ {order.Number}");
            Console.WriteLine();

            order.OnReady -= GetOrder;
        }
    }
}
