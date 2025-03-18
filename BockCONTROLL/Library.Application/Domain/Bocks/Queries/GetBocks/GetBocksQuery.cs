using MediatR;

namespace Library.Application.Domain.Bocks.Queries.GetBocks;

public record GetBocksQuery(int Page, int PageSize) : IRequest<BockDto[]>;