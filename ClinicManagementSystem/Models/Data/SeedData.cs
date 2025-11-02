using ClinicManagementSystem.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Security.Claims;

namespace ClinicManagementSystem.Models.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // 1️ إنشاء الأدوار (Roles)
            string[] roleNames = { UserRoles.Admin, UserRoles.Doctor, UserRoles.Receptionist };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                    Console.WriteLine($"✅ Role '{roleName}' created successfully.");
                }
            }

            // 2️ إنشاء مستخدم إداري (Admin)
            var adminEmail = "admin@clinic.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "System Administrator",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
                    Console.WriteLine("✅ Admin user created successfully.");
                }
                else
                {
                    Console.WriteLine("❌ Failed to create admin user:");
                    foreach (var error in result.Errors)
                        Console.WriteLine($" - {error.Description}");
                }
            }

            // 3️ إنشاء دكتور تجريبي
            var doctorEmail = "doctor@clinic.com";
            var doctorUser = await userManager.FindByEmailAsync(doctorEmail);
            if (doctorUser == null)
            {
                doctorUser = new ApplicationUser
                {
                    UserName = doctorEmail,
                    Email = doctorEmail,
                    FullName = "Dr. Ahmed Hassan",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(doctorUser, "Doctor@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(doctorUser, UserRoles.Doctor);
                    await userManager.AddClaimAsync(doctorUser, new Claim("Specialty", "GeneralPractitioner"));

                    Console.WriteLine("✅ Doctor user created successfully.");
                }
            }

            // 4 إنشاء ريسيبشن تجريبي
            var receptionEmail = "reception@clinic.com";
            var receptionUser = await userManager.FindByEmailAsync(receptionEmail);
            if (receptionUser == null)
            {
                receptionUser = new ApplicationUser
                {
                    UserName = receptionEmail,
                    Email = receptionEmail,
                    FullName = "Reception Desk",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(receptionUser, "Reception@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(receptionUser, UserRoles.Receptionist);
                    Console.WriteLine("✅ Receptionist user created successfully.");
                }
            }

            // 4️⃣ Seed Doctor Availability
            if (!context.DoctorAvailabilities.Any())
            {
                var availabilityList = new List<DoctorAvailability>
                {
                    new DoctorAvailability
                    {
                        DoctorId = doctorUser.Id,
                        DayOfWeek = DayOfWeek.Sunday,
                        StartTime = new TimeSpan(9, 0, 0),
                        EndTime = new TimeSpan(13, 0, 0)
                    },
                    new DoctorAvailability
                    {
                        DoctorId = doctorUser.Id,
                        DayOfWeek = DayOfWeek.Tuesday,
                        StartTime = new TimeSpan(15, 0, 0),
                        EndTime = new TimeSpan(19, 0, 0)
                    }
                };

                await context.DoctorAvailabilities.AddRangeAsync(availabilityList);
                await context.SaveChangesAsync();
            }

            // 5️⃣ إنشاء مريض تجريبي
            if (!context.Patients.Any())
            {
                var patient = new Patient
                {
                    FullName = "Ali aloka",
                    PhoneNumber = "01012345678",
                    Gender = Enums.Gender.Male,
                    Address = "Cairo",
                    DateOfBirth = new DateTime(2000, 5, 15)
                };

                await context.Patients.AddAsync(patient);
                await context.SaveChangesAsync();

                // 6️⃣ إنشاء حجز (Appointment)
                var doctorAvailability = await context.DoctorAvailabilities.FirstOrDefaultAsync();
                if (doctorAvailability != null)
                {
                    var appointment = new Appointment
                    {
                        DoctorId = doctorAvailability.DoctorId,
                        PatientId = patient.Id,
                        ReceptionistId = receptionUser.Id,
                        Date = DateTime.Today.AddDays(1).AddHours(10), // غدًا الساعة 10 صباحًا
                        Status = "Completed",
                        Notes = ""
                    };

                    await context.Appointments.AddAsync(appointment);
                    await context.SaveChangesAsync();
                }
            }

            // 7️⃣ إنشاء Visit بعد الحجز
            var existingAppointment = await context.Appointments
                .Include(a => a.Patient)
                .FirstOrDefaultAsync();

            if (existingAppointment != null && !context.Visits.Any())
            {
                var visit = new Visit
                {
                    AppointmentId = existingAppointment.Id,
                    //DoctorId = existingAppointment.DoctorId,
                    //PatientId = existingAppointment.PatientId,
                    VisitDate = DateTime.Now,
                    Diagnosis = "Seasonal flu",
                    Prescription = "Paracetamol 500mg, 3 times a day after meals",
                    DoctorNotes = "Patient advised to rest and drink fluids"
                };

                await context.Visits.AddAsync(visit);
                await context.SaveChangesAsync();
            }


        }
    }
}
