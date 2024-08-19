namespace credo_bank.Application.Models.DTO.Request;

public record RegisterUserInputDto(
    string FirstName, 
    string LastName, 
    int IdentificationNumber, 
    string Password, 
    DateTime DateOfBirth);