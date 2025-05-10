using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechXpress.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        [Required(ErrorMessage = "Payment Method is required.")]
        [StringLength(50, ErrorMessage = "Payment Method cannot exceed 50 characters.")]
        public string PaymentMethod { get; set; }
        [Required(ErrorMessage = "Transaction ID is required.")]
        [StringLength(100, ErrorMessage = "Transaction ID cannot exceed 100 characters.")]
        public string TransactionId { get; set; }
        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, 1000000.00, ErrorMessage = "Amount must be between 0.01 and 1,000,000.00.")]
        [DataType(DataType.Currency, ErrorMessage = "Invalid amount format.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Payment Date is required.")]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }
        [Required(ErrorMessage = "Payment Status is required.")]
        [StringLength(50, ErrorMessage = "Payment Status cannot exceed 50 characters.")]
        public string PaymentStatus { get; set; } // e.g., Completed, Pending, Failed

        [ForeignKey("User")]
        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; } // Foreign key to the User
        public User? User { get; set; } // Navigation property to the User

        [ForeignKey("Order")]
        public int OrderId { get; set; } // Foreign key to the Order
        public Order? Order { get; set; } // Navigation property to the Order
    }
}
