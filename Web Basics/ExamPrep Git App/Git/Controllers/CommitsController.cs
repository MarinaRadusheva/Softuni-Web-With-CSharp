using Git.Data;
using Git.Data.Models;
using Git.Models.Commits;
using Git.Services;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Linq;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly GitDbContext data;
        private readonly IValidator validator;

        public CommitsController(GitDbContext data, IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }

        [Authorize]

        public HttpResponse Create(string Id)
        {
            var repo = this.data.Repositories.Where(x=>x.Id==Id).Select(x=> new RepoToCommitViewModel
            {
                Id=x.Id,
                Name=x.Name
            }).FirstOrDefault();
            if (repo==null)
            {
                return BadRequest();
            }
            return View(repo);
        }

        [Authorize]
        [HttpPost]

        public HttpResponse Create(CreateCommitFormModel model)
        {
            var errors = this.validator.ValidateCommit(model);
            if (errors.Any())
            {
                return Error(errors);
            }

            var commit = new Commit
            {
                Description = model.Description,
                RepositoryId = model.Id,
                CreatorId = this.User.Id,
                CreatedOn = DateTime.UtcNow,

            };

            this.data.Commits.Add(commit);
            this.data.SaveChanges();

            return Redirect("/Commits/All");
        }
        [Authorize]
        public HttpResponse All()
        {
            var commits = this.data.Commits.Where(x => x.CreatorId == this.User.Id)
                .OrderByDescending(x => x.CreatedOn).Select(x => new CommitListingViewModel
                {
                    Repository = x.Repository.Name,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn.ToString("R"),
                    Id=x.Id,
                }).ToList();

            return View(commits);
        }

        [Authorize]
        public HttpResponse Delete(string id)
        {
            var commit = this.data.Commits.Find(id);

            if (commit == null || commit.CreatorId != this.User.Id)
            {
                return BadRequest();
            }

            this.data.Commits.Remove(commit);
            this.data.SaveChanges();

            return Redirect("/Commits/All");
        }
    }
}
