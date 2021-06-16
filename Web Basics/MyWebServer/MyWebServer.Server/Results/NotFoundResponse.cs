using MyWebServer.Server.Http;

namespace MyWebServer.Server.Results
{
    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse() : base(HttpStatusCode.NotFound)
        {
        }
    }
}
