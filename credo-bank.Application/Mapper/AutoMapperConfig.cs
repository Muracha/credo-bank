using AutoMapper;
using credo_bank.Application.MediatR.User.Commands.Register;
using credo_bank.Application.MediatR.User.DTO;
using credo_bank.Domain.Models;

namespace credo_bank.Application.Mapper;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<User, RegisterUserResult>().ReverseMap();
        CreateMap<LoanApplication, LoanApplicationDto>().ReverseMap();
    }
}