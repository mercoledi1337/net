using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Przychodnia.Models;

namespace Przychodnia.Interfaces
{
    public interface IUzytkownikRepository
    {
        ICollection<Uzytkownik> GetUzytkowniks();
        Uzytkownik GetUzytkownik(int id);
        Uzytkownik GetUzytkownikPoPeselu(string pesel);
        public ICollection<WizytaUzytkownika> GetWizyty(IMapper _mapper);
        string GetEmail();
        bool UzytkownikExists(int uzytkownikId);
        
    }
}
