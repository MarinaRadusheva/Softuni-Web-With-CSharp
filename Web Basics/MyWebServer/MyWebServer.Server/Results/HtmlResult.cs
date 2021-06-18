using MyWebServer.Server.Http;

namespace MyWebServer.Server.Results
{
    public class HtmlResult : ContentResult
    {
        public HtmlResult(HttpResponse response, string text) : base(response, text, HttpContentType.Html)
        { 
        }
    }
}
