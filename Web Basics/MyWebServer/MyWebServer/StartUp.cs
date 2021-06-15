using MyWebServer.Server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer
{
    public class StartUp
    {
        public static async Task Main()
        {
            // http://localhost:8000

            var server = new HttpServer("127.0.0.1", 8000);
            await server.Start();

            Console.WriteLine($"Server started on port");
            Console.WriteLine("Listening for requests...");
            //while (true)
            //{
            //    var client = serverListener.AcceptTcpClient();
            //    var networkStream = client.GetStream();
            //    byte[] buffer = new byte[1000000];
            //    var length = networkStream.Read(buffer, 0, buffer.Length);
            //    string requestString = Encoding.UTF8.GetString(buffer, 0, length);
            //    Console.WriteLine(requestString);

            //    var responseContent = "Hello from my server!";

            //    var responseContentLength = Encoding.UTF8.GetByteCount(responseContent);

            //    var response = $@"HTTP/1.1 200 OK{NewLine}Content-Length: {responseContentLength}{NewLine}Content-Type: text/html; charset=UTF-8{NewLine}{NewLine}{responseContent}";

            //    var responseBytes = Encoding.UTF8.GetBytes(response);
            //    await networkStream.WriteAsync(responseBytes);
            //}
            //while (true)
            //{
            //    var connection = await serverListener.AcceptTcpClientAsync();

            //    var networkStream = connection.GetStream();
            //    var bufferLength = 4096;
            //    var buffer = new byte[bufferLength];

            //    var requestBuilder = new StringBuilder();
            //    while (networkStream.DataAvailable)
            //    {
            //        var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);
            //        requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            //    }
            //    Console.WriteLine(requestBuilder);

            //    var responseContent = "Hello from my server! Ти си машина!";

            //    var responseContentLength = Encoding.UTF8.GetByteCount(responseContent);

            //    var response = $@"HTTP/1.1 200 OK{NewLine}Server: My Web Server{NewLine}Date: {DateTime.UtcNow.ToString("R")}{NewLine}Content-Length: {responseContentLength}{NewLine}Content-Type: text/html; charset=UTF-8{NewLine}{NewLine}{responseContent}";

            //    var responseBytes = Encoding.UTF8.GetBytes(response);

            //    await networkStream.WriteAsync(responseBytes);
            //    connection.Close();
            //}
          

        }
    }
}
