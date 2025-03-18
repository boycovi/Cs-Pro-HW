namespace Library.Core.Domain.Bocks.Models;

public class Bock
{
    private readonly List<BocksAuthors> _bocksAuthors = new ();

    private Bock()
    {
    }

    public Guid Id { get; private set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public IReadOnlyCollection<BocksAuthors> BocksAuthors => _bocksAuthors.AsReadOnly();

    public static Bock Create(string title, string description)
    {
        return new Bock
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description
        };
    }
}