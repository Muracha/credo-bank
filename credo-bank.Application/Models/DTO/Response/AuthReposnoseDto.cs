using System.ComponentModel.DataAnnotations;

namespace credo_bank.Application.Models.DTO.Response;

public record AuthReposnoseDto([Required] string Token, [Required] string RefreshToken);