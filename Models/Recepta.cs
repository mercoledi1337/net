namespace Przychodnia.Models
{
    public class Recepta
    {
        public int Id { get; set; }
        public DateTime DataWystawienia { get; set; }
        public string KodRecepty { get; set; }
        public string Wystawiajacy { get; set; }
        public string Zalecenia { get; set; }
        public string Dawkowanie { get; set; }
        public int PacjentId { get; set; }
        public Pacjent Pacjent { get; set; }
        public ICollection<LekRecepta> LekiRecepty { get; set; }
    }
}
