using credo_bank.Domain.Models;

namespace credo_bank.Infrastructure.Repositories.Interfaces;

public interface IUserRepository
{
    Task<int> AddUserAsync(User? user,
        CancellationToken cancellationToken = default);

    Task<User?> GetUserByIdAsync(int userId,
        CancellationToken cancellationToken = default);

    Task<User> GetUserWithLoans(int userId,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateUserAsync(User? user,
        CancellationToken cancellationToken = default);

    Task<User> GetUserByIdentificationNumber(int number,
        CancellationToken cancellationToken = default);

    Task<User> GetByRefreshTokenAsync(string refreshToken,
        CancellationToken cancellationToken = default);
}