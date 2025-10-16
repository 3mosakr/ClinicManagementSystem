namespace ClinicManagementSystem.Repository.Interfaces
{
    public interface IUnitOfWork
    {
		IPatientRepository PatientRepository { get; }
        IVisitRepository VisitRepository { get; }

		void Save();
    }
}
