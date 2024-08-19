using AutoMapper;
using credo_bank.Application.MediatR.User.Commands.Register;
using credo_bank.Application.MediatR.User.Models;
using credo_bank.Application.MediatR.User.Models.DTO;
using credo_bank.Domain.Models;

namespace credo_bank.Application.Mapper;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<User, RegisterUserResult>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<LoanApplication, LoanApplicationDto>().ReverseMap();
    }
}