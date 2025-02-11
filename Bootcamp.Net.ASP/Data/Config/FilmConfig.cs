using Bootcamp.Net.ASP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bootcamp.Net.ASP.Data.Config
{
    public class FilmConfig : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            // PK et Identity
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();


            // Titre : Contraintes requis, taille et unique
            builder.Property(f => f.Titre)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasIndex(f => f.Titre).IsUnique();

            // AnneeSortie : Contraintes requis
            builder.Property(f => f.AnneeSortie)
                .IsRequired();

            // Genre : Contraintes requis, taille
            builder.Property(f => f.Genre)
                .IsRequired()
                .HasMaxLength(100);

            // AnneeSortie : Contraintes de check
            builder.ToTable(f => f.HasCheckConstraint("AnneeSortie", "AnneeSortie > 1975"));


            builder.HasOne<Personne>(f => f.Realisateur)
                   .WithMany(p => p.FilmReal)
                   .HasForeignKey(f => f.RealisateurId)
                   .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
