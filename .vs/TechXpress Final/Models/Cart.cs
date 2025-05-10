using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechXpress.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 1000000.00, ErrorMessage = "Price must be between 0.01 and 1,000,000.00.")]
        [DataType(DataType.Currency, ErrorMessage = "Invalid price format.")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        [ForeignKey("User")]
        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; } // Foreign key to the User
        public User? User { get; set; } // Navigation property to the User

        [ForeignKey("Product")]
        public int ProductId { get; set; } // Foreign key to the Product
        public Product? Product { get; set; } // Navigation property to the Product

        public ICollection<CartItem> CartItems { get; set; } // A Cart can have many CartItems.

    }
}
