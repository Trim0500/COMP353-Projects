using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MYVCApp.Models;

namespace MYVCApp.Contexts;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Clubmember> Clubmembers { get; set; }

    public virtual DbSet<Familymember> Familymembers { get; set; }

    public virtual DbSet<Familymemberlocation> Familymemberlocations { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Locationphone> Locationphones { get; set; }

    public virtual DbSet<Logemail> Logemails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Personnel> Personnel { get; set; }

    public virtual DbSet<Personnellocation> Personnellocations { get; set; }

    public virtual DbSet<Secondaryfamilymember> Secondaryfamilymembers { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Teamformation> Teamformations { get; set; }

    public virtual DbSet<Teammember> Teammembers { get; set; }

    public virtual DbSet<Teamsession> Teamsessions { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseMySQL("Server=127.0.0.1;port=3306;Database=mainproject;uid=root;pwd=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clubmember>(entity =>
        {
            entity.HasKey(e => e.Cmn).HasName("PRIMARY");

            entity.ToTable("clubmember");

            entity.HasIndex(e => e.FamilyMemberIdFk, "family_member_id_fk");

            entity.HasIndex(e => e.MedCardNum, "med_card_num").IsUnique();

            entity.HasIndex(e => e.SocialSecNum, "social_sec_num").IsUnique();

            entity.Property(e => e.Cmn).HasColumnName("cmn");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FamilyMemberIdFk).HasColumnName("family_member_id_fk");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.Height)
                .HasPrecision(5)
                .HasColumnName("height");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.MedCardNum)
                .HasMaxLength(12)
                .IsFixedLength()
                .HasColumnName("med_card_num");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("phone_number");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("postal_code");
            entity.Property(e => e.PrimaryRelationship)
                .HasMaxLength(20)
                .HasColumnName("primary_relationship");
            entity.Property(e => e.ProgressReport)
                .HasColumnType("text")
                .HasColumnName("progress_report");
            entity.Property(e => e.Province)
                .HasMaxLength(2)
                .HasColumnName("province");
            entity.Property(e => e.SecondaryRelationship)
                .HasMaxLength(20)
                .HasColumnName("secondary_relationship");
            entity.Property(e => e.SocialSecNum)
                .HasMaxLength(9)
                .IsFixedLength()
                .HasColumnName("social_sec_num");
            entity.Property(e => e.Weight)
                .HasPrecision(5)
                .HasColumnName("weight");

            entity.HasOne(d => d.FamilyMemberIdFkNavigation).WithMany(p => p.Clubmembers)
                .HasForeignKey(d => d.FamilyMemberIdFk)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("clubmember_ibfk_1");
        });

        modelBuilder.Entity<Familymember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("familymember");

            entity.HasIndex(e => e.MedCardNum, "med_card_num").IsUnique();

            entity.HasIndex(e => e.SocialSecNum, "social_sec_num").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.MedCardNum)
                .HasMaxLength(12)
                .IsFixedLength()
                .HasColumnName("med_card_num");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("phone_number");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("postal_code");
            entity.Property(e => e.Province)
                .HasMaxLength(2)
                .HasColumnName("province");
            entity.Property(e => e.SocialSecNum)
                .HasMaxLength(9)
                .IsFixedLength()
                .HasColumnName("social_sec_num");
        });

        modelBuilder.Entity<Familymemberlocation>(entity =>
        {
            entity.HasKey(e => new { e.LocationIdFk, e.FamilyMemberIdFk, e.StartDate }).HasName("PRIMARY");

            entity.ToTable("familymemberlocation");

            entity.HasIndex(e => e.FamilyMemberIdFk, "family_member_id_fk");

            entity.Property(e => e.LocationIdFk).HasColumnName("location_id_fk");
            entity.Property(e => e.FamilyMemberIdFk).HasColumnName("family_member_id_fk");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("start_date");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("end_date");

            entity.HasOne(d => d.FamilyMemberIdFkNavigation).WithMany(p => p.Familymemberlocations)
                .HasForeignKey(d => d.FamilyMemberIdFk)
                .HasConstraintName("familymemberlocation_ibfk_2");

            entity.HasOne(d => d.LocationIdFkNavigation).WithMany(p => p.Familymemberlocations)
                .HasForeignKey(d => d.LocationIdFk)
                .HasConstraintName("familymemberlocation_ibfk_1");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("location");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("postal_code");
            entity.Property(e => e.Province)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("province");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasColumnName("type");
            entity.Property(e => e.WebsiteUrl)
                .HasMaxLength(255)
                .HasColumnName("website_url");
        });

        modelBuilder.Entity<Locationphone>(entity =>
        {
            entity.HasKey(e => new { e.LocationIdFk, e.PhoneNumber }).HasName("PRIMARY");

            entity.ToTable("locationphone");

            entity.Property(e => e.LocationIdFk).HasColumnName("location_id_fk");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("phone_number");

            entity.HasOne(d => d.LocationIdFkNavigation).WithMany(p => p.Locationphones)
                .HasForeignKey(d => d.LocationIdFk)
                .HasConstraintName("locationphone_ibfk_1");
        });

        modelBuilder.Entity<Logemail>(entity =>
        {
            entity.HasKey(e => new { e.Recipient, e.DeliveryDateTime }).HasName("PRIMARY");

            entity.ToTable("logemail");

            entity.Property(e => e.Recipient)
                .HasMaxLength(50)
                .HasColumnName("recipient");
            entity.Property(e => e.DeliveryDateTime)
                .HasMaxLength(50)
                .HasColumnName("delivery_date_time");
            entity.Property(e => e.Body)
                .HasColumnType("text")
                .HasColumnName("body");
            entity.Property(e => e.Sender)
                .HasMaxLength(50)
                .HasColumnName("sender");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .HasColumnName("subject");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("payment");

            entity.HasIndex(e => e.CmnFk, "cmn_fk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(5)
                .HasColumnName("amount");
            entity.Property(e => e.CmnFk).HasColumnName("cmn_fk");
            entity.Property(e => e.EffectiveDate)
                .HasColumnType("date")
                .HasColumnName("effectiveDate");
            entity.Property(e => e.Method)
                .HasMaxLength(10)
                .HasColumnName("method");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("date")
                .HasColumnName("paymentDate");

            entity.HasOne(d => d.CmnFkNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CmnFk)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("payment_ibfk_1");
        });

        modelBuilder.Entity<Personnel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("personnel");

            entity.HasIndex(e => e.MedCardNum, "med_card_num").IsUnique();

            entity.HasIndex(e => e.SocialSecNum, "social_sec_num").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Mandate)
                .HasMaxLength(10)
                .HasColumnName("mandate");
            entity.Property(e => e.MedCardNum)
                .HasMaxLength(12)
                .IsFixedLength()
                .HasColumnName("med_card_num");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("phone_number");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("postal_code");
            entity.Property(e => e.Province)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("province");
            entity.Property(e => e.SocialSecNum)
                .HasMaxLength(9)
                .IsFixedLength()
                .HasColumnName("social_sec_num");
        });

        modelBuilder.Entity<Personnellocation>(entity =>
        {
            entity.HasKey(e => new { e.PersonnelIdFk, e.LocationIdFk, e.StartDate }).HasName("PRIMARY");

            entity.ToTable("personnellocation");

            entity.HasIndex(e => e.LocationIdFk, "location_id_fk");

            entity.Property(e => e.PersonnelIdFk).HasColumnName("personnel_id_fk");
            entity.Property(e => e.LocationIdFk).HasColumnName("location_id_fk");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("start_date");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("end_date");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");

            entity.HasOne(d => d.LocationIdFkNavigation).WithMany(p => p.Personnellocations)
                .HasForeignKey(d => d.LocationIdFk)
                .HasConstraintName("personnellocation_ibfk_2");

            entity.HasOne(d => d.PersonnelIdFkNavigation).WithMany(p => p.Personnellocations)
                .HasForeignKey(d => d.PersonnelIdFk)
                .HasConstraintName("personnellocation_ibfk_1");
        });

        modelBuilder.Entity<Secondaryfamilymember>(entity =>
        {
            entity.HasKey(e => e.PrimaryFamilyMemberIdFk).HasName("PRIMARY");

            entity.ToTable("secondaryfamilymember");

            entity.HasIndex(e => e.PrimaryFamilyMemberIdFk, "primary_family_member_id_fk").IsUnique();

            entity.Property(e => e.PrimaryFamilyMemberIdFk).HasColumnName("primary_family_member_id_fk");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("phone_number");
            entity.Property(e => e.RelationshipToPrimary)
                .HasMaxLength(20)
                .HasColumnName("relationship_to_primary");

            entity.HasOne(d => d.PrimaryFamilyMemberIdFkNavigation).WithOne(p => p.Secondaryfamilymember)
                .HasForeignKey<Secondaryfamilymember>(d => d.PrimaryFamilyMemberIdFk)
                .HasConstraintName("secondaryfamilymember_ibfk_1");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("session");

            entity.HasIndex(e => e.LocationIdFk, "location_id_fk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EventDateTime)
                .HasColumnType("datetime")
                .HasColumnName("event_date_time");
            entity.Property(e => e.EventType)
                .HasMaxLength(20)
                .HasColumnName("event_type");
            entity.Property(e => e.LocationIdFk).HasColumnName("location_id_fk");

            entity.HasOne(d => d.LocationIdFkNavigation).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.LocationIdFk)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("session_ibfk_1");
        });

        modelBuilder.Entity<Teamformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("teamformation");

            entity.HasIndex(e => e.CaptainIdFk, "captain_id_fk");

            entity.HasIndex(e => e.LocationIdFk, "location_id_fk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaptainIdFk).HasColumnName("captain_id_fk");
            entity.Property(e => e.LocationIdFk).HasColumnName("location_id_fk");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.CaptainIdFkNavigation).WithMany(p => p.Teamformations)
                .HasForeignKey(d => d.CaptainIdFk)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("teamformation_ibfk_1");

            entity.HasOne(d => d.LocationIdFkNavigation).WithMany(p => p.Teamformations)
                .HasForeignKey(d => d.LocationIdFk)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("teamformation_ibfk_2");
        });

        modelBuilder.Entity<Teammember>(entity =>
        {
            entity.HasKey(e => new { e.TeamFormationIdFk, e.CmnFk }).HasName("PRIMARY");

            entity.ToTable("teammember");

            entity.HasIndex(e => e.CmnFk, "cmn_fk");

            entity.Property(e => e.TeamFormationIdFk).HasColumnName("team_formation_id_fk");
            entity.Property(e => e.CmnFk).HasColumnName("cmn_fk");
            entity.Property(e => e.AssignmentDateTime)
                .HasColumnType("datetime")
                .HasColumnName("assignment_date_time");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");

            entity.HasOne(d => d.CmnFkNavigation).WithMany(p => p.Teammembers)
                .HasForeignKey(d => d.CmnFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("teammember_ibfk_2");

            entity.HasOne(d => d.TeamFormationIdFkNavigation).WithMany(p => p.Teammembers)
                .HasForeignKey(d => d.TeamFormationIdFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("teammember_ibfk_1");
        });

        modelBuilder.Entity<Teamsession>(entity =>
        {
            entity.HasKey(e => new { e.TeamFormationIdFk, e.SessionIdFk }).HasName("PRIMARY");

            entity.ToTable("teamsession");

            entity.HasIndex(e => e.SessionIdFk, "session_id_fk");

            entity.Property(e => e.TeamFormationIdFk).HasColumnName("team_formation_id_fk");
            entity.Property(e => e.SessionIdFk).HasColumnName("session_id_fk");
            entity.Property(e => e.Score).HasColumnName("score");

            entity.HasOne(d => d.SessionIdFkNavigation).WithMany(p => p.Teamsessions)
                .HasForeignKey(d => d.SessionIdFk)
                .HasConstraintName("teamsession_ibfk_2");

            entity.HasOne(d => d.TeamFormationIdFkNavigation).WithMany(p => p.Teamsessions)
                .HasForeignKey(d => d.TeamFormationIdFk)
                .HasConstraintName("teamsession_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
