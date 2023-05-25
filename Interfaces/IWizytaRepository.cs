using Przychodnia.Models;

namespace Przychodnia.Interfaces
{
    public interface IWizytaRepository
    {
        ICollection<Wizyta> GetWizytas();
        Wizyta GetWizyta(int id);
        bool WizytaExists(int wizytaId);
    }
}
