using System.ComponentModel.DataAnnotations;

namespace WebSimba.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}