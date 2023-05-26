using Microsoft.EntityFrameworkCore;

namespace HairSalon.Models
{
  public class HairSalonContext : DbContext
  {
    public HairSalonContext(DbContextOptions<HairSalonContext> options)
      : base(options)
    {
    }

    public DbSet<Stylist> Stylists { get; set; } = new DbSet<Stylist>();
    public DbSet<Client> Clients { get; set; } = new DbSet<Client>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Stylist>()
        .HasMany(s => s.Clients)
        .WithOne(c => c.Stylist)
        .HasForeignKey(c => c.StylistId);
    }
  }
}
