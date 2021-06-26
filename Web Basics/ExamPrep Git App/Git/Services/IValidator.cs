using Git.Models.Repositories;
using Git.Models.Users;
using Git.Models.Commits;
using System.Collections.Generic;

namespace Git.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel user);

        ICollection<string> ValidateRepository(CreateRepoFormModel model);

        ICollection<string> ValidateCommit(CreateCommitFormModel model);
    }
}
