// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MagicGetPrice
{
    public class MTGSet
    {
        public string Code { get; private set; }
        public string Name { get; set; }

        public MTGSet(string setCode, string setName)
        {
            this.Code = setCode;
            this.Name = setName;
        }
    }
}
