namespace Przychodnia.Models
{
    public class Wizyta
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public Pacjent Pacjent { get; set; }
        public int TerminId { get; set; }
        public Termin Termin { get; set; } = null!;
        

    }
}
