using NewRestaurantAPI.Models.Entities;

namespace NewRestaurantAPI.Models.ViewModels
{
    public class CustomerFoodVM
    {
        public Customer? customer { get; set; }
        public Food? food { get; set; }
    }
}
