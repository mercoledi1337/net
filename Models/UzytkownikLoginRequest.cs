using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Models
{
    public class UzytkownikLoginRequest
    {
        [Required, EmailAddress]
        public string emailAddress { get; set; } = string.Empty;
        [Required]
        public string password { get; set; } = string.Empty;
    }
}
