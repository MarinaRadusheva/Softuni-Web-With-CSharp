using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;
using MyWebServer.Server.Results;

namespace MyWebServer.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(HttpRequest request) : base(request)
        {
        }

        public HttpResponse ActionWithCookies()
        {
            this.Response.AddCookie("My-Cookie", "My value");
            return Text("Hello!");
        } 

    }
}
