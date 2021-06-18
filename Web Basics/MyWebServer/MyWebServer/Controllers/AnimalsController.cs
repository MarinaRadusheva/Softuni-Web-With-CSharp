using MyWebServer.Models.Animals;
using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;
using MyWebServer.Server.Results;

namespace MyWebServer.Controllers
{
    public class AnimalsController : Controller
    {
        public AnimalsController(HttpRequest request) : base(request)
        {
        }

        public HttpResponse Cats()
        {
            // localhost:8000/Cats?Name=Pesho&Age=5
            const string nameKey = "Name";
            const string ageKey = "Age";
            var query = this.Request.Query;
            var catName = query.ContainsKey(nameKey) ? query[nameKey] : "the cats";
            var catAge = query.ContainsKey(ageKey) ? int.Parse(query[ageKey]) : 0;
            var catViewModel = new CatViewModel
            {
                Name = catName,
                Age = catAge
            };
            
            return View(catViewModel);
        }
        
        public HttpResponse Dogs() => View( new DogViewModel
        {
            Name = "Rachael",
            Age = 14,
            Breed = "Street shepherd"
        });
        public HttpResponse Bunnies() => View("Rabbits");
        public HttpResponse Turtles() => View("Animals/Wild/Turtles");

    }
}
