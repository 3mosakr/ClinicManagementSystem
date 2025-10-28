using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services.Interfaces
{
    public interface IPatientService
    {
        IEnumerable<Patient> GetAllPatients(string search = null);
        Patient GetPatientById(int id);
        void AddPatient(Patient patient);
        void UpdatePatient(Patient patient);
        void DeletePatient(int id);

        List<Patient> GetPatientByName(string name);
    }
}
