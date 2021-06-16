using System;
using MyWebServer.Server.Http;

namespace MyWebServer.Server.Routing
{
    // Routing table is going to map different methods and URLs to corresponding responses.
    // GET - / - Hello from Marina's home page
    // GET - /Cats - Hello from Marina's cats!
    // GET - /Dogs - Hello from the dogs!
    public interface IRoutingTable
    {
        IRoutingTable Map(HttpMethod method, string path, HttpResponse response);

        IRoutingTable Map(HttpMethod method, string path, Func<HttpRequest, HttpResponse> responseFunction);

        IRoutingTable MapGet(string path, HttpResponse response);

        IRoutingTable MapGet(string path, Func<HttpRequest, HttpResponse> responseFunction);



        IRoutingTable MapPost(string path, HttpResponse response);

        IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunction);

    }
}
