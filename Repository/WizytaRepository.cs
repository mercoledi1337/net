using Przychodnia.Data;
using Przychodnia.Interfaces;
using Przychodnia.Models;

namespace Przychodnia.Repository
{
    public class WizytaRepository : IWizytaRepository
    {
        private readonly DataContext _context;

        public WizytaRepository(DataContext context)
        {
            _context = context;
        }
        public Wizyta GetWizyta(int id)
        {
            return _context.Wizyty.Where(w => w.Id == id).FirstOrDefault();
        }

        public ICollection<Wizyta> GetWizytas()
        {
            return _context.Wizyty.OrderBy(w => w.Id).ToList();
        }

        public bool WizytaExists(int wizytaId)
        {
            return _context.Wizyty.Any(u => u.Id == wizytaId);
        }
    }
}
