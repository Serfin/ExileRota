using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>()
                .HasKey(x => x.UserId);

            modelBuilder.Entity<Rotation>()
                .HasKey(x => x.RotationId);

            modelBuilder.Entity<Rotation>()
                .HasMany<User>(x => x.Members)
                .WithMany(x => x.Rotations)
                .Map(x =>
                {
                    x.MapLeftKey("RotationID");
                    x.MapRightKey("UserID");
                    x.ToTable("Members");
                });
        }
    }
}