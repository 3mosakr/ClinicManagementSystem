namespace ClinicManagementSystem.Repository.Interfaces
{
    public interface IGenericRepository<IEntity> where IEntity : class
    {
        List<IEntity> GetAll();
        IEntity GetById(object id);
        void Add(IEntity entity);
        void Update(IEntity entity);
        void Delete(IEntity entity);
        
    }
}
