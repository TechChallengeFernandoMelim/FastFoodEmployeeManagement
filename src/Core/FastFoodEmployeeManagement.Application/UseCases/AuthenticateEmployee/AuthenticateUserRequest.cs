﻿using MediatR;

namespace FastFoodEmployeeManagement.Application.UseCases.AuthenticateEmployee;

public sealed record AuthenticateEmployeeRequest(string Email, string Password) :
 IRequest<AuthenticateEmployeeResponse>;
