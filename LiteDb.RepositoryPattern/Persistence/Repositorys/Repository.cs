using System.Collections.Generic;
using LiteDb.RepositoryPattern.Core.Application.Global;
using LiteDb.RepositoryPattern.Core.Application.LiteDb;
using LiteDb.RepositoryPattern.Core.Repositorys;

namespace LiteDb.RepositoryPattern.Persistence.Repositorys
{
    public abstract class Repository<T> : IRepository<T>
    {
        public IFactory<IDb<T>> Db { get; }
        private readonly string _collectionName;

        protected Repository(IFactory<IDb<T>> db, string collectionName = null)
        {
            Db = db;
            _collectionName = collectionName;
        }

        public virtual IEnumerable<T> GetAll()
        {
            using (var db = Db.Get())
            {
                return db.Collection(_collectionName).FindAll();
            }
        }

        public virtual T GetById(int? id)
        {
            if (!id.HasValue) return default(T);
            using (var db = Db.Get())
            {
                return db.Collection(_collectionName).FindById(id);
            }
        }

        public virtual int Create(T model)
        {
            if (model == null) return -1;
            using (var db = Db.Get())
            {
                return db.Collection(_collectionName).Insert(model);
            }
        }

        public virtual bool Update(T entity)
        {
            using (var db = Db.Get())
            {
                return db.Collection(_collectionName).Update(entity);
            }
        }

        public virtual bool Delete(int id)
        {
            using (var db = Db.Get())
            {
                return db.Collection(_collectionName).Delete(id);
            }
        }
    }
}