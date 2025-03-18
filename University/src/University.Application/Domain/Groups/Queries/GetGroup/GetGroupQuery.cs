using MediatR;

namespace University.Application.Domain.Groups.Queries.GetGroup;

public record GetGroupQuery(int PageNumber, int PageSize) : IRequest<GroupDto[]>;
