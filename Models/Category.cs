using System.ComponentModel.DataAnnotations;

namespace TechXpress.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, ErrorMessage = "Category name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public ICollection<Product>? Products { get; set; }


    }
}
