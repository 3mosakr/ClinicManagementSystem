using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Models.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // One Doctor -> Many Appointments
            builder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(u => u.DoctorAppointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // One Receptionist -> Many Appointments
            builder.Entity<Appointment>()
                .HasOne(a => a.Receptionist)
                .WithMany(u => u.ReceptionistAppointments)
                .HasForeignKey(a => a.ReceptionistId)
                .OnDelete(DeleteBehavior.Restrict);

            // One Patient -> Many Appointments
            builder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId);

            // One Appointment -> One Visit
            builder.Entity<Visit>()
                .HasOne(v => v.Appointment)
                .WithOne(a => a.Visit)
                .HasForeignKey<Visit>(v => v.AppointmentId);

            // One DoctorAvailability -> Many Appointments
            builder.Entity<Appointment>()
                .HasOne(a => a.DoctorAvailability)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorAvailabilityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
