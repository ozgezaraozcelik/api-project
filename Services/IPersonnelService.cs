using PersonnelTrainingAPI.Models;

namespace PersonnelTrainingAPI.Services
{
    public interface IPersonnelService
    {
        Task<ServiceResponse<List<Personnel>>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<ServiceResponse<Personnel>> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<ServiceResponse<Personnel>> RegisterAsync(Personnel request, CancellationToken cancellationToken = default);

        Task<ServiceResponse<Personnel>> UpdateAsync(int id, Personnel request, CancellationToken cancellationToken = default);

        Task<ServiceResponse<bool>> CheckTrainingAsync(int id, CancellationToken cancellationToken = default);

        Task<ServiceResponse<object?>> RemoveAsync(int id, CancellationToken cancellationToken = default);
    }
}

