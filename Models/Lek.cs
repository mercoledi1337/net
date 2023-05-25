namespace Przychodnia.Models
{
    public class Lek
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Refundacja { get; set; }
        public ICollection<LekRecepta> LekiRecepty { get; set; }
    } 
}
