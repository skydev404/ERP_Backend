using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface IPlatformRepository<TEntity,in TInputModel>
    {
        Task<TEntity> Select(Guid id);
        Task<IEnumerable<TEntity>> Select(IEnumerable<Guid> ids = null);
    }
}
