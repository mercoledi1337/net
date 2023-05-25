namespace Przychodnia.Models
{
    public class Pacjent
    {
        public int Id { get; set; }
        public string PrzyjmowaneLeki { get; set; }
        public string Alergie { get; set; }
        public string Miejscowosc { get; set; }
        public string KodPocztowy { get; set; }
        public string Ulica { get; set; }
        public Uzytkownik Uzytkownik { get; set; }
        public ICollection<Wizyta> Wizytas { get; set; }
        public ICollection<Recepta> Receptas { get; set; }

    }
}
