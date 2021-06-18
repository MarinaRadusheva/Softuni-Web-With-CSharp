using MyWebServer.Server.Http;

namespace MyWebServer.Server.Results
{
    public class TextResult : ContentResult
    {
       
        public TextResult(HttpResponse response, string text) : base(response, text, HttpContentType.TextPlain)
        {
        }
    }
}
