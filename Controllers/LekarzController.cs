using AutoMapper;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MimeKit.Text;
using MimeKit;
using Przychodnia.Data;
using Przychodnia.Dto;
using Przychodnia.Interfaces;
using Przychodnia.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Przychodnia.Repository;
using Microsoft.EntityFrameworkCore;

namespace Przychodnia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LekarzController : Controller
    {
        private readonly IUzytkownikRepository _uzytkownikRepository;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILekarzRepository _lekarzRepository;

        public LekarzController(IUzytkownikRepository uzytkownikRepository,
            DataContext context,
            IConfiguration configuration,
            IMapper mapper,
            ILekarzRepository lekarzRepository)
        {
            _uzytkownikRepository = uzytkownikRepository;
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
            _lekarzRepository = lekarzRepository;
        }

        [HttpGet("wizyty"), Authorize]
        public IActionResult GetWizyty()
        {
            var wizyty = _lekarzRepository.GetWizyty(_uzytkownikRepository, _mapper);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wizyty);
        }

        [HttpGet("pacjenci")]
        public IActionResult GetPacjenci()
        {
            var pacjenci = _lekarzRepository.GetPacjenci(_uzytkownikRepository);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pacjenci);
        }

        [HttpPut("odwolaj-wizyte-pacjenta"), Authorize]
        public async Task<IActionResult> OdwolajWizyte(OdwolanieWizyta request)
        {

            var wizytaOdwolanie = await _context.Wizyty
                .Include(p => p.Termin)
                .Where(w => w.Id == request.WizytaId)
                .FirstOrDefaultAsync();

            if (wizytaOdwolanie == null)
            {
                return BadRequest("nie ma takiej wizyty.");
            }

            wizytaOdwolanie.Status = "odwołana";
            await _context.SaveChangesAsync();
            return Ok("Wizyta odwołana.");
        }

        [HttpPut("Visits/{$id}/Confirm"), Authorize]
        public async Task<IActionResult> PotwierdzWizyte(int id)
        {

            var wizytaPodtwierdzenie = await _context.Wizyty
                .Include(p => p.Termin)
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();

            if (wizytaPodtwierdzenie == null)
            {
                return BadRequest("nie ma takiej wizyty.");
            }

            wizytaPodtwierdzenie.Status = "Potwierdzona";
            await _context.SaveChangesAsync();
            return Ok("Wizyta Potwierdzona.");
        }

        [HttpPost("wypiszRecepte"), Authorize]
        public async Task<IActionResult> WypiszRecepteAsync(lekarzRecepta reqest)
        {

            if (reqest == null)
            {
                return BadRequest("Request is null");
            }

            var userEmail = _uzytkownikRepository.GetEmail(); // email żeby wiedzieć jaki jest teraz lekarz

            var pacjentW = _context.Pacjenci
                .Include(p => p.Wizytas)
                .FirstOrDefault(p => p.Id == reqest.patientId); // sprawdzamy wizyty w których jest Id tego pacjenta ale jest jeszcze źle troche

            if (pacjentW == null)
            {
                return BadRequest("Pacjent not found");
            }

            var lekarz = await _context.Lekarze
                .Include(p => p.Uzytkownik)
                .FirstOrDefaultAsync(p => p.Uzytkownik.Email == _uzytkownikRepository.GetEmail());

            if (lekarz == null)
            {
                return BadRequest(_uzytkownikRepository.GetEmail());
            }

            // id pacjenta brane z guzika przypisać dla guzika id pacjenta!
            var recepta = new Recepta
            {
                DataWystawienia = DateTime.UtcNow,
                KodRecepty = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
                Wystawiajacy = lekarz.Uzytkownik.Imie + " " + lekarz.Uzytkownik.Nazwisko,
                Zalecenia = reqest.recommendations,
                Dawkowanie = reqest.dosage,
                PacjentId = reqest.patientId,
            };

            // lekarz wyszukuje sobie leki, klika i pobierana jest nazwa do reqesta trzeba liste
            foreach (var lekNazwa in reqest.drug)
            {
                var lek = _context.Leki.FirstOrDefault(l => l.Nazwa == lekNazwa);

                var lr = new LekRecepta
                {
                    Lek = lek,
                    Recepta = recepta
                };               
                if (lr != null )
                    _context.LekiRecepty.Add(lr);               
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            _context.Recepty.Add(recepta);
            await _context.SaveChangesAsync();

            return Ok("git");
        }
    }
}