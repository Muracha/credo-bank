using credo_bank.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace credo_bank.DAL.DataContext;

public class CredoBankDbContext : DbContext
{
    public CredoBankDbContext() { }

    public CredoBankDbContext(DbContextOptions<CredoBankDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<LoanApplication> LoanApplications { get; set; }
}

