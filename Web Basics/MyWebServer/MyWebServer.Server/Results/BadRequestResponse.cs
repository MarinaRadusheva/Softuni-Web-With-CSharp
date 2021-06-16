using MyWebServer.Server.Http;

namespace MyWebServer.Server.Results
{
    public class BadRequestResponse : HttpResponse
    {
        public BadRequestResponse() : base(HttpStatusCode.BadRequest)
        {
        }
    }
}
