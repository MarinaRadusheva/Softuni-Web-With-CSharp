using System;
using System.ComponentModel.DataAnnotations;

using static Git.Data.DataConstants;

namespace Git.Data.Models
{
    public class Commit
    {
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [MaxLength(IdMaxLength)]
        public string CreatorId { get; set; }

        public User Creator { get; set; }

        [Required]
        [MaxLength(IdMaxLength)]
        public string RepositoryId { get; set; }

        public Repository Repository { get; set; }
    }
}
