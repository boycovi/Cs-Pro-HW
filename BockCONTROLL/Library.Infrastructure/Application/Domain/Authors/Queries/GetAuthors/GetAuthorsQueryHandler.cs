using Library.Application.Domain.Authors.Queries.GetAuthors;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PagesResponses;

namespace Library.Infrastructure.Application.Domain.Authors.Queries.GetAuthors;

public class GetAuthorsQueryHandler(LibrariesDbContest librariesDbContest) : IRequestHandler<GetAuthorsQuery, PageResponse<AuthorDto[]>>
{
    public async Task<PageResponse<AuthorDto[]>> Handle(GetAuthorsQuery query, CancellationToken cancellationToken)
    {
        var authors = await librariesDbContest
            .Authors
            .Select(a => new AuthorDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                MiddleName = a.MiddleName
            })
            .ToArrayAsync(cancellationToken);

        return new PageResponse<AuthorDto[]>(authors.Length, authors);
    }
}