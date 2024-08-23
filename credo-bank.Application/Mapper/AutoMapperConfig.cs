using AutoMapper;
using credo_bank.Application.MediatR.Admin.Command.Update;
using credo_bank.Application.MediatR.LoanApplication.Command.Add;
using credo_bank.Application.MediatR.LoanApplication.Command.Update;
using credo_bank.Application.MediatR.User.Commands.Login;
using credo_bank.Application.MediatR.User.Commands.RefreshToken;
using credo_bank.Application.MediatR.User.Commands.Register;
using credo_bank.Application.MediatR.User.Commands.Update;
using credo_bank.Application.Models.DTO;
using credo_bank.Application.Models.DTO.Request;
using credo_bank.Application.Models.DTO.Response;
using credo_bank.Domain.Models;

namespace credo_bank.Application.Mapper;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<LoanApplication, LoanApplicationDto>().ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();
        
        //Request Dtos
        CreateMap<ChangePasswordInputDto, UpdateUserPasswordCommand>().ReverseMap();
        CreateMap<RegisterUserInputDto, RegisterUserCommand>().ReverseMap();
        CreateMap<LoginInputDto, LoginUserCommand>().ReverseMap();
        CreateMap<RefreshTokenInputDto, RefreshTokenCommand>().ReverseMap();
        CreateMap<ApplyForLoanInputDto, AddLoanApplicationCommand>().ReverseMap();
        CreateMap<UpdateLoanApplicationCommand, UpdateLoanDto>().ReverseMap();
        CreateMap<UpdateLoanApplicationAdminCommand, UpdateLoanDto>().ReverseMap();
    }
}