using Microsoft.EntityFrameworkCore;

namespace TestApp2.Data
{
    public class ClientsContext : DbContext
    {
        public ClientsContext (DbContextOptions<ClientsContext> options)
            : base(options)
        {
        }

        public DbSet<TestApp.Models.Founder> Founder { get; set; } = default!;

        public DbSet<TestApp.Models.Client> Client { get; set; } = default!;
    }
}
