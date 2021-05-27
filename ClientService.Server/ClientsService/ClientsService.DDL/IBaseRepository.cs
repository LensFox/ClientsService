using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientsService.DDL
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAll();

        Task<T> Get(Guid id);

        Task Remove(Guid id);

        Task Update(T entity);
    }
}
