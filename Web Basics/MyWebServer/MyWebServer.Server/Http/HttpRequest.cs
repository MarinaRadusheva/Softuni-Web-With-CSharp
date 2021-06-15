using System;
using System.Collections.Generic;
using System.Linq;

namespace MyWebServer.Server.Http
{
    public class HttpRequest
    {
        public HttpMethod Method { get; private set; }
        public string Path { get; private set; }
        public HttpHeaderCollection Headers { get; private set; }
        public string Body { get; private set; }
        public static HttpRequest Parse(string request)
        {
            var lines = request.Split(Constants.NewLine);
            var startLine = lines.First().Split(" ");
            var method = ParseHttpMethod(startLine[0]);
            var url = startLine[1];
            var headerCollection = ParseHttpHeaderCollection(lines.Skip(1));
            var bodyLines = lines.Skip(headerCollection.Count + 2).ToArray();
            var body = string.Join(Constants.NewLine, bodyLines);
            return new HttpRequest
            {
                Method = method,
                Path = url,
                Headers = headerCollection,
                Body = body
            };
        }

        private static HttpHeaderCollection ParseHttpHeaderCollection(IEnumerable<string> headerLines)
        {
            var headerCollection = new HttpHeaderCollection();
            foreach (var headerLine in headerLines)
            {
                if (headerLine == string.Empty)
                {
                    break;
                }
                var headerParts = headerLine.Split(":", 2);

                if (headerParts.Count()!=2)
                {
                    throw new InvalidOperationException("Request is invalid!");
                }

                var headerName = headerParts[0];
                var headerValue = headerParts[1].Trim();
 
                headerCollection.Add(headerName, headerValue);

            }
            return headerCollection;
        }

        private static HttpMethod ParseHttpMethod(string method) => method switch
        {
            "GET" => HttpMethod.GET,
            "PUT" => HttpMethod.PUT,
            "POST" => HttpMethod.POST,
            "DELETE" => HttpMethod.DELETE,
            _ => throw new InvalidOperationException($"Method {method} is not supported")
        };
    }
}
