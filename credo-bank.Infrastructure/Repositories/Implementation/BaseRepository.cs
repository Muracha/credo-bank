using credo_bank.Infrastructure.DataContext;

namespace credo_bank.DAL.Repositories.Implementation;

public abstract class BaseRepository
{
    protected readonly CredoBankDbContext _context;
    public BaseRepository(CredoBankDbContext credoBankDbContext)
    {
        _context = credoBankDbContext;
    }
}