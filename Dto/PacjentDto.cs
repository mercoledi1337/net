using Przychodnia.Models;

namespace Przychodnia.Dto
{
    public class PacjentDto
    {
        public string PrzyjmowaneLeki { get; set; }
        public string Alergie { get; set; }
        public string Miejscowosc { get; set; }
        public string KodPocztowy { get; set; }
        public string Ulica { get; set; }
        public UzytkownikDto Uzytkownik { get; set; }
    }
}
