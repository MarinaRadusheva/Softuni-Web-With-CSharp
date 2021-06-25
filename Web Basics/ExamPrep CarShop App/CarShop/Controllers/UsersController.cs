using CarShop.Models.Users;
using CarShop.Services;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;
using CarShop.Data.Models;

using static CarShop.Data.DataConstatnts;
using CarShop.Data;
using CarShop.Services;

namespace CarShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly CarShopDbContext data;
        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;
        public UsersController(IValidator validator, IPasswordHasher passwordHasher, CarShopDbContext data)
        {
            this.validator = validator;
            this.passwordHasher = passwordHasher;
            this.data = data;
        }
        public HttpResponse Register() => View();

        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel model)
        {
            var modelErrors = this.validator.ValidateUserRegistration(model);
            if (this.data.Users.Any(u => u.Username==model.Username))
            {
                modelErrors.Add("Username already taken");
            }
            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var user = new User
            {
                Username = model.Username,
                Password = passwordHasher.HashPassword(model.Password),
                Email = model.Email,
                IsMechanic = model.UserType == UserTypeMechanic
            };

            data.Users.Add(user);
            data.SaveChanges();

            return Redirect("/Users/Login");
        }

        public HttpResponse Login() => View();

        [HttpPost]
        public HttpResponse Login(LoginFormModel model)
        {
            var hashedPassword = this.passwordHasher.HashPassword(model.Password);
            var userId = this.data.Users.Where(u => u.Username == model.Username && u.Password == hashedPassword).Select(u => u.Id).FirstOrDefault();
            if (userId==null)
            {
                return Error("Username and password combination is not valid");
            }
            this.SignIn(userId);
            return Redirect("/Cars/All");
        }

        public HttpResponse Logout()
        {
            this.SignOut();
            return Redirect("/");
        }
    }
}
