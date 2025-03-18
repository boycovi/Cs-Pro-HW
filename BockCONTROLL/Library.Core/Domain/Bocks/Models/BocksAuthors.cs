using Library.Core.Domain.Authors.Models;

namespace Library.Core.Domain.Bocks.Models;

public class BocksAuthors
{
    private BocksAuthors()
    {
    }

    internal BocksAuthors(Guid bockId, Guid authorId)
    {
        BockId = bockId;
        AuthorId = authorId;
    }

    public Guid BockId { get; set; }

    public Bock Bock { get; set; }

    public Guid AuthorId { get; set; }

    public Author Author { get; set; }

    public static BocksAuthors Create(Guid bockId, Guid authorId)
    {
        return new BocksAuthors(bockId, authorId);
    }
}