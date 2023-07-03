using Przychodnia.Dto;

namespace Przychodnia.Models
{
    public class WizytaUzytkownika
    {
        public DateTime Data { get; set; }
        public LekarzDto Lekarz { get; set; } = null!;
        public string status { get; set; } = null!;

        
    }
}
