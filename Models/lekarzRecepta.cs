namespace Przychodnia.Models
{
    public class lekarzRecepta
    {
        public int patientId { get; set; }
        public int doctorId { get; set; }
        public string recommendations { get; set; }
        public string dosage { get; set; }
        public List<string> drug { get; set; }
    }
}
