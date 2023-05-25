using Microsoft.AspNetCore.Mvc;
using Przychodnia.Interfaces;
using Przychodnia.Models;

namespace Przychodnia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UzytkownikController : Controller
    {
        private readonly IUzytkownikRepository _uzytkownikRepository;

        public UzytkownikController(IUzytkownikRepository uzytkownikRepository)
        {
            _uzytkownikRepository = uzytkownikRepository;
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
    }
}
