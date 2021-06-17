using MyWebServer.Server.Http;

namespace MyWebServer.Server.Results
{
    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string text) : base(text, HttpContentType.Html)
        { 
        }
    }
}
