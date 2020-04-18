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

namespace Microsoft_window_manager
{
    /// <summary>
    /// Interaction logic for Passwordinput.xaml
    /// </summary>
    public partial class Passwordinput : Window
    {
        public string password = "";
        public Passwordinput()
        {
            InitializeComponent();
        }

        private void ok(object sender, RoutedEventArgs e)
        {
            password = passw.Password;
            this.Close();
        }
    }
}
