using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VotieAPI.Auth.Entity;
using VotieAPI.Data.Entities;

namespace VotieAPI.Data

{
    public class VotieDbContext : IdentityDbContext<VotieUser>
    {
        public DbSet<Voter> Voters { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Candidate> Candidates { get; set; }

        public VotieDbContext(DbContextOptions<VotieDbContext> options) : base(options)
        {
            
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Vote>()
        //        .HasOne(v => v.Voter)
        //        .WithMany()
        //        .IsRequired()
        //        .OnDelete(DeleteBehavior.Restrict);
        //    modelBuilder.Entity<Vote>()
        //        .HasOne(v => v.Candidate)
        //        .WithMany()
        //        .IsRequired()
        //        .OnDelete(DeleteBehavior.Restrict);
        //}
    }
}
