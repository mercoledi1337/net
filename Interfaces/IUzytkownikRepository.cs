using Przychodnia.Models;

namespace Przychodnia.Interfaces
{
    public interface IUzytkownikRepository
    {
        ICollection<Uzytkownik> GetUzytkowniks();
        Uzytkownik GetUzytkownik(int id);
        Uzytkownik GetUzytkownikPoPeselu(string pesel);
        bool UzytkownikExists(int uzytkownikId);
    }
}
