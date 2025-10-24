namespace ClinicManagementSystem.Repository.Interfaces
{
    public interface IUnitOfWork
    {
		IPatientRepository PatientRepository { get; }
        IVisitRepository VisitRepository { get; }
        IDoctorAvailabilityRepository DoctorAvailabilityRepository { get; }
        IAppointmentRepositry AppointmentRepositry { get; }
        void Save();
    }
}
