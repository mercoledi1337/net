using Przychodnia.Models;

namespace Przychodnia.Dto
{
    public class WizytaDto
    {
        public string Status { get; set; }
        public int PacjentId { get; set; }
        public int TerminId { get; set; }
    }
}
