using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Data;

namespace Przychodnia.Models
{
    public class Uzytkownik
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public string Telefon { get; set; }
        public string Pesel { get; set; }
        public string Rodzaj { get; set; } = "pacjent";
    }
}
