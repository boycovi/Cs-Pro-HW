using Library.Core.Common;
using Library.Infrastructure.Processing;
using Library.Persistence;

namespace Library.Infrastructure.Core.Common;

internal class UnitOfWork(
    LibrariesDbContest librariesDbContest,
    IEnumerationIgnorer enumerationIgnorer)
    : IUnitOfWork
{
    private readonly LibrariesDbContest _librariesDbContest = librariesDbContest ?? throw new ArgumentNullException(nameof(librariesDbContest));

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        enumerationIgnorer.IgnoreEnumerations();
        return await _librariesDbContest.SaveChangesAsync(cancellationToken);
    }
}
