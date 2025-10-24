using ClinicManagementSystem.Models.Data;
using ClinicManagementSystem.Repository.Interfaces;

namespace ClinicManagementSystem.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private IPatientRepository _patientRepository;
        private IVisitRepository _visitRepository;


		ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IPatientRepository PatientRepository
        {
            get
            {
                if (_patientRepository == null)
                {
                    _patientRepository = new PatientRepository(_context);
                }
                return _patientRepository;
            }
        }

        public IVisitRepository VisitRepository
        {
            get
            {
                if (_visitRepository == null)
                {
                    _visitRepository = new VisitRepository(_context);
				}
                return _visitRepository;
			}
        }

        public void Save()
        {
           _context.SaveChanges();
        }
    }
}
