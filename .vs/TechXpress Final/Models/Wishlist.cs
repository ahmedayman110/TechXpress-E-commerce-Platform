using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechXpress.Models
{
    public class Wishlist
    {
        [Key]
        public int WishlistId { set; get; }

        [ForeignKey("User")]
        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; } // Foreign key to the User
        public User? User { get; set; } // Navigation property to the User

        [ForeignKey("Product")]
        [Required(ErrorMessage = "Product ID is required.")]
        public int ProductId { get; set; } // Foreign key to the Product
        public Product? Product { get; set; } // Navigation property to the Product
    }
}
