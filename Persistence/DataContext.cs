using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientType> ClientTypes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publishing_Company> PublishingCompanies { get; set; }
        public DbSet<ClientBuysBook> ClientBuysBooks { get; set; }
    }
}