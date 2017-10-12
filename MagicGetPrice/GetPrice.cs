using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MagicGetPrice
{
    public class GetPrice
    {
        private string _httpPage;

        public void GatherPrice(string cardName, string setName)
        {

            string card = cardName.Replace(' ', '+');
            card = card.Replace("\'","");
            card = card.Replace(",","");
            string set = setName.Replace(' ', '+');
            // ... Target page.
            string page = $"https://www.mtggoldfish.com/price/{set}/{card}#online";

            // ... Use HttpClient.
            
                using (WebClient client = new WebClient())
                {
                    // ... Read the string.
                    string result = client.DownloadString(page);

                    // ... Display the result.
                    if (result != null &&
                        result.Length >= 50)
                    {
                        _httpPage = result;
                    }
                    else
                    {
                        _httpPage = "";
                    }
                }
            
            
            
                
        }
        public double ReadPrice()
        {
            string http = _httpPage.Substring(_httpPage.IndexOf("price-box-price")+17,40);
            string[] split = http.Split('<');
            return double.Parse(split[0], System.Globalization.CultureInfo.InvariantCulture);
        }
        public static void AddCard(string cardName, string setCode, List<MTGCard> list)
        {
            if (list.Where(x => x.Set == setCode || x.Name == cardName).Count() == 0)
            {
                list.Add(new MTGCard(cardName, setCode));
            }

        }
    }
}
