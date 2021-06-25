using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static CarShop.Data.DataConstatnts;

namespace CarShop.Data.Models
{
    public class Car
    {
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(StringMaxLength)]
        public string Model { get; set; }

        public int Year { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        [RegularExpression(@"[A-Z]{2}[0-9]{4}[A-Z]{2}")]
        public string PlateNumber { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<Issue> Issues { get; set; } = new List<Issue>();

    }
}
