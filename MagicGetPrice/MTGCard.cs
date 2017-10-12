// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MagicGetPrice
{
    public class MTGCard
    {
        public string Name { get; private set; }
        public string Set { get; private set; }
        public int qty { get; set; }
        public double Price { get; set; }

        public MTGCard(string name, string set, int qty = 1)
        {
            this.Name = name;
            this.Set = set;
            this.qty = qty;
            Price = 0;
        }
    }
}
