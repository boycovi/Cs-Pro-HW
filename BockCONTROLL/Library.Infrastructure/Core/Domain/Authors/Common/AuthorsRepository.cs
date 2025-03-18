using Library.Core.Domain.Authors.Common;
using Library.Core.Domain.Authors.Models;
using Library.Core.Exceptions;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Core.Domain.Authors.Common;

public class AuthorsRepository(LibrariesDbContest librariesDbContest) : IAuthorsRepository
{
    public async Task<Author> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        var author = await librariesDbContest.Authors.SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
        return author ?? throw new NotFoundException($"{nameof(Author)} with id: '{id}' was not found.");
    }

    public async Task<IReadOnlyCollection<Author>> FindManyAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken)
    {
        return await librariesDbContest.Authors.Where(a => ids.Contains(a.Id)).ToArrayAsync(cancellationToken);
    }

    public void Add(Author author)
    {
        librariesDbContest.Authors.Add(author);
    }

    public void Delete(IReadOnlyCollection<Author> authors)
    {
        librariesDbContest.Authors.RemoveRange(authors);
    }
}