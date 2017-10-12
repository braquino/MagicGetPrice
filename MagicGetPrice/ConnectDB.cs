using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace MagicGetPrice
{
    public class ConnectDB
    {
        private string _connectionString = @"data source=C:\Users\br_aq\source\repos\MTGOCardPrice\DBMP.db";
        SQLiteConnection connection;

        public ConnectDB()
        {
            connection = new SQLiteConnection(_connectionString);
        }
        public void GetSetData(List<MTGSet> list)
        {
            var table = Consult("select * from sets");
            foreach (DataRow row in table.Rows)
                list.Add(new MTGSet(row[0].ToString(), row[1].ToString()));
        }
        public void GetCardsData(List<MTGCard> list)
        {
            var table = Consult("select * from cards");
            foreach (DataRow row in table.Rows)
                list.Add(new MTGCard(row[0].ToString(), row[1].ToString(), int.Parse(row[2].ToString())));
        }
        public void SaveSet(List<MTGSet> list)
        {
            var table = Consult("select * from sets");
            foreach (MTGSet set in list)
            {
                if (table.Select($"set_code = '{set.Code}'").Count() == 0)
                {
                    var command = new SQLiteCommand($@"INSERT INTO [sets]
                                                           ([set_code]
                                                           ,[set_name])
                                                     VALUES
                                                           ('{set.Code}'
                                                           ,'{set.Name}'); ", connection);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        public void SaveCard(List<MTGCard> list)
        {
            Consult("delete from cards");
            foreach (MTGCard card in list)
            {
                var command = new SQLiteCommand($@"INSERT INTO [cards]
                                                    ([card_name]
                                                    ,[set_code]
                                                    ,[qty]
                                                    ,[price])
                                                VALUES
                                                    ('{card.Name}'
                                                    ,'{card.Set}'
                                                    ,{card.qty}
                                                    ,{card.Price}); ", connection);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                
            }
        }
        public void PopulateSets(List<MTGSet> list)
        {
            var table = Consult("select set_code from cards group by set_code");
            foreach (DataRow row in table.Rows)
            {
                GetSet.AddSet(row[0].ToString(), list);
            }
        }
        public DataTable Consult(string command)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command, connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        
    }
}
