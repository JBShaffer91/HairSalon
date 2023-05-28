using System.Collections.Generic;

namespace HairSalon.Models
{
  public class Stylist
  {
    public Stylist()
    {
      this.Clients = new HashSet<Client>();
      this.Name = string.Empty;
      this.Specialty = string.Empty;
    }

    public int StylistId { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }

    public ICollection<Client> Clients { get; set; } = new List<Client>();
  }
}
