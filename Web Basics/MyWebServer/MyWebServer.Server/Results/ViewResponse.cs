using MyWebServer.Server.Http;
using System.IO;
using System.Linq;

namespace MyWebServer.Server.Results
{
    public class ViewResponse : HttpResponse
    {
        private const char PathSeparator = '/';

        public ViewResponse(string viewPath, string controllerName, object model) : base(HttpStatusCode.OK)
        {
            this.GetHtml(viewPath, controllerName, model);
        }

        private void GetHtml(string viewName, string controllerName, object model)
        
        {
            if (!viewName.Contains(PathSeparator))
            {
                viewName = controllerName + PathSeparator + viewName;
            }

            var viewPath = Path.GetFullPath("./Views/" + viewName.TrimStart('/') + ".cshtml");

            if (!File.Exists(viewPath))
            {
                this.PrepareMissingViewError(viewPath);
                return;
            }
            var viewContent = File.ReadAllText(viewPath);

            if (model != null)
            {
                viewContent = this.PopulateModel(viewContent, model);
            }

            this.PrepareContent(viewContent, HttpContentType.Html);
        }

        private void PrepareMissingViewError(string viewPath)
        {
            this.StatusCode = HttpStatusCode.NotFound;
            var errorMessage = $"View '{viewPath}' was not found.";
            this.PrepareContent(errorMessage, HttpContentType.TextPlain);
        }
        private string PopulateModel(string viewContent, object model)
        {
            var data = model
                .GetType()
                .GetProperties()
                .Select(pr => new
            {
                Name = pr.Name,
                Value = pr.GetValue(model)
            });
            foreach (var entry in data)
            {
                viewContent = viewContent.Replace($"{{{{{entry.Name}}}}}", entry.Value.ToString());
                
            }
            return viewContent;
        }
    }
}
