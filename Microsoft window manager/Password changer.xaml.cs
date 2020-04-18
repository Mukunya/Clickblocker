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
using System.IO;
using System.Security.Cryptography;

namespace Microsoft_window_manager
{
    /// <summary>
    /// Interaction logic for Password_changer.xaml
    /// </summary>
    public partial class Password_changer : Window
    {
        public Password_changer()
        {
            InitializeComponent();
        }
        public Password_changer(object noold)
        {
            InitializeComponent();
            old.IsEnabled = false;
        }

        private void Finish(object sender, RoutedEventArgs e)
        {
            
            if (old.IsEnabled)
            {
                string filepath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/.clicklocker/pass.dat";
                if (old.Password==File.ReadAllText(filepath))
                {
                    File.WriteAllText(filepath, newpass.Password);
                    MessageBox.Show("Password changed.");
                    Close();
                }
                else
                {
                    MessageBox.Show("Wrong password");
                }
            }
            else
            {
                string filepath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/.clicklocker/pass.dat";
                File.WriteAllText(filepath, newpass.Password);
                MessageBox.Show("Password set.");
                Close();
            }

        }
    }
}
