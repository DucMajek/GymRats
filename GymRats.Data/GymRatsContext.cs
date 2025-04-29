using System;
using System.Collections.Generic;
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

    public virtual DbSet<Coach> Coaches { get; set; }

    public virtual DbSet<FoodEbook> FoodEbooks { get; set; }

    public virtual DbSet<GroupClass> GroupClasses { get; set; }

    public virtual DbSet<ParticipationInClass> ParticipationInClasses { get; set; }

    public virtual DbSet<PassStatus> PassStatuses { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PersonalTraining> PersonalTrainings { get; set; }

    public virtual DbSet<PurchasedCourse> PurchasedCourses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TrainerCourse> TrainerCourses { get; set; }

    public virtual DbSet<TrainingPlan> TrainingPlans { get; set; }

    public virtual DbSet<TypePass> TypePasses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserEbook> UserEbooks { get; set; }

    public virtual DbSet<UserPass> UserPasses { get; set; }

    public virtual DbSet<UserTrainingPlan> UserTrainingPlans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-RAS4J05;Database=GymRats;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coach>(entity =>
        {
            entity.HasKey(e => e.IdCoach).HasName("Coach_pk");

            entity.ToTable("Coach");

            entity.Property(e => e.IdCoach)
                .ValueGeneratedNever()
                .HasColumnName("id_coach");
            entity.Property(e => e.Specialization)
                .IsUnicode(false)
                .HasColumnName("specialization");

            entity.HasOne(d => d.IdCoachNavigation).WithOne(p => p.Coach)
                .HasForeignKey<Coach>(d => d.IdCoach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Trener_Osoba");
        });

        modelBuilder.Entity<FoodEbook>(entity =>
        {
            entity.HasKey(e => e.IdEbook).HasName("Food_ebook_pk");

            entity.ToTable("Food_ebook");

            entity.Property(e => e.IdEbook).HasColumnName("id_ebook");
            entity.Property(e => e.Calories)
                .IsUnicode(false)
                .HasColumnName("calories");
            entity.Property(e => e.DietType)
                .IsUnicode(false)
                .HasColumnName("diet_type");
            entity.Property(e => e.EbookFile).HasColumnName("ebook_file");
        });

        modelBuilder.Entity<GroupClass>(entity =>
        {
            entity.HasKey(e => e.IdGroup).HasName("Group_classes_pk");

            entity.ToTable("Group_classes");

            entity.Property(e => e.IdGroup).HasColumnName("id_group");
            entity.Property(e => e.ClassType)
                .IsUnicode(false)
                .HasColumnName("class_type");
            entity.Property(e => e.GroupSize).HasColumnName("group_size");
            entity.Property(e => e.IdCoach).HasColumnName("id_coach");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");

            entity.HasOne(d => d.IdCoachNavigation).WithMany(p => p.GroupClasses)
                .HasForeignKey(d => d.IdCoach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Zajecia_Grupowe_Trener");
        });

        modelBuilder.Entity<ParticipationInClass>(entity =>
        {
            entity.HasKey(e => e.IdParticipation).HasName("Participation_in_classes_pk");

            entity.ToTable("Participation_in_classes");

            entity.Property(e => e.IdParticipation).HasColumnName("id_participation");
            entity.Property(e => e.IdGroup).HasColumnName("id_group");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdGroupNavigation).WithMany(p => p.ParticipationInClasses)
                .HasForeignKey(d => d.IdGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Udzial_w_zajeciach_Zajecia_Grupowe");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.ParticipationInClasses)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Udzial_w_zajeciach_Uzytkownik");
        });

        modelBuilder.Entity<PassStatus>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("Pass_status_pk");

            entity.ToTable("Pass_status");

            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.StatusType)
                .IsUnicode(false)
                .HasColumnName("status_type");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.IdPerson).HasName("Person_pk");

            entity.ToTable("Person");

            entity.Property(e => e.IdPerson).HasColumnName("id_person");
            entity.Property(e => e.Address)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.FlatNumber)
                .IsUnicode(false)
                .HasColumnName("flat_number");
            entity.Property(e => e.Gender)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Name)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.Place)
                .IsUnicode(false)
                .HasColumnName("place");
            entity.Property(e => e.Surname)
                .IsUnicode(false)
                .HasColumnName("surname");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("weight");
            entity.Property(e => e.ZipCode)
                .IsUnicode(false)
                .HasColumnName("zip_code");
        });

        modelBuilder.Entity<PersonalTraining>(entity =>
        {
            entity.HasKey(e => e.IdPersonalTraining).HasName("Personal_training_pk");

            entity.ToTable("Personal_training");

            entity.Property(e => e.IdPersonalTraining).HasColumnName("id_personal_training");
            entity.Property(e => e.IdCoach).HasColumnName("id_coach");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.ReservationDateTime)
                .HasColumnType("datetime")
                .HasColumnName("reservation_date_time");

            entity.HasOne(d => d.IdCoachNavigation).WithMany(p => p.PersonalTrainings)
                .HasForeignKey(d => d.IdCoach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Trening_Personalny_Trener");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.PersonalTrainings)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Trening_Personalny_Uzytkownik");
        });

        modelBuilder.Entity<PurchasedCourse>(entity =>
        {
            entity.HasKey(e => e.IdPurchasedCourse).HasName("Purchased_course_pk");

            entity.ToTable("Purchased_course");

            entity.Property(e => e.IdPurchasedCourse).HasColumnName("id_purchased_course");
            entity.Property(e => e.IdCourse).HasColumnName("id_course");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdCourseNavigation).WithMany(p => p.PurchasedCourses)
                .HasForeignKey(d => d.IdCourse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Kurs_Trenera_Kurs_Trenera");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.PurchasedCourses)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Kurs_Trenera_Uzytkownik");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("Role_pk");

            entity.ToTable("Role");

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<TrainerCourse>(entity =>
        {
            entity.HasKey(e => e.IdCourse).HasName("Trainer_Course_pk");

            entity.ToTable("Trainer_Course");

            entity.Property(e => e.IdCourse).HasColumnName("id_course");
            entity.Property(e => e.CourseName)
                .IsUnicode(false)
                .HasColumnName("course_name");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Duration)
                .IsUnicode(false)
                .HasColumnName("duration");
            entity.Property(e => e.IdCoach).HasColumnName("id_coach");

            entity.HasOne(d => d.IdCoachNavigation).WithMany(p => p.TrainerCourses)
                .HasForeignKey(d => d.IdCoach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Kurs_Trenera_Trener");
        });

        modelBuilder.Entity<TrainingPlan>(entity =>
        {
            entity.HasKey(e => e.IdTrainingPlan).HasName("Training_plan_pk");

            entity.ToTable("Training_plan");

            entity.Property(e => e.IdTrainingPlan).HasColumnName("id_training_plan");
            entity.Property(e => e.TrainingPlanFile).HasColumnName("training_plan_file");
            entity.Property(e => e.TrainingPlanName)
                .IsUnicode(false)
                .HasColumnName("training_plan_name");
        });

        modelBuilder.Entity<TypePass>(entity =>
        {
            entity.HasKey(e => e.IdTypePass).HasName("Type_pass_pk");

            entity.ToTable("Type_pass");

            entity.Property(e => e.IdTypePass).HasColumnName("id_type_pass");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.DurationPass).HasColumnName("duration_pass");
            entity.Property(e => e.GymPassName)
                .IsUnicode(false)
                .HasColumnName("gym_pass_name");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("User_pk");

            entity.ToTable("User");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnName("id_user");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Rola");

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Osoba");
        });

        modelBuilder.Entity<UserEbook>(entity =>
        {
            entity.HasKey(e => e.IdUserEbook).HasName("User_ebook_pk");

            entity.ToTable("User_ebook");

            entity.Property(e => e.IdUserEbook).HasColumnName("id_user_ebook");
            entity.Property(e => e.IdEbook).HasColumnName("id_ebook");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdEbookNavigation).WithMany(p => p.UserEbooks)
                .HasForeignKey(d => d.IdEbook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Jadlospis_Jadlospis");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserEbooks)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Uzytkownik_Jadlospis_Uzytkownik");
        });

        modelBuilder.Entity<UserPass>(entity =>
        {
            entity.HasKey(e => e.IdPass).HasName("User_pass_pk");

            entity.ToTable("User_pass");

            entity.Property(e => e.IdPass).HasColumnName("id_pass");
            entity.Property(e => e.DateEnd).HasColumnName("date_end");
            entity.Property(e => e.DateStart).HasColumnName("date_start");
            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.IdTypePass).HasColumnName("id_type_pass");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.UserPasses)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_pass_Pass_status");

            entity.HasOne(d => d.IdTypePassNavigation).WithMany(p => p.UserPasses)
                .HasForeignKey(d => d.IdTypePass)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Karnet_Typ_Karnetu");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserPasses)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Karnet_Uzytkownik");
        });

        modelBuilder.Entity<UserTrainingPlan>(entity =>
        {
            entity.HasKey(e => e.IdUserTrainingPlan).HasName("User_training_plan_pk");

            entity.ToTable("User_training_plan");

            entity.Property(e => e.IdUserTrainingPlan).HasColumnName("id_user_training_plan");
            entity.Property(e => e.IdTrainingPlan).HasColumnName("id_training_plan");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdTrainingPlanNavigation).WithMany(p => p.UserTrainingPlans)
                .HasForeignKey(d => d.IdTrainingPlan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Plan_treningowy_Uzytkownik_Plan_treningowy");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserTrainingPlans)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Plan_treningowy_Uzytkownik_Uzytkownik");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
