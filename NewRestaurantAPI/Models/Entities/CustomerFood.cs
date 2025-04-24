using System.ComponentModel.DataAnnotations;

namespace NewRestaurantAPI.Models.Entities
{
    public class CustomerFood
    { //This is where the food and the customer entities will come together.
        public int Id { get; set; }
        [StringLength(32, MinimumLength = 10)]
        public string menuItem { get; set; } = string.Empty;

        public int customerId { get; set; }
        public Customer? customer { get; set; }

        public int FoodId { get; set; }
        public Food? food { get; set; }
    }
}
