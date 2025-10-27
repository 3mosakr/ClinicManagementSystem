using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repository.Interfaces;
using ClinicManagementSystem.Services.Interfaces;
using NuGet.Protocol.Core.Types;

namespace ClinicManagementSystem.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Patient> GetAllPatients(string search = null)
        {
            var patients = _unitOfWork.PatientRepository.GetAll();
            return patients;
        }

        public Patient GetPatientById(int id) => _unitOfWork.PatientRepository.GetById(id);

        public void AddPatient(Patient patient)
        {
            _unitOfWork.PatientRepository.Add(patient);
            _unitOfWork.Save();
        }

        public void UpdatePatient(Patient patient)
        {
            _unitOfWork.PatientRepository.Update(patient);
            _unitOfWork.Save();
        }

        public void DeletePatient(int id)
        {
            var patient = _unitOfWork.PatientRepository.GetById(id);
            _unitOfWork.PatientRepository.Delete(patient);
            _unitOfWork.Save();
        }
    }
}
