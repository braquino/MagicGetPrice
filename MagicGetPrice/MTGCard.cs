// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MagicGetPrice
{
    public class MTGCard
    {
        public string Name { get; private set; }
        public string Set { get; private set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Value { get; private set; }

        public MTGCard(string name, string set, int qty = 1, double price = 0)
        {
            this.Name = name;
            this.Set = set;
            this.Qty = qty;
            this.Price = price;
            Calculate();
        }
        public void Calculate()
        {
            Value = Price * Qty;
        }
    }
}
