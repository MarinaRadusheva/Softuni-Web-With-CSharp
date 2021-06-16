using MyWebServer.Server.Http;
using MyWebServer.Server.Results;

namespace MyWebServer.Server.Controllers
{
    public abstract class Controller
    {
        protected Controller(HttpRequest request)
        {
            this.Request = request;
        }
        protected HttpRequest Request { get; private set; }
        protected HttpResponse Text(string text) => new TextResponse(text);
        protected HttpResponse Html(string html) => new HtmlResponse(html);
    }
}
