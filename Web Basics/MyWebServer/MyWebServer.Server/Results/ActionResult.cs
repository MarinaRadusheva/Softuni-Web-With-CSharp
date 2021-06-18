using MyWebServer.Server.Http;
using System.Collections.Generic;

namespace MyWebServer.Server.Results
{
    public abstract class ActionResult : HttpResponse
    {
        protected ActionResult(HttpResponse response) : base(response.StatusCode)
        {
            this.PrepareHeaders(response.Headers);
            this.PrepareCookies(response.Cookies);
            this.Content = response.Content;

        }
        protected HttpResponse Response { get; private set; }
        private void PrepareHeaders(HttpHeaderCollection headers)
        {
            foreach (var header in headers)
            {
                if (!this.Headers.Contains(header.Name))
                {
                    this.Headers.Add(header.Name, header.Value);
                }
                
            }
        }
        private void PrepareCookies(IDictionary<string, HttpCookie> cookies)
        {
            foreach (var cookie in cookies)
            {
                this.Cookies[cookie.Key] = cookie.Value;
            }
        }
    }
}
