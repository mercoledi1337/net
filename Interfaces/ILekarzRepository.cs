using Przychodnia.Models;

namespace Przychodnia.Interfaces
{
    public interface ILekarzRepository
    {
        ICollection<Lekarz> GetLekarzs();
        Lekarz GetLekarz(int id);
       
        bool LekarzExists(int lekId);
    }
}
