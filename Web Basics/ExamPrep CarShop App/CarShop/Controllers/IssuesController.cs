using CarShop.Data;
using CarShop.Data.Models;
using CarShop.Models.Issues;
using CarShop.Services;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace CarShop.Controllers
{
    public class IssuesController : Controller
    {
        private readonly CarShopDbContext data;
        private readonly IValidator validator;
        public IssuesController(CarShopDbContext data, IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse CarIssues(string carId)
        {
            if (!this.UserIsMechanic())
            {
                var userOwnsCar = this.data.Cars.Any(c => c.Id == carId && c.OwnerId == this.User.Id);
                if (!userOwnsCar)
                {
                    return Error("This user does not own a car");
                }
            }
            var car = this.data.Cars.Where(c => c.Id == carId).Select(c => new CarIssueViewModel
            {
                Id = c.Id,
                Model = c.Model,
                Year = c.Year,
                Issues = c.Issues.Select(i => new IssueListingViewModel
                {
                    Id = i.Id,
                    Description = i.Description,
                    IsFixed = i.IsFixed,
                }).ToList()
            }).FirstOrDefault();

            if (car == null)
            {
                return Error($"Car with {carId} id does not exist");
            }
            return View(car);
        }
        [Authorize]
        public HttpResponse Add(string carId)
        {
            var addIssueModel = new AddIssueViewModel
            {
                CarId = carId
            };
            return View(addIssueModel);
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Add(AddIssueViewModel model)
        {
            if (!this.UserCanAccessCar(model.CarId))
            {
                return Unauthorized();
            }
            var errors = this.validator.ValidateIssue(model);
            if (errors.Any())
            {
                return Error(errors);
            }
            var issue = new Issue
            {
                Description = model.Description,
                CarId = model.CarId,
            };

            data.Issues.Add(issue);
            data.SaveChanges();

            return Redirect($"/Issues/CarIssues?carId={model.CarId}");
        }

        [Authorize]
        public HttpResponse Fix(string issueId, string carId)
        {
            var userIsMechanic = this.UserIsMechanic();
            if (!userIsMechanic)
            {
                return Unauthorized();
            }
            var issue = this.data.Issues.FirstOrDefault(i => i.Id == issueId);
            if (issue==null)
            {
                return Error("Cannot find issue");
            }
            issue.IsFixed = true;
            this.data.SaveChanges();

            return Redirect($"/Issues/CarIssues?carId={carId}");
        }

        [Authorize]
        public HttpResponse Delete(string issueId, string carId)
        {          
            

            var issue = this.data.Issues.Find(issueId);
            this.data.Issues.Remove(issue);
            this.data.SaveChanges();

            return Redirect($"/Issues/CarIssues?carId={carId}");
        }

        private bool UserIsMechanic()
        {
            return this.data.Users.Any(u => u.Id == this.User.Id && u.IsMechanic);
        }

        private bool UserCanAccessCar(string CarId)
        {
            var userIsMechanic = this.UserIsMechanic();
            if (!userIsMechanic)
            {
                var userOwnsCar = this.data.Cars.Where(c => c.Id == CarId && c.OwnerId == this.User.Id).Any();
                if (!userOwnsCar)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
