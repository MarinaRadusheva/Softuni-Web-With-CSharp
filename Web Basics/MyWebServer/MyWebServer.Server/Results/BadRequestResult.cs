using MyWebServer.Server.Http;

namespace MyWebServer.Server.Results
{
    public class BadRequestResult : ActionResult
    {
        public BadRequestResult(HttpResponse response) : base(response)
        {
            this.StatusCode = HttpStatusCode.BadRequest;
        }
    }
}
