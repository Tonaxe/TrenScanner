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
        private readonly ITrenService _trenService;
        private readonly IUserService _userService;

        public ValuesController(ISeleniumService seleniumScript, ICSVProcessorService csvProcessorService, IUserService userService, ITrenService trenService)
        {
            _seleniumService = seleniumScript;
            _csvProcessorService = csvProcessorService;
            _userService = userService;
            _trenService = trenService;
        }

        [HttpPost("TrenData")]
        public IActionResult PostTrenData([FromBody] TrenData trenData)
        {
            if (trenData == null)
            {
                return BadRequest("Los datos del tren son inválidos.");
            }

            _seleniumService.GenerateSeleniumScript(trenData);
            _csvProcessorService.ImportarTrenesDesdeCsv(trenData);

            return Ok(new
            {
                message = "Datos scrapeados y añadidos en la base de datos.",
            });
        }

        [HttpPost("UserData")]
        public IActionResult PostUserData([FromBody] UserData userData)
        {
            if (userData == null)
            {
                return BadRequest("Los datos del usuario son inválidos.");
            }

            _userService.RegisterUser(userData);

            return Ok(new
            {
                message = "Usuario añadido correctamente.",
            });
        }

        [HttpPost("GetUser")]
        public IActionResult GetUser([FromBody] string user)
        {
            if (string.IsNullOrEmpty(user))
            {
                return BadRequest("El correo del usuario es inválido.");
            }

            var userExists = _userService.GetUser(user);

            if (userExists)
            {
                var token = _userService.GenerateJwtToken(user);

                _userService.SaveToken(user, token);

                return Ok(new { message = "true", token = token });
            }
            else
            {
                return NotFound(new { message = "false" });
            }
        }

        [HttpGet("GetRecomendedTrains")]
        public IActionResult GetRecomendedTrains()
        {
            var recommendedTrains = _trenService.GetRecomendedTrains();

            if (recommendedTrains == null || !recommendedTrains.Any())
            {
                return NotFound("No recommended trains found");
            }

            return Ok(recommendedTrains);
        }
    }
}
