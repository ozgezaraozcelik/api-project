using Microsoft.AspNetCore.Mvc;
using PersonnelTrainingAPI.Data;
using PersonnelTrainingAPI.Models;

namespace PersonnelTrainingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonnelController : ControllerBase
    {
        /// <summary>
        /// Listedeki tüm personelleri döner.
        /// </summary>
        /// <returns>Tüm personellerin listesi.</returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return Ok(DataStore.Personnels);
        }

        /// <summary>
        /// Verilen ID'ye göre personeli döner.
        /// </summary>
        /// <param name="id">Personel ID.</param>
        /// <returns>Personel kaydı.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var personnel = DataStore.Personnels.FirstOrDefault(p => p.Id == id);
            if (personnel is null)
                return NotFound();

            return Ok(personnel);
        }

        /// <summary>
        /// Yeni personel kaydı oluşturur. ID otomatik olarak (Mevcut Max Id + 1) atanır.
        /// </summary>
        /// <param name="request">Eklenecek personel bilgileri.</param>
        /// <returns>Oluşturulan personel kaydı.</returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] Personnel request)
        {
            if (request is null)
                return BadRequest("Request body is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var nextId = DataStore.Personnels.Count == 0 ? 1 : DataStore.Personnels.Max(p => p.Id) + 1;

            var newPersonnel = new Personnel
            {
                Id = nextId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Department = request.Department,
                Email = request.Email,
                IsTrainingCompleted = request.IsTrainingCompleted,
                JoinDate = request.JoinDate == default ? DateTime.UtcNow : request.JoinDate
            };

            DataStore.Personnels.Add(newPersonnel);

            return CreatedAtAction(nameof(GetById), new { id = newPersonnel.Id }, newPersonnel);
        }

        /// <summary>
        /// Verilen ID'ye sahip personelin bilgilerini günceller.
        /// </summary>
        /// <param name="id">Güncellenecek personel ID.</param>
        /// <param name="request">Yeni personel bilgileri.</param>
        /// <returns>Güncellenmiş personel kaydı.</returns>
        [HttpPut("update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] Personnel request)
        {
            if (request is null)
                return BadRequest("Request body is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var existing = DataStore.Personnels.FirstOrDefault(p => p.Id == id);
            if (existing is null)
                return NotFound();

            existing.FirstName = request.FirstName;
            existing.LastName = request.LastName;
            existing.Department = request.Department;
            existing.Email = request.Email;
            existing.IsTrainingCompleted = request.IsTrainingCompleted;
            existing.JoinDate = request.JoinDate == default ? existing.JoinDate : request.JoinDate;

            return Ok(existing);
        }

        /// <summary>
        /// Verilen ID'ye sahip personelin eğitim tamamlama durumunu kontrol eder.
        /// </summary>
        /// <param name="id">Personel ID.</param>
        /// <returns>Eğitim tamamlama durumu (true/false).</returns>
        [HttpGet("check-training/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CheckTraining(int id)
        {
            var personnel = DataStore.Personnels.FirstOrDefault(p => p.Id == id);
            if (personnel is null)
                return NotFound();

            return Ok(personnel.IsTrainingCompleted);
        }

        /// <summary>
        /// Verilen ID'ye sahip personeli listeden siler.
        /// </summary>
        /// <param name="id">Silinecek personel ID.</param>
        /// <returns>İşlem başarılıysa 204 No Content.</returns>
        [HttpDelete("remove/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Remove(int id)
        {
            var personnel = DataStore.Personnels.FirstOrDefault(p => p.Id == id);
            if (personnel is null)
                return NotFound();

            DataStore.Personnels.Remove(personnel);
            return NoContent();
        }
    }
}

