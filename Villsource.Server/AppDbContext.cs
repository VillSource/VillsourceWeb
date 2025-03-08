using Microsoft.EntityFrameworkCore;

namespace Villsource.Server;

public class AppDbContext : DbContext
{
    public string DbPath { get; }

    public virtual DbSet<ListItem> Lists { get; set; }

    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "app.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class ListItem
{
    public long Id { get; set; }
    public string Content { get; set; } = string.Empty;
}
