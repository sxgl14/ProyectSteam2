using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectSteam2.Models;

public partial class SteamContext : DbContext
{
    public SteamContext()
    {
    }

    public SteamContext(DbContextOptions<SteamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategGame> CategGames { get; set; }

    public virtual DbSet<Category> Categorys { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GamesUser> GamesUsers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Steam;user=ypaez; password=123; Trusted_Connection=True; integrated security=true; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategGame>(entity =>
        {
            entity.HasKey(e => e.IdCg).HasName("PK__Categ_Ga__16EC97FA6A1D4B5C");

            entity.ToTable("Categ_Games");

            entity.Property(e => e.IdCg)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("Id_CG");
            entity.Property(e => e.IdCateg).HasColumnName("Id_Categ");
            entity.Property(e => e.IdGame).HasColumnName("Id_Game");

            entity.HasOne(d => d.IdCategNavigation).WithMany(p => p.CategGames)
                .HasForeignKey(d => d.IdCateg)
                .HasConstraintName("FK_Categ");

            entity.HasOne(d => d.IdGameNavigation).WithMany(p => p.CategGames)
                .HasForeignKey(d => d.IdGame)
                .HasConstraintName("FK_Games_Cat");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCat).HasName("PK__Category__5EE0B144FF94A970");

            entity.Property(e => e.IdCat)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("Id_Cat");
            entity.Property(e => e.NameCat)
                .HasMaxLength(30)
                .HasColumnName("Name_Cat");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.IdG).HasName("PK__Games__B770B53723CD731C");

            entity.Property(e => e.IdG)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("Id_G");
            entity.Property(e => e.Creator).HasColumnName("creator");
            entity.Property(e => e.DateR).HasColumnName("Date_R");
            entity.Property(e => e.NameG).HasColumnName("Name_G");
            entity.Property(e => e.Price).HasColumnType("decimal(6, 2)");
        });

        modelBuilder.Entity<GamesUser>(entity =>
        {
            entity.HasKey(e => e.IdGU).HasName("PK__Games_Us__522036B032002195");

            entity.ToTable("Games_Users");

            entity.Property(e => e.IdGU)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("Id_G_U");
            entity.Property(e => e.IdG).HasColumnName("Id_G");
            entity.Property(e => e.IdU).HasColumnName("Id_U");

            entity.HasOne(d => d.IdGNavigation).WithMany(p => p.GamesUsers)
                .HasForeignKey(d => d.IdG)
                .HasConstraintName("FK_Games");

            entity.HasOne(d => d.IdUNavigation).WithMany(p => p.GamesUsers)
                .HasForeignKey(d => d.IdU)
                .HasConstraintName("FK_Users");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.IdI).HasName("PK__Invoice__B770B5355AC25CA8");

            entity.ToTable("Invoice");

            entity.Property(e => e.IdI)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("Id_I");
            entity.Property(e => e.IdG).HasColumnName("Id_G");
            entity.Property(e => e.IdU).HasColumnName("Id_U");

            entity.HasOne(d => d.IdGNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.IdG)
                .HasConstraintName("FK_ID_Games");

            entity.HasOne(d => d.IdUNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.IdU)
                .HasConstraintName("FK_ID_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdU).HasName("PK__Users__B770B53960B4A823");

            entity.Property(e => e.IdU)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("Id_U");
            entity.Property(e => e.Mail)
                .HasMaxLength(30)
                .HasColumnName("mail");
            entity.Property(e => e.NameU)
                .HasMaxLength(30)
                .HasColumnName("Name_U");
            entity.Property(e => e.Pass).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
