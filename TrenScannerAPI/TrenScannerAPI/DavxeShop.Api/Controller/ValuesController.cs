using DavxeShop.Library.Services.Interfaces;
using DavxeShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace DavxeShop.Api.Controller
{
    [Route("api")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ISeleniumService _seleniumService;
        private readonly ICSVProcessorService _csvProcessorService;
        public ValuesController(ISeleniumService seleniumScript, ICSVProcessorService csvProcessorService)
        {
            _seleniumService = seleniumScript;
            _csvProcessorService = csvProcessorService;
        }

        [HttpPost("TrenData")]
        public IActionResult PostFlightData([FromBody] TrenData trenData)
        {
            if (trenData == null)
            {
                return BadRequest("Los datos del vuelo son inválidos.");
            }

            _seleniumService.GenerateSeleniumScript(trenData);
            _csvProcessorService.ImportarTrenesDesdeCsv(trenData);

            return Ok(new
            {
                message = "Datos de vuelo recibidos correctamente",
            });
        }
    }
}
