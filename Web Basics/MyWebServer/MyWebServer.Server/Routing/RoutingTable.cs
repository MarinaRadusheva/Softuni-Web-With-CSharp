using MyWebServer.Server.Common;
using MyWebServer.Server.Http;
using MyWebServer.Server.Results;
using System;
using System.Collections.Generic;

namespace MyWebServer.Server.Routing
{
    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, HttpResponse>> routes;
        public RoutingTable()
        {
            this.routes = new Dictionary<HttpMethod, Dictionary<string, HttpResponse>>()
            {
                [HttpMethod.GET] = new Dictionary<string, HttpResponse>(),
                [HttpMethod.POST] = new Dictionary<string, HttpResponse>(),
                [HttpMethod.PUT] = new Dictionary<string, HttpResponse>(),
                [HttpMethod.DELETE] = new Dictionary<string, HttpResponse>(),
            };
        }
        public IRoutingTable Map(HttpMethod method, string path,  HttpResponse response)
        {
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(response, nameof(response));
            this.routes[method][path] = response;
            return this;
        }

        public IRoutingTable MapGet(string path, HttpResponse response)
        {
            return Map(HttpMethod.GET, path, response);
        }

        public IRoutingTable MapPost(string path, HttpResponse response)
        {
            return Map(HttpMethod.POST, path, response);
        }
        public HttpResponse MatchRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestPath = request.Path;
            if (!this.routes.ContainsKey(requestMethod) || !this.routes[requestMethod].ContainsKey(requestPath))
            {
                return new NotFoundResponse();
            }

            return this.routes[requestMethod][requestPath];
        }
    }
}
