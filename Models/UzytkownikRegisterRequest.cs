using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Models
{
    public class UzytkownikRegisterRequest
    {
        [Required, EmailAddress]
        public string emailAddress { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string password { get; set; } = string.Empty;
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string pesel { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string streetAddress { get; set; }
    }
}
