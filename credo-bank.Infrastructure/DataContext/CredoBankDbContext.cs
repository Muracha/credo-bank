using credo_bank.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace credo_bank.Infrastructure.DataContext;

public class CredoBankDbContext : DbContext
{
    public CredoBankDbContext() { }

    public CredoBankDbContext(DbContextOptions<CredoBankDbContext> options) : base(options) { }
    
    public DbSet<User?> Users { get; set; }
    public DbSet<LoanApplication?> LoanApplications { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
}

