
using System.Collections.Generic;
using System.IO;

namespace MagicGetPrice
{
    public class ReadCSVFile
    {
        private string filePath;
        private List<string> read = new List<string>();
        public List<MTGCard> list = new List<MTGCard>();

        public ReadCSVFile(string filePath)
        {
            this.filePath = filePath;
        }

        public void ReadFile()
        {
            using (StreamReader stream = new StreamReader(filePath))
            {
                string line;
                
                while ((line = stream.ReadLine()) != null)
                {
                    read.Add(line);
                }
                for (int i = 1; i < read.Count; i++)
                {
                    var coll = read[i].Split(',');
                    coll[0] = coll[0].Replace("\"", "");
                    coll[0] = coll[0].Replace("'", "");
                    if (read[i] != "")
                        list.Add(new MTGCard(coll[0], coll[4], int.Parse(coll[1])));
                }
            }
        }
    }
}
