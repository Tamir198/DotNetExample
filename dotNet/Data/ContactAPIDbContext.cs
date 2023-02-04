using dotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace dotNet.Data
{
    public class ContactAPIDbContext : DbContext
    {
        public ContactAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        //The property will act as tables for entity framework core
        public DbSet<Contact> Contacts { get; set; }
    }
}
