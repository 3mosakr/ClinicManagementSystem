using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IPatientRepository PatientRepository { get; }
        IDoctorAvailabilityRepository DoctorAvailabilityRepository { get; }

        void Save();
    }
}
