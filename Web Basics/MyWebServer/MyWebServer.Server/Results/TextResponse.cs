using MyWebServer.Server.Http;

namespace MyWebServer.Server.Results
{
    public class TextResponse : ContentResponse
    {
       
        public TextResponse(string text) : base(text, HttpContentType.TextPlain)
        {
        }
    }
}
