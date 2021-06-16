using MyWebServer.Server.Common;
using MyWebServer.Server.Http;
using MyWebServer.Server.Results;
using System;
using System.Collections.Generic;

namespace MyWebServer.Server.Routing
{
    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, Func<HttpRequest, HttpResponse>>> routes;
        public RoutingTable()
        {
            this.routes = new Dictionary<HttpMethod, Dictionary<string, Func<HttpRequest, HttpResponse>>>()
            {
                [HttpMethod.GET] = new Dictionary<string, Func<HttpRequest, HttpResponse>>(),
                [HttpMethod.POST] = new Dictionary<string, Func<HttpRequest, HttpResponse>>(),
                [HttpMethod.PUT] = new Dictionary<string, Func<HttpRequest, HttpResponse>>(),
                [HttpMethod.DELETE] = new Dictionary<string, Func<HttpRequest, HttpResponse>>(),
            };
        }
        public IRoutingTable Map(HttpMethod method, string path,  HttpResponse response)
        {
            Guard.AgainstNull(response, nameof(response));
            return this.Map(method, path, request => response);
        }

        public IRoutingTable Map(HttpMethod method, string path, Func<HttpRequest, HttpResponse> responseFunction)
        {
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(responseFunction, nameof(responseFunction));
            this.routes[method][path] = responseFunction;
            return this;
        }

        public IRoutingTable MapGet(string path, HttpResponse response)
        {
            return MapGet(path, request => response);
        }

        public IRoutingTable MapGet(string path, Func<HttpRequest, HttpResponse> responseFunction)
        {
            return Map(HttpMethod.GET, path, responseFunction);
        }

        public IRoutingTable MapPost(string path, HttpResponse response)
        {
            return MapPost(path, request => response);
        }

        public IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunction)
        {
            return Map(HttpMethod.POST, path, responseFunction);
        }

        public HttpResponse ExecuteRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestPath = request.Path;
            if (!this.routes.ContainsKey(requestMethod) || !this.routes[requestMethod].ContainsKey(requestPath))
            {
                return new NotFoundResponse();
            }

            var responseFunction = this.routes[requestMethod][requestPath];

            return responseFunction(request);
        }

       
    }
}
