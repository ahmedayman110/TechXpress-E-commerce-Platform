using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechXpress.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string? Comment { get; set; }

        public DateTime ReviewDateTime { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public int UserId { get; set; } // Foreign key to the User
        public User? User { get; set; } // Navigation property to the User

        [ForeignKey("Product")]
        public int ProductId { get; set; } // Foreign key to the Product
        public Product? Product { get; set; } // Navigation property to the Product


    }
}
