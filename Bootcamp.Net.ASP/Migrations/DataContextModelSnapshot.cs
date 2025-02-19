﻿// <auto-generated />
using Bootcamp.Net.ASP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bootcamp.Net.ASP.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bootcamp.Net.ASP.Models.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnneeSortie")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("RealisateurId")
                        .HasColumnType("int");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("RealisateurId");

                    b.HasIndex("Titre")
                        .IsUnique();

                    b.ToTable("Films", null, t =>
                        {
                            t.HasCheckConstraint("AnneeSortie", "AnneeSortie > 1975");
                        });
                });

            modelBuilder.Entity("Bootcamp.Net.ASP.Models.FilmPersonne", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<int>("PersonneId")
                        .HasColumnType("int");

                    b.HasKey("FilmId", "PersonneId");

                    b.HasIndex("PersonneId");

                    b.ToTable("FilmPersonnes", (string)null);
                });

            modelBuilder.Entity("Bootcamp.Net.ASP.Models.Personne", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Personnes", (string)null);
                });

            modelBuilder.Entity("Bootcamp.Net.ASP.Models.Utilisateur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("utilisateurs", (string)null);
                });

            modelBuilder.Entity("Bootcamp.Net.ASP.Models.Film", b =>
                {
                    b.HasOne("Bootcamp.Net.ASP.Models.Personne", "Realisateur")
                        .WithMany("FilmReal")
                        .HasForeignKey("RealisateurId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Realisateur");
                });

            modelBuilder.Entity("Bootcamp.Net.ASP.Models.FilmPersonne", b =>
                {
                    b.HasOne("Bootcamp.Net.ASP.Models.Film", "Film")
                        .WithMany("Acteurs")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Bootcamp.Net.ASP.Models.Personne", "Personne")
                        .WithMany("Roles")
                        .HasForeignKey("PersonneId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Personne");
                });

            modelBuilder.Entity("Bootcamp.Net.ASP.Models.Film", b =>
                {
                    b.Navigation("Acteurs");
                });

            modelBuilder.Entity("Bootcamp.Net.ASP.Models.Personne", b =>
                {
                    b.Navigation("FilmReal");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
