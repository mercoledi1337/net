using Przychodnia.Dto;

namespace Przychodnia.Models
{
    public class Lekarz
    {
        public int Id { get; set; }
        public int KosztWizyty { get; set; }
        public string Specjalizacja { get; set; }
        public Uzytkownik Uzytkownik { get; set; }
        public ICollection<Termin> Terminy { get; set; }
        
    }
}
