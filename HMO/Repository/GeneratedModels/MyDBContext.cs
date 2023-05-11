using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repository.GeneratedModels;

public partial class MyDBContext : DbContext
{
    public MyDBContext()
    {
    }

    public MyDBContext(DbContextOptions<MyDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CoronaDetail> CoronaDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VaccineDetail> VaccineDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("MyDBConnectionString");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CoronaDetail>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("CoronaDetails_pkey");

            entity.Property(e => e.Code)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(20L, null, null, null, null, null)
                .HasColumnName("code");
            entity.Property(e => e.PositiveAnswerDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.RecoveryDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.CoronaDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("Users_pkey");

            entity.Property(e => e.UserId)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(1000L, null, null, null, null, null)
                .HasColumnName("UserID");
            entity.Property(e => e.BirthDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.City).HasColumnType("character varying");
            entity.Property(e => e.FullName).HasColumnType("character varying");
            entity.Property(e => e.HousePhon).HasColumnType("character varying");
            entity.Property(e => e.Phon).HasColumnType("character varying");
            entity.Property(e => e.Street).HasColumnType("character varying");
            entity.Property(e => e.UserTz)
                .HasColumnType("character varying")
                .HasColumnName("UserTZ");
        });

        modelBuilder.Entity<VaccineDetail>(entity =>
        {
            entity.HasKey(e => e.VaccinationId).HasName("VaccineDetails_pkey");

            entity.Property(e => e.VaccinationId)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(20L, null, null, null, null, null)
                .HasColumnName("VaccinationID");
            entity.Property(e => e.Manufacturer).HasColumnType("character varying");
            entity.Property(e => e.ReceivingVaccine).HasColumnType("timestamp without time zone");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.VaccineDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("VaccineDetails_UserID_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
