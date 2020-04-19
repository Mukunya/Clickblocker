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
using VBase;

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
            this.Hide();
            Passwordinput i = new Passwordinput();
            i.ShowDialog();
            if (i.password == File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/.clicklocker/pass.dat"))
            {
                this.Show();
            }
            else
            {
                MessageBox.Show("Wrong password");
            }
        }
        private void Changepassword(object sender, RoutedEventArgs e)
        {
            Window w = new Password_changer();
            w.Show();
        }

        private void changeblock(object sender, RoutedEventArgs e)
        {

            Areaselector a = new Areaselector();
            this.Hide();
            a.ShowDialog();
            this.Show();

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            closeevent?.Invoke(this, new EventArgs());
        }

        private void openblock(object sender, RoutedEventArgs e)
        {
            FileBrowser browser = new FileBrowser(".area", "AREA file");
            string fname=browser.GetFileName(FileBrowser.EFileOperations.Open);
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/.clicklocker/blockedareas/selectedpath.txt", fname);
        }
    }
}
