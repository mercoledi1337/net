using Przychodnia.Data;
using Przychodnia.Interfaces;
using Przychodnia.Models;

namespace Przychodnia.Repository
{
    public class UzytkownikRepository : IUzytkownikRepository
    {
        private readonly DataContext _context;

        public UzytkownikRepository(DataContext context) 
        {
            _context = context;
        }

        public Uzytkownik GetUzytkownik(int id)
        {
            return _context.Uzytkownicy.Where(u => u.Id == id).FirstOrDefault();
        }

        public Uzytkownik GetUzytkownikPoPeselu(string pesel)
        {
            return _context.Uzytkownicy.Where(u => u.Pesel == pesel).FirstOrDefault();
        }

        public ICollection<Uzytkownik> GetUzytkowniks() 
        {
            return _context.Uzytkownicy.OrderBy(u => u.Id).ToList();
        }

        public bool UzytkownikExists(int uzytkownikId)
        {
            return _context.Uzytkownicy.Any(u => u.Id == uzytkownikId);
        }
    }
}
