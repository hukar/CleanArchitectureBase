using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories;

public interface IRepositoryAsync<TEntity>
    where TEntity : class?
{
    Task<TEntity?> GetById(int id);
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> Create(TEntity entityToCreate);
    Task<bool> Update(TEntity entityToUpdate);
    Task<bool> Delete(int id);
}
