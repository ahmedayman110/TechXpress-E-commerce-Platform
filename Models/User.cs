using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace TechXpress.Models
{
    public class User
    {
        public enum UserRole
        {
            Admin,
            Customer,
            Vendor
        }

        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Email is required")]
        [EmailAddress (ErrorMessage = "Invalid email address")]
        public string EmailAddress { get; set; }

        public string PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }

        [Phone]
        [StringLength(15, ErrorMessage = "Phone number cannot be longer than 15 characters")]
        public string? PhoneNumber { get; set; } 

        public string? Address { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
