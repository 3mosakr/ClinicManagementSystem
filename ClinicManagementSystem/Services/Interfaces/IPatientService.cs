using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services.Interfaces
{
    public interface IPatientService
    {
        // get all patients
        List<Patient> GetAllPatients();
        // get patient by id
        Patient GetPatientById(int id);

        List<Patient> GetPatientByName(string name);
    }
}
