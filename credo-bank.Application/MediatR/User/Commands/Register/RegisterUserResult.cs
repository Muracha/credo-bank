using System.ComponentModel.DataAnnotations;
using credo_bank.Application.MediatR.User.Models.DTO;

namespace credo_bank.Application.MediatR.User.Commands.Register;

public record RegisterUserResult(AuthReposnoseDto AuthReposnoseDto);
