namespace Przychodnia.Models
{
    public class Uzytkownik
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string Haslo { get; set; }
        public string Telefon { get; set; }
        public string Pesel { get; set; }
        public string Rodzaj { get; set; }
    }
}
