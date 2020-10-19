using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace GetVideoStream
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage result = null;
            //var task = Task.Run(async () => { 
            //    result = await httpClient.GetAsync("http://83.128.74.78:8083/mjpg/video.mjpg");
            //    var content = result.Content.ReadAsStringAsync();
            //});
            //result = httpClient.GetAsync("http://83.128.74.78:8083/mjpg/video.mjpg").Result;
            //var content = result.Content.ReadAsStringAsync();

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://83.128.74.78:8083/mjpg/video.mjpg");
            myRequest.Method = "GET";
            HttpWebResponse httpResponse = (HttpWebResponse)myRequest.GetResponse();
            var content = new StreamContent(httpResponse.GetResponseStream());
            content.Headers.Add("Content-Type", httpResponse.ContentType);
            var multipart = Task.Run( async() => await content.ReadAsStreamAsync()).Result;
            byte[] buffer = new byte[1024];

            while (multipart.CanRead)
            {
                multipart.Read(buffer, 0, 1024);
                Console.WriteLine(buffer.ToString());
            }
        }
    }
}
