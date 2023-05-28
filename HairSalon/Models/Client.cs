using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HairSalon.Models
{
    public class Client
    {
        private HairSalonContext _context;

        public Client(string name, string email, string phoneNumber, Stylist stylist, HairSalonContext context)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phoneNumber) || stylist == null)
                throw new ArgumentNullException("Name, Email, PhoneNumber and Stylist cannot be null");

            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Stylist = stylist;
            _context = context;
        }

        [Key]
        public int ClientId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public int StylistId { get; set; }

        [Required]
        public Stylist Stylist { get; set; }

        public void Save()
        {
            _context.Clients.Add(this);
            _context.SaveChanges();
        }

        public static List<Client> GetAll(HairSalonContext context)
        {
            return context.Clients?.Include(c => c.Stylist).ToList() ?? new List<Client>();
        }
    }
}
