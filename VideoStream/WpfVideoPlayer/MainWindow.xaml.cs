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

namespace WpfVideoPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //wpfPlayer.Play();
            //MediaPlayer player = new MediaPlayer();
            ////player.Open(new Uri(@"C:\Users\stavenchukya\Downloads\videoSample.mp4"));
            ////player.Open(new Uri(@"C:\Users\stavenchukya\Downloads\videoSample.mp4"));
            //VideoDrawing videoDrawing = new VideoDrawing();
            //videoDrawing.Rect = new Rect(0, 0, 800, 600);
            //videoDrawing.Player = player;
            //player.Play();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            wpfPlayer.Play();
        }
    }
}
