using Bootcamp.Net.ASP.Data.Config;
using Bootcamp.Net.ASP.Models;
using Microsoft.EntityFrameworkCore;
using Bootcamp.Net.ASP.Models.DTO;

namespace Bootcamp.Net.ASP.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Film> Films { get; set; }
        public DbSet<Personne> Personnes { get; set; }

        public DbSet<FilmPersonne> FilmPersonnes { get; set; }

        public DbSet<Utilisateur> utilisateurs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FilmConfig());
            modelBuilder.ApplyConfiguration(new FilmPersonneConfig());
        }
        public DbSet<Bootcamp.Net.ASP.Models.DTO.UpdatePersonneForm> UpdatePersonneForm { get; set; } = default!;

    }
}
