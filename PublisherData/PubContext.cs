using Microsoft.EntityFrameworkCore;
using PublisherDomain;

namespace PublisherData;
public class PubContext:DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=PublisherData.db");
    }

}

