using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MagicGetPrice
{

    public partial class MainWindow : Window
    {
        GetSet set = new GetSet();
        List<MTGCard> Cards = new List<MTGCard>();
        List<MTGSet> Sets = new List<MTGSet>();
        ConnectDB connect = new ConnectDB();

        public MainWindow()
        {
            
            InitializeComponent();
            
            connect.GetSetData(Sets);

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var task = Task.Run(() => set.GatherPrice("9ED")).ContinueWith(i => TextSet.Text = set.SetGhatered, 
            //   TaskScheduler.FromCurrentSynchronizationContext());


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GetSet.AddSet(TextCode.Text, Sets);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            connect.SaveSet(Sets);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fbd = new OpenFileDialog();
            fbd.RestoreDirectory = true;
            fbd.Title = "Select a csv exported from Magic Online";
            fbd.Filter = "Magic Online csv file | *.csv";
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReadCSVFile read = new ReadCSVFile(fbd.FileName);
                read.ReadFile();
                connect.SaveCard(read.list);
                connect.PopulateSets(Sets);
                connect.SaveSet(Sets);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Sets.Clear();
            connect.GetSetData(Sets);
            connect.GetCardsData(Cards);
            for (int i = 0; i < Cards.Count; i++)
            {
                GetPrice getPrice = new GetPrice();
                getPrice.GatherPrice(Cards[i].Name, Sets.Single(x => x.Code == Cards[i].Set).Name);
                Cards[i].Price = getPrice.ReadPrice();
            }
            connect.SaveCard(Cards);
        }
    }
}
