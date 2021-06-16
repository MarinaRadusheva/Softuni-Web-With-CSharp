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

        private readonly RoutingTable routingTable;
        public HttpServer(string ipAddress, int port, Action<IRoutingTable> routingTableConfiguration)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);

            this.port = port;
            this.listener = new TcpListener(this.ipAddress, this.port);
            this.routingTable = new RoutingTable();
            routingTableConfiguration(routingTable);
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
                var response = this.routingTable.ExecuteRequest(request);

                await WriteResponse(networkStream, response);
               
                connection.Close();
            }
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
        private async Task WriteResponse(NetworkStream networkStream, HttpResponse response)
        {
            var responseBytes = Encoding.UTF8.GetBytes(response.ToString());

            await networkStream.WriteAsync(responseBytes);
        }
    }
}
