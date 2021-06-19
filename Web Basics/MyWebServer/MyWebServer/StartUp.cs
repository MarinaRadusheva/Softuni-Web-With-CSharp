using MyWebServer.Controllers;
using MyWebServer.Server;
using MyWebServer.Server.Results;
using System;
using MyWebServer.Server.Controllers;
using System.Threading.Tasks;

namespace MyWebServer
{
    public class StartUp
    {
        // http://localhost:8000
        // http://localhost:8000/Dogs
        // http://localhost:8000/Bunnies
        // http://localhost:8000/Turtles
        // http://localhost:8000/Cats
        // http://localhost:8000/Cats/CreateCat
        // http://localhost:8000/Cats?Name=Pisi&Age=1
        // http://localhost:8000/Cookies

        public static async Task Main()
            => await new HttpServer(routingTable => routingTable
            .MapGet<HomeController>("/", c => c.Index())
            .MapGet<AnimalsController>("/Cats", c => c.Cats())
            .MapGet<AnimalsController>("/Dogs", c => c.Dogs())
            .MapGet<AnimalsController>("/Turtles", c => c.Turtles())
            .MapGet<AnimalsController>("/Bunnies", c => c.Bunnies())
            .MapGet<AccountController>("/Cookies", c => c.ActionWithCookies())
            .MapGet<CatsController>("/Cats/CreateCat", c => c.CreateCat())
            .MapPost<CatsController>("/Cats/Save", c => c.Save()))
            .Start();

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
