using PersonnelTrainingAPI.Data;
using PersonnelTrainingAPI.Models;

namespace PersonnelTrainingAPI.Services
{
    public class PersonnelService : IPersonnelService
    {
        private static readonly object _sync = new();

        public Task<ServiceResponse<List<Personnel>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            List<Personnel> data;
            lock (_sync)
            {
                data = DataStore.Personnels
                    .OrderBy(p => p.Id)
                    .ToList();
            }

            return Task.FromResult(ServiceResponse<List<Personnel>>.Success(data));
        }

        public Task<ServiceResponse<Personnel>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Personnel? personnel;
            lock (_sync)
            {
                personnel = DataStore.Personnels.FirstOrDefault(p => p.Id == id);
            }

            return Task.FromResult(
                personnel is null
                    ? ServiceResponse<Personnel>.Fail("Personnel not found.")
                    : ServiceResponse<Personnel>.Success(personnel)
            );
        }

        public Task<ServiceResponse<Personnel>> RegisterAsync(Personnel request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (request is null)
                return Task.FromResult(ServiceResponse<Personnel>.Fail("Request body is required."));

            if (string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.LastName) ||
                string.IsNullOrWhiteSpace(request.Department) ||
                string.IsNullOrWhiteSpace(request.Email))
            {
                return Task.FromResult(ServiceResponse<Personnel>.Fail("Required fields are missing."));
            }

            Personnel newPersonnel;
            lock (_sync)
            {
                var nextId = DataStore.Personnels.Count == 0 ? 1 : DataStore.Personnels.Max(p => p.Id) + 1;

                newPersonnel = new Personnel
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
            }

            return Task.FromResult(ServiceResponse<Personnel>.Success(newPersonnel, "Personnel created."));
        }

        public Task<ServiceResponse<Personnel>> UpdateAsync(int id, Personnel request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (request is null)
                return Task.FromResult(ServiceResponse<Personnel>.Fail("Request body is required."));

            if (string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.LastName) ||
                string.IsNullOrWhiteSpace(request.Department) ||
                string.IsNullOrWhiteSpace(request.Email))
            {
                return Task.FromResult(ServiceResponse<Personnel>.Fail("Required fields are missing."));
            }

            Personnel? existing;
            lock (_sync)
            {
                existing = DataStore.Personnels.FirstOrDefault(p => p.Id == id);
                if (existing is null)
                    return Task.FromResult(ServiceResponse<Personnel>.Fail("Personnel not found."));

                existing.FirstName = request.FirstName;
                existing.LastName = request.LastName;
                existing.Department = request.Department;
                existing.Email = request.Email;
                existing.IsTrainingCompleted = request.IsTrainingCompleted;
                existing.JoinDate = request.JoinDate == default ? existing.JoinDate : request.JoinDate;
            }

            return Task.FromResult(ServiceResponse<Personnel>.Success(existing, "Personnel updated."));
        }

        public Task<ServiceResponse<bool>> CheckTrainingAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Personnel? personnel;
            lock (_sync)
            {
                personnel = DataStore.Personnels.FirstOrDefault(p => p.Id == id);
            }

            return Task.FromResult(
                personnel is null
                    ? ServiceResponse<bool>.Fail("Personnel not found.")
                    : ServiceResponse<bool>.Success(personnel.IsTrainingCompleted)
            );
        }

        public Task<ServiceResponse<object?>> RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var removed = false;
            lock (_sync)
            {
                var personnel = DataStore.Personnels.FirstOrDefault(p => p.Id == id);
                if (personnel is null)
                    return Task.FromResult(ServiceResponse<object?>.Fail("Personnel not found."));

                removed = DataStore.Personnels.Remove(personnel);
            }

            return Task.FromResult(
                removed
                    ? ServiceResponse<object?>.Success(null, "Personnel removed.")
                    : ServiceResponse<object?>.Fail("Personnel could not be removed.")
            );
        }
    }
}

