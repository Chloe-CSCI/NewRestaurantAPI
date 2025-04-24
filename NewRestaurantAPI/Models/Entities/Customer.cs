using System.ComponentModel.DataAnnotations;

namespace NewRestaurantAPI.Models.Entities
{
    public class Customer
    {// This is the information that the customer will give.
        public int Id { get; set; }
        [StringLength(32)]
        public string Name { get; set; } = string.Empty;
        [StringLength(32)]
        public string PhoneNumber { get; set; } = string.Empty;
        public int ReservationNumber { get; set; }
        public ICollection<CustomerFood> CustomersFood { get; set; }
        = new List<CustomerFood>();
    }
}
