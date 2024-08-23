using credo_bank.Domain.Models;

namespace credo_bank.Application.Interfaces;

public interface IRoleRepository
{
    Task<Role> GetByIdAsync(int id, 
        CancellationToken cancellationToken = default);

    Task<List<Role>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}