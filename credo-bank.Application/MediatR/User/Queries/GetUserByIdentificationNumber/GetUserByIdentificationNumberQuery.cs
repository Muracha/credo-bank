﻿using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.User.Queries.GetUserByIdentificationNumber;

public record class GetUserByIdentificationNumberQuery(int IdentificationNumber) : IRequest<ApiWrapper<GetUserByIdnetificationNumberResult>>;