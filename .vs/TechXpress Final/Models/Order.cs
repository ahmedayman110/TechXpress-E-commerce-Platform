using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechXpress.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Order date is required.")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Total amount is required.")]
        [DataType(DataType.Currency, ErrorMessage = "Invalid price format.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 10000000.00, ErrorMessage = "Total amount must be between 0.01 and 10,000,000.00.")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Order status is required.")]
        [StringLength(50, ErrorMessage = "Order status cannot be longer than 50 characters.")]
        public string OrderStatus { get; set; } // e.g., Pending, Shipped, Delivered, Cancelled

        [ForeignKey("User")]
        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; } // Customer who placed the order
        public User? User { get; set; } // Navigation property to the User who placed the order
        public string? UserAddress { get; set; } // Address where the order will be shipped

        public Payment? Payment { get; set; } // Navigation property to the Payment associated with the order

        public ICollection<OrderItem> OrderItems { get; set; }


    }
}
