using credo_bank.DAL.DataContext;

namespace credo_bank.DAL.Repositories.Implementation;

public abstract class BaseRepository
{
    protected readonly CredoBankDbContext _context;
    public BaseRepository(CredoBankDbContext credoBankDbContext)
    {
        _context = credoBankDbContext;
    }
}