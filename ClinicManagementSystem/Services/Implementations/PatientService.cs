using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repository.Interfaces;
using ClinicManagementSystem.Services.Interfaces;

namespace ClinicManagementSystem.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Patient> GetAllPatients()
        {
            return _unitOfWork.PatientRepository.GetAll();
        }

        public Patient GetPatientById(int id)
        {
            return _unitOfWork.PatientRepository.GetById(id);
        }
    }
}
