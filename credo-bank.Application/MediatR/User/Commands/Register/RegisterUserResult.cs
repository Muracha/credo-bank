namespace credo_bank.Application.MediatR.User.Commands.Register;

public record RegisterUserResult(bool B, string IfExsists, int? ExistingUserId);
