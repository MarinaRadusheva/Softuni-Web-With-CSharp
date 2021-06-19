using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;
using MyWebServer.Server.Results;
using System;

namespace MyWebServer.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(HttpRequest request) : base(request)
        {
        }

        public HttpResponse ActionWithCookies()
        {
            const string cookieName = "My-Cookie";
            if (this.Request.Cookies.ContainsKey(cookieName))
            {
                var cookie = this.Request.Cookies[cookieName];
                return Text($"Cookies already exist - {cookieName}");
            }
            this.Response.AddCookie(cookieName, "My value");
            this.Response.AddCookie("My-SecondCookie", "My second cookie value");
            return Text("Hello! Cookies set!");
        } 

        public HttpResponse ActionWithSession()
        {
            const string currendDateKey = "CurrentDate";
            if (this.Request.Session.ContainsKey(currendDateKey))
            {
                var currentDate = this.Request.Session[currendDateKey];
                return Text($"Stored date: {currentDate}!");
            }
            this.Request.Session[currendDateKey] = DateTime.UtcNow.ToString();
            return Text("Current date stored!");
        }
    }
}
