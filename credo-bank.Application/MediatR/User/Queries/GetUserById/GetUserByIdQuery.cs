﻿using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.User.Queries.GetUserById;

public record class GetUserByIdQuery : IRequest<ApiServiceResponse<GetUserByIdResult>>
{
    public int Id { get; set; }
}