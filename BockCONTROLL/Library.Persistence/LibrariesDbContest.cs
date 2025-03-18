using Library.Core.Domain.Authors.Models;
using Library.Core.Domain.Bocks.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence;

public class LibrariesDbContest(DbContextOptions<LibrariesDbContest> options) : DbContext(options)
{
    internal const string LibDbSchema = "libdb";
    internal const string LibDbMigrationsHistoryTable = "__LiDdbMigrationsHistory";

    public DbSet<Bock> Bocks { get; set; }

    public DbSet<Author> Authors { get; set; }

    public DbSet<BocksAuthors> BocksAuthors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // todo: do it only for local development
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(LibDbSchema);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibrariesDbContest).Assembly);
    }
}