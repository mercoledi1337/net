using Microsoft.AspNetCore.Mvc;
using Przychodnia.Interfaces;
using Przychodnia.Models;

namespace Przychodnia.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class WizytaController : Controller
    {
        private readonly IWizytaRepository _wizytaRepository;

        public WizytaController(IWizytaRepository wizytaRepository)
        {
            _wizytaRepository = wizytaRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Wizyta>))]
        public IActionResult GetWizytas()
        {
            var wizytas = _wizytaRepository.GetWizytas();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wizytas);
        }
    }
    
}
