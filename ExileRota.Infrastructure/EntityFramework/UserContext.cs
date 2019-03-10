using System.Data.Entity;
using ExileRota.Core.Domain;

namespace ExileRota.Infrastructure.EntityFramework
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext() : base("ExileRota")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var itemBuilder = modelBuilder.Entity<User>();
            itemBuilder.HasKey(x => x.UserId);
        }
    }
}