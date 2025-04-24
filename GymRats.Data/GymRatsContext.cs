using System;
using System.Collections.Generic;
using GymRats.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymRats.Data.Entities;

public partial class GymRatsContext : DbContext
{
    public GymRatsContext()
    {
    }

    public GymRatsContext(DbContextOptions<GymRatsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Jadlospi> Jadlospis { get; set; }

    public virtual DbSet<Karnet> Karnets { get; set; }

    public virtual DbSet<KursTrenera> KursTreneras { get; set; }

    public virtual DbSet<Osoba> Osobas { get; set; }

    public virtual DbSet<PlanTreningowy> PlanTreningowies { get; set; }

    public virtual DbSet<PlanTreningowyUzytkownik> PlanTreningowyUzytkowniks { get; set; }

    public virtual DbSet<Trener> Treners { get; set; }

    public virtual DbSet<TreningPersonalny> TreningPersonalnies { get; set; }

    public virtual DbSet<TypKarnetu> TypKarnetus { get; set; }

    public virtual DbSet<UdzialWZajeciach> UdzialWZajeciaches { get; set; }

    public virtual DbSet<Uzytkownik> Uzytkowniks { get; set; }

    public virtual DbSet<UzytkownikJadlospi> UzytkownikJadlospis { get; set; }

    public virtual DbSet<UzytkownikKursTrenera> UzytkownikKursTreneras { get; set; }

    public virtual DbSet<ZajeciaGrupowe> ZajeciaGrupowes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-RAS4J05;Database=GymRats;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Jadlospi>(entity =>
        {
            entity.HasKey(e => e.IdJadlospisu).HasName("Jadlospis_pk");

            entity.Property(e => e.IdJadlospisu).HasColumnName("id_jadlospisu");
            entity.Property(e => e.Kalorycznosc)
                .IsUnicode(false)
                .HasColumnName("kalorycznosc");
            entity.Property(e => e.RodzajDiety)
                .IsUnicode(false)
                .HasColumnName("rodzaj_diety");
            entity.Property(e => e.ZawartoscJadlospisu).HasColumnName("zawartosc_jadlospisu");
        });

        modelBuilder.Entity<Karnet>(entity =>
        {
            entity.HasKey(e => e.IdKarnet).HasName("Karnet_pk");

            entity.ToTable("Karnet");

            entity.Property(e => e.IdKarnet).HasColumnName("id_karnet");
            entity.Property(e => e.Aktywny).HasColumnName("aktywny");
            entity.Property(e => e.KoniecUmowy).HasColumnName("koniec_umowy");
            entity.Property(e => e.StartKarnetu).HasColumnName("start_karnetu");
            entity.Property(e => e.TypKarnetuIdTypKarnetu).HasColumnName("Typ_Karnetu_id_typ_karnetu");
            entity.Property(e => e.UzytkownikIdUzytkownik).HasColumnName("Uzytkownik_id_uzytkownik");

            entity.HasOne(d => d.TypKarnetuIdTypKarnetuNavigation).WithMany(p => p.Karnets)
                .HasForeignKey(d => d.TypKarnetuIdTypKarnetu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Karnet_Typ_Karnetu");

            entity.HasOne(d => d.UzytkownikIdUzytkownikNavigation).WithMany(p => p.Karnets)
                .HasForeignKey(d => d.UzytkownikIdUzytkownik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Karnet_Uzytkownik");
        });

        modelBuilder.Entity<KursTrenera>(entity =>
        {
            entity.HasKey(e => e.IdKursu).HasName("Kurs_Trenera_pk");

            entity.ToTable("Kurs_Trenera");

            entity.Property(e => e.IdKursu).HasColumnName("id_kursu");
            entity.Property(e => e.CzasTrwania)
                .IsUnicode(false)
                .HasColumnName("czas_trwania");
            entity.Property(e => e.Nazwa)
                .IsUnicode(false)
                .HasColumnName("nazwa");
            entity.Property(e => e.Opis)
                .IsUnicode(false)
                .HasColumnName("opis");
            entity.Property(e => e.TrenerIdTrener).HasColumnName("Trener_id_trener");

            entity.HasOne(d => d.TrenerIdTrenerNavigation).WithMany(p => p.KursTreneras)
                .HasForeignKey(d => d.TrenerIdTrener)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Kurs_Trenera_Trener");
        });

        modelBuilder.Entity<Osoba>(entity =>
        {
            entity.HasKey(e => e.IdOsoba).HasName("Osoba_pk");

            entity.ToTable("Osoba");

            entity.Property(e => e.IdOsoba).HasColumnName("id_osoba");
            entity.Property(e => e.Adres)
                .IsUnicode(false)
                .HasColumnName("adres");
            entity.Property(e => e.DataUrodzenia).HasColumnName("data_urodzenia");
            entity.Property(e => e.Imie)
                .IsUnicode(false)
                .HasColumnName("imie");
            entity.Property(e => e.KodPocztowy)
                .IsUnicode(false)
                .HasColumnName("kod_pocztowy");
            entity.Property(e => e.Miejscowosc)
                .IsUnicode(false)
                .HasColumnName("miejscowosc");
            entity.Property(e => e.Nazwisko)
                .IsUnicode(false)
                .HasColumnName("nazwisko");
            entity.Property(e => e.NrTel)
                .IsUnicode(false)
                .HasColumnName("nr_tel");
            entity.Property(e => e.NumerBudynku)
                .IsUnicode(false)
                .HasColumnName("numer_budynku");
            entity.Property(e => e.Plec)
                .IsUnicode(false)
                .HasColumnName("plec");
            entity.Property(e => e.Waga)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("waga");
            entity.Property(e => e.Wzrost).HasColumnName("wzrost");
        });

        modelBuilder.Entity<PlanTreningowy>(entity =>
        {
            entity.HasKey(e => e.IdPlanTreningowy).HasName("Plan_treningowy_pk");

            entity.ToTable("Plan_treningowy");

            entity.Property(e => e.IdPlanTreningowy).HasColumnName("id_plan_treningowy");
            entity.Property(e => e.NazwaPlanu)
                .IsUnicode(false)
                .HasColumnName("nazwa_planu");
            entity.Property(e => e.ZawartoscPlanuTreningowego).HasColumnName("zawartosc_planu_treningowego");
        });

        modelBuilder.Entity<PlanTreningowyUzytkownik>(entity =>
        {
            entity.HasKey(e => e.IdPlanTreningowy).HasName("Plan_treningowy_Uzytkownik_pk");

            entity.ToTable("Plan_treningowy_Uzytkownik");

            entity.Property(e => e.IdPlanTreningowy).HasColumnName("id_plan_treningowy");
            entity.Property(e => e.PlanTreningowyIdPlanTreningowy).HasColumnName("Plan_treningowy_id_plan_treningowy");
            entity.Property(e => e.UzytkownikIdUzytkownik).HasColumnName("Uzytkownik_id_uzytkownik");

            entity.HasOne(d => d.PlanTreningowyIdPlanTreningowyNavigation).WithMany(p => p.PlanTreningowyUzytkowniks)
                .HasForeignKey(d => d.PlanTreningowyIdPlanTreningowy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Plan_treningowy_Uzytkownik_Plan_treningowy");

            entity.HasOne(d => d.UzytkownikIdUzytkownikNavigation).WithMany(p => p.PlanTreningowyUzytkowniks)
                .HasForeignKey(d => d.UzytkownikIdUzytkownik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Plan_treningowy_Uzytkownik_Uzytkownik");
        });

        modelBuilder.Entity<Trener>(entity =>
        {
            entity.HasKey(e => e.IdTrener).HasName("Trener_pk");

            entity.ToTable("Trener");

            entity.Property(e => e.IdTrener)
                .ValueGeneratedNever()
                .HasColumnName("id_trener");
            entity.Property(e => e.Specjalizacja)
                .IsUnicode(false)
                .HasColumnName("specjalizacja");

            entity.HasOne(d => d.IdTrenerNavigation).WithOne(p => p.Trener)
                .HasForeignKey<Trener>(d => d.IdTrener)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Trener_Osoba");
        });

        modelBuilder.Entity<TreningPersonalny>(entity =>
        {
            entity.HasKey(e => e.IdTreninguPersonalnego).HasName("Trening_Personalny_pk");

            entity.ToTable("Trening_Personalny");

            entity.Property(e => e.IdTreninguPersonalnego).HasColumnName("id_treningu_personalnego");
            entity.Property(e => e.DataDo).HasColumnName("data_do");
            entity.Property(e => e.DataOd).HasColumnName("data_od");
            entity.Property(e => e.TrenerIdTrener).HasColumnName("Trener_id_trener");
            entity.Property(e => e.UzytkownikIdUzytkownik).HasColumnName("Uzytkownik_id_uzytkownik");

            entity.HasOne(d => d.TrenerIdTrenerNavigation).WithMany(p => p.TreningPersonalnies)
                .HasForeignKey(d => d.TrenerIdTrener)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Trening_Personalny_Trener");

            entity.HasOne(d => d.UzytkownikIdUzytkownikNavigation).WithMany(p => p.TreningPersonalnies)
                .HasForeignKey(d => d.UzytkownikIdUzytkownik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Trening_Personalny_Uzytkownik");
        });

        modelBuilder.Entity<TypKarnetu>(entity =>
        {
            entity.HasKey(e => e.IdTypKarnetu).HasName("Typ_Karnetu_pk");

            entity.ToTable("Typ_Karnetu");

            entity.Property(e => e.IdTypKarnetu).HasColumnName("id_typ_karnetu");
            entity.Property(e => e.Cena).HasColumnName("cena");
            entity.Property(e => e.Nazwa)
                .IsUnicode(false)
                .HasColumnName("nazwa");
            entity.Property(e => e.Opis)
                .IsUnicode(false)
                .HasColumnName("opis");
        });

        modelBuilder.Entity<UdzialWZajeciach>(entity =>
        {
            entity.HasKey(e => e.IdUdzial).HasName("Udzial_w_zajeciach_pk");

            entity.ToTable("Udzial_w_zajeciach");

            entity.Property(e => e.IdUdzial).HasColumnName("id_udzial");
            entity.Property(e => e.UzytkownikIdUzytkownik).HasColumnName("Uzytkownik_id_uzytkownik");
            entity.Property(e => e.ZajeciaGrupoweIdZajec).HasColumnName("Zajecia_Grupowe_id_zajec");

            entity.HasOne(d => d.UzytkownikIdUzytkownikNavigation).WithMany(p => p.UdzialWZajeciaches)
                .HasForeignKey(d => d.UzytkownikIdUzytkownik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Udzial_w_zajeciach_Uzytkownik");

            entity.HasOne(d => d.ZajeciaGrupoweIdZajecNavigation).WithMany(p => p.UdzialWZajeciaches)
                .HasForeignKey(d => d.ZajeciaGrupoweIdZajec)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_43_Zajecia_Grupowe");
        });

        modelBuilder.Entity<Uzytkownik>(entity =>
        {
            entity.HasKey(e => e.IdUzytkownik).HasName("Uzytkownik_pk");

            entity.ToTable("Uzytkownik");

            entity.Property(e => e.IdUzytkownik)
                .ValueGeneratedNever()
                .HasColumnName("id_uzytkownik");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Haslo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("haslo");

            entity.HasOne(d => d.IdUzytkownikNavigation).WithOne(p => p.Uzytkownik)
                .HasForeignKey<Uzytkownik>(d => d.IdUzytkownik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Osoba");
        });

        modelBuilder.Entity<UzytkownikJadlospi>(entity =>
        {
            entity.HasKey(e => e.IdUzytkownikJadlospis).HasName("Uzytkownik_Jadlospis_pk");

            entity.ToTable("Uzytkownik_Jadlospis");

            entity.Property(e => e.IdUzytkownikJadlospis).HasColumnName("id_uzytkownik_jadlospis");
            entity.Property(e => e.JadlospisIdJadlospisu).HasColumnName("Jadlospis_id_jadlospisu");
            entity.Property(e => e.UzytkownikIdUzytkownik).HasColumnName("Uzytkownik_id_uzytkownik");

            entity.HasOne(d => d.JadlospisIdJadlospisuNavigation).WithMany(p => p.UzytkownikJadlospis)
                .HasForeignKey(d => d.JadlospisIdJadlospisu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Jadlospis_Jadlospis");

            entity.HasOne(d => d.UzytkownikIdUzytkownikNavigation).WithMany(p => p.UzytkownikJadlospis)
                .HasForeignKey(d => d.UzytkownikIdUzytkownik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Jadlospis_Uzytkownik");
        });

        modelBuilder.Entity<UzytkownikKursTrenera>(entity =>
        {
            entity.HasKey(e => e.IdUzytkownikKursTrenera).HasName("Uzytkownik_Kurs_Trenera_pk");

            entity.ToTable("Uzytkownik_Kurs_Trenera");

            entity.Property(e => e.IdUzytkownikKursTrenera).HasColumnName("id_uzytkownik_kurs_trenera");
            entity.Property(e => e.KursTreneraIdKursu).HasColumnName("Kurs_Trenera_id_kursu");
            entity.Property(e => e.UzytkownikIdUzytkownik).HasColumnName("Uzytkownik_id_uzytkownik");

            entity.HasOne(d => d.KursTreneraIdKursuNavigation).WithMany(p => p.UzytkownikKursTreneras)
                .HasForeignKey(d => d.KursTreneraIdKursu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Kurs_Trenera_Kurs_Trenera");

            entity.HasOne(d => d.UzytkownikIdUzytkownikNavigation).WithMany(p => p.UzytkownikKursTreneras)
                .HasForeignKey(d => d.UzytkownikIdUzytkownik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Kurs_Trenera_Uzytkownik");
        });

        modelBuilder.Entity<ZajeciaGrupowe>(entity =>
        {
            entity.HasKey(e => e.IdZajec).HasName("Zajecia_Grupowe_pk");

            entity.ToTable("Zajecia_Grupowe");

            entity.Property(e => e.IdZajec).HasColumnName("id_zajec");
            entity.Property(e => e.Data)
                .HasColumnType("datetime")
                .HasColumnName("data");
            entity.Property(e => e.NazwaZajec)
                .IsUnicode(false)
                .HasColumnName("nazwa_zajec");
            entity.Property(e => e.TrenerIdTrener).HasColumnName("Trener_id_trener");

            entity.HasOne(d => d.TrenerIdTrenerNavigation).WithMany(p => p.ZajeciaGrupowes)
                .HasForeignKey(d => d.TrenerIdTrener)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Zajecia_Grupowe_Trener");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
