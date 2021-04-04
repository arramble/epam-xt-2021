using System;
using System.Collections.Generic;

namespace Task3_3_3
{
    class Program
    {
        static void Main()
        {
            Pizzeria pizzeria = new Pizzeria();
            List <Customer> customers = new List<Customer>();

            customers.Add(new Customer("Александр"));
            customers.Add(new Customer("Сергей"));
            customers.Add(new Customer("Дмитрий"));

            TestCreateOrders(pizzeria, customers);
            TestOnReadyEvent(pizzeria);

            Console.ReadKey();
        }

        static void TestCreateOrders(Pizzeria pizzeria, List<Customer> customers)
        {
            List<Position> positions;

            foreach (Customer customer in customers)
            {
                positions = customer.CheckPositions();
                if (positions.Count != 0)
                {
                    pizzeria.CreateOrder(positions, customer);
                }
            }
        }

        static void TestOnReadyEvent(Pizzeria pizzeria)
        {
            Order order;

            while (pizzeria.OrderList.Count > 0)
            {
                int orderNumber = InputOrderNumber();

                order = pizzeria.OrderList.Find(o => o.Number == orderNumber);
                order?.Ready();
            }

            Console.WriteLine("Список заказов пуст");
        }
        
        static int InputOrderNumber()
        {
            int num;

            do
            {
                Console.Write("Нажмите цифру номера заказа, когда он будет готов: ");
                Int32.TryParse(Console.ReadLine(), out num);
            }
            while ((num < 1) || (num > 3));

            return num;
        }
    }
}
