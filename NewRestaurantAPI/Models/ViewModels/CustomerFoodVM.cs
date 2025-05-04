using NewRestaurantAPI.Models.Entities;

namespace NewRestaurantAPI.Models.ViewModels
{// this will be used to be referenced in the CustomerFoodController.
    public class CustomerFoodVM
    {
        public Customer? customer { get; set; }
        public Food? food { get; set; }
    }
}
