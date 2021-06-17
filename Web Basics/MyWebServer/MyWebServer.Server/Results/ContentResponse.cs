using MyWebServer.Server.Common;
using MyWebServer.Server.Http;
using System.Text;

namespace MyWebServer.Server.Results
{
    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string content, string contentType) : base(HttpStatusCode.OK)
        {

            this.PrepareContent(content, contentType);
        }
    }
}
