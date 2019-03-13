using System.Data.Entity;
using ExileRota.Core.Domain;

namespace ExileRota.Infrastructure.EntityFramework
{
    public class PoeRotaContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Rotation> Rotations { get; set; }

        public PoeRotaContext() : base("ExileRota")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var userBuilder = modelBuilder.Entity<User>();
            userBuilder.HasKey(x => x.UserId);

            var rotationBuilder = modelBuilder.Entity<Rotation>();
            rotationBuilder.HasKey(x => x.RotationId);

            //modelBuilder.Entity<Rotation>()
            //    .HasRequired(x => x.Creator)
            //    .WithRequiredPrincipal(y =);
        }
    }
}