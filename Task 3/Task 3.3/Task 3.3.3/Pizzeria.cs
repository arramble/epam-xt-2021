using System;
using System.Collections.Generic;

namespace Task3_3_3
{
    public class Pizzeria
    {
        public List<Order> OrderList { get; }

        public int OrderNumber { get; private set; }

        public Pizzeria()
        {
            OrderList = new List<Order>();
        }

        public void CreateOrder(List<Position> positions, Customer customer)
        {
            Order order = new (++OrderNumber, positions);

            OrderList.Add(order);

            Console.WriteLine($"Заказ {order.Number} для клиента {customer.Name} создан:");
            order.Display();

            customer.Subscribe(order);

            order.OnReady += RemoveOrder;
        }

        private void RemoveOrder(Order order)
        {
            if (OrderList.Contains(order))
            {
                OrderList.Remove(order);
            }

            order.OnReady -= RemoveOrder;
        }
    }
}
