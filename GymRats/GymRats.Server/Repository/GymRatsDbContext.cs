using System;
using System.Collections.Generic;
using GymRats.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Server.Repository;

public partial class GymRatsDbContext : DbContext
{
    public GymRatsDbContext()
    {
    }

    public GymRatsDbContext(DbContextOptions<GymRatsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ankietum> Ankieta { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Jadlospi> Jadlospis { get; set; }

    public virtual DbSet<Karnet> Karnets { get; set; }

    public virtual DbSet<KursTrenera> KursTreneras { get; set; }

    public virtual DbSet<Osoba> Osobas { get; set; }

    public virtual DbSet<PlanTreningowy> PlanTreningowies { get; set; }

    public virtual DbSet<PlanTreningowyUzytkownik> PlanTreningowyUzytkowniks { get; set; }

    public virtual DbSet<Trener> Treners { get; set; }

    public virtual DbSet<Trening> Trenings { get; set; }

    public virtual DbSet<TypKarnetu> TypKarnetus { get; set; }

    public virtual DbSet<Uzytkownik> Uzytkowniks { get; set; }

    public virtual DbSet<UzytkownikBlog> UzytkownikBlogs { get; set; }

    public virtual DbSet<UzytkownikJadlospi> UzytkownikJadlospis { get; set; }

    public virtual DbSet<UzytkownikKursTrenera> UzytkownikKursTreneras { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-IQ0C31H;Initial Catalog=GymRats;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ankietum>(entity =>
        {
            entity.HasKey(e => e.IdAnkieta).HasName("Ankieta_pk");

            entity.Property(e => e.IdAnkieta).ValueGeneratedNever();

            entity.HasOne(d => d.PlanTreningowyIdPlanTreningowyNavigation).WithMany(p => p.Ankieta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Ankieta_Plan_treningowy");

            entity.HasOne(d => d.UzytkownikIdUzytkownikaNavigation).WithMany(p => p.Ankieta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Ankieta_Uzytkownik");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.IdBlogu).HasName("Blog_pk");

            entity.Property(e => e.IdBlogu).ValueGeneratedNever();
        });

        modelBuilder.Entity<Jadlospi>(entity =>
        {
            entity.HasKey(e => e.IdJadlospisu).HasName("Jadlospis_pk");

            entity.Property(e => e.IdJadlospisu).ValueGeneratedNever();
        });

        modelBuilder.Entity<Karnet>(entity =>
        {
            entity.HasKey(e => e.IdKarnet).HasName("Karnet_pk");

            entity.Property(e => e.IdKarnet).ValueGeneratedNever();

            entity.HasOne(d => d.TypKarnetuIdTypKarnetuNavigation).WithMany(p => p.Karnets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Karnet_Typ_Karnetu");

            entity.HasOne(d => d.UzytkownikIdUzytkownikaNavigation).WithMany(p => p.Karnets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Karnet_Uzytkownik");
        });

        modelBuilder.Entity<KursTrenera>(entity =>
        {
            entity.HasKey(e => e.IdKursu).HasName("Kurs_Trenera_pk");

            entity.Property(e => e.IdKursu).ValueGeneratedNever();

            entity.HasOne(d => d.TrenerIdTreneraNavigation).WithMany(p => p.KursTreneras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Kurs_Trenera_Trener");
        });

        modelBuilder.Entity<Osoba>(entity =>
        {
            entity.HasKey(e => e.IdOsoba).HasName("Osoba_pk");

            entity.Property(e => e.IdOsoba).ValueGeneratedNever();
        });

        modelBuilder.Entity<PlanTreningowy>(entity =>
        {
            entity.HasKey(e => e.IdPlanTreningowy).HasName("Plan_treningowy_pk");

            entity.Property(e => e.IdPlanTreningowy).ValueGeneratedNever();

            entity.HasOne(d => d.TrenerIdTreneraNavigation).WithMany(p => p.PlanTreningowies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Plan_treningowy_Trener");

            entity.HasOne(d => d.TreningIdTreningNavigation).WithMany(p => p.PlanTreningowies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Plan_treningowy_Trening");
        });

        modelBuilder.Entity<PlanTreningowyUzytkownik>(entity =>
        {
            entity.HasKey(e => e.IdPlanTreningowy).HasName("Plan_treningowy_Uzytkownik_pk");

            entity.Property(e => e.IdPlanTreningowy).ValueGeneratedNever();

            entity.HasOne(d => d.PlanTreningowyIdPlanTreningowyNavigation).WithMany(p => p.PlanTreningowyUzytkowniks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Plan_treningowy_Uzytkownik_Plan_treningowy");

            entity.HasOne(d => d.UzytkownikIdUzytkownikaNavigation).WithMany(p => p.PlanTreningowyUzytkowniks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Plan_treningowy_Uzytkownik_Uzytkownik");
        });

        modelBuilder.Entity<Trener>(entity =>
        {
            entity.HasKey(e => e.IdTrenera).HasName("Trener_pk");

            entity.Property(e => e.IdTrenera).ValueGeneratedNever();

            entity.HasOne(d => d.OsobaIdOsobaNavigation).WithMany(p => p.Treners)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Trener_Osoba");
        });

        modelBuilder.Entity<Trening>(entity =>
        {
            entity.HasKey(e => e.IdTrening).HasName("Trening_pk");

            entity.Property(e => e.IdTrening).ValueGeneratedNever();
        });

        modelBuilder.Entity<TypKarnetu>(entity =>
        {
            entity.HasKey(e => e.IdTypKarnetu).HasName("Typ_Karnetu_pk");

            entity.Property(e => e.IdTypKarnetu).ValueGeneratedNever();
        });

        modelBuilder.Entity<Uzytkownik>(entity =>
        {
            entity.HasKey(e => e.IdUzytkownika).HasName("Uzytkownik_pk");

            entity.Property(e => e.IdUzytkownika).ValueGeneratedNever();

            entity.HasOne(d => d.OsobaIdOsobaNavigation).WithMany(p => p.Uzytkowniks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Osoba");
        });

        modelBuilder.Entity<UzytkownikBlog>(entity =>
        {
            entity.HasKey(e => e.IdUzytkownikBlog).HasName("Uzytkownik_Blog_pk");

            entity.Property(e => e.IdUzytkownikBlog).ValueGeneratedNever();

            entity.HasOne(d => d.BlogIdBloguNavigation).WithMany(p => p.UzytkownikBlogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Blog_Blog");

            entity.HasOne(d => d.UzytkownikIdUzytkownikaNavigation).WithMany(p => p.UzytkownikBlogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Blog_Uzytkownik");
        });

        modelBuilder.Entity<UzytkownikJadlospi>(entity =>
        {
            entity.HasKey(e => e.IdUzytkownikJadlospis).HasName("Uzytkownik_Jadlospis_pk");

            entity.Property(e => e.IdUzytkownikJadlospis).ValueGeneratedNever();

            entity.HasOne(d => d.JadlospisIdJadlospisuNavigation).WithMany(p => p.UzytkownikJadlospis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Jadlospis_Jadlospis");

            entity.HasOne(d => d.UzytkownikIdUzytkownikaNavigation).WithMany(p => p.UzytkownikJadlospis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Jadlospis_Uzytkownik");
        });

        modelBuilder.Entity<UzytkownikKursTrenera>(entity =>
        {
            entity.HasKey(e => e.IdUzytkownikKursTrenera).HasName("Uzytkownik_Kurs_Trenera_pk");

            entity.Property(e => e.IdUzytkownikKursTrenera).ValueGeneratedNever();

            entity.HasOne(d => d.KursTreneraIdKursuNavigation).WithMany(p => p.UzytkownikKursTreneras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Kurs_Trenera_Kurs_Trenera");

            entity.HasOne(d => d.UzytkownikIdUzytkownikaNavigation).WithMany(p => p.UzytkownikKursTreneras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Kurs_Trenera_Uzytkownik");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
