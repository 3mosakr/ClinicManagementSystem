using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repository.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Patient GetByName(string name);
        List<Patient> SearchPatientsByName(string name);
    }
}
