using CarShop.Data;
using CarShop.Data.Models;
using CarShop.Models.Cars;
using CarShop.Services;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Collections.Generic;
using System.Linq;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly IValidator validator;
        private readonly CarShopDbContext data;
        public CarsController(IValidator validator, CarShopDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }

        [Authorize]
        public HttpResponse Add()
        {
            if (this.UserIsMechanic())
            {
                return Unauthorized();
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Add(AddCarFormModel model)
        {

            if (this.UserIsMechanic())
            {
                return Unauthorized();
            }

            var modelErrors = this.validator.ValidateNewCar(model);
            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }
            var newCar = new Car
            {
                Model = model.Model,
                Year = int.Parse(model.Year),
                PictureUrl = model.Image,
                PlateNumber = model.PlateNumber,
                OwnerId = this.User.Id
            };

            this.data.Cars.Add(newCar);
            this.data.SaveChanges();

            return Redirect("/Cars/All");
        }
        public HttpResponse All()
        {
            List<CarListingViewModel> cars;

            if (this.UserIsMechanic())
            {
                cars = this.data.Cars.Where(c=>c.Issues.Any(i=>!i.IsFixed)).Select(c => new CarListingViewModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    Year = c.Year.ToString(),
                    Image = c.PictureUrl,
                    PlateNumber = c.PlateNumber,
                    FixedIssues = c.Issues.Where(i => i.IsFixed).Count(),
                    RemainingIssues = c.Issues.Where(i => !i.IsFixed).Count(),
                }).ToList();
            }
            else
            {
                cars = this.data.Cars.Where(c => c.OwnerId == this.User.Id).Select(c => new CarListingViewModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    Year = c.Year.ToString(),
                    Image = c.PictureUrl,
                    PlateNumber = c.PlateNumber,
                    FixedIssues = c.Issues.Where(i => i.IsFixed).Count(),
                    RemainingIssues = c.Issues.Where(i => !i.IsFixed).Count(),
                }).ToList();
            }
         

            return View(cars);
        }

        private bool UserIsMechanic()
        {
            return this.data.Users.Any(u => u.Id == this.User.Id && u.IsMechanic);
        }
    }
}
