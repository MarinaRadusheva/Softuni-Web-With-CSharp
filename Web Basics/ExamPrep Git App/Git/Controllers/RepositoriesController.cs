using Git.Data;
using Git.Data.Models;
using Git.Models.Repositories;
using Git.Services;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Linq;

using static Git.Data.DataConstants;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly GitDbContext data;
        private readonly IValidator validator;

        public RepositoriesController(GitDbContext data, IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }

        public HttpResponse All()
        {
            var reposQuery = this.data.Repositories.Where(x => x.IsPublic).AsQueryable();
            var repos = reposQuery.Select(x => new RepoListingViewModel
            {
                Name = x.Name,
                Owner = x.Owner.Username,
                CreatedOn = x.CreatedOn.ToString(),
                Commits=x.Commits.Count(),
                Id=x.Id,
            }).ToList();


            return View(repos);
        }

        [Authorize]
        public HttpResponse Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]

        public HttpResponse Create(CreateRepoFormModel model)
        {
            var errors = this.validator.ValidateRepository(model);
            if (errors.Any())
            {
                return Error(errors);
            }

            var repository = new Repository
            {
                Name = model.Name,
                CreatedOn = DateTime.UtcNow,
                IsPublic = model.repositoryType == RepoTypePublic,
                OwnerId = this.User.Id
            };

            data.Repositories.Add(repository);
            data.SaveChanges();

            return Redirect("/Repositories/All");
        }
    }
}
