namespace MyWebServer.Server.Http
{
    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; private set; }
        public HttpHeaderCollection Headers { get; } = new HttpHeaderCollection();
        public string Content { get; private set; }
    }
}
