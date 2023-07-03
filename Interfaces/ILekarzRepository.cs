using AutoMapper;
using Przychodnia.Dto;
using Przychodnia.Models;
using Przychodnia.Repository;

namespace Przychodnia.Interfaces
{
    public interface ILekarzRepository
    {
        ICollection<Lekarz> GetLekarzs();
        Lekarz GetLekarz(int id);
        ICollection<WizytaLekarza> GetWizyty(IUzytkownikRepository _uzytkownikRepository, IMapper mapper);
        ICollection<LekarzWyswietlaniePacjentow> GetPacjenci(IUzytkownikRepository _uzytkownikRepository);
        bool LekarzExists(int lekId);
    }
}
