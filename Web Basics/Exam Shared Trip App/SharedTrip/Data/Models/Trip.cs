using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static SharedTrip.Data.DataConstants;

namespace SharedTrip.Data.Models
{
    public class Trip
    {
        [Key]
        [Required][MaxLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string StartPoint { get; set; }
        [Required]
        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }
        public int Seats { get; set; }

        [Required]
        [MaxLength(TripDescriptionMaxLength)]
        public string Description { get; set; }

        public string ImagePath { get; set; }
        public ICollection<UserTrip> UserTrips { get; set; } = new HashSet<UserTrip>();
    }
}
