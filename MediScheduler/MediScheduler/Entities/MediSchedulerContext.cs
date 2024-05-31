using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MediScheduler.Entities;

public partial class MediSchedulerContext : DbContext
{
    public MediSchedulerContext()
    {
    }

    public MediSchedulerContext(DbContextOptions<MediSchedulerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentStatus> AppointmentStatuses { get; set; }

    public virtual DbSet<ContactData> ContactData { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorSchedule> DoctorSchedules { get; set; }

    public virtual DbSet<DoctorScheduleStatus> DoctorScheduleStatuses { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<RecordHistory> RecordHistories { get; set; }

    public virtual DbSet<Specialty> Specialties { get; set; }

    public virtual DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__D06765FEAB01A626");

            entity.ToTable("Appointment");

            entity.Property(e => e.AppointmentId).HasColumnName("appointmentId");
            entity.Property(e => e.AppointmentStatusId).HasColumnName("appointmentStatusId");
            entity.Property(e => e.DoctorScheduleId).HasColumnName("doctorScheduleId");
            entity.Property(e => e.PatientId).HasColumnName("patientId");

            entity.HasOne(d => d.AppointmentStatus).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.AppointmentStatusId)
                .HasConstraintName("FK__Appointme__appoi__70DDC3D8");

            entity.HasOne(d => d.DoctorSchedule).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorScheduleId)
                .HasConstraintName("FK__Appointme__docto__6EF57B66");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__Appointme__patie__6FE99F9F");
        });

        modelBuilder.Entity<AppointmentStatus>(entity =>
        {
            entity.HasKey(e => e.AppointmentStatusId).HasName("PK__Appointm__AA367B5556F15225");

            entity.ToTable("AppointmentStatus");

            entity.Property(e => e.AppointmentStatusId).HasColumnName("appointmentStatusId");
            entity.Property(e => e.AppointmentStatusName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("appointmentStatusName");
        });

        modelBuilder.Entity<ContactData>(entity =>
        {
            entity.HasKey(e => e.ContactDataId).HasName("PK__ContactD__53796A9E3E5271F7");

            entity.Property(e => e.ContactDataId).HasColumnName("contactDataId");
            entity.Property(e => e.Adress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("adress");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctor__722484760E1BD76C");

            entity.ToTable("Doctor");

            entity.Property(e => e.DoctorId).HasColumnName("doctorId");
            entity.Property(e => e.ContactDataId).HasColumnName("contactDataId");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.LicenseNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("licenseNo");
            entity.Property(e => e.SpecialtyId).HasColumnName("specialtyId");

            entity.HasOne(d => d.ContactData).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.ContactDataId)
                .HasConstraintName("FK__Doctor__licenseN__60A75C0F");

            entity.HasOne(d => d.Specialty).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.SpecialtyId)
                .HasConstraintName("FK__Doctor__specialt__619B8048");
        });

        modelBuilder.Entity<DoctorSchedule>(entity =>
        {
            entity.HasKey(e => e.DoctorScheduleId).HasName("PK__DoctorSc__94013AFA1C3DCB96");

            entity.ToTable("DoctorSchedule");

            entity.Property(e => e.DoctorScheduleId).HasColumnName("doctorScheduleId");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DoctorId).HasColumnName("doctorId");
            entity.Property(e => e.DoctorScheduleStatusId).HasColumnName("doctorScheduleStatusId");
            entity.Property(e => e.EndTime).HasColumnName("endTime");
            entity.Property(e => e.StartTime).HasColumnName("startTime");

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorSchedules)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__DoctorSch__docto__693CA210");

            entity.HasOne(d => d.DoctorScheduleStatus).WithMany(p => p.DoctorSchedules)
                .HasForeignKey(d => d.DoctorScheduleStatusId)
                .HasConstraintName("FK__DoctorSch__docto__6A30C649");
        });

        modelBuilder.Entity<DoctorScheduleStatus>(entity =>
        {
            entity.HasKey(e => e.DoctorScheduleStatusId).HasName("PK__DoctorSc__AEA2749A9F34DB14");

            entity.ToTable("DoctorScheduleStatus");

            entity.Property(e => e.DoctorScheduleStatusId).HasColumnName("doctorScheduleStatusId");
            entity.Property(e => e.DoctorScheduleStatusName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("doctorScheduleStatusName");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patient__A17005ECCF5E51FF");

            entity.ToTable("Patient");

            entity.Property(e => e.PatientId).HasColumnName("patientId");
            entity.Property(e => e.ContactDataId).HasColumnName("contactDataId");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastName");

            entity.HasOne(d => d.ContactData).WithMany(p => p.Patients)
                .HasForeignKey(d => d.ContactDataId)
                .HasConstraintName("FK__Patient__contact__6477ECF3");
        });

        modelBuilder.Entity<RecordHistory>(entity =>
        {
            entity.HasKey(e => e.RecordHistoryId).HasName("PK__RecordHi__044F4A9CBB0580D5");

            entity.ToTable("RecordHistory");

            entity.Property(e => e.RecordHistoryId).HasColumnName("recordHistoryId");
            entity.Property(e => e.AppointmentId).HasColumnName("appointmentId");
            entity.Property(e => e.DoctorId).HasColumnName("doctorId");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.PatientId).HasColumnName("patientId");

            entity.HasOne(d => d.Appointment).WithMany(p => p.RecordHistories)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__RecordHis__appoi__75A278F5");

            entity.HasOne(d => d.Doctor).WithMany(p => p.RecordHistories)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__RecordHis__docto__74AE54BC");

            entity.HasOne(d => d.Patient).WithMany(p => p.RecordHistories)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__RecordHis__image__73BA3083");
        });

        modelBuilder.Entity<Specialty>(entity =>
        {
            entity.HasKey(e => e.SpecialtyId).HasName("PK__Specialt__81FB91AEBE4B1F9B");

            entity.ToTable("Specialty");

            entity.Property(e => e.SpecialtyId).HasColumnName("specialtyId");
            entity.Property(e => e.SpecialtyName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("specialtyName");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__CB9A1CFFE77C2DE7");

            entity.ToTable("User");

            entity.HasIndex(e => e.Username, "UQ__User__F3DBC572C0189560").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PatientId).HasColumnName("patientId");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Patient).WithMany(p => p.Users)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__User__patientId__03F0984C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
