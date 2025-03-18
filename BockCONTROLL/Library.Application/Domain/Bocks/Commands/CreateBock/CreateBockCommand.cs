using MediatR;

namespace Library.Application.Domain.Bocks.Commands.CreateBock;

public record CreateBockCommand() : IRequest<Guid>;