using System.ComponentModel.DataAnnotations;

namespace CoreLogin.Models
{
    public class User
    {
        [Key] // Define primary key attribute
        public int Id { get; set; } // Example property for primary key
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
    }
}
