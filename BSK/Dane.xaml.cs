using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
namespace BSK
{
    /// <summary>
    /// Interaction logic for Dane.xaml
    /// </summary>
    /// 

        //proste rozwiazanie: stworzyc tyle klas co tabel w sql i dodawac ladnie

    public class MyItem
    {
        public int Id { get; set; }
        public string Gatunek { get; set; }
    }

    public class Zwierze
    {
        public string Gatunek { get; set; }
        public string Rasa { get; set; }
        public string Imie { get; set; }
        public string Wlasciciel { get; set; }
    }
    public partial class Dane : Window
    {
        public Dane()
        {
            InitializeComponent();

            //TUTAJ TRZEBA WSADZIC DODAWANIE WSZYSTKICH TABEL Z BAZY DANYCH
            comboBox.Items.Add("Zwierze");
            comboBox.Items.Add("Klient");

            // TUTAJ WSADŹ ZAPYTANIE DO BAZY DANYCH
            //LISTA COLUMNS TO MUSZA BYC KOLUMNY Z BAZY DANYCH I BEDZIE DZIALAC
            List<string> columns =new List<string>();
        
            columns.Add("Gatunek");
            columns.Add("Id");
            foreach (string text in columns)
            {
                // now set up a column and binding for each property
                var column = new DataGridTextColumn
                {
                    Header = text,
                    Binding = new Binding(text)
                };

                dataGrid1.Columns.Add(column);
               
            }
            dataGrid1.Items.Add(new MyItem { Id = 1, Gatunek = "piesel" });
           // dataGrid1.Items.Add()
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Window1 win2 = new Window1();
            win2.Show();
            this.Close();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
