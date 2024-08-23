using credo_bank.Application.Interfaces;
using credo_bank.Domain.Models;
using credo_bank.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace credo_bank.Infrastructure.Repositories.Implementation;

public class RoleRepository(CredoBankDbContext context) : BaseRepository(context), IRoleRepository
{
    public Task<Role> GetByIdAsync(int id, 
        CancellationToken cancellationToken = default)
        => context.Roles
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken: cancellationToken);

    public Task<List<Role>> GetAllAsync(
        CancellationToken cancellationToken = default)
        => context.Roles
            .AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
}