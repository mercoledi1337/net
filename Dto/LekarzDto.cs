namespace Przychodnia.Dto
{
    public class LekarzDto
    {
        public int Id { get; set; }
        public int KosztWizyty { get; set; }
        public string Specjalizacja { get; set; }
        public UzytkownikDto Uzytkownik { get; set; }
    }
}
