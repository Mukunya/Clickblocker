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
using System.Xml.Serialization;

namespace Microsoft_window_manager
{
    /// <summary>
    /// Interaction logic for Areaselector.xaml
    /// </summary>
    public partial class Areaselector : Window
    {
        bool ismousedown = false;
        Point startpoint = new Point();
        int childrenI=0;
        public Areaselector()
        {
            InitializeComponent();
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;
            this.KeyUp += keyup;
        }

        private void Mousedown(object sender, MouseButtonEventArgs e)
        {
            startpoint = Mouse.GetPosition(this);
            Rectangle r = new Rectangle() { Fill=new SolidColorBrush(new Color() { A=255, B=0, G=0, R=0 }), StrokeThickness=1 };
            r.RenderTransform = new TranslateTransform(startpoint.X, startpoint.Y);
            if (Maincanvas.Children.Count==0)
            {
                childrenI = 0;
            }
            Maincanvas.Children.Add(r);
            System.Threading.Thread.Sleep(50);
            ismousedown = true;
        }

        private void Mousemove(object sender, MouseEventArgs e)
        {
            if (ismousedown)
            {
                var x = Mouse.GetPosition(this).X - startpoint.X;
                var y = Mouse.GetPosition(this).Y - startpoint.Y;
                var a = Maincanvas.Children[childrenI] as Rectangle;
                a.Height = Math.Abs(y);
                a.Width = Math.Abs(x);
                var transform = a.RenderTransform as TranslateTransform;
                if (x<0)
                {
                    transform.X = Mouse.GetPosition(this).X;
                }
                if (y < 0)
                {
                    transform.Y = Mouse.GetPosition(this).Y;
                }
                a.RenderTransform = transform;
                Maincanvas.Children[childrenI] = a;
            }
        }

        private void Mouseup(object sender, MouseButtonEventArgs e)
        {
            ismousedown = false;
            Maincanvas.Children[childrenI].MouseRightButtonDown += Areaselector_MouseRightButtonDown;
            childrenI++;
        }

        private void Areaselector_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            int i=Maincanvas.Children.IndexOf(sender as Rectangle);
            Maincanvas.Children.RemoveAt(i);
            childrenI--;
        }

        private void keyup(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Return)
            {
                VBase.FileBrowser browser = new VBase.FileBrowser(".area", "AREA file");
                string fname = browser.GetFileName(VBase.FileBrowser.EFileOperations.Save);
                var stream=File.Create(fname);
                List<Rect> rects = new List<Rect>();
                foreach (var item in Maincanvas.Children)
                {
                    var rectangle = item as Rectangle;
                    rects.Add(new Rect((rectangle.RenderTransform as TranslateTransform).X, (rectangle.RenderTransform as TranslateTransform).Y, rectangle.Width, rectangle.Height));
                }
                XmlSerializer x = new XmlSerializer(typeof(List<Rect>));
                x.Serialize(stream, rects);
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/.clicklocker/blockedareas/selectedpath.txt", fname);
                MessageBox.Show("Area saved");
                Close();
            }
            else if (e.Key==Key.Escape)
            {
                Close();
            }
        }
    }
}
