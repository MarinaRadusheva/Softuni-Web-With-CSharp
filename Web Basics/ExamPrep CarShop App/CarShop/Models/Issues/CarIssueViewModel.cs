using System.Collections.Generic;

namespace CarShop.Models.Issues
{
    public class CarIssueViewModel
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public ICollection<IssueListingViewModel> Issues { get; set; }
    }
}
