using System.Collections.Generic;
using System.Threading.Tasks;
using DotNETVueJSTemplate.Services.Messaging;

namespace DotNETVueJSTemplate.Services.Interfaces
{
    public interface IEntityReadServiceAsync<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);
    }

    public interface IEntityWriteServiceAsync<T>
    {
        Task<SaveResult<T>> Create(T entity);

        Task<SaveResult<T>> Update(T entity);
    }

    public interface IEntityServiceAsync<T> : IEntityReadServiceAsync<T>, IEntityWriteServiceAsync<T> { }
}
