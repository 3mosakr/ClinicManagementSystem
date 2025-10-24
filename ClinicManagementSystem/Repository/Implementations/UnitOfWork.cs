using ClinicManagementSystem.Models.Data;
using ClinicManagementSystem.Repository.Interfaces;

namespace ClinicManagementSystem.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext _context;
        private IPatientRepository _patientRepository;
        private IVisitRepository _visitRepository;

        private IDoctorAvailabilityRepository _doctorAvailabilityRepository;


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
        public IDoctorAvailabilityRepository DoctorAvailabilityRepository
        {
            get
            {
                if (_doctorAvailabilityRepository == null)
                    _doctorAvailabilityRepository = new DoctorAvailabilityRepository(_context);
                return _doctorAvailabilityRepository;
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
