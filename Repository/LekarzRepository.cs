using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Data;
using Przychodnia.Dto;
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

        public ICollection<LekarzWyswietlaniePacjentow> GetPacjenci(IUzytkownikRepository _uzytkownikRepository)
        {
            var pacjenci = _context.Uzytkownicy
                .OrderBy(l => l.Id)
                .ToList();

            var pacjenciDto = new List<LekarzWyswietlaniePacjentow>();
            foreach (var pacjent in pacjenci)
            {


                var IdPacjenta = new LekarzWyswietlaniePacjentow
                {
                    Imie = pacjent.Imie,
                    Nazwisko = pacjent.Nazwisko,
                    IdPacjent = pacjent.Id
                };

                pacjenciDto.Add(IdPacjenta);
            }

            return pacjenciDto;
        }

        public ICollection<WizytaLekarza> GetWizyty(IUzytkownikRepository _uzytkownikRepository, IMapper _mapper)
        {
            var userEmail = _uzytkownikRepository.GetEmail();

            var wizyty = _context.Wizyty
                .Where(u => u.Termin.Lekarz.Uzytkownik.Email == userEmail)
                .ToList();

            var wizytyUzytkownika = new List<WizytaLekarza>();

            foreach (var wizyta in wizyty)
            {
                var data1 = _context.Terminy
                    .Where(t => t.Id == wizyta.TerminId)
                    .FirstOrDefault();

                var pacjent = _mapper.Map<PacjentDto>(_context.Pacjenci
                    .Where(p => p.Id == wizyta.PacjentId)
                    .FirstOrDefault());

                var wizytaLekarza = new WizytaLekarza
                {
                    Data = data1.Data,
                    status = wizyta.Status,
                    pacjent = pacjent
                };

                wizytyUzytkownika.Add(wizytaLekarza);
            }

           

            return wizytyUzytkownika;
        }

        public bool LekarzExists(int lekId)
        {
            throw new NotImplementedException();
        }
    }
}
