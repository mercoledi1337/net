using Przychodnia.Data;
using Przychodnia.Interfaces;
using Przychodnia.Models;

namespace Przychodnia.Repository
{
    public class LekarzRepository : ILekarzRepository
    {
        private readonly DataContext _context;

        public LekarzRepository(DataContext context)
        {
            _context = context;
        }

        public Lekarz GetLekarz(int id)
        {
            return _context.Lekarze.Where(l => l.Id == id).FirstOrDefault();
        }

        public Lekarz GetLekarzNazwisko(string nazwisko)
        {
            return _context.Lekarze.Where(l => l.Specjalizacja == nazwisko).FirstOrDefault();
        }

        public ICollection<Lekarz> GetLekarzs()
        {
            return _context.Lekarze.OrderBy(l => l.Id).ToList();
        }

        public bool LekarzExists(int lekId)
        {
            throw new NotImplementedException();
        }
    }
}
