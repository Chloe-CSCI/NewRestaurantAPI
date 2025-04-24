namespace NewRestaurantAPI.Models.Entities
{
    public class Transactions
    {// This is where the customer will put how much money they paid, the price of the food, and the total amount for both.
        public int Id { get; set; }

        public float CustomersMoney { get; set; }

        public float FoodPrice { get; set; }

        public float TotalPrice { get; set; }
        public ICollection<CustomerFood> CustomersFood { get; set; }
       = new List<CustomerFood>();
    }
}
