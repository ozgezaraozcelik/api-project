using Microsoft.AspNetCore.Mvc;

namespace PersonnelTrainingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingsController : ControllerBase
    {
        /// <summary>
        /// Tüm eğitim programlarını listeler.
        /// </summary>
        [HttpGet]
        public IActionResult Get() => Ok("Eğitim listesi hazır!");

        /// <summary>
        /// Yeni bir eğitim tanımlar.
        /// </summary>
        [HttpPost]
        public IActionResult Post() => Ok("Eğitim başarıyla eklendi.");
    }
}