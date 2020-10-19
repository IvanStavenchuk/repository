using System;
using Windows.Media;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Media.Streaming.Adaptive;
using System.Threading.Tasks;
using System.Linq;
using Windows.Web.Http;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Http;
using Windows.Media.MediaProperties;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace VideoStream
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //Uri uri00 = new Uri("http://83.128.74.78:8083/mjpg/video.mjpg");
            System.Uri manifestUri = new Uri("http://83.128.74.78:8083/mjpg/video.mjpg");
            //player00.Source = MediaSource.CreateFromUri(manifestUri);
            //player00.MediaPlayer.Play();

            //MediaStreamSource mediaStreamSource = new MediaStreamSource(
            //    new VideoStreamDescriptor(
            //        VideoEncodingProperties.CreateUncompressed(
            //            CodecSubtypes.VideoFormatMjpg, 800, 600
            //        )
            //    )
            //);

            //player00.SetMediaPlayer(new MediaPlayer() { Source = mediaStreamSource.MediaProtectionManager. });


            //mediaStreamSource.BufferTime = TimeSpan.FromSeconds(1);
            //mediaStreamSource.SampleRequested += async (MediaStreamSource sender, MediaStreamSourceSampleRequestedEventArgs args) =>
            //{
            //    var deferal = args.Request.GetDeferral();
            //    try
            //    {
            //        var timestamp = new TimeSpan(0);

            //        var file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(@"Assets\grpPC1.jpg");
            //        using (var stream = await file.OpenReadAsync())
            //        {
            //            args.Request.Sample = await MediaStreamSample.CreateFromStreamAsync(
            //                stream.GetInputStreamAt(0), (uint)stream.Size, timestamp);
            //        }
            //        args.Request.Sample.Duration = TimeSpan.FromSeconds(5);
            //    }
            //    finally
            //    {
            //        deferal.Complete();
            //    }
            //    var buffer = new Windows.Storage.Streams.Buffer(800 * 600 * 4);
            //    // latestBitmap is SoftwareBitmap
            //    // latestBitmap.CopyToBuffer(buffer);
            //    args.Request.Sample = MediaStreamSample.CreateFromBuffer(buffer, new TimeSpan(0));
            //};

            //HttpClient httpClient = new Windows.Web.Http.HttpClient();
            //httpClient.DefaultRequestHeaders.TryAppendWithoutValidation("X-CustomHeader", "This is a custom header");
            //AdaptiveMediaSourceCreationResult result = AdaptiveMediaSource.CreateFromUriAsync(manifestUri, httpClient).GetResults();

            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///C:/Users/stavenchukya/Downloads/videoSample.mp4"));

            player00.SetMediaPlayer(mediaPlayer);

            player00.MediaPlayer.Play();



        }
        //https://docs.microsoft.com/ru-ru/windows/uwp/audio-video-camera/adaptive-streaming
        //private async void DownloadRequested(AdaptiveMediaSource sender, AdaptiveMediaSourceDownloadRequestedEventArgs args)
        //{
        //    // rewrite key URIs to replace http:// with https://
        //    if (args.ResourceType == AdaptiveMediaSourceResourceType.Key)
        //    {
        //        string originalUri = args.ResourceUri.ToString();
        //        string secureUri = originalUri.Replace("http:", "https:");

        //        // override the URI by setting property on the result sub object
        //        args.Result.ResourceUri = new Uri(secureUri);
        //    }

        //    if (args.ResourceType == AdaptiveMediaSourceResourceType.Manifest)
        //    {
        //        AdaptiveMediaSourceDownloadRequestedDeferral deferral = args.GetDeferral();
        //        args.Result.Buffer = await CreateMyCustomManifest(args.ResourceUri);
        //        deferral.Complete();
        //    }

        //    if (args.ResourceType == AdaptiveMediaSourceResourceType.MediaSegment)
        //    {
        //        var resourceUri = args.ResourceUri.ToString() + "?range=" +
        //            args.ResourceByteRangeOffset + "-" + (args.ResourceByteRangeLength - 1);

        //        // override the URI by setting a property on the result sub object
        //        args.Result.ResourceUri = new Uri(resourceUri);

        //        // clear the byte range properties on the result sub object
        //        args.Result.ResourceByteRangeOffset = null;
        //        args.Result.ResourceByteRangeLength = null;
        //    }
        //}

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //var button = sender as Button;            
            //var dlg = new ContentDialog1();
            ////ParentGrid.Children.Add(dlg);
            //ParentGrid.Children.Add(dlg);
            //dlg.CenterPoint = button.CenterPoint;
            //dlg. = button.ActualOffset;
            //await dlg.ShowAsync();
        }
    }
}
