using Przychodnia.Models;

namespace Przychodnia.Interfaces
{
    public interface ILekRepository
    {
        ICollection<Lek> GetLeks();
        Lek GetLek(int id);
        Lek GetLekNazwa(string nazwa);
        bool LekExists(int lekId);
    }
}
