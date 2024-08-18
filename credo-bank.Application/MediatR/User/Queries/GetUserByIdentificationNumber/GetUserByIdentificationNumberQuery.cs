using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.User.Queries.GetUserByIdentificationNumber;

public record class GetUserByIdentificationNumberQuery : IRequest<ApiServiceResponse<GetUserByIdnetificationNumberResult>>
{
    public int IdentificationNumber { get; set; }
}