using Microsoft.AspNetCore.Mvc;
using Przychodnia.Interfaces;
using Przychodnia.Models;

namespace Przychodnia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LekController : Controller
    {
        private readonly ILekRepository _lekRepository;

        public LekController(ILekRepository lekRepository)
        {
            _lekRepository = lekRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lek>))]
        public IActionResult GetLeks()
        {
            var leks = _lekRepository.GetLeks();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(leks);
        }

        [HttpGet("lekiId")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lek>))]
        public IActionResult GetLek(int nazwa)
        {
            var lek = _lekRepository.GetLek(nazwa);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(lek);
        }
    }
}
