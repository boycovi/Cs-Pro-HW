﻿using MediatR;

namespace Library.Application.Domain.Authors.Commands.CreateAuthor;

public record CreateAuthorCommand(
    string FirstName,
    string LastName,
    string? MiddleName = default) : IRequest<Guid>;