using Przychodnia.Data;
using Przychodnia.Interfaces;
using Przychodnia.Models;

namespace Przychodnia.Repository
{
    public class LekRepository : ILekRepository
    {
        private readonly DataContext _context;

        public LekRepository(DataContext context)
        {
            _context = context;
        }
        public Lek GetLek(int id)
        {
            return _context.Leki.Where(l => l.Id == id).FirstOrDefault();
        }

        public Lek GetLekNazwa(string nazwa)
        {
            return _context.Leki.Where(l => l.Nazwa == nazwa).FirstOrDefault();
        }

        public ICollection<Lek> GetLeks()
        {
            return _context.Leki.OrderBy(l => l.Id).ToList();
        }

        public bool LekExists(int lekId)
        {
            return _context.Leki.Any(u => u.Id == lekId);
        }
    }
}
