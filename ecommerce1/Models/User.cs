// Models/User.cs
using ecommerce_net.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;

namespace ecommerce_net.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Role { get; set; } = "user";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}





