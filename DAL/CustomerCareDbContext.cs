using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CusomerCareModule.DAL;

public partial class CustomerCareDbContext : DbContext
{
    public CustomerCareDbContext()
    {
    }

    public CustomerCareDbContext(DbContextOptions<CustomerCareDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Complaint> Complaints { get; set; }

    public virtual DbSet<ComplaintHistory> ComplaintHistories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StatusMaster> StatusMasters { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=CCDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Complain__3214EC27167B4AB9");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActionDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfRegistration).HasColumnType("datetime");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Status).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Complaint__Statu__38996AB5");

            entity.HasOne(d => d.User).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Complaint__UserI__398D8EEE");
        });

        modelBuilder.Entity<ComplaintHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Complain__3214EC0798F56ED4");

            entity.ToTable("ComplaintHistory");

            entity.Property(e => e.ActionDate).HasColumnType("datetime");
            entity.Property(e => e.ComplaintId).HasColumnName("ComplaintID");
            entity.Property(e => e.Description).IsUnicode(false);

            entity.HasOne(d => d.Complaint).WithMany(p => p.ComplaintHistories)
                .HasForeignKey(d => d.ComplaintId)
                .HasConstraintName("FK__Complaint__Compl__49C3F6B7");

            entity.HasOne(d => d.User).WithMany(p => p.ComplaintHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Complaint__UserI__5AEE82B9");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC0781723EB0");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StatusMa__3214EC079AFCE132");

            entity.ToTable("StatusMaster");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC27F16577C9");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__286302EC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
