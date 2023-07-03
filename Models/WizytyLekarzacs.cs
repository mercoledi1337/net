using Przychodnia.Dto;

namespace Przychodnia.Models
{
    public class WizytaLekarza
    {
        public DateTime Data { get; set; }
        public string status { get; set; } = null!;
        public PacjentDto pacjent { get; set; } = null!;
    }
}
