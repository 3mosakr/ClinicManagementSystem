namespace ClinicManagementSystem.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IPatientRepository PatientRepository { get; }
    }
}
