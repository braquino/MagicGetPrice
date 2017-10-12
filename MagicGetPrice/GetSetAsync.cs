using System.Net.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MagicGetPrice
{
    public class GetSetAsync
    {
        private string _httpPage;
        public string SetGhatered { get; private set; }

        public async void GatherPrice(string setCode)
        {

            // ... Target page.
            string page = $"https://www.mtggoldfish.com/index/{setCode}";
            
            // ... Use HttpClient.
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                // ... Read the string.
                string result = await content.ReadAsStringAsync();

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
            string http = _httpPage.Substring(_httpPage.IndexOf(">Price List</a>") +18,20);
            string[] split = http.Split('<');
            return split[0];
        }
    }
}
