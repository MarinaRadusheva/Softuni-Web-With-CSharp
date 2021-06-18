using MyWebServer.Server.Http;

namespace MyWebServer.Server.Results
{
    public class NotFoundResult : ActionResult
    {
        public NotFoundResult(HttpResponse response) : base(response)
        {
            this.StatusCode = HttpStatusCode.NotFound;
        }
    }
}
