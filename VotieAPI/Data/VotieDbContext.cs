using Microsoft.EntityFrameworkCore;
using VotieAPI.Data.Entities;

namespace VotieAPI.Data

{
    public class VotieDbContext : DbContext
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
    }
}
