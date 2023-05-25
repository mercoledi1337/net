namespace Przychodnia.Models
{
    public class LekRecepta
    {
        public int LekId { get; set; }
        public int ReceptaId { get; set; }
        public Lek Lek { get; set; }
        public Recepta Recepta { get; set; }

    }
}
