using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Data;
using Przychodnia.Dto;
using Przychodnia.Interfaces;
using Przychodnia.Models;
using System.Security.Claims;

namespace Przychodnia.Repository
{
    public class UzytkownikRepository : IUzytkownikRepository
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UzytkownikRepository(DataContext context, IHttpContextAccessor httpContextAccessor) 
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetEmail()
        {
            var result = string.Empty;
            if(_httpContextAccessor.HttpContext != null) 
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            }

            return result;
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

        public ICollection<WizytaUzytkownika> GetWizyty(IMapper _mapper)
        {
            var userEmail = GetEmail();

            var wizyty = _mapper.Map<List<WizytaDto>>(_context.Wizyty
                .Where(u => u.Pacjent.Uzytkownik.Email == userEmail)
                .ToList());

            var wizytyUzytkownika = new List<WizytaUzytkownika>();

            foreach (var wizyta in wizyty)
            {
                var data1 = _context.Terminy
                    .Where(t => t.Id == wizyta.TerminId)
                    .FirstOrDefault();

                var lekarz = _mapper.Map<LekarzDto>(_context.Lekarze
                    .Include(p => p.Uzytkownik)
                    .Where(l => l.Id == data1.LekarzId)
                    .FirstOrDefault());

                var pacjent = _context.Pacjenci
                    .Where(p => p.Id == wizyta.PacjentId)
                    .FirstOrDefault();

                var wizytaUzytkownika = new WizytaUzytkownika
                {
                    Data = data1.Data,
                    status = wizyta.Status,
                    Lekarz = lekarz
                };

                wizytyUzytkownika.Add(wizytaUzytkownika);
            }

            return wizytyUzytkownika;
        }

        public bool UzytkownikExists(int uzytkownikId)
        {
            return _context.Uzytkownicy.Any(u => u.Id == uzytkownikId);
        }

        
    }
}
