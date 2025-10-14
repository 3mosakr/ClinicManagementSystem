using ClinicManagementSystem.Models.Data;
using ClinicManagementSystem.Repository.Interfaces;

namespace ClinicManagementSystem.Repository.Implementations
{
    public class GenericRepository<IEntity> : IGenericRepository<IEntity> where IEntity : class
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<IEntity> GetAll()
        {
            return _context.Set<IEntity>().ToList();
        }

        public IEntity GetById(object id)
        {
            return _context.Set<IEntity>().Find(id);
        }

        public void Add(IEntity entity)
        {
            _context.Set<IEntity>().Add(entity);
        }

        public void Delete(IEntity entity)
        {
            _context.Set<IEntity>().Remove(entity);
        }

        public void Update(IEntity entity)
        {
            _context.Set<IEntity>().Update(entity);
        }

       

        
    }
}
