using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MagicGetPrice
{
    public class GetSet
    {
        private string _httpPage;
        public string SetGhatered { get; private set; }

        public void GatherPrice(string setCode)
        {

            // ... Target page.
            string page = $"https://www.mtggoldfish.com/index/{setCode}";
            
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
                SetGhatered = ReadSet();
            }
            
                
        }
        public string ReadSet()
        {
            string http = _httpPage.Substring(_httpPage.IndexOf("/price/") +7,50);
            string[] split = http.Split('/');
            return split[0];
        }
        public static void AddSet(string setCode, List<MTGSet> list)
        {
            if (list.Where(x => x.Code == setCode).Count() == 0)
            {
                GetSet getSet = new GetSet();
                getSet.GatherPrice(setCode);
                list.Add(new MTGSet(setCode, getSet.ReadSet()));
            }
            
        }
    }
}
