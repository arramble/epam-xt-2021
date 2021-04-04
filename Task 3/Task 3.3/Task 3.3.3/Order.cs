using System;
using System.Collections.Generic;

namespace Task3_3_3
{
    public class Order
    {
        public event Action<Order> OnReady = delegate { };

        public int Number { get; }

        public List<Position> Positions { get; }

        public Order(int number, List<Position> positions)
        {
            Number = number;

            Positions = positions;
        }

        public void Display()
        {
            decimal total = 0;

            foreach (Position item in Positions)
            {
                Console.WriteLine($"{item.Name}, {item.Price}р, {item.Qty} шт");
                total += item.Price * item.Qty;
            }

            Console.WriteLine($"Итого {total}р");

            Console.WriteLine();
        }

        public void Ready()
        {
            Console.WriteLine($"Заказ {Number} готов");
            OnReady?.Invoke(this);
        }
    }
}
