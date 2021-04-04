namespace Task3_3_3
{
    public class Position
    {
        public string Name { get; }
        public decimal Price { get; }
        public int Qty { get; }

        public Position(string name, decimal price, int qty)
        {
            Name = name;
            Price = price;
            Qty = qty;
        }
    }
}
