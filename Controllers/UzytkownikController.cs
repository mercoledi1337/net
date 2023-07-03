using AutoMapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
using Org.BouncyCastle.Bcpg;
using Przychodnia.Data;
using Przychodnia.Dto;
using Przychodnia.Interfaces;
using Przychodnia.Models;
using Przychodnia.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Przychodnia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UzytkownikController : Controller
    {
        private readonly IUzytkownikRepository _uzytkownikRepository;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UzytkownikController(IUzytkownikRepository uzytkownikRepository,
            DataContext context,
            IConfiguration configuration,
            IMapper mapper)
        {
            _uzytkownikRepository = uzytkownikRepository;
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpGet("jwt-return-email"), Authorize]
        public ActionResult<string> GetMe()
        {
            var userEmail = _uzytkownikRepository.GetEmail();
            return Ok(userEmail);

        }

        [HttpPost("zapisz-wizyte"), Authorize]
        public async Task<IActionResult> ZapiszWizyte(ZapiszWizyteRequest request)
        {
            var userEmail = _uzytkownikRepository.GetEmail();
            var pacjent = await _context.Pacjenci
                .Include(p => p.Uzytkownik)
                .Where(p => p.Uzytkownik.Email == userEmail)
                .FirstOrDefaultAsync();

            // Sprawdź, czy pacjent istnieje
            if (pacjent == null)
            {
                return BadRequest("Nie znaleziono pacjenta.");
            }

            // Sprawdź, czy termin istnieje
            var termin = new Termin
            {
                Data = request.data,
                LekarzId = request.LekarzId
            };

            // Sprawdź, czy termin jest dostępny
            _context.Terminy.Add(termin);
            await _context.SaveChangesAsync();

            // Utwórz nową wizytę
            var wizyta = new Wizyta
            {
                Status = "Zaplanowana",
                PacjentId = pacjent.Id,
                TerminId = termin.Id
            };
            _context.Wizyty.Add(wizyta);
            await _context.SaveChangesAsync();
            // Dodaj wizytę do kontekstu danych


            return Ok("Wizyta została zapisana.");
        }

        [HttpPut("Visits/{id}/Cancel"), Authorize]
        public async Task<IActionResult> OdwolajWizyte(int id)
        {

            var wizytaOdwolanie = await _context.Wizyty
                .Include(p => p.Termin)
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();

            if (wizytaOdwolanie == null)
            {
                return BadRequest("nie ma takiej wizyty.");
            }

            _context.Wizyty.Remove(wizytaOdwolanie);
            await _context.SaveChangesAsync();

            _context.Terminy.Remove(wizytaOdwolanie.Termin);
            
            await _context.SaveChangesAsync();
            return Ok("Wizyta odwołana.");
        }

        [HttpGet("lekarze")]
        public IActionResult GetPacjenci()
        {
            var lekarze = _context.Lekarze
                .OrderBy(l => l.Id)
                .ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(lekarze);
        }

        [HttpGet("lekarze/{id}")]
        public IActionResult GetLekarz(string id)
        {
            var lekarze = _context.Lekarze
                .Include(u => u.Uzytkownik)
                .Where(u => u.Uzytkownik.Email == id)
                .FirstOrDefault();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(lekarze);
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Uzytkownik>))]
        public IActionResult GetUzytkowniks()
        {
            var uzytkowniks = _uzytkownikRepository.GetUzytkowniks();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(uzytkowniks);
        }

        [HttpGet("Dane-personalne"), Authorize]
        public IActionResult GetDane()
        {
            var userEmail = _uzytkownikRepository.GetEmail();

            var pacjent = _mapper.Map<PacjentDto>(_context.Pacjenci
                .Include(p => p.Uzytkownik)
                .Where(p => p.Uzytkownik.Email == userEmail)
                .FirstOrDefault());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pacjent);
        }

        [HttpGet("Dane-personalne/{id}"), Authorize]
        public IActionResult GetDane1()
        {
            var userEmail = _uzytkownikRepository.GetEmail();

            var pacjent = _mapper.Map<PacjentDto>(_context.Pacjenci
                .Include(p => p.Uzytkownik)
                .Where(p => p.Uzytkownik.Email == userEmail)
                .FirstOrDefault());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pacjent);
        }

        [HttpGet("Patients/{id}"), Authorize]
        public IActionResult recepty()
        {
            var userEmail = _uzytkownikRepository.GetEmail();

            var pacjent = _mapper.Map<PacjentDto>(_context.Pacjenci
                .Include(p => p.Uzytkownik)
                .Where(p => p.Uzytkownik.Email == userEmail)
                .FirstOrDefault());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pacjent);
        }

        [HttpGet("Patients/{id}/Visits"), Authorize]
        public IActionResult GetWizyty()
        {
            var wizytyUzytkownika = _uzytkownikRepository.GetWizyty(_mapper);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wizytyUzytkownika);
        }

        [HttpGet("terminy"), Authorize]
        public IActionResult GetTerminy()
        {
            var terminy = _context.Terminy.ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(terminy);
        }

        [HttpPost("rejestracja")]
        public async Task<IActionResult> Register(UzytkownikRegisterRequest request)
        {
            if(_context.Uzytkownicy.Any(u => u.Email == request.emailAddress))
            {
                return BadRequest("user already exists.");
            }

            CreatePasswordHash(request.password,
                out byte[] passwordHash,
                out byte[] passwordSalt);

            var uzytkownik = new Uzytkownik
            {
                Imie = request.firstName,
                Nazwisko = request.lastName,
                Telefon = request.phoneNumber,
                Pesel = request.pesel,
                Rodzaj = "Patient",
                Email = request.emailAddress,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
                
            };

            var pacjent = new Pacjent
            {
                PrzyjmowaneLeki = string.Empty,
                Alergie = string.Empty,
                Miejscowosc = request.city,
                KodPocztowy = request.postalCode,
                Ulica = request.streetAddress,
                Uzytkownik = uzytkownik
               
                
            };

            _context.Uzytkownicy.Add(uzytkownik);
            await _context.SaveChangesAsync();

            _context.Pacjenci.Add(pacjent);
            await _context.SaveChangesAsync();

            return Ok("Uzytkownik stworzony");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UzytkownikLoginRequest request)
        {
            var uzytkownik = await _context.Uzytkownicy.FirstOrDefaultAsync(u => u.Email ==  request.emailAddress);
            if(uzytkownik == null)
            {
                return BadRequest("Złe hasło lub email");
            }

            if(!VerifyPasswordHash(request.password, uzytkownik.PasswordHash, uzytkownik.PasswordSalt))
            {
                return BadRequest("Złe hasło lub email1");
            }

            string token = CreateToken(uzytkownik);
            return Ok(token);
        }

        [HttpPut("edytuj-haslo"), Authorize]
        public async Task<IActionResult> EdytujHaslo(EdytujHaslo request)
        {

            var userEmail = _uzytkownikRepository.GetEmail();

            var pacjent = await _context.Pacjenci
                .Include(p => p.Uzytkownik)
                .Where(p => p.Uzytkownik.Email == userEmail)
                .FirstOrDefaultAsync();

            if(request.password == request.repeatPassword)
            {
                CreatePasswordHash(request.password,
               out byte[] passwordHash,
               out byte[] passwordSalt);

                pacjent.Uzytkownik.PasswordHash = passwordHash;
                pacjent.Uzytkownik.PasswordSalt = passwordSalt;
                await _context.SaveChangesAsync();

                return Ok("password reset");
            } else
            {
                return BadRequest("Hasła się różnią");
            }

           
             
           
        }
        [HttpPut("edytuj-profil"), Authorize]
        public async Task<IActionResult> EdytujProfil(EdytujProfilRequest request)
        {
            var userEmail = _uzytkownikRepository.GetEmail();

            var pacjent = await _context.Pacjenci
                .Include(p => p.Uzytkownik)
                .Where(p => p.Uzytkownik.Email == userEmail)
                .FirstOrDefaultAsync();
                

            if (pacjent == null)
            {
                return BadRequest("Nie znaleziono pacjenta.");
            }
            if (!string.IsNullOrEmpty(request.firstName))
            {
                pacjent.Uzytkownik.Imie = request.firstName;
            }
            if (!string.IsNullOrEmpty(request.lastName))
            {
                pacjent.Uzytkownik.Nazwisko = request.lastName;
            }
            if (!string.IsNullOrEmpty(request.phoneNumber))
            {
                pacjent.Uzytkownik.Telefon = request.phoneNumber;
            }
            if (!string.IsNullOrEmpty(request.PrzyjmowaneLeki))
            {
                pacjent.PrzyjmowaneLeki = request.PrzyjmowaneLeki;
            }
            if (!string.IsNullOrEmpty(request.Alergie))
            {
                pacjent.Alergie = request.Alergie;
            }
            if (!string.IsNullOrEmpty(request.city))
            {
                pacjent.Miejscowosc = request.city;
            }
            if (!string.IsNullOrEmpty(request.postalCode))
            {
                pacjent.KodPocztowy = request.postalCode;
            }
            if (!string.IsNullOrEmpty(request.street))
            {
                pacjent.Ulica = request.street;
            }
            await _context.SaveChangesAsync();

            return Ok("Profil został zaktualizowany.");
        }

        [HttpPost("zapomniane-haslo")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var uzytkownik = await _context.Uzytkownicy.FirstOrDefaultAsync(u => u.Email == email);
            if (uzytkownik == null)
            {
                return BadRequest("nie znaleziono");
            }

            uzytkownik.PasswordResetToken = CreateRandomToken();
            uzytkownik.ResetTokenExpires = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            var email1 = new MimeMessage();
            email1.From.Add(MailboxAddress.Parse("sklepgrubazyla@wp.pl"));
            email1.To.Add(MailboxAddress.Parse(email));
            email1.Subject = "Reset hasła";
            email1.Body = new TextPart(TextFormat.Html) { Text = uzytkownik.PasswordResetToken };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.wp.pl", 465, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate("sklepgrubazyla@wp.pl", "projappweb");
            smtp.Send(email1);
            smtp.Disconnect(true);

            
            return Ok("you may now reset your passwrod");
        }

        [HttpPost("reset-hasla")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var uzytkownik = await _context.Uzytkownicy.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            if (uzytkownik == null || uzytkownik.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Zly token");
            }

            CreatePasswordHash(request.Password,
                out byte[] passwordHash,
                out byte[] passwordSalt);

            uzytkownik.PasswordHash = passwordHash;
            uzytkownik.PasswordSalt = passwordSalt;
            uzytkownik.PasswordResetToken = null;
            uzytkownik.ResetTokenExpires = null;

            await _context.SaveChangesAsync();

            return Ok("password reset");
        }


        private string CreateToken(Uzytkownik uzytkownik)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, uzytkownik.Email),
                new Claim(ClaimTypes.Name, uzytkownik.Imie),
                new Claim(ClaimTypes.Surname, uzytkownik.Nazwisko),
                new Claim(ClaimTypes.Role, uzytkownik.Rodzaj)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password,
                out byte[] passwordHash,
                out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password,
                    byte[] passwordHash,
                    byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
