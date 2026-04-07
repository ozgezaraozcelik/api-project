using Microsoft.AspNetCore.Mvc;
using PersonnelTrainingAPI.Models;
using PersonnelTrainingAPI.Services;

namespace PersonnelTrainingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonnelController : ControllerBase
    {
        private readonly IPersonnelService _personnelService;

        public PersonnelController(IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }

        /// <summary>
        /// Listedeki tüm personelleri döner.
        /// </summary>
        /// <returns>Tüm personellerin listesi.</returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _personnelService.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Verilen ID'ye göre personeli döner.
        /// </summary>
        /// <param name="id">Personel ID.</param>
        /// <param name="cancellationToken">İsteğin iptal token'ı.</param>
        /// <returns>Personel kaydı.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _personnelService.GetByIdAsync(id, cancellationToken);
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        /// <summary>
        /// Yeni personel kaydı oluşturur. ID otomatik olarak (Mevcut Max Id + 1) atanır.
        /// </summary>
        /// <param name="request">Eklenecek personel bilgileri.</param>
        /// <param name="cancellationToken">İsteğin iptal token'ı.</param>
        /// <returns>Oluşturulan personel kaydı.</returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] Personnel request, CancellationToken cancellationToken)
        {
            if (request is null)
                return BadRequest(ServiceResponse<Personnel>.Fail("Request body is required."));

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _personnelService.RegisterAsync(request, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
        }

        /// <summary>
        /// Verilen ID'ye sahip personelin bilgilerini günceller.
        /// </summary>
        /// <param name="id">Güncellenecek personel ID.</param>
        /// <param name="request">Yeni personel bilgileri.</param>
        /// <param name="cancellationToken">İsteğin iptal token'ı.</param>
        /// <returns>Güncellenmiş personel kaydı.</returns>
        [HttpPut("update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] Personnel request, CancellationToken cancellationToken)
        {
            if (request is null)
                return BadRequest(ServiceResponse<Personnel>.Fail("Request body is required."));

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _personnelService.UpdateAsync(id, request, cancellationToken);
            if (!result.IsSuccess)
                return result.Message.Contains("not found", StringComparison.OrdinalIgnoreCase)
                    ? NotFound(result)
                    : BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Verilen ID'ye sahip personelin eğitim tamamlama durumunu kontrol eder.
        /// </summary>
        /// <param name="id">Personel ID.</param>
        /// <param name="cancellationToken">İsteğin iptal token'ı.</param>
        /// <returns>Eğitim tamamlama durumu (true/false).</returns>
        [HttpGet("check-training/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CheckTraining(int id, CancellationToken cancellationToken)
        {
            var result = await _personnelService.CheckTrainingAsync(id, cancellationToken);
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        /// <summary>
        /// Verilen ID'ye sahip personeli listeden siler.
        /// </summary>
        /// <param name="id">Silinecek personel ID.</param>
        /// <param name="cancellationToken">İsteğin iptal token'ı.</param>
        /// <returns>İşlem başarılıysa 204 No Content.</returns>
        [HttpDelete("remove/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int id, CancellationToken cancellationToken)
        {
            var result = await _personnelService.RemoveAsync(id, cancellationToken);
            if (!result.IsSuccess)
                return NotFound(result);

            return NoContent();
        }
    }
}

