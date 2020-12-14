using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;
using System.Runtime.InteropServices;


namespace RestoAPPWPF
{
    /// <summary>
    /// Lógica de interacción para TutorialMenusWPF.xaml
    /// </summary>
    public partial class TutorialMenusWPF : Window
    {
        public TutorialMenusWPF()
        {
            InitializeComponent();
            InitializeComponent();
            var videoPath = Directory.GetCurrentDirectory();
            video.Source = new Uri(videoPath + @"\AyudaMenus.mp4", UriKind.Relative);
        }
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            video.Play();
        }
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            video.Pause();
        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            video.Stop();
            video.Play();
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            video.Volume = (double)slider2.Value;
        }
    }
}
