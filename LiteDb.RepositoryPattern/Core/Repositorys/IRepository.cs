using System.Collections.Generic;
using LiteDb.RepositoryPattern.Core.Application.Global;
using LiteDb.RepositoryPattern.Core.Application.LiteDb;

namespace LiteDb.RepositoryPattern.Core.Repositorys
{
    public interface IRepository<T> : ILiteDbRepository<T>, IReadRepository<T>, IWriteRepository<T>
    {
    }

    public interface ILiteDbRepository<T>
    {
        IFactory<IDb<T>> Db { get; }
    }

    public interface IReadRepository<out T>
    {
        IEnumerable<T> GetAll();
        T GetById(int? id);
    }

    public interface IWriteRepository<in T>
    {
        int Create(T model);
        bool Update(T entity);
        bool Delete(int id);
    }
}