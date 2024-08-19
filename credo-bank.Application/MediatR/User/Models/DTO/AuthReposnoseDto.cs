using System.ComponentModel.DataAnnotations;

namespace credo_bank.Application.MediatR.User.Models.DTO;

public record AuthReposnoseDto([Required] string Token, [Required] string RefreshToken);