using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;
using System;

namespace MyWebServer.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(HttpRequest request) : base(request)
        {
        }

        public HttpResponse Index() => Text("Hello from Marina's server!");

        public HttpResponse LocalRedirect() => Redirect("/Cats");
        public HttpResponse Error() => throw new InvalidOperationException("Invalid action!");
    }
}
