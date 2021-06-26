using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.Models.Trips;
using SharedTrip.Services;
using System;
using System.Globalization;
using System.Linq;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IValidator validator;

        public TripsController(ApplicationDbContext data, IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse All()
        {
            var trips = this.data.Trips.Select(x => new TripListingViewModel
            {
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                DepartureTime = x.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                Seats = x.Seats-x.UserTrips.Count,
                Id = x.Id
                
            }).ToList();

            return View(trips);
        }

        [Authorize]
        public HttpResponse Add()
        {

            return this.View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddTripFormModel model)
        {
            var errors = this.validator.ValidateTrip(model);
            if (errors.Any())
            {
                return Error(errors);
            }

            var trip = new Trip
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                DepartureTime = DateTime.ParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                Seats = model.Seats,
                ImagePath = model.ImagePath?? "",
                Description = model.Description
            };

            this.data.Trips.Add(trip);
            this.data.SaveChanges();

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse Details(string tripId)
        {
            var trip = this.data.Trips.Where(x => x.Id == tripId).Select(x => new TripDetailsViewModel
            {
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                DepartureTime = x.DepartureTime.ToString("s"),
                Seats = x.Seats - x.UserTrips.Count,
                Description = x.Description,
                Id = x.Id


            }).FirstOrDefault();

            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        [Authorize]
        public HttpResponse AddUserToTrip(string tripId)
        {
            if (UserAlreadyJoinedTrip(tripId))
            {
                return Redirect($"/Trips/Details?tripId={tripId}");
            }
            if (NotEnoughSeats(tripId))
            {
                return Redirect($"/Trips/Details?tripId={tripId}");
            }

            var userTrip = new UserTrip
            {
                UserId = this.User.Id,
                TripId = tripId,
            };

            this.data.UserTrips.Add(userTrip);
            this.data.SaveChanges();

            return Redirect("/Trips/All");
        }

        private bool NotEnoughSeats (string tripId)
        {
            return this.data.Trips.First(x => x.Id == tripId).Seats == 0;
        }

        private bool UserAlreadyJoinedTrip(string tripId)
        {
            if (this.data.UserTrips.Where(x=>x.UserId==this.User.Id && x.TripId==tripId).Any())
            {
                return true;
            }
            return false;
        }

    }
}
