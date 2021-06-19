using System;
using System.Collections.Generic;
using System.Linq;

namespace MyWebServer.Server.Http
{
    public class HttpRequest
    {

        private static Dictionary<string, HttpSession> Sessions = new Dictionary<string, HttpSession>();

        public HttpMethod Method { get; private set; }

        public string Path { get; private set; }

        public Dictionary<string, string> Query { get; private set; }

        public Dictionary<string, string> Form { get; private set; }

        public HttpHeaderCollection Headers { get; set; }

        public IDictionary<string, HttpCookie> Cookies { get; private set; } = new Dictionary<string, HttpCookie>();

        public HttpSession Session { get; private set; }

        public string Body { get; private set; }

        public static HttpRequest Parse(string request)
        {
            var lines = request.Split(Constants.NewLine);
            var startLine = lines.First().Split(" ");
            var method = ParseHttpMethod(startLine[0]);
            var url = startLine[1];
            var (path, query) = ParseUrl(url);
            var headerCollection = ParseHttpHeaderCollection(lines.Skip(1));
            var cookies = ParseCookies(headerCollection);

            var session = GetSession(cookies);

            var bodyLines = lines.Skip(headerCollection.Count + 2).ToArray();
            var body = string.Join(Constants.NewLine, bodyLines);
            var form = ParseForm(headerCollection, body);
            return new HttpRequest
            {
                Method = method,
                Path = path,
                Query = query,
                Headers = headerCollection,
                Cookies = cookies,
                Session = session,
                Body = body,
                Form = form
            };
        }

        private static Dictionary<string, string> ParseForm(HttpHeaderCollection headerCollection, string body)
        {
            var result = new Dictionary<string, string>();
            if (headerCollection.Contains(HttpHeader.ContentType) && headerCollection[HttpHeader.ContentType].Value == HttpContentType.FormUrlEncoded)
            {
                result = ParseQuery(body);
            }
            return result;
        }

        //  /Cats?name=Pesho&Age=2
        private static (string Path, Dictionary<string, string> Query) ParseUrl(string url)
        {
            var urlParts = url.Split("?", 2);
            var path = urlParts[0];
            var query = urlParts.Length > 1 ? ParseQuery(urlParts[1]) : new Dictionary<string, string>();
            return (path, query);
        }

        private static Dictionary<string, string> ParseQuery(string queryString)
        {
            return queryString
                    .Split("&")
                    .Select(part => part.Split("="))
                    .Where(part => part.Length == 2)
                    .ToDictionary(p => p[0], p => p[1]);

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

                if (headerParts.Count() != 2)
                {
                    throw new InvalidOperationException("Request is invalid!");
                }

                var headerName = headerParts[0];
                var headerValue = headerParts[1].Trim();

                headerCollection.Add(headerName, headerValue);

            }
            return headerCollection;
        }
        private static Dictionary<string, HttpCookie> ParseCookies(HttpHeaderCollection headers)
        {
            var cookieCollection = new Dictionary<string, HttpCookie>();
            if (headers.Contains(HttpHeader.Cookie))
            {
                var cookieHeader = headers[HttpHeader.Cookie];
                var allCookies = cookieHeader.Value.Split(";");
                foreach (var cookie in allCookies)
                {
                    var cookieParts = cookie.Split("=");
                    var cookieName = cookieParts[0].Trim();
                    var cookieValue = cookieParts[1].Trim();
                    var newCookie = new HttpCookie(cookieName, cookieValue);
                    cookieCollection.Add(cookieName, newCookie);
                }
            }
            return cookieCollection;
        }

        private static HttpSession GetSession(Dictionary<string, HttpCookie> cookies)
        {
            var sessionId = cookies.ContainsKey(HttpSession.SessionCookieName) ? cookies[HttpSession.SessionCookieName].Value : Guid.NewGuid().ToString();

            if (!Sessions.ContainsKey(sessionId))
            {
                Sessions[sessionId] = new HttpSession(sessionId);
            }

            return Sessions[sessionId];

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
