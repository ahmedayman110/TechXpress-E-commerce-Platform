using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechXpress.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 1000000.00, ErrorMessage = "Price must be between 0.01 and 1,000,000.00.")]
        [DataType(DataType.Currency, ErrorMessage = "Invalid price format.")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        [ForeignKey("Order")]
        [Required(ErrorMessage = "Order ID is required.")]
        public int OrderId { get; set; } // Foreign key to the Order
        public Order? Order { get; set; } // Navigation property to the Order

        [ForeignKey("Product")]
        [Required(ErrorMessage = "Product ID is required.")]
        public int ProductId { get; set; } // Foreign key to the Product
        public Product? Product { get; set; } // Navigation property to the Product
    }
}
