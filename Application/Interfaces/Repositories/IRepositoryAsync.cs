namespace Application.Interfaces.Repositories;

public interface IRepositoryAsync<TEntity>
    where TEntity : class?
{
    Task<TEntity?> GetById(int id);
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> Create(TEntity entityToCreate);
    Task<int> Update(TEntity entityToUpdate);
    Task<int> Delete(int id);
}
