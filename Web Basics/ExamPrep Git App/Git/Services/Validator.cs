using Git.Models.Commits;
using Git.Models.Repositories;
using Git.Models.Users;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static Git.Data.DataConstants;

namespace Git.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateCommit(CreateCommitFormModel model)
        {
            var errors = new List<string>();
            if (model.Description.Length<CommitMinLength )
            {
                errors.Add($"Description must be longer than {CommitMinLength} symbols.");
            }
            return errors;
        }

        public ICollection<string> ValidateRepository(CreateRepoFormModel model)
        {
            var errors = new List<string>();

            if (model.Name.Length<RepoMinLength || model.Name.Length>RepoMaxLength)
            {
                errors.Add($"Repository name '{model.Name}' is not valid. It must be between {RepoMinLength} and {RepoMaxLength} characters long.");
            }
            if (model.repositoryType!=RepoTypePrivate && model.repositoryType!=RepoTypePublic)
            {
                errors.Add($"Invalid repository type. MUst be either {RepoTypePrivate} or {RepoTypePublic}");
            }
            return errors;

        }

        public ICollection<string> ValidateUser(RegisterUserFormModel user)
        {
            var errors = new List<string>();

            if (user.Username == null || user.Username.Length < UserMinLength || user.Username.Length > UserMaxLength)
            {
                errors.Add($"Username '{user.Username}' is not valid. It must be between {UserMinLength} and {UserMaxLength} characters long.");
            }

            if (user.Email == null || !Regex.IsMatch(user.Email, EmailTemplate))
            {
                errors.Add($"Email '{user.Email}' is not a valid e-mail address.");
            }

            if (user.Password == null || user.Password.Length < PassWordMinLength || user.Password.Length > PasswordMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {PassWordMinLength} and {PasswordMaxLength} characters long.");
            }

            if (user.Password != null && user.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (user.Password != user.ConfirmPassword)
            {
                errors.Add("Password and its confirmation are different.");
            }

           

            return errors;
        }
    }
}
