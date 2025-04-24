using System.ComponentModel.DataAnnotations;

namespace NewRestaurantAPI.Models.Entities
{
    public class Food
    {//This is where the food and its information that the customer wants will be.
        public int Id { get; set; }
        [StringLength(50)]
        public string FoodName { get; set; } = string.Empty;
        [StringLength(32)]
        public string Description { get; set; } = string.Empty;
        public string Ingredients { get; set; } = string.Empty;
        public ICollection<CustomerFood> CustomerFoods { get; set; }
            = new List<CustomerFood>();
    }
}
