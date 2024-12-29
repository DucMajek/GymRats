using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace gymrats.server.Models;

public partial class GymRatsContext : DbContext
{
    public GymRatsContext()
    {
    }

    public GymRatsContext(DbContextOptions<GymRatsContext> options)
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-IQ0C31H;Initial Catalog=GymRats;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Polish_CI_AS");

        modelBuilder.Entity<Ankietum>(entity =>
        {
            entity.HasKey(e => e.IdAnkieta).HasName("Ankieta_pk");

            entity.Property(e => e.IdAnkieta).HasColumnName("id_ankieta");
            entity.Property(e => e.OdpowiedziPytania).HasColumnName("odpowiedzi_pytania");
            entity.Property(e => e.PlanTreningowyIdPlanTreningowy).HasColumnName("Plan_treningowy_id_plan_treningowy");
            entity.Property(e => e.UzytkownikIdUzytkownika).HasColumnName("Uzytkownik_id_uzytkownika");

            entity.HasOne(d => d.PlanTreningowyIdPlanTreningowyNavigation).WithMany(p => p.Ankieta)
                .HasForeignKey(d => d.PlanTreningowyIdPlanTreningowy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Ankieta_Plan_treningowy");

            entity.HasOne(d => d.UzytkownikIdUzytkownikaNavigation).WithMany(p => p.Ankieta)
                .HasForeignKey(d => d.UzytkownikIdUzytkownika)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Ankieta_Uzytkownik");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.IdBlogu).HasName("Blog_pk");

            entity.ToTable("Blog");

            entity.Property(e => e.IdBlogu).HasColumnName("id_blogu");
            entity.Property(e => e.DataPublikacji)
                .HasColumnType("datetime")
                .HasColumnName("data_publikacji");
            entity.Property(e => e.Tresc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tresc");
            entity.Property(e => e.Tytul)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tytul");
        });

        modelBuilder.Entity<Jadlospi>(entity =>
        {
            entity.HasKey(e => e.IdJadlospisu).HasName("Jadlospis_pk");

            entity.Property(e => e.IdJadlospisu).HasColumnName("id_jadlospisu");
            entity.Property(e => e.Kalorycznosc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("kalorycznosc");
            entity.Property(e => e.Plec)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("plec");
            entity.Property(e => e.RodzajDiety)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rodzaj_diety");
            entity.Property(e => e.ZawartoscJadlospisu).HasColumnName("zawartosc_jadlospisu");
        });

        modelBuilder.Entity<Karnet>(entity =>
        {
            entity.HasKey(e => e.IdKarnet).HasName("Karnet_pk");

            entity.ToTable("Karnet");

            entity.Property(e => e.IdKarnet).HasColumnName("id_karnet");
            entity.Property(e => e.DataWydania).HasColumnName("data_wydania");
            entity.Property(e => e.DataWygasniecia).HasColumnName("data_wygasniecia");
            entity.Property(e => e.TypKarnetuIdTypKarnetu).HasColumnName("Typ_Karnetu_id_typ_karnetu");
            entity.Property(e => e.UzytkownikIdUzytkownika).HasColumnName("Uzytkownik_id_uzytkownika");

            entity.HasOne(d => d.TypKarnetuIdTypKarnetuNavigation).WithMany(p => p.Karnets)
                .HasForeignKey(d => d.TypKarnetuIdTypKarnetu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Karnet_Typ_Karnetu");

            entity.HasOne(d => d.UzytkownikIdUzytkownikaNavigation).WithMany(p => p.Karnets)
                .HasForeignKey(d => d.UzytkownikIdUzytkownika)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Karnet_Uzytkownik");
        });

        modelBuilder.Entity<KursTrenera>(entity =>
        {
            entity.HasKey(e => e.IdKursu).HasName("Kurs_Trenera_pk");

            entity.ToTable("Kurs_Trenera");

            entity.Property(e => e.IdKursu).HasColumnName("id_kursu");
            entity.Property(e => e.CzasTrwania)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("czas_trwania");
            entity.Property(e => e.Nazwa)
                .IsUnicode(false)
                .HasColumnName("nazwa");
            entity.Property(e => e.TrenerIdTrenera).HasColumnName("Trener_id_trenera");

            entity.HasOne(d => d.TrenerIdTreneraNavigation).WithMany(p => p.KursTreneras)
                .HasForeignKey(d => d.TrenerIdTrenera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Kurs_Trenera_Trener");
        });

        modelBuilder.Entity<Osoba>(entity =>
        {
            entity.HasKey(e => e.IdOsoba).HasName("Osoba_pk");

            entity.ToTable("Osoba");

            entity.Property(e => e.IdOsoba).HasColumnName("id_osoba");
            entity.Property(e => e.Adres)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Bmi).HasColumnName("BMI");
            entity.Property(e => e.DataUrodzenia)
                .HasColumnType("datetime")
                .HasColumnName("Data_urodzenia");
            entity.Property(e => e.Imie)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Nazwisko)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.NrTel)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("nr_tel");
            entity.Property(e => e.Plec)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("plec");
            entity.Property(e => e.Waga).HasColumnName("waga");
            entity.Property(e => e.Wzrost).HasColumnName("wzrost");
        });

        modelBuilder.Entity<PlanTreningowy>(entity =>
        {
            entity.HasKey(e => e.IdPlanTreningowy).HasName("Plan_treningowy_pk");

            entity.ToTable("Plan_treningowy");

            entity.Property(e => e.IdPlanTreningowy).HasColumnName("id_plan_treningowy");
            entity.Property(e => e.NazwaPlanu)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nazwa_planu");
            entity.Property(e => e.Poziom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("poziom");
            entity.Property(e => e.TrenerIdTrenera).HasColumnName("Trener_id_trenera");
            entity.Property(e => e.TreningIdTrening).HasColumnName("Trening_id_trening");
            entity.Property(e => e.ZawartoscPlanuTreningowego).HasColumnName("zawartosc_planu_treningowego");

            entity.HasOne(d => d.TrenerIdTreneraNavigation).WithMany(p => p.PlanTreningowies)
                .HasForeignKey(d => d.TrenerIdTrenera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Plan_treningowy_Trener");

            entity.HasOne(d => d.TreningIdTreningNavigation).WithMany(p => p.PlanTreningowies)
                .HasForeignKey(d => d.TreningIdTrening)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Plan_treningowy_Trening");
        });

        modelBuilder.Entity<PlanTreningowyUzytkownik>(entity =>
        {
            entity.HasKey(e => e.IdPlanTreningowy).HasName("Plan_treningowy_Uzytkownik_pk");

            entity.ToTable("Plan_treningowy_Uzytkownik");

            entity.Property(e => e.IdPlanTreningowy).HasColumnName("id_plan_treningowy");
            entity.Property(e => e.PlanTreningowyIdPlanTreningowy).HasColumnName("Plan_treningowy_id_plan_treningowy");
            entity.Property(e => e.UzytkownikIdUzytkownika).HasColumnName("Uzytkownik_id_uzytkownika");

            entity.HasOne(d => d.PlanTreningowyIdPlanTreningowyNavigation).WithMany(p => p.PlanTreningowyUzytkowniks)
                .HasForeignKey(d => d.PlanTreningowyIdPlanTreningowy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Plan_treningowy_Uzytkownik_Plan_treningowy");

            entity.HasOne(d => d.UzytkownikIdUzytkownikaNavigation).WithMany(p => p.PlanTreningowyUzytkowniks)
                .HasForeignKey(d => d.UzytkownikIdUzytkownika)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Plan_treningowy_Uzytkownik_Uzytkownik");
        });

        modelBuilder.Entity<Trener>(entity =>
        {
            entity.HasKey(e => e.IdTrenera).HasName("Trener_pk");

            entity.ToTable("Trener");

            entity.Property(e => e.IdTrenera)
                .ValueGeneratedNever()
                .HasColumnName("id_trenera");
            entity.Property(e => e.Doswiadczenie)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("doswiadczenie");
            entity.Property(e => e.OsobaIdOsoba)
                .ValueGeneratedOnAdd()
                .HasColumnName("Osoba_id_osoba");
            entity.Property(e => e.Specjalizacja)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("specjalizacja");

            entity.HasOne(d => d.OsobaIdOsobaNavigation).WithMany(p => p.Treners)
                .HasForeignKey(d => d.OsobaIdOsoba)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Trener_Osoba");
        });

        modelBuilder.Entity<Trening>(entity =>
        {
            entity.HasKey(e => e.IdTrening).HasName("Trening_pk");

            entity.ToTable("Trening");

            entity.Property(e => e.IdTrening).HasColumnName("id_trening");
            entity.Property(e => e.DzienTygodnia)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("dzien_tygodnia");
            entity.Property(e => e.IloscCwiczen).HasColumnName("ilosc_cwiczen");
            entity.Property(e => e.NazwaCwiczenia)
                .IsUnicode(false)
                .HasColumnName("nazwa_cwiczenia");
            entity.Property(e => e.PowtorzeniaNaSerie)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("powtorzenia_na_serie");
        });

        modelBuilder.Entity<TypKarnetu>(entity =>
        {
            entity.HasKey(e => e.IdTypKarnetu).HasName("Typ_Karnetu_pk");

            entity.ToTable("Typ_Karnetu");

            entity.Property(e => e.IdTypKarnetu).HasColumnName("id_typ_karnetu");
            entity.Property(e => e.Cena).HasColumnName("cena");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nazwa");
            entity.Property(e => e.Opis)
                .IsUnicode(false)
                .HasColumnName("opis");
        });

        modelBuilder.Entity<Uzytkownik>(entity =>
        {
            entity.HasKey(e => e.IdUzytkownika).HasName("Uzytkownik_pk");

            entity.ToTable("Uzytkownik");

            entity.Property(e => e.IdUzytkownika).HasColumnName("id_uzytkownika");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Haslo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("haslo");
            entity.Property(e => e.OsobaIdOsoba).HasColumnName("Osoba_id_osoba");

            entity.HasOne(d => d.OsobaIdOsobaNavigation).WithMany(p => p.Uzytkowniks)
                .HasForeignKey(d => d.OsobaIdOsoba)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Osoba");
        });

        modelBuilder.Entity<UzytkownikBlog>(entity =>
        {
            entity.HasKey(e => e.IdUzytkownikBlog).HasName("Uzytkownik_Blog_pk");

            entity.ToTable("Uzytkownik_Blog");

            entity.Property(e => e.IdUzytkownikBlog).HasColumnName("id_uzytkownik_blog");
            entity.Property(e => e.BlogIdBlogu).HasColumnName("Blog_id_blogu");
            entity.Property(e => e.UzytkownikIdUzytkownika).HasColumnName("Uzytkownik_id_uzytkownika");

            entity.HasOne(d => d.BlogIdBloguNavigation).WithMany(p => p.UzytkownikBlogs)
                .HasForeignKey(d => d.BlogIdBlogu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Blog_Blog");

            entity.HasOne(d => d.UzytkownikIdUzytkownikaNavigation).WithMany(p => p.UzytkownikBlogs)
                .HasForeignKey(d => d.UzytkownikIdUzytkownika)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Blog_Uzytkownik");
        });

        modelBuilder.Entity<UzytkownikJadlospi>(entity =>
        {
            entity.HasKey(e => e.IdUzytkownikJadlospis).HasName("Uzytkownik_Jadlospis_pk");

            entity.ToTable("Uzytkownik_Jadlospis");

            entity.Property(e => e.IdUzytkownikJadlospis).HasColumnName("id_uzytkownik_jadlospis");
            entity.Property(e => e.JadlospisIdJadlospisu).HasColumnName("Jadlospis_id_jadlospisu");
            entity.Property(e => e.UzytkownikIdUzytkownika).HasColumnName("Uzytkownik_id_uzytkownika");

            entity.HasOne(d => d.JadlospisIdJadlospisuNavigation).WithMany(p => p.UzytkownikJadlospis)
                .HasForeignKey(d => d.JadlospisIdJadlospisu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Jadlospis_Jadlospis");

            entity.HasOne(d => d.UzytkownikIdUzytkownikaNavigation).WithMany(p => p.UzytkownikJadlospis)
                .HasForeignKey(d => d.UzytkownikIdUzytkownika)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Jadlospis_Uzytkownik");
        });

        modelBuilder.Entity<UzytkownikKursTrenera>(entity =>
        {
            entity.HasKey(e => e.IdUzytkownikKursTrenera).HasName("Uzytkownik_Kurs_Trenera_pk");

            entity.ToTable("Uzytkownik_Kurs_Trenera");

            entity.Property(e => e.IdUzytkownikKursTrenera).HasColumnName("id_uzytkownik_kurs_trenera");
            entity.Property(e => e.KursTreneraIdKursu).HasColumnName("Kurs_Trenera_id_kursu");
            entity.Property(e => e.UzytkownikIdUzytkownika).HasColumnName("Uzytkownik_id_uzytkownika");

            entity.HasOne(d => d.KursTreneraIdKursuNavigation).WithMany(p => p.UzytkownikKursTreneras)
                .HasForeignKey(d => d.KursTreneraIdKursu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Kurs_Trenera_Kurs_Trenera");

            entity.HasOne(d => d.UzytkownikIdUzytkownikaNavigation).WithMany(p => p.UzytkownikKursTreneras)
                .HasForeignKey(d => d.UzytkownikIdUzytkownika)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Kurs_Trenera_Uzytkownik");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
