using System.ComponentModel.DataAnnotations;

namespace credo_bank.Application.Models.DTO;

public record AuthReposnoseDto([Required] string Token, [Required] string RefreshToken);