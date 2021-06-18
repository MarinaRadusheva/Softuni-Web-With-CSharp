using MyWebServer.Server.Http;
using MyWebServer.Server.Results;
using System.Runtime.CompilerServices;

namespace MyWebServer.Server.Controllers
{
    public abstract class Controller
    {
        protected Controller(HttpRequest request)
        {
            this.Request = request;
            this.Response = new HttpResponse(HttpStatusCode.OK);
        }
        protected HttpRequest Request { get; private set; }
        protected HttpResponse Response { get; private set; }

        protected ActionResult Text(string text) => new TextResult(this.Response, text);

        protected ActionResult Html(string html) => new HtmlResult(this.Response, html);

        protected ActionResult Redirect(string location) => new RedirectResult(this.Response, location);

        protected ActionResult View([CallerMemberName] string viewName ="") =>new ViewResult(this.Response, viewName, this.GetControllerName(), null);

        protected ActionResult View(string viewName, object model) => new ViewResult(this.Response, viewName, this.GetControllerName(), model);

        protected ActionResult View(object model,[CallerMemberName] string viewName = "") => new ViewResult(this.Response, viewName, this.GetControllerName(), model);

        private string GetControllerName()
        {
            return this.GetType().Name.Replace(nameof(Controller), string.Empty);
        }

    }
}
