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
using System.Data;

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
        MySqlConnection connection;
        MySqlDataAdapter adapter;
        DataTable dt;
        public enum Tables
        {
            Zwierze,
            Klient,
            Weterynarz,
            Pracownik,
            Lecznica,
            Usluga,
            Wizyta
        }
        public Dane()
        {

            InitializeComponent();
            populateComboBox();
           

            String connString = "server=localhost;uid=root;pwd=piesnicpon;database=zwierzyniec;";
            connection = new MySqlConnection(connString);

            connection.Open();

            
            adapter = new MySqlDataAdapter("SELECT * FROM Zwierze", connection);
            MySqlCommandBuilder cmb = new MySqlCommandBuilder(adapter);


            dt = new DataTable();
            dt.RowChanged += new DataRowChangeEventHandler(dataTable_RowChanged);
            adapter.Fill(dt);
            dataGrid1.ItemsSource = dt.DefaultView;
            connection.Close();

        }
        private void populateComboBox()
        {
            foreach (Tables table in Enum.GetValues(typeof(Tables)))
            {
                comboBox.Items.Add(table);

            }


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
        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
          //  MessageBox.Show(comboBox.Text);
            //   ComboBox SelectBox = (ComboBox)sender;
           // string text = (sender as ComboBox).SelectedItem as string;
            //   MessageBox.Show("Called SelectionChanged: " + text);
        //    List<string> listOfTables = new List<string>() { "Klient", "Zwierze", };

            // Tables table = SelectBox.Text;
            switch (comboBox.Text)
            {
                case nameof(Tables.Klient):
                     showTable(Tables.Klient);
                    break;
                case nameof(Tables.Zwierze):
                     showTable(Tables.Zwierze);
                    break;
                case nameof(Tables.Lecznica):
                    showTable(Tables.Lecznica);
                    break;
                case nameof(Tables.Pracownik):
                    showTable(Tables.Pracownik);
                    break;
                case nameof(Tables.Usluga):
                    showTable(Tables.Usluga);
                    break;
                case nameof(Tables.Weterynarz):
                    showTable(Tables.Weterynarz);
                    break;
                case nameof(Tables.Wizyta):
                    showTable(Tables.Wizyta);
                    break;
            }
        }
        private void showTable(Tables table)
        {

            string command="SELECT * FROM ";
            switch (table)
            {
                case Tables.Klient:
                    command += "Klient";
                    break;
                case Tables.Zwierze:
                    command += "Zwierze";
                    break;
                case Tables.Lecznica:
                    command += "Lecznica";
                    break;
                case Tables.Pracownik:
                    command += "Pracownik";
                    break;
                case Tables.Usluga:
                    command += "Usluga";
                    break;
                case Tables.Weterynarz:
                    command += "Weterynarz";
                    break;
                case Tables.Wizyta:
                    command += "Wizyta";
                    break;
                
            }
            try
            {
                String connString = "server=localhost;uid=root;pwd=piesnicpon;database=zwierzyniec;";
                connection = new MySqlConnection(connString);

                connection.Open();
                adapter = new MySqlDataAdapter(command, connection);
            }
            catch (MySqlException er)
            {
                MessageBox.Show(er.ToString());

            }
       
            dt = new DataTable();
           
            adapter.Fill(dt);
            dataGrid1.ItemsSource = dt.DefaultView;
            connection.Close();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            //adapter.UpdateCommand = "";
        }


        void dataTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            
                UpdateDBIssues();
            
        }
        private void UpdateDBIssues()
        {
            String connString = "server=localhost;uid=root;pwd=piesnicpon;database=zwierzyniec;Allow User Variables=True;";
            connection = new MySqlConnection(connString);

            connection.Open();
            //  MySqlConnection connection = new MySqlConnection("server=localhost;uid=root;pwd=xxxx;database=comics;");
            string updateString = "UPDATE Zwierze SET id_zwierzecia=?id_zwierzecia,imie=?imie, gatunek=?gatunek, rasa=?rasa, data_urodzenia=?data_urodzenia, ubarwienie=?ubarwienie,szczepienia=?szczepienia"+ "Key_Issues=?oldKey_Issues"; ;
         //     "IssueDay=?IssueDay, IssueMonth=?IssueMonth, IssueYear=?IssueYear, ComicVine=?ComicVine WHERE " +
           //   "Key_Issues=?oldKey_Issues";
            MySqlCommand updateCommand = new MySqlCommand(updateString, connection);
            updateCommand.Parameters.Add("?id_zwierzecia", MySqlDbType.Int32,11, "id_zwierzecia");
            updateCommand.Parameters.Add("?imie", MySqlDbType.VarChar, 45, "imie");
            updateCommand.Parameters.Add("?gatunek", MySqlDbType.VarChar, 45, "gatunek");
            updateCommand.Parameters.Add("?rasa", MySqlDbType.VarChar, 45, "rasa");
            updateCommand.Parameters.Add("?data_urodzenia", MySqlDbType.Date, 10, "data_urodzenia");
            updateCommand.Parameters.Add("?ubarwienie", MySqlDbType.VarChar, 45, "ubarwienie");
            updateCommand.Parameters.Add("?szczepienia", MySqlDbType.VarChar, 45, "szczepienia");

           MySqlParameter parameter = updateCommand.Parameters.Add("?oldKey_Issues", MySqlDbType.Int32, 10, "Key_Issues");
            parameter.SourceVersion = DataRowVersion.Original;
          //  adapter = new MySqlDataAdapter();
            adapter.UpdateCommand = updateCommand;

           string insertString = "INSERT INTO Zwierze ( imie,gatunek, rasa, data_urodzenia,ubarwienie,szczepienia,fk_klienta) " +
            "VALUES (?imie, ?gatunek, ?rasa, ?data_urodzenia,?ubarwienie,?szczepienia,?fk_klienta)";
            MySqlCommand insertCommand = new MySqlCommand(insertString, connection);
            
            insertCommand.Parameters.Add("?imie", MySqlDbType.VarChar, 45, "imie");
            insertCommand.Parameters.Add("?gatunek", MySqlDbType.VarChar, 45, "gatunek");
            insertCommand.Parameters.Add("?rasa", MySqlDbType.VarChar, 45, "rasa");
            insertCommand.Parameters.Add("?data_urodzenia", MySqlDbType.Date, 10, "data_urodzenia");
            insertCommand.Parameters.Add("?ubarwienie", MySqlDbType.VarChar, 45, "ubarwienie");
            insertCommand.Parameters.Add("?szczepienia", MySqlDbType.VarChar, 45, "szczepienia");
            insertCommand.Parameters.Add("?fk_klienta", MySqlDbType.Int64, 11, "fk_klienta");
            //insertCommand.Parameters.Add("?IssueDay", MySqlDbType.Int32, 10, "IssueDay");
            //insertCommand.Parameters.Add("?IssueMonth", MySqlDbType.Int32, 10, "IssueMonth");
            //insertCommand.Parameters.Add("?IssueYear", MySqlDbType.Int32, 10, "IssueYear");
            //insertCommand.Parameters.Add("?ComicVine", MySqlDbType.VarChar, 100, "ComicVine");
            adapter.InsertCommand = insertCommand;

            MySqlCommand deleteCommand = new MySqlCommand("DELETE FROM Zwierzeta WHERE id_zwierzecia=?id_zwierzecia", connection);
            MySqlParameter delParameter = deleteCommand.Parameters.Add("?id_zwierzecia", MySqlDbType.Int32, 10, "id_zwierzecia");
            delParameter.SourceVersion = DataRowVersion.Original;
            adapter.DeleteCommand = deleteCommand;

            //DataTable dataTable = (DataTable)((DataSourceProvider)FindResource("DataTable")).Data;
            adapter.Update(dt);
        }
    }
}
