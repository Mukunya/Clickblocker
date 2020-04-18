using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.SimpleChildWindow;
using System.Security.Cryptography;
using System.IO;

namespace Microsoft_window_manager
{
    /// <summary>
    /// Interaction logic for ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : MetroWindow
    {
        public event EventHandler<EventArgs> closeevent;
        public ConfigWindow()
        {
            InitializeComponent();
        }
        private void Changepassword(object sender, RoutedEventArgs e)
        {
            Window w = new Password_changer();
            w.Show();
        }

        private void changeblock(object sender, RoutedEventArgs e)
        {

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Passwordinput i = new Passwordinput();
            i.ShowDialog();
            if (i.password == File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/.clicklocker/pass.dat"))
            {
                Environment.Exit(0);
            }
            else
            {
                MessageBox.Show("Wrong password");
            }
        }

        private void close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            closeevent?.Invoke(this, new EventArgs());
        }
    }
}
