using Git.Data;
using Git.Data.Models;
using Git.Models.Users;
using Git.Services;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace Git.Controllers
{
    public class UsersController : Controller
    {
        private readonly GitDbContext data;
        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;

        public UsersController(GitDbContext data, IValidator validator, IPasswordHasher passwordHasher)
        {
            this.data = data;
            this.validator = validator;
            this.passwordHasher = passwordHasher;
        }
        public HttpResponse Register()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel model)
        {
            var errors = this.validator.ValidateUser(model);

            if (this.data.Users.Any(u => u.Username == model.Username))
            {
                errors.Add($"User with '{model.Username}' username already exists.");
            }

            if (this.data.Users.Any(u => u.Email == model.Email))
            {
                errors.Add($"User with '{model.Email}' e-mail already exists.");
            }

            if (errors.Any())
            {
                return Error(errors);
            }

            var user = new User
            {
                Username = model.Username,
                Password = this.passwordHasher.HashPassword(model.Password),
                Email = model.Email,
            };

            data.Users.Add(user);
            data.SaveChanges();

            return Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Login(LoginUserFormModel model)
        {
            var hashedPassword = this.passwordHasher.HashPassword(model.Password);

            var userId = this.data
                .Users
                .Where(u => u.Username == model.Username && u.Password == hashedPassword)
                .Select(u => u.Id)
                .FirstOrDefault();

            if (userId == null)
            {
                return Error("Username and password combination is not valid.");
            }

            this.SignIn(userId);

            return Redirect("/Repositories/All");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}
