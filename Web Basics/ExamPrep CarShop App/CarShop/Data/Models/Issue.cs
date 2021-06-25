using System;
using System.ComponentModel.DataAnnotations;

using static CarShop.Data.DataConstatnts;

namespace CarShop.Data.Models
{
    public class Issue
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsFixed { get; set; }

        [Required]
        public string CarId { get; set; }

        public Car Car { get; set; }
    }
}
