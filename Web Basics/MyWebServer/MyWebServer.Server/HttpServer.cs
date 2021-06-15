using MyWebServer.Server.Http;
using MyWebServer.Server.Routing;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener listener;
        public HttpServer(string ipAddress, int port, Action<IRoutingTable> routingTable)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);

            this.port = port;
            this.listener = new TcpListener(this.ipAddress, port);
        }
        public HttpServer(int port, Action<IRoutingTable> routingTable) : this("127.0.0.1", port, routingTable)
        {
        }
        public HttpServer(Action<IRoutingTable> routingTable) : this(8000, routingTable) 
        { 
        }
        public async Task Start()
        {
            this.listener.Start();

            Console.WriteLine($"Server started on port {port}");
            Console.WriteLine("Listening for requests...");
            while (true)
            {
                var connection = await this.listener.AcceptTcpClientAsync();

                var networkStream = connection.GetStream();

                var requestText = await this.ReadRequest(networkStream);

                Console.WriteLine(requestText);
                var request = HttpRequest.Parse(requestText);

                await WriteResponse(networkStream);
               
                connection.Close();
            }
            // from Niki's workshop
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
        }
        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            var bufferLength = 4096;
            var buffer = new byte[bufferLength];

            var requestBuilder = new StringBuilder();
            while (networkStream.DataAvailable)
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);
                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
            return requestBuilder.ToString();
        }
        private async Task WriteResponse(NetworkStream networkStream)
        {
            var responseContent = "Hello from my server! Ти си машина!";

            var responseContentLength = Encoding.UTF8.GetByteCount(responseContent);

            var response = $@"HTTP/1.1 200 OK{Constants.NewLine}Server: My Web Server{Constants.NewLine}Date: {DateTime.UtcNow.ToString("R")}{Constants.NewLine}Content-Length: {responseContentLength}{Constants.NewLine}Content-Type: text/html; charset=UTF-8{Constants.NewLine}{Constants.NewLine}{responseContent}";

            var responseBytes = Encoding.UTF8.GetBytes(response);

            await networkStream.WriteAsync(responseBytes);
        }
    }
}
