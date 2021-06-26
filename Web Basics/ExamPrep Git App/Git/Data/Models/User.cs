using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static Git.Data.DataConstants;
namespace Git.Data.Models
{
    public class User
    {

        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(UserMaxLength)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<Repository> Repositories { get; set; } = new List<Repository>();
        public ICollection<Commit> Commits { get; set; } = new List<Commit>();
    }
}
