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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Xml.Serialization;

namespace Microsoft_window_manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConfigWindow config;
        bool AuthClose = false;
        public MainWindow()
        {
            InitializeComponent();
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;
            string filespath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/.clicklocker";
            if (!Directory.Exists(filespath))
            {
                Directory.CreateDirectory(filespath);
            }
            if (!Directory.Exists(filespath+"/blocedareas"))
            {
                Directory.CreateDirectory(filespath+"/blockedareas");
            }
            if (!File.Exists(filespath + "/pass.dat"))
            {
                Window w = new Password_changer();
                w.Show();
            }
            if (!File.Exists(filespath+"/blockedareas/selectedpath.txt"))
            {
                File.Create(filespath + "/blockedareas/selectedpath.txt").Close();
                File.WriteAllText(filespath + "/blockedareas/selectedpath.txt", filespath + "/blockedareas/default.area");
                var stream=File.Create(filespath + "/blockedareas/default.area");
                List<Rect> Rectanglelist = new List<Rect>();
                XmlSerializer x = new XmlSerializer(typeof (List<Rect>));
                x.Serialize(stream, Rectanglelist);
                stream.Close();
            }
            InitalizeBlockingarea();
        }
        private void InitalizeBlockingarea()
        {
            maincanvas.Children.Clear();
            string filespath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/.clicklocker";
            var stream = File.Open(File.ReadAllText(filespath + "/blockedareas/selectedpath.txt"), FileMode.Open);
            List <Rect> rects= new XmlSerializer(typeof(List<Rect>)).Deserialize(stream) as List<Rect>;
            foreach (var item in rects)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Fill = new SolidColorBrush(new Color() { A = 1, B = 0, G = 0, R = 0 });
                rectangle.Height = item.Height;
                rectangle.Width = item.Width;
                rectangle.RenderTransform = new TranslateTransform(item.X, item.Y);
                maincanvas.Children.Add(rectangle);
            }
            stream.Close();
        }
        private void Config_closeevent(object sender, EventArgs e)
        {
            
            config.closeevent -= Config_closeevent;
            InitalizeBlockingarea();
        }

        private void Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!AuthClose)
            {
                Window w = new MainWindow();
                w.Show();
            }
            System.Threading.Thread.Sleep(50);
        }

        private void keup(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.F1)
            {

                config = new ConfigWindow();
                config.closeevent += Config_closeevent;
            }
        }
    }
}
