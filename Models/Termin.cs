namespace Przychodnia.Models
{
    public class Termin
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public Lekarz Lekarz { get; set; } = null!;

        
        
    }
}
