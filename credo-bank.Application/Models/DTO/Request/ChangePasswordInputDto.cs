namespace credo_bank.Application.Models.DTO.Request;

public record ChangePasswordInputDto(string CurrentPassword, string NewPassword);