using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace WpfAppNF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //HttpListener httpListener = new HttpListener();
            //httpListener.Prefixes.Add(@"http://83.128.74.78:8083/mjpg/video.mjpg");
            //httpListener.Start();
            //var context = httpListener.GetContext();
            //var request = context.Request;
            //var responce = context.Response;
            //var output = responce.OutputStream;

            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer. = MediaSource.CreateFromUri(new Uri("ms-appx:///C:/Users/stavenchukya/Downloads/videoSample.mp4"));

            player00.SetMediaPlayer(mediaPlayer);

            player00.MediaPlayer.Play();
        }
    }
}
