using Microsoft.EntityFrameworkCore;

namespace HairSalon.Models
{
  public class HairSalonContext : DbContext
  {
    public HairSalonContext(DbContextOptions<HairSalonContext> options)
      : base(options)
    {
    }

    public virtual DbSet<Stylist> Stylists { get; set; } = null!;
    public virtual DbSet<Client> Clients { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Stylist>()
        .HasMany(s => s.Clients)
        .WithOne(c => c.Stylist)
        .HasForeignKey(c => c.StylistId);
    }
  }
}
