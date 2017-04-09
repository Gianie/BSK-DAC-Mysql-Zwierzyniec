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

namespace BSK
{
    /// <summary>
    /// Interaction logic for Uprawnienia.xaml
    /// </summary>
    public partial class Uprawnienia : Window
    {
        public Uprawnienia()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            Window1 win2 = new Window1();
            win2.Show();
            this.Close();

    }
    }
}
